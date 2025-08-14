using Microsoft.EntityFrameworkCore;
using LibraryManagementAPI.Data;
using LibraryManagementAPI.DTOs;
using LibraryManagementAPI.Models;
using System.Web;

namespace LibraryManagementAPI.Services
{
    public class BookInquiryService : IBookInquiryService
    {
        private readonly LibraryDbContext _context;

        public BookInquiryService(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<BookInquiryDto>> GetBookInquiriesAsync(BookInquirySearchDto searchDto)
        {
            var query = _context.BookInquiries
                .Include(bi => bi.Book)
                    .ThenInclude(b => b!.Author)
                .Include(bi => bi.User)
                .AsQueryable();

            // Apply filters
            if (!string.IsNullOrEmpty(searchDto.SearchTerm))
            {
                var searchTerm = searchDto.SearchTerm.ToLower();
                query = query.Where(bi =>
                    bi.CustomerName.ToLower().Contains(searchTerm) ||
                    bi.CustomerPhone.Contains(searchTerm) ||
                    (bi.CustomerEmail != null && bi.CustomerEmail.ToLower().Contains(searchTerm)) ||
                    (bi.Book != null && bi.Book.Title.ToLower().Contains(searchTerm)) ||
                    (bi.Book != null && bi.Book.TitleArabic != null && bi.Book.TitleArabic.ToLower().Contains(searchTerm))
                );
            }

            if (!string.IsNullOrEmpty(searchDto.Status))
            {
                query = query.Where(bi => bi.Status.ToString() == searchDto.Status);
            }

            if (searchDto.FromDate.HasValue)
            {
                query = query.Where(bi => bi.CreatedAt >= searchDto.FromDate.Value);
            }

            if (searchDto.ToDate.HasValue)
            {
                query = query.Where(bi => bi.CreatedAt <= searchDto.ToDate.Value);
            }

            // Apply sorting
            query = searchDto.SortBy.ToLower() switch
            {
                "customername" => searchDto.SortOrder.ToLower() == "desc"
                    ? query.OrderByDescending(bi => bi.CustomerName)
                    : query.OrderBy(bi => bi.CustomerName),
                "status" => searchDto.SortOrder.ToLower() == "desc"
                    ? query.OrderByDescending(bi => bi.Status)
                    : query.OrderBy(bi => bi.Status),
                "booktitle" => searchDto.SortOrder.ToLower() == "desc"
                    ? query.OrderByDescending(bi => bi.Book!.Title)
                    : query.OrderBy(bi => bi.Book!.Title),
                _ => searchDto.SortOrder.ToLower() == "desc"
                    ? query.OrderByDescending(bi => bi.CreatedAt)
                    : query.OrderBy(bi => bi.CreatedAt)
            };

            var totalCount = await query.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalCount / searchDto.PageSize);

            var inquiries = await query
                .Skip((searchDto.Page - 1) * searchDto.PageSize)
                .Take(searchDto.PageSize)
                .Select(bi => MapToBookInquiryDto(bi))
                .ToListAsync();

            return new PagedResult<BookInquiryDto>
            {
                Items = inquiries,
                TotalCount = totalCount,
                Page = searchDto.Page,
                PageSize = searchDto.PageSize,
                TotalPages = totalPages,
                HasNextPage = searchDto.Page < totalPages,
                HasPreviousPage = searchDto.Page > 1
            };
        }

        public async Task<BookInquiryDto?> GetBookInquiryByIdAsync(int id)
        {
            var inquiry = await _context.BookInquiries
                .Include(bi => bi.Book)
                    .ThenInclude(b => b!.Author)
                .Include(bi => bi.User)
                .FirstOrDefaultAsync(bi => bi.Id == id);

            return inquiry != null ? MapToBookInquiryDto(inquiry) : null;
        }

        public async Task<BookInquiryDto?> CreateBookInquiryAsync(CreateBookInquiryDto createDto, string? userId = null)
        {
            var book = await _context.Books.FindAsync(createDto.BookId);
            if (book == null) return null;

            var inquiry = new BookInquiry
            {
                BookId = createDto.BookId,
                UserId = userId,
                CustomerName = createDto.CustomerName,
                CustomerPhone = createDto.CustomerPhone,
                CustomerEmail = createDto.CustomerEmail,
                Message = createDto.Message,
                Status = InquiryStatus.Pending,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.BookInquiries.Add(inquiry);
            await _context.SaveChangesAsync();

            return await GetBookInquiryByIdAsync(inquiry.Id);
        }

        public async Task<bool> UpdateBookInquiryStatusAsync(int id, UpdateBookInquiryStatusDto updateDto)
        {
            var inquiry = await _context.BookInquiries.FindAsync(id);
            if (inquiry == null) return false;

            if (Enum.TryParse<InquiryStatus>(updateDto.Status, out var status))
            {
                inquiry.Status = status;
                inquiry.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteBookInquiryAsync(int id)
        {
            var inquiry = await _context.BookInquiries.FindAsync(id);
            if (inquiry == null) return false;

            _context.BookInquiries.Remove(inquiry);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<string> GenerateWhatsAppMessageAsync(int bookId, string customerName)
        {
            var book = await _context.Books
                .Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.Id == bookId);

            if (book == null) return string.Empty;

            var message = $"مرحباً، أنا {customerName}.\n\n";
            message += $"أود الاستفسار عن توفر الكتاب التالي:\n";
            message += $"📚 العنوان: {book.TitleArabic ?? book.Title}\n";
            
            if (book.Author != null)
            {
                message += $"✍️ المؤلف: {book.Author.NameArabic ?? book.Author.Name}\n";
            }

            if (book.Price.HasValue)
            {
                message += $"💰 السعر: {book.Price:C}\n";
            }

            message += $"\nهل الكتاب متوفر حالياً؟\n";
            message += $"شكراً لكم.";

            return message;
        }

        public async Task<string> GetWhatsAppUrlAsync(int bookId, string customerName, string phoneNumber)
        {
            // Get WhatsApp number from system settings
            var whatsappSetting = await _context.SystemSettings
                .FirstOrDefaultAsync(s => s.SettingKey == "WhatsAppNumber");
            
            var whatsappNumber = whatsappSetting?.SettingValue ?? "+1234567890";
            
            // Remove any non-digit characters except +
            whatsappNumber = System.Text.RegularExpressions.Regex.Replace(whatsappNumber, @"[^\d+]", "");
            
            var message = await GenerateWhatsAppMessageAsync(bookId, customerName);
            var encodedMessage = HttpUtility.UrlEncode(message);
            
            return $"https://wa.me/{whatsappNumber}?text={encodedMessage}";
        }

        private static BookInquiryDto MapToBookInquiryDto(BookInquiry inquiry)
        {
            return new BookInquiryDto
            {
                Id = inquiry.Id,
                BookId = inquiry.BookId,
                BookTitle = inquiry.Book?.Title ?? "",
                BookTitleArabic = inquiry.Book?.TitleArabic,
                BookAuthor = inquiry.Book?.Author?.Name,
                BookCoverImageUrl = inquiry.Book?.CoverImageUrl,
                UserId = inquiry.UserId,
                CustomerName = inquiry.CustomerName,
                CustomerPhone = inquiry.CustomerPhone,
                CustomerEmail = inquiry.CustomerEmail,
                Message = inquiry.Message,
                Status = inquiry.Status.ToString(),
                CreatedAt = inquiry.CreatedAt,
                UpdatedAt = inquiry.UpdatedAt
            };
        }
    }
}


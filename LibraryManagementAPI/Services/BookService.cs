using Microsoft.EntityFrameworkCore;
using LibraryManagementAPI.Data;
using LibraryManagementAPI.DTOs;
using LibraryManagementAPI.Models;

namespace LibraryManagementAPI.Services
{
    public class BookService : IBookService
    {
        private readonly LibraryDbContext _context;

        public BookService(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<BookDto>> GetBooksAsync(BookSearchDto searchDto)
        {
            var query = _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.Category)
                .Include(b => b.BookImages)
                .AsQueryable();

            // Apply filters
            if (!string.IsNullOrEmpty(searchDto.SearchTerm))
            {
                var searchTerm = searchDto.SearchTerm.ToLower();
                query = query.Where(b => 
                    b.Title.ToLower().Contains(searchTerm) ||
                    (b.TitleArabic != null && b.TitleArabic.ToLower().Contains(searchTerm)) ||
                    (b.Description != null && b.Description.ToLower().Contains(searchTerm)) ||
                    (b.DescriptionArabic != null && b.DescriptionArabic.ToLower().Contains(searchTerm)) ||
                    (b.Author != null && b.Author.Name.ToLower().Contains(searchTerm)) ||
                    (b.Author != null && b.Author.NameArabic != null && b.Author.NameArabic.ToLower().Contains(searchTerm))
                );
            }

            if (searchDto.CategoryId.HasValue)
                query = query.Where(b => b.CategoryId == searchDto.CategoryId);

            if (searchDto.AuthorId.HasValue)
                query = query.Where(b => b.AuthorId == searchDto.AuthorId);

            if (searchDto.PublisherId.HasValue)
                query = query.Where(b => b.PublisherId == searchDto.PublisherId);

            if (!string.IsNullOrEmpty(searchDto.Language))
                query = query.Where(b => b.Language == searchDto.Language);

            if (searchDto.IsAvailable.HasValue)
                query = query.Where(b => b.IsAvailable == searchDto.IsAvailable);

            if (searchDto.IsFeatured.HasValue)
                query = query.Where(b => b.IsFeatured == searchDto.IsFeatured);

            if (searchDto.IsNewRelease.HasValue)
                query = query.Where(b => b.IsNewRelease == searchDto.IsNewRelease);

            if (searchDto.MinPrice.HasValue)
                query = query.Where(b => b.Price >= searchDto.MinPrice);

            if (searchDto.MaxPrice.HasValue)
                query = query.Where(b => b.Price <= searchDto.MaxPrice);

            // Apply sorting
            query = searchDto.SortBy.ToLower() switch
            {
                "title" => searchDto.SortOrder.ToLower() == "desc" 
                    ? query.OrderByDescending(b => b.Title)
                    : query.OrderBy(b => b.Title),
                "price" => searchDto.SortOrder.ToLower() == "desc"
                    ? query.OrderByDescending(b => b.Price)
                    : query.OrderBy(b => b.Price),
                "rating" => searchDto.SortOrder.ToLower() == "desc"
                    ? query.OrderByDescending(b => b.Rating)
                    : query.OrderBy(b => b.Rating),
                "createdat" => searchDto.SortOrder.ToLower() == "desc"
                    ? query.OrderByDescending(b => b.CreatedAt)
                    : query.OrderBy(b => b.CreatedAt),
                "viewcount" => searchDto.SortOrder.ToLower() == "desc"
                    ? query.OrderByDescending(b => b.ViewCount)
                    : query.OrderBy(b => b.ViewCount),
                _ => query.OrderBy(b => b.Title)
            };

            var totalCount = await query.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalCount / searchDto.PageSize);

            var books = await query
                .Skip((searchDto.Page - 1) * searchDto.PageSize)
                .Take(searchDto.PageSize)
                .Select(b => MapToBookDto(b))
                .ToListAsync();

            return new PagedResult<BookDto>
            {
                Items = books,
                TotalCount = totalCount,
                Page = searchDto.Page,
                PageSize = searchDto.PageSize,
                TotalPages = totalPages,
                HasNextPage = searchDto.Page < totalPages,
                HasPreviousPage = searchDto.Page > 1
            };
        }

        public async Task<BookDto?> GetBookByIdAsync(int id)
        {
            var book = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.Category)
                .Include(b => b.BookImages)
                .FirstOrDefaultAsync(b => b.Id == id);

            return book != null ? MapToBookDto(book) : null;
        }

        public async Task<BookDto?> CreateBookAsync(CreateBookDto createBookDto)
        {
            try
            {
                var book = new Book
                {
                    Title = createBookDto.Title,
                    TitleArabic = createBookDto.TitleArabic,
                    ISBN = createBookDto.ISBN,
                    Description = createBookDto.Description,
                    DescriptionArabic = createBookDto.DescriptionArabic,
                    AuthorId = createBookDto.AuthorId,
                    PublisherId = createBookDto.PublisherId,
                    CategoryId = createBookDto.CategoryId,
                    PublicationDate = createBookDto.PublicationDate,
                    Pages = createBookDto.Pages,
                    Language = createBookDto.Language,
                    Price = createBookDto.Price,
                    OriginalPrice = createBookDto.OriginalPrice,
                    StockQuantity = createBookDto.StockQuantity,
                    CoverImageUrl = createBookDto.CoverImageUrl,
                    IsAvailable = createBookDto.IsAvailable,
                    IsFeatured = createBookDto.IsFeatured,
                    IsNewRelease = createBookDto.IsNewRelease,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.Books.Add(book);
                await _context.SaveChangesAsync();

                return await GetBookByIdAsync(book.Id);
            }
            catch (Exception ex)
            {
                // Log the exception details
                Console.WriteLine($"Error creating book: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                throw new Exception($"Failed to create book: {ex.Message}", ex);
            }
        }

        public async Task<BookDto?> UpdateBookAsync(int id, UpdateBookDto updateBookDto)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return null;

            book.Title = updateBookDto.Title;
            book.TitleArabic = updateBookDto.TitleArabic;
            book.ISBN = updateBookDto.ISBN;
            book.Description = updateBookDto.Description;
            book.DescriptionArabic = updateBookDto.DescriptionArabic;
            book.AuthorId = updateBookDto.AuthorId;
            book.PublisherId = updateBookDto.PublisherId;
            book.CategoryId = updateBookDto.CategoryId;
            book.PublicationDate = updateBookDto.PublicationDate;
            book.Pages = updateBookDto.Pages;
            book.Language = updateBookDto.Language;
            book.Price = updateBookDto.Price;
            book.OriginalPrice = updateBookDto.OriginalPrice;
            book.StockQuantity = updateBookDto.StockQuantity;
            book.CoverImageUrl = updateBookDto.CoverImageUrl;
            book.IsAvailable = updateBookDto.IsAvailable;
            book.IsFeatured = updateBookDto.IsFeatured;
            book.IsNewRelease = updateBookDto.IsNewRelease;
            book.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return await GetBookByIdAsync(id);
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return false;

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<BookDto>> GetFeaturedBooksAsync(int count = 10)
        {
            var books = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.Category)
                .Include(b => b.BookImages)
                .Where(b => b.IsFeatured && b.IsAvailable)
                .OrderByDescending(b => b.Rating)
                .ThenByDescending(b => b.ViewCount)
                .Take(count)
                .Select(b => MapToBookDto(b))
                .ToListAsync();

            return books;
        }

        public async Task<List<BookDto>> GetNewReleasesAsync(int count = 10)
        {
            var books = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.Category)
                .Include(b => b.BookImages)
                .Where(b => b.IsNewRelease && b.IsAvailable)
                .OrderByDescending(b => b.CreatedAt)
                .Take(count)
                .Select(b => MapToBookDto(b))
                .ToListAsync();

            return books;
        }

        public async Task<List<BookDto>> GetBooksByCategoryAsync(int categoryId, int count = 10)
        {
            var books = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.Category)
                .Include(b => b.BookImages)
                .Where(b => b.CategoryId == categoryId && b.IsAvailable)
                .OrderByDescending(b => b.Rating)
                .ThenByDescending(b => b.ViewCount)
                .Take(count)
                .Select(b => MapToBookDto(b))
                .ToListAsync();

            return books;
        }

        public async Task<List<BookDto>> GetBooksByAuthorAsync(int authorId, int count = 10)
        {
            var books = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.Category)
                .Include(b => b.BookImages)
                .Where(b => b.AuthorId == authorId && b.IsAvailable)
                .OrderByDescending(b => b.CreatedAt)
                .Take(count)
                .Select(b => MapToBookDto(b))
                .ToListAsync();

            return books;
        }

        public async Task<List<BookDto>> GetBooksByPublisherAsync(int publisherId, int count = 10)
        {
            var books = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.Category)
                .Include(b => b.BookImages)
                .Where(b => b.PublisherId == publisherId && b.IsAvailable)
                .OrderByDescending(b => b.CreatedAt)
                .Take(count)
                .Select(b => MapToBookDto(b))
                .ToListAsync();

            return books;
        }

        public async Task<bool> UpdateBookStockAsync(int id, int quantity)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return false;

            book.StockQuantity = quantity;
            book.IsAvailable = quantity > 0;
            book.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> IncrementViewCountAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return false;

            book.ViewCount++;
            book.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<BookImageDto>> GetBookImagesAsync(int bookId)
        {
            var images = await _context.BookImages
                .Where(bi => bi.BookId == bookId && bi.IsActive)
                .OrderBy(bi => bi.DisplayOrder)
                .Select(bi => new BookImageDto
                {
                    Id = bi.Id,
                    BookId = bi.BookId,
                    ImageUrl = bi.ImageUrl,
                    ImageType = bi.ImageType.ToString(),
                    DisplayOrder = bi.DisplayOrder,
                    IsActive = bi.IsActive,
                    CreatedAt = bi.CreatedAt
                })
                .ToListAsync();

            return images;
        }

        public async Task<BookImageDto?> AddBookImageAsync(CreateBookImageDto createImageDto)
        {
            var bookImage = new BookImage
            {
                BookId = createImageDto.BookId,
                ImageUrl = createImageDto.ImageUrl,
                ImageType = Enum.Parse<ImageType>(createImageDto.ImageType),
                DisplayOrder = createImageDto.DisplayOrder,
                IsActive = createImageDto.IsActive,
                CreatedAt = DateTime.UtcNow
            };

            _context.BookImages.Add(bookImage);
            await _context.SaveChangesAsync();

            return new BookImageDto
            {
                Id = bookImage.Id,
                BookId = bookImage.BookId,
                ImageUrl = bookImage.ImageUrl,
                ImageType = bookImage.ImageType.ToString(),
                DisplayOrder = bookImage.DisplayOrder,
                IsActive = bookImage.IsActive,
                CreatedAt = bookImage.CreatedAt
            };
        }

        public async Task<bool> DeleteBookImageAsync(int imageId)
        {
            var bookImage = await _context.BookImages.FindAsync(imageId);
            if (bookImage == null) return false;

            _context.BookImages.Remove(bookImage);
            await _context.SaveChangesAsync();
            return true;
        }

        private static BookDto MapToBookDto(Book book)
        {
            return new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                TitleArabic = book.TitleArabic,
                ISBN = book.ISBN,
                Description = book.Description,
                DescriptionArabic = book.DescriptionArabic,
                AuthorId = book.AuthorId,
                AuthorName = book.Author?.Name,
                AuthorNameArabic = book.Author?.NameArabic,
                PublisherId = book.PublisherId,
                PublisherName = book.Publisher?.Name,
                PublisherNameArabic = book.Publisher?.NameArabic,
                CategoryId = book.CategoryId,
                CategoryName = book.Category?.Name,
                CategoryNameArabic = book.Category?.NameArabic,
                PublicationDate = book.PublicationDate,
                Pages = book.Pages,
                Language = book.Language,
                Price = book.Price,
                OriginalPrice = book.OriginalPrice,
                StockQuantity = book.StockQuantity,
                CoverImageUrl = book.CoverImageUrl,
                IsAvailable = book.IsAvailable,
                IsFeatured = book.IsFeatured,
                IsNewRelease = book.IsNewRelease,
                Rating = book.Rating,
                RatingCount = book.RatingCount,
                ViewCount = book.ViewCount,
                CreatedAt = book.CreatedAt,
                UpdatedAt = book.UpdatedAt,
                Images = book.BookImages?.Where(bi => bi.IsActive).Select(bi => new BookImageDto
                {
                    Id = bi.Id,
                    BookId = bi.BookId,
                    ImageUrl = bi.ImageUrl,
                    ImageType = bi.ImageType.ToString(),
                    DisplayOrder = bi.DisplayOrder,
                    IsActive = bi.IsActive,
                    CreatedAt = bi.CreatedAt
                }).ToList() ?? new List<BookImageDto>()
            };
        }
    }
}


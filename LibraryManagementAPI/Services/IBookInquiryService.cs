using LibraryManagementAPI.DTOs;

namespace LibraryManagementAPI.Services
{
    public interface IBookInquiryService
    {
        Task<PagedResult<BookInquiryDto>> GetBookInquiriesAsync(BookInquirySearchDto searchDto);
        Task<BookInquiryDto?> GetBookInquiryByIdAsync(int id);
        Task<BookInquiryDto?> CreateBookInquiryAsync(CreateBookInquiryDto createDto, string? userId = null);
        Task<bool> UpdateBookInquiryStatusAsync(int id, UpdateBookInquiryStatusDto updateDto);
        Task<bool> DeleteBookInquiryAsync(int id);
        Task<string> GenerateWhatsAppMessageAsync(int bookId, string customerName);
        Task<string> GetWhatsAppUrlAsync(int bookId, string customerName, string phoneNumber);
    }
}


using LibraryManagementAPI.DTOs;

namespace LibraryManagementAPI.Services
{
    public interface IBookService
    {
        Task<PagedResult<BookDto>> GetBooksAsync(BookSearchDto searchDto);
        Task<BookDto?> GetBookByIdAsync(int id);
        Task<BookDto?> CreateBookAsync(CreateBookDto createBookDto);
        Task<BookDto?> UpdateBookAsync(int id, UpdateBookDto updateBookDto);
        Task<bool> DeleteBookAsync(int id);
        Task<List<BookDto>> GetFeaturedBooksAsync(int count = 10);
        Task<List<BookDto>> GetNewReleasesAsync(int count = 10);
        Task<List<BookDto>> GetBooksByCategoryAsync(int categoryId, int count = 10);
        Task<List<BookDto>> GetBooksByAuthorAsync(int authorId, int count = 10);
        Task<List<BookDto>> GetBooksByPublisherAsync(int publisherId, int count = 10);
        Task<bool> UpdateBookStockAsync(int id, int quantity);
        Task<bool> IncrementViewCountAsync(int id);
        Task<List<BookImageDto>> GetBookImagesAsync(int bookId);
        Task<BookImageDto?> AddBookImageAsync(CreateBookImageDto createImageDto);
        Task<bool> DeleteBookImageAsync(int imageId);
    }
}


using LibraryManagementAPI.DTOs;

namespace LibraryManagementAPI.Services
{
    public interface IPublisherService
    {
        Task<List<PublisherDto>> GetPublishersAsync();
        Task<PublisherDto?> GetPublisherByIdAsync(int id);
        Task<PublisherDto?> CreatePublisherAsync(CreatePublisherDto createPublisherDto);
        Task<PublisherDto?> UpdatePublisherAsync(int id, UpdatePublisherDto updatePublisherDto);
        Task<bool> DeletePublisherAsync(int id);
        Task<List<PublisherDto>> GetActivePublishersAsync();
        Task<List<PublisherDto>> SearchPublishersAsync(string searchTerm);
    }
}


using LibraryManagementAPI.DTOs;

namespace LibraryManagementAPI.Services
{
    public interface IAuthorService
    {
        Task<List<AuthorDto>> GetAuthorsAsync();
        Task<AuthorDto?> GetAuthorByIdAsync(int id);
        Task<AuthorDto?> CreateAuthorAsync(CreateAuthorDto createAuthorDto);
        Task<AuthorDto?> UpdateAuthorAsync(int id, UpdateAuthorDto updateAuthorDto);
        Task<bool> DeleteAuthorAsync(int id);
        Task<List<AuthorDto>> GetActiveAuthorsAsync();
        Task<List<AuthorDto>> SearchAuthorsAsync(string searchTerm);
    }
}


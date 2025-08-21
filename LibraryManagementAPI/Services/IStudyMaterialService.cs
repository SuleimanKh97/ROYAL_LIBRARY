using LibraryManagementAPI.DTOs;

namespace LibraryManagementAPI.Services
{
    public interface IStudyMaterialService
    {
        Task<StudyMaterialDto?> GetByIdAsync(int id);
        Task<IEnumerable<StudyMaterialDto>> GetAllAsync(StudyMaterialFilterDto filter);
        Task<StudyMaterialDto> CreateAsync(CreateStudyMaterialDto createDto);
        Task<StudyMaterialDto> UpdateAsync(int id, UpdateStudyMaterialDto updateDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<string>> GetCategoriesAsync();
        Task<IEnumerable<string>> GetSubjectsAsync();
    }
}

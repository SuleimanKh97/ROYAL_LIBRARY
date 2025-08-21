using LibraryManagementAPI.DTOs;

namespace LibraryManagementAPI.Services
{
    public interface IStudyTipService
    {
        Task<StudyTipDto?> GetByIdAsync(int id);
        Task<IEnumerable<StudyTipDto>> GetAllAsync(StudyTipFilterDto filter);
        Task<StudyTipDto> CreateAsync(CreateStudyTipDto createDto);
        Task<StudyTipDto> UpdateAsync(int id, UpdateStudyTipDto updateDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<string>> GetCategoriesAsync();
        Task<IEnumerable<string>> GetGradesAsync();
        Task<IEnumerable<string>> GetSubjectsAsync();
    }
}

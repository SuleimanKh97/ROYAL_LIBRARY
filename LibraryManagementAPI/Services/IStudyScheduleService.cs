using LibraryManagementAPI.DTOs;

namespace LibraryManagementAPI.Services
{
    public interface IStudyScheduleService
    {
        Task<StudyScheduleDto?> GetByIdAsync(int id);
        Task<IEnumerable<StudyScheduleDto>> GetAllAsync(StudyScheduleFilterDto filter);
        Task<StudyScheduleDto> CreateAsync(CreateStudyScheduleDto createDto);
        Task<StudyScheduleDto> UpdateAsync(int id, UpdateStudyScheduleDto updateDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<string>> GetTypesAsync();
        Task<IEnumerable<string>> GetGradesAsync();
    }
}

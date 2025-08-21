using LibraryManagementAPI.Data;
using LibraryManagementAPI.DTOs;
using LibraryManagementAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace LibraryManagementAPI.Services
{
    public class StudyScheduleService : IStudyScheduleService
    {
        private readonly LibraryDbContext _context;

        public StudyScheduleService(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<StudyScheduleDto?> GetByIdAsync(int id)
        {
            var schedule = await _context.StudySchedules
                .FirstOrDefaultAsync(s => s.Id == id && s.IsActive);

            if (schedule == null) return null;

            return MapToDto(schedule);
        }

        public async Task<IEnumerable<StudyScheduleDto>> GetAllAsync(StudyScheduleFilterDto filter)
        {
            var query = _context.StudySchedules.AsQueryable();

            // Apply filters
            if (!string.IsNullOrEmpty(filter.Type))
                query = query.Where(s => s.Type == filter.Type);

            if (!string.IsNullOrEmpty(filter.Grade))
                query = query.Where(s => s.Grade == filter.Grade || s.Grade == "all");

            if (!string.IsNullOrEmpty(filter.Search))
                query = query.Where(s => s.Title!.Contains(filter.Search) || 
                                       s.TitleArabic!.Contains(filter.Search) ||
                                       s.Description!.Contains(filter.Search));

            if (filter.IsActive.HasValue)
                query = query.Where(s => s.IsActive == filter.IsActive.Value);

            // Apply pagination
            var skip = (filter.Page - 1) * filter.PageSize;
            var schedules = await query
                .OrderBy(s => s.OrderIndex)
                .ThenBy(s => s.CreatedAt)
                .Skip(skip)
                .Take(filter.PageSize)
                .ToListAsync();

            return schedules.Select(MapToDto);
        }

        public async Task<StudyScheduleDto> CreateAsync(CreateStudyScheduleDto createDto)
        {
            var schedule = new StudySchedule
            {
                Type = createDto.Type,
                Day = createDto.Day,
                Title = createDto.Title,
                TitleArabic = createDto.TitleArabic,
                Description = createDto.Description,
                DescriptionArabic = createDto.DescriptionArabic,
                Grade = createDto.Grade,
                Subjects = JsonSerializer.Serialize(createDto.Subjects),
                Focus = createDto.Focus,
                FocusArabic = createDto.FocusArabic,
                OrderIndex = createDto.OrderIndex,
                IsActive = createDto.IsActive,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.StudySchedules.Add(schedule);
            await _context.SaveChangesAsync();

            return MapToDto(schedule);
        }

        public async Task<StudyScheduleDto> UpdateAsync(int id, UpdateStudyScheduleDto updateDto)
        {
            var schedule = await _context.StudySchedules.FindAsync(id);
            if (schedule == null)
                throw new ArgumentException("Study schedule not found");

            if (updateDto.Type != null)
                schedule.Type = updateDto.Type;

            if (updateDto.Day != null)
                schedule.Day = updateDto.Day;

            if (updateDto.Title != null)
                schedule.Title = updateDto.Title;

            if (updateDto.TitleArabic != null)
                schedule.TitleArabic = updateDto.TitleArabic;

            if (updateDto.Description != null)
                schedule.Description = updateDto.Description;

            if (updateDto.DescriptionArabic != null)
                schedule.DescriptionArabic = updateDto.DescriptionArabic;

            if (updateDto.Grade != null)
                schedule.Grade = updateDto.Grade;

            if (updateDto.Subjects != null)
                schedule.Subjects = JsonSerializer.Serialize(updateDto.Subjects);

            if (updateDto.Focus != null)
                schedule.Focus = updateDto.Focus;

            if (updateDto.FocusArabic != null)
                schedule.FocusArabic = updateDto.FocusArabic;

            if (updateDto.OrderIndex.HasValue)
                schedule.OrderIndex = updateDto.OrderIndex.Value;

            if (updateDto.IsActive.HasValue)
                schedule.IsActive = updateDto.IsActive.Value;

            schedule.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return MapToDto(schedule);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var schedule = await _context.StudySchedules.FindAsync(id);
            if (schedule == null) return false;

            _context.StudySchedules.Remove(schedule);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<string>> GetTypesAsync()
        {
            return await _context.StudySchedules
                .Where(s => s.IsActive)
                .Select(s => s.Type)
                .Distinct()
                .ToListAsync();
        }

        public async Task<IEnumerable<string>> GetGradesAsync()
        {
            return await _context.StudySchedules
                .Where(s => s.IsActive)
                .Select(s => s.Grade)
                .Where(g => !string.IsNullOrEmpty(g))
                .Distinct()
                .ToListAsync();
        }

        private static StudyScheduleDto MapToDto(StudySchedule schedule)
        {
            var subjects = new List<string>();
            if (!string.IsNullOrEmpty(schedule.Subjects))
            {
                try
                {
                    subjects = JsonSerializer.Deserialize<List<string>>(schedule.Subjects) ?? new List<string>();
                }
                catch
                {
                    subjects = new List<string>();
                }
            }

            return new StudyScheduleDto
            {
                Id = schedule.Id,
                Type = schedule.Type,
                Day = schedule.Day,
                Title = schedule.Title,
                TitleArabic = schedule.TitleArabic,
                Description = schedule.Description,
                DescriptionArabic = schedule.DescriptionArabic,
                Grade = schedule.Grade,
                Subjects = subjects,
                Focus = schedule.Focus,
                FocusArabic = schedule.FocusArabic,
                OrderIndex = schedule.OrderIndex,
                IsActive = schedule.IsActive,
                CreatedAt = schedule.CreatedAt,
                UpdatedAt = schedule.UpdatedAt
            };
        }
    }
}

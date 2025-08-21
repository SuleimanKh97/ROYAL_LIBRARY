using LibraryManagementAPI.Data;
using LibraryManagementAPI.DTOs;
using LibraryManagementAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace LibraryManagementAPI.Services
{
    public class StudyTipService : IStudyTipService
    {
        private readonly LibraryDbContext _context;

        public StudyTipService(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<StudyTipDto?> GetByIdAsync(int id)
        {
            var tip = await _context.StudyTips
                .FirstOrDefaultAsync(t => t.Id == id && t.IsActive);

            if (tip == null) return null;

            return MapToDto(tip);
        }

        public async Task<IEnumerable<StudyTipDto>> GetAllAsync(StudyTipFilterDto filter)
        {
            var query = _context.StudyTips.AsQueryable();

            // Apply filters
            if (!string.IsNullOrEmpty(filter.Category))
                query = query.Where(t => t.Category == filter.Category);

            if (!string.IsNullOrEmpty(filter.Grade))
                query = query.Where(t => t.Grade == filter.Grade || t.Grade == "all");

            if (!string.IsNullOrEmpty(filter.Subject))
                query = query.Where(t => t.Subject == filter.Subject || t.Subject == "all");

            if (!string.IsNullOrEmpty(filter.Search))
                query = query.Where(t => t.Title.Contains(filter.Search) || 
                                       t.TitleArabic!.Contains(filter.Search) ||
                                       t.Description!.Contains(filter.Search));

            if (filter.IsActive.HasValue)
                query = query.Where(t => t.IsActive == filter.IsActive.Value);

            // Apply pagination
            var skip = (filter.Page - 1) * filter.PageSize;
            var tips = await query
                .OrderBy(t => t.OrderIndex)
                .ThenBy(t => t.CreatedAt)
                .Skip(skip)
                .Take(filter.PageSize)
                .ToListAsync();

            return tips.Select(MapToDto);
        }

        public async Task<StudyTipDto> CreateAsync(CreateStudyTipDto createDto)
        {
            var tip = new StudyTip
            {
                Category = createDto.Category,
                Title = createDto.Title,
                TitleArabic = createDto.TitleArabic,
                Description = createDto.Description,
                DescriptionArabic = createDto.DescriptionArabic,
                Grade = createDto.Grade,
                Subject = createDto.Subject,
                Tips = JsonSerializer.Serialize(createDto.Tips),
                OrderIndex = createDto.OrderIndex,
                IsActive = createDto.IsActive,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.StudyTips.Add(tip);
            await _context.SaveChangesAsync();

            return MapToDto(tip);
        }

        public async Task<StudyTipDto> UpdateAsync(int id, UpdateStudyTipDto updateDto)
        {
            var tip = await _context.StudyTips.FindAsync(id);
            if (tip == null)
                throw new ArgumentException("Study tip not found");

            if (updateDto.Category != null)
                tip.Category = updateDto.Category;

            if (updateDto.Title != null)
                tip.Title = updateDto.Title;

            if (updateDto.TitleArabic != null)
                tip.TitleArabic = updateDto.TitleArabic;

            if (updateDto.Description != null)
                tip.Description = updateDto.Description;

            if (updateDto.DescriptionArabic != null)
                tip.DescriptionArabic = updateDto.DescriptionArabic;

            if (updateDto.Grade != null)
                tip.Grade = updateDto.Grade;

            if (updateDto.Subject != null)
                tip.Subject = updateDto.Subject;

            if (updateDto.Tips != null)
                tip.Tips = JsonSerializer.Serialize(updateDto.Tips);

            if (updateDto.OrderIndex.HasValue)
                tip.OrderIndex = updateDto.OrderIndex.Value;

            if (updateDto.IsActive.HasValue)
                tip.IsActive = updateDto.IsActive.Value;

            tip.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return MapToDto(tip);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var tip = await _context.StudyTips.FindAsync(id);
            if (tip == null) return false;

            _context.StudyTips.Remove(tip);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<string>> GetCategoriesAsync()
        {
            return await _context.StudyTips
                .Where(t => t.IsActive)
                .Select(t => t.Category)
                .Distinct()
                .ToListAsync();
        }

        public async Task<IEnumerable<string>> GetGradesAsync()
        {
            return await _context.StudyTips
                .Where(t => t.IsActive)
                .Select(t => t.Grade)
                .Where(g => !string.IsNullOrEmpty(g))
                .Distinct()
                .ToListAsync();
        }

        public async Task<IEnumerable<string>> GetSubjectsAsync()
        {
            return await _context.StudyTips
                .Where(t => t.IsActive)
                .Select(t => t.Subject)
                .Where(s => !string.IsNullOrEmpty(s))
                .Distinct()
                .ToListAsync();
        }

        private static StudyTipDto MapToDto(StudyTip tip)
        {
            var tips = new List<string>();
            if (!string.IsNullOrEmpty(tip.Tips))
            {
                try
                {
                    tips = JsonSerializer.Deserialize<List<string>>(tip.Tips) ?? new List<string>();
                }
                catch
                {
                    tips = new List<string>();
                }
            }

            return new StudyTipDto
            {
                Id = tip.Id,
                Category = tip.Category,
                Title = tip.Title,
                TitleArabic = tip.TitleArabic,
                Description = tip.Description,
                DescriptionArabic = tip.DescriptionArabic,
                Grade = tip.Grade,
                Subject = tip.Subject,
                Tips = tips,
                OrderIndex = tip.OrderIndex,
                IsActive = tip.IsActive,
                CreatedAt = tip.CreatedAt,
                UpdatedAt = tip.UpdatedAt
            };
        }
    }
}

using LibraryManagementAPI.Data;
using LibraryManagementAPI.DTOs;
using LibraryManagementAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace LibraryManagementAPI.Services
{
    public class StudyMaterialService : IStudyMaterialService
    {
        private readonly LibraryDbContext _context;

        public StudyMaterialService(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<StudyMaterialDto?> GetByIdAsync(int id)
        {
            var material = await _context.StudyMaterials
                .FirstOrDefaultAsync(m => m.Id == id && m.IsActive);

            if (material == null) return null;

            return MapToDto(material);
        }

        public async Task<IEnumerable<StudyMaterialDto>> GetAllAsync(StudyMaterialFilterDto filter)
        {
            var query = _context.StudyMaterials.AsQueryable();

            // Apply filters
            if (!string.IsNullOrEmpty(filter.Category))
                query = query.Where(m => m.Category == filter.Category);

            if (!string.IsNullOrEmpty(filter.Subject))
                query = query.Where(m => m.Subject == filter.Subject);

            if (!string.IsNullOrEmpty(filter.Search))
                query = query.Where(m => m.Title.Contains(filter.Search) || 
                                       m.TitleArabic!.Contains(filter.Search) ||
                                       m.Description!.Contains(filter.Search));

            if (filter.IsActive.HasValue)
                query = query.Where(m => m.IsActive == filter.IsActive.Value);

            // Apply pagination
            var skip = (filter.Page - 1) * filter.PageSize;
            var materials = await query
                .OrderBy(m => m.CreatedAt)
                .Skip(skip)
                .Take(filter.PageSize)
                .ToListAsync();

            return materials.Select(MapToDto);
        }

        public async Task<StudyMaterialDto> CreateAsync(CreateStudyMaterialDto createDto)
        {
            var material = new StudyMaterial
            {
                Title = createDto.Title,
                TitleArabic = createDto.TitleArabic,
                Category = createDto.Category,
                Subject = createDto.Subject,
                Teacher = createDto.Teacher,
                Description = createDto.Description,
                DescriptionArabic = createDto.DescriptionArabic,
                Price = createDto.Price,
                Status = createDto.Status,
                Delivery = createDto.Delivery,
                Duration = createDto.Duration,
                Features = JsonSerializer.Serialize(createDto.Features),
                IsActive = createDto.IsActive,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.StudyMaterials.Add(material);
            await _context.SaveChangesAsync();

            return MapToDto(material);
        }

        public async Task<StudyMaterialDto> UpdateAsync(int id, UpdateStudyMaterialDto updateDto)
        {
            var material = await _context.StudyMaterials.FindAsync(id);
            if (material == null)
                throw new ArgumentException("Study material not found");

            if (updateDto.Title != null)
                material.Title = updateDto.Title;

            if (updateDto.TitleArabic != null)
                material.TitleArabic = updateDto.TitleArabic;

            if (updateDto.Category != null)
                material.Category = updateDto.Category;

            if (updateDto.Subject != null)
                material.Subject = updateDto.Subject;

            if (updateDto.Teacher != null)
                material.Teacher = updateDto.Teacher;

            if (updateDto.Description != null)
                material.Description = updateDto.Description;

            if (updateDto.DescriptionArabic != null)
                material.DescriptionArabic = updateDto.DescriptionArabic;

            if (updateDto.Price.HasValue)
                material.Price = updateDto.Price.Value;

            if (updateDto.Status != null)
                material.Status = updateDto.Status;

            if (updateDto.Delivery != null)
                material.Delivery = updateDto.Delivery;

            if (updateDto.Duration != null)
                material.Duration = updateDto.Duration;

            if (updateDto.Features != null)
                material.Features = JsonSerializer.Serialize(updateDto.Features);

            if (updateDto.IsActive.HasValue)
                material.IsActive = updateDto.IsActive.Value;

            material.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return MapToDto(material);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var material = await _context.StudyMaterials.FindAsync(id);
            if (material == null) return false;

            _context.StudyMaterials.Remove(material);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<string>> GetCategoriesAsync()
        {
            return await _context.StudyMaterials
                .Where(m => m.IsActive)
                .Select(m => m.Category)
                .Distinct()
                .ToListAsync();
        }

        public async Task<IEnumerable<string>> GetSubjectsAsync()
        {
            return await _context.StudyMaterials
                .Where(m => m.IsActive)
                .Select(m => m.Subject)
                .Distinct()
                .ToListAsync();
        }

        private static StudyMaterialDto MapToDto(StudyMaterial material)
        {
            var features = new List<string>();
            if (!string.IsNullOrEmpty(material.Features))
            {
                try
                {
                    features = JsonSerializer.Deserialize<List<string>>(material.Features) ?? new List<string>();
                }
                catch
                {
                    features = new List<string>();
                }
            }

            return new StudyMaterialDto
            {
                Id = material.Id,
                Title = material.Title,
                TitleArabic = material.TitleArabic,
                Category = material.Category,
                Subject = material.Subject,
                Teacher = material.Teacher,
                Description = material.Description,
                DescriptionArabic = material.DescriptionArabic,
                Price = material.Price,
                Status = material.Status,
                Delivery = material.Delivery,
                Rating = material.Rating,
                Downloads = material.Downloads,
                Duration = material.Duration,
                Features = features,
                IsActive = material.IsActive,
                CreatedAt = material.CreatedAt,
                UpdatedAt = material.UpdatedAt
            };
        }
    }
}

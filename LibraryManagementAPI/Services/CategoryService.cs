using Microsoft.EntityFrameworkCore;
using LibraryManagementAPI.Data;
using LibraryManagementAPI.DTOs;
using LibraryManagementAPI.Models;

namespace LibraryManagementAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly LibraryDbContext _context;

        public CategoryService(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<List<CategoryDto>> GetCategoriesAsync()
        {
            var categories = await _context.Categories
                .Include(c => c.Books)
                .Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    NameArabic = c.NameArabic,
                    Description = c.Description,
                    DescriptionArabic = c.DescriptionArabic,
                    Icon = c.Icon,
                    IsActive = c.IsActive,
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt,
                    BookCount = c.Books.Count(b => b.IsAvailable)
                })
                .OrderBy(c => c.Name)
                .ToListAsync();

            return categories;
        }

        public async Task<CategoryDto?> GetCategoryByIdAsync(int id)
        {
            var category = await _context.Categories
                .Include(c => c.Books)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null) return null;

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                NameArabic = category.NameArabic,
                Description = category.Description,
                DescriptionArabic = category.DescriptionArabic,
                Icon = category.Icon,
                IsActive = category.IsActive,
                CreatedAt = category.CreatedAt,
                UpdatedAt = category.UpdatedAt,
                BookCount = category.Books.Count(b => b.IsAvailable)
            };
        }

        public async Task<CategoryDto?> CreateCategoryAsync(CreateCategoryDto createCategoryDto)
        {
            var category = new Category
            {
                Name = createCategoryDto.Name,
                NameArabic = createCategoryDto.NameArabic,
                Description = createCategoryDto.Description,
                DescriptionArabic = createCategoryDto.DescriptionArabic,
                Icon = createCategoryDto.Icon,
                IsActive = createCategoryDto.IsActive,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return await GetCategoryByIdAsync(category.Id);
        }

        public async Task<CategoryDto?> UpdateCategoryAsync(int id, UpdateCategoryDto updateCategoryDto)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return null;

            category.Name = updateCategoryDto.Name;
            category.NameArabic = updateCategoryDto.NameArabic;
            category.Description = updateCategoryDto.Description;
            category.DescriptionArabic = updateCategoryDto.DescriptionArabic;
            category.Icon = updateCategoryDto.Icon;
            category.IsActive = updateCategoryDto.IsActive;
            category.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return await GetCategoryByIdAsync(id);
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return false;

            // Check if category has books
            var hasBooks = await _context.Books.AnyAsync(b => b.CategoryId == id);
            if (hasBooks)
            {
                // Soft delete - just deactivate
                category.IsActive = false;
                category.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
            else
            {
                // Hard delete if no books
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }

            return true;
        }

        public async Task<List<CategoryDto>> GetActiveCategoriesAsync()
        {
            var categories = await _context.Categories
                .Include(c => c.Books)
                .Where(c => c.IsActive)
                .Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    NameArabic = c.NameArabic,
                    Description = c.Description,
                    DescriptionArabic = c.DescriptionArabic,
                    Icon = c.Icon,
                    IsActive = c.IsActive,
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt,
                    BookCount = c.Books.Count(b => b.IsAvailable)
                })
                .OrderBy(c => c.Name)
                .ToListAsync();

            return categories;
        }
    }
}


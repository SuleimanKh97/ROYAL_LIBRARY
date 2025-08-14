using Microsoft.EntityFrameworkCore;
using LibraryManagementAPI.Data;
using LibraryManagementAPI.DTOs;
using LibraryManagementAPI.Models;

namespace LibraryManagementAPI.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly LibraryDbContext _context;

        public AuthorService(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<List<AuthorDto>> GetAuthorsAsync()
        {
            var authors = await _context.Authors
                .Include(a => a.Books)
                .Select(a => new AuthorDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    NameArabic = a.NameArabic,
                    Biography = a.Biography,
                    BiographyArabic = a.BiographyArabic,
                    BirthDate = a.BirthDate,
                    Nationality = a.Nationality,
                    IsActive = a.IsActive,
                    CreatedAt = a.CreatedAt,
                    UpdatedAt = a.UpdatedAt,
                    BookCount = a.Books.Count(b => b.IsAvailable)
                })
                .OrderBy(a => a.Name)
                .ToListAsync();

            return authors;
        }

        public async Task<AuthorDto?> GetAuthorByIdAsync(int id)
        {
            var author = await _context.Authors
                .Include(a => a.Books)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (author == null) return null;

            return new AuthorDto
            {
                Id = author.Id,
                Name = author.Name,
                NameArabic = author.NameArabic,
                Biography = author.Biography,
                BiographyArabic = author.BiographyArabic,
                BirthDate = author.BirthDate,
                Nationality = author.Nationality,
                IsActive = author.IsActive,
                CreatedAt = author.CreatedAt,
                UpdatedAt = author.UpdatedAt,
                BookCount = author.Books.Count(b => b.IsAvailable)
            };
        }

        public async Task<AuthorDto?> CreateAuthorAsync(CreateAuthorDto createAuthorDto)
        {
            var author = new Author
            {
                Name = createAuthorDto.Name,
                NameArabic = createAuthorDto.NameArabic,
                Biography = createAuthorDto.Biography,
                BiographyArabic = createAuthorDto.BiographyArabic,
                BirthDate = createAuthorDto.BirthDate,
                Nationality = createAuthorDto.Nationality,
                IsActive = createAuthorDto.IsActive,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            return await GetAuthorByIdAsync(author.Id);
        }

        public async Task<AuthorDto?> UpdateAuthorAsync(int id, UpdateAuthorDto updateAuthorDto)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null) return null;

            author.Name = updateAuthorDto.Name;
            author.NameArabic = updateAuthorDto.NameArabic;
            author.Biography = updateAuthorDto.Biography;
            author.BiographyArabic = updateAuthorDto.BiographyArabic;
            author.BirthDate = updateAuthorDto.BirthDate;
            author.Nationality = updateAuthorDto.Nationality;
            author.IsActive = updateAuthorDto.IsActive;
            author.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return await GetAuthorByIdAsync(id);
        }

        public async Task<bool> DeleteAuthorAsync(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null) return false;

            // Check if author has books
            var hasBooks = await _context.Books.AnyAsync(b => b.AuthorId == id);
            if (hasBooks)
            {
                // Soft delete - just deactivate
                author.IsActive = false;
                author.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
            else
            {
                // Hard delete if no books
                _context.Authors.Remove(author);
                await _context.SaveChangesAsync();
            }

            return true;
        }

        public async Task<List<AuthorDto>> GetActiveAuthorsAsync()
        {
            var authors = await _context.Authors
                .Include(a => a.Books)
                .Where(a => a.IsActive)
                .Select(a => new AuthorDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    NameArabic = a.NameArabic,
                    Biography = a.Biography,
                    BiographyArabic = a.BiographyArabic,
                    BirthDate = a.BirthDate,
                    Nationality = a.Nationality,
                    IsActive = a.IsActive,
                    CreatedAt = a.CreatedAt,
                    UpdatedAt = a.UpdatedAt,
                    BookCount = a.Books.Count(b => b.IsAvailable)
                })
                .OrderBy(a => a.Name)
                .ToListAsync();

            return authors;
        }

        public async Task<List<AuthorDto>> SearchAuthorsAsync(string searchTerm)
        {
            var term = searchTerm.ToLower();
            var authors = await _context.Authors
                .Include(a => a.Books)
                .Where(a => a.IsActive && 
                    (a.Name.ToLower().Contains(term) ||
                     (a.NameArabic != null && a.NameArabic.ToLower().Contains(term))))
                .Select(a => new AuthorDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    NameArabic = a.NameArabic,
                    Biography = a.Biography,
                    BiographyArabic = a.BiographyArabic,
                    BirthDate = a.BirthDate,
                    Nationality = a.Nationality,
                    IsActive = a.IsActive,
                    CreatedAt = a.CreatedAt,
                    UpdatedAt = a.UpdatedAt,
                    BookCount = a.Books.Count(b => b.IsAvailable)
                })
                .OrderBy(a => a.Name)
                .ToListAsync();

            return authors;
        }
    }
}


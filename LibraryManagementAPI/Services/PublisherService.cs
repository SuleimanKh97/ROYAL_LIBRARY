using Microsoft.EntityFrameworkCore;
using LibraryManagementAPI.Data;
using LibraryManagementAPI.DTOs;
using LibraryManagementAPI.Models;

namespace LibraryManagementAPI.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly LibraryDbContext _context;

        public PublisherService(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<List<PublisherDto>> GetPublishersAsync()
        {
            var publishers = await _context.Publishers
                .Include(p => p.Books)
                .Select(p => new PublisherDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    NameArabic = p.NameArabic,
                    Address = p.Address,
                    Phone = p.Phone,
                    Email = p.Email,
                    Website = p.Website,
                    IsActive = p.IsActive,
                    CreatedAt = p.CreatedAt,
                    UpdatedAt = p.UpdatedAt,
                    BookCount = p.Books.Count(b => b.IsAvailable)
                })
                .OrderBy(p => p.Name)
                .ToListAsync();

            return publishers;
        }

        public async Task<PublisherDto?> GetPublisherByIdAsync(int id)
        {
            var publisher = await _context.Publishers
                .Include(p => p.Books)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (publisher == null) return null;

            return new PublisherDto
            {
                Id = publisher.Id,
                Name = publisher.Name,
                NameArabic = publisher.NameArabic,
                Address = publisher.Address,
                Phone = publisher.Phone,
                Email = publisher.Email,
                Website = publisher.Website,
                IsActive = publisher.IsActive,
                CreatedAt = publisher.CreatedAt,
                UpdatedAt = publisher.UpdatedAt,
                BookCount = publisher.Books.Count(b => b.IsAvailable)
            };
        }

        public async Task<PublisherDto?> CreatePublisherAsync(CreatePublisherDto createPublisherDto)
        {
            var publisher = new Publisher
            {
                Name = createPublisherDto.Name,
                NameArabic = createPublisherDto.NameArabic,
                Address = createPublisherDto.Address,
                Phone = createPublisherDto.Phone,
                Email = createPublisherDto.Email,
                Website = createPublisherDto.Website,
                IsActive = createPublisherDto.IsActive,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Publishers.Add(publisher);
            await _context.SaveChangesAsync();

            return await GetPublisherByIdAsync(publisher.Id);
        }

        public async Task<PublisherDto?> UpdatePublisherAsync(int id, UpdatePublisherDto updatePublisherDto)
        {
            var publisher = await _context.Publishers.FindAsync(id);
            if (publisher == null) return null;

            publisher.Name = updatePublisherDto.Name;
            publisher.NameArabic = updatePublisherDto.NameArabic;
            publisher.Address = updatePublisherDto.Address;
            publisher.Phone = updatePublisherDto.Phone;
            publisher.Email = updatePublisherDto.Email;
            publisher.Website = updatePublisherDto.Website;
            publisher.IsActive = updatePublisherDto.IsActive;
            publisher.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return await GetPublisherByIdAsync(id);
        }

        public async Task<bool> DeletePublisherAsync(int id)
        {
            var publisher = await _context.Publishers.FindAsync(id);
            if (publisher == null) return false;

            // Check if publisher has books
            var hasBooks = await _context.Books.AnyAsync(b => b.PublisherId == id);
            if (hasBooks)
            {
                // Soft delete - just deactivate
                publisher.IsActive = false;
                publisher.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
            else
            {
                // Hard delete if no books
                _context.Publishers.Remove(publisher);
                await _context.SaveChangesAsync();
            }

            return true;
        }

        public async Task<List<PublisherDto>> GetActivePublishersAsync()
        {
            var publishers = await _context.Publishers
                .Include(p => p.Books)
                .Where(p => p.IsActive)
                .Select(p => new PublisherDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    NameArabic = p.NameArabic,
                    Address = p.Address,
                    Phone = p.Phone,
                    Email = p.Email,
                    Website = p.Website,
                    IsActive = p.IsActive,
                    CreatedAt = p.CreatedAt,
                    UpdatedAt = p.UpdatedAt,
                    BookCount = p.Books.Count(b => b.IsAvailable)
                })
                .OrderBy(p => p.Name)
                .ToListAsync();

            return publishers;
        }

        public async Task<List<PublisherDto>> SearchPublishersAsync(string searchTerm)
        {
            var term = searchTerm.ToLower();
            var publishers = await _context.Publishers
                .Include(p => p.Books)
                .Where(p => p.IsActive && 
                    (p.Name.ToLower().Contains(term) ||
                     (p.NameArabic != null && p.NameArabic.ToLower().Contains(term))))
                .Select(p => new PublisherDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    NameArabic = p.NameArabic,
                    Address = p.Address,
                    Phone = p.Phone,
                    Email = p.Email,
                    Website = p.Website,
                    IsActive = p.IsActive,
                    CreatedAt = p.CreatedAt,
                    UpdatedAt = p.UpdatedAt,
                    BookCount = p.Books.Count(b => b.IsAvailable)
                })
                .OrderBy(p => p.Name)
                .ToListAsync();

            return publishers;
        }
    }
}


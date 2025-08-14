using Microsoft.EntityFrameworkCore;
using LibraryManagementAPI.Data;
using LibraryManagementAPI.DTOs;

namespace LibraryManagementAPI.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly LibraryDbContext _context;

        public DashboardService(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardStatsDto> GetDashboardStatsAsync()
        {
            var totalBooks = await _context.Books.CountAsync();
            var availableBooks = await _context.Books.CountAsync(b => b.IsAvailable);
            var totalCategories = await _context.Categories.CountAsync(c => c.IsActive);
            var totalAuthors = await _context.Authors.CountAsync(a => a.IsActive);
            var totalPublishers = await _context.Publishers.CountAsync(p => p.IsActive);
            var totalUsers = await _context.Users.CountAsync(u => u.IsActive);
            var totalInquiries = await _context.BookInquiries.CountAsync();
            var featuredBooks = await _context.Books.CountAsync(b => b.IsFeatured && b.IsAvailable);
            var newReleases = await _context.Books.CountAsync(b => b.IsNewRelease && b.IsAvailable);
            var totalInventoryValue = await _context.Books
                .Where(b => b.IsAvailable && b.Price.HasValue)
                .SumAsync(b => (b.Price ?? 0) * b.StockQuantity);
            var lowStockBooks = await _context.Books.CountAsync(b => b.StockQuantity <= 5 && b.StockQuantity > 0);
            var outOfStockBooks = await _context.Books.CountAsync(b => b.StockQuantity == 0);

            return new DashboardStatsDto
            {
                TotalBooks = totalBooks,
                AvailableBooks = availableBooks,
                TotalCategories = totalCategories,
                TotalAuthors = totalAuthors,
                TotalPublishers = totalPublishers,
                TotalUsers = totalUsers,
                TotalInquiries = totalInquiries,
                FeaturedBooks = featuredBooks,
                NewReleases = newReleases,
                TotalInventoryValue = totalInventoryValue,
                LowStockBooks = lowStockBooks,
                OutOfStockBooks = outOfStockBooks
            };
        }

        public async Task<List<RecentActivityDto>> GetRecentActivitiesAsync(int count = 10)
        {
            var activities = new List<RecentActivityDto>();

            // Recent book additions
            var recentBooks = await _context.Books
                .Include(b => b.Author)
                .OrderByDescending(b => b.CreatedAt)
                .Take(count / 2)
                .Select(b => new RecentActivityDto
                {
                    ActivityType = "Book Added",
                    Description = $"New book '{b.Title}' by {b.Author!.Name} was added",
                    UserName = "System",
                    Timestamp = b.CreatedAt,
                    EntityType = "Book",
                    EntityId = b.Id
                })
                .ToListAsync();

            activities.AddRange(recentBooks);

            // Recent inquiries
            var recentInquiries = await _context.BookInquiries
                .Include(i => i.Book)
                .Include(i => i.User)
                .OrderByDescending(i => i.CreatedAt)
                .Take(count / 2)
                .Select(i => new RecentActivityDto
                {
                    ActivityType = "Book Inquiry",
                    Description = $"Inquiry for '{i.Book!.Title}' from {i.CustomerName}",
                    UserName = i.User != null ? i.User.FirstName + " " + i.User.LastName : "Guest",
                    Timestamp = i.CreatedAt,
                    EntityType = "BookInquiry",
                    EntityId = i.Id
                })
                .ToListAsync();

            activities.AddRange(recentInquiries);

            return activities.OrderByDescending(a => a.Timestamp).Take(count).ToList();
        }

        public async Task<List<PopularBookDto>> GetPopularBooksAsync(int count = 10)
        {
            var popularBooks = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Category)
                .Where(b => b.IsAvailable)
                .OrderByDescending(b => b.ViewCount)
                .ThenByDescending(b => b.Rating)
                .Take(count)
                .Select(b => new PopularBookDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    TitleArabic = b.TitleArabic,
                    AuthorName = b.Author!.Name,
                    CategoryName = b.Category!.Name,
                    ViewCount = b.ViewCount,
                    Rating = b.Rating,
                    RatingCount = b.RatingCount,
                    CoverImageUrl = b.CoverImageUrl
                })
                .ToListAsync();

            return popularBooks;
        }

        public async Task<List<CategoryStatsDto>> GetCategoryStatsAsync()
        {
            var totalBooks = await _context.Books.CountAsync(b => b.IsAvailable);
            
            var categoryStats = await _context.Categories
                .Include(c => c.Books)
                .Where(c => c.IsActive)
                .Select(c => new CategoryStatsDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    NameArabic = c.NameArabic,
                    BookCount = c.Books.Count,
                    AvailableBooks = c.Books.Count(b => b.IsAvailable),
                    Percentage = totalBooks > 0 ? (decimal)c.Books.Count(b => b.IsAvailable) / totalBooks * 100 : 0
                })
                .OrderByDescending(c => c.BookCount)
                .ToListAsync();

            return categoryStats;
        }

        public async Task<List<MonthlyStatsDto>> GetMonthlyStatsAsync(int months = 12)
        {
            var startDate = DateTime.UtcNow.AddMonths(-months);
            var monthlyStats = new List<MonthlyStatsDto>();

            for (int i = 0; i < months; i++)
            {
                var monthStart = startDate.AddMonths(i);
                var monthEnd = monthStart.AddMonths(1);

                var booksAdded = await _context.Books
                    .CountAsync(b => b.CreatedAt >= monthStart && b.CreatedAt < monthEnd);

                var inquiries = await _context.BookInquiries
                    .CountAsync(i => i.CreatedAt >= monthStart && i.CreatedAt < monthEnd);

                var newUsers = await _context.Users
                    .CountAsync(u => u.CreatedAt >= monthStart && u.CreatedAt < monthEnd);

                // For view count, we'll use a simple calculation since we don't track historical data
                var viewCount = await _context.Books
                    .Where(b => b.CreatedAt >= monthStart && b.CreatedAt < monthEnd)
                    .SumAsync(b => b.ViewCount);

                monthlyStats.Add(new MonthlyStatsDto
                {
                    Month = monthStart.ToString("MMM yyyy"),
                    BooksAdded = booksAdded,
                    Inquiries = inquiries,
                    NewUsers = newUsers,
                    ViewCount = viewCount
                });
            }

            return monthlyStats;
        }

        public async Task<List<LowStockBookDto>> GetLowStockBooksAsync()
        {
            var lowStockBooks = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Category)
                .Where(b => b.StockQuantity <= 5)
                .OrderBy(b => b.StockQuantity)
                .Select(b => new LowStockBookDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    TitleArabic = b.TitleArabic,
                    AuthorName = b.Author!.Name,
                    CategoryName = b.Category!.Name,
                    StockQuantity = b.StockQuantity,
                    IsAvailable = b.IsAvailable,
                    Price = b.Price
                })
                .ToListAsync();

            return lowStockBooks;
        }

        public async Task<UserStatsDto> GetUserStatsAsync()
        {
            var totalUsers = await _context.Users.CountAsync(u => u.IsActive);
            var adminUsers = await _context.UserRoles
                .Join(_context.Roles, ur => ur.RoleId, r => r.Id, (ur, r) => new { ur.UserId, r.Name })
                .CountAsync(x => x.Name == "Admin");
            var librarianUsers = await _context.UserRoles
                .Join(_context.Roles, ur => ur.RoleId, r => r.Id, (ur, r) => new { ur.UserId, r.Name })
                .CountAsync(x => x.Name == "Librarian");
            var customerUsers = await _context.UserRoles
                .Join(_context.Roles, ur => ur.RoleId, r => r.Id, (ur, r) => new { ur.UserId, r.Name })
                .CountAsync(x => x.Name == "Customer");
            var activeUsers = await _context.Users.CountAsync(u => u.IsActive);
            var inactiveUsers = await _context.Users.CountAsync(u => !u.IsActive);

            // Monthly registrations for the last 12 months
            var monthlyRegistrations = new List<MonthlyUserRegistrationDto>();
            for (int i = 11; i >= 0; i--)
            {
                var monthStart = DateTime.UtcNow.AddMonths(-i).Date;
                var monthEnd = monthStart.AddMonths(1);

                var count = await _context.Users
                    .CountAsync(u => u.CreatedAt >= monthStart && u.CreatedAt < monthEnd);

                monthlyRegistrations.Add(new MonthlyUserRegistrationDto
                {
                    Month = monthStart.ToString("MMM yyyy"),
                    Count = count
                });
            }

            return new UserStatsDto
            {
                TotalUsers = totalUsers,
                AdminUsers = adminUsers,
                LibrarianUsers = librarianUsers,
                CustomerUsers = customerUsers,
                ActiveUsers = activeUsers,
                InactiveUsers = inactiveUsers,
                MonthlyRegistrations = monthlyRegistrations
            };
        }

        public async Task<SystemSettingsDto> GetSystemSettingsAsync()
        {
            var settings = await _context.SystemSettings.ToListAsync();
            
            return new SystemSettingsDto
            {
                LibraryName = GetSettingValue(settings, "LibraryName", "ROYAL STUDY"),
                LibraryNameArabic = GetSettingValue(settings, "LibraryNameArabic", "ROYAL STUDY"),
                ContactEmail = GetSettingValue(settings, "ContactEmail", "info@royalstudy.com"),
                ContactPhone = GetSettingValue(settings, "ContactPhone", "+962785462983"),
                WhatsAppNumber = GetSettingValue(settings, "WhatsAppNumber", "+962785462983"),
                Address = GetSettingValue(settings, "LibraryAddress", "إربد، الأردن"),
                AddressArabic = GetSettingValue(settings, "LibraryAddress", "إربد، الأردن"),
                Website = GetSettingValue(settings, "Website", "https://royalstudy.com"),
                FacebookUrl = GetSettingValue(settings, "FacebookUrl", ""),
                TwitterUrl = GetSettingValue(settings, "TwitterUrl", ""),
                InstagramUrl = GetSettingValue(settings, "InstagramUrl", ""),
                Currency = GetSettingValue(settings, "Currency", "د.أ"),
                CurrencyEnglish = GetSettingValue(settings, "CurrencyEnglish", "JOD"),
                LowStockThreshold = int.Parse(GetSettingValue(settings, "LowStockThreshold", "5")),
                EnableWhatsAppIntegration = bool.Parse(GetSettingValue(settings, "EnableWhatsAppIntegration", "true")),
                EnableEmailNotifications = bool.Parse(GetSettingValue(settings, "EnableEmailNotifications", "true")),
                DefaultLanguage = GetSettingValue(settings, "DefaultLanguage", "Arabic")
            };
        }

        public async Task<bool> UpdateSystemSettingsAsync(UpdateSystemSettingsDto updateDto)
        {
            var settingsToUpdate = new Dictionary<string, string>
            {
                { "LibraryName", updateDto.LibraryName },
                { "LibraryNameArabic", updateDto.LibraryNameArabic },
                { "ContactEmail", updateDto.ContactEmail },
                { "ContactPhone", updateDto.ContactPhone },
                { "WhatsAppNumber", updateDto.WhatsAppNumber },
                { "LibraryAddress", updateDto.Address },
                { "LibraryAddress", updateDto.AddressArabic },
                { "Website", updateDto.Website },
                { "FacebookUrl", updateDto.FacebookUrl },
                { "TwitterUrl", updateDto.TwitterUrl },
                { "InstagramUrl", updateDto.InstagramUrl },
                { "Currency", updateDto.Currency },
                { "CurrencyEnglish", updateDto.CurrencyEnglish },
                { "LowStockThreshold", updateDto.LowStockThreshold.ToString() },
                { "EnableWhatsAppIntegration", updateDto.EnableWhatsAppIntegration.ToString() },
                { "EnableEmailNotifications", updateDto.EnableEmailNotifications.ToString() },
                { "DefaultLanguage", updateDto.DefaultLanguage }
            };

            foreach (var setting in settingsToUpdate)
            {
                var existingSetting = await _context.SystemSettings
                    .FirstOrDefaultAsync(s => s.SettingKey == setting.Key);

                if (existingSetting != null)
                {
                    existingSetting.SettingValue = setting.Value;
                    existingSetting.UpdatedAt = DateTime.UtcNow;
                }
                else
                {
                    _context.SystemSettings.Add(new Models.SystemSetting
                    {
                        SettingKey = setting.Key,
                        SettingValue = setting.Value,
                        UpdatedAt = DateTime.UtcNow
                    });
                }
            }

            await _context.SaveChangesAsync();
            return true;
        }

        private string GetSettingValue(List<Models.SystemSetting> settings, string key, string defaultValue)
        {
            var setting = settings.FirstOrDefault(s => s.SettingKey == key);
            return setting?.SettingValue ?? defaultValue;
        }
    }
}


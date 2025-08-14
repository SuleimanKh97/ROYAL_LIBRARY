namespace LibraryManagementAPI.DTOs
{
    public class DashboardStatsDto
    {
        public int TotalBooks { get; set; }
        public int AvailableBooks { get; set; }
        public int TotalCategories { get; set; }
        public int TotalAuthors { get; set; }
        public int TotalPublishers { get; set; }
        public int TotalUsers { get; set; }
        public int TotalInquiries { get; set; }
        public int FeaturedBooks { get; set; }
        public int NewReleases { get; set; }
        public decimal TotalInventoryValue { get; set; }
        public int LowStockBooks { get; set; }
        public int OutOfStockBooks { get; set; }
    }

    public class RecentActivityDto
    {
        public string ActivityType { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
        public string EntityType { get; set; } = string.Empty;
        public int? EntityId { get; set; }
    }

    public class PopularBookDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? TitleArabic { get; set; }
        public string? AuthorName { get; set; }
        public string? CategoryName { get; set; }
        public int ViewCount { get; set; }
        public decimal Rating { get; set; }
        public int RatingCount { get; set; }
        public string? CoverImageUrl { get; set; }
    }

    public class CategoryStatsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string NameArabic { get; set; } = string.Empty;
        public int BookCount { get; set; }
        public int AvailableBooks { get; set; }
        public decimal Percentage { get; set; }
    }

    public class MonthlyStatsDto
    {
        public string Month { get; set; } = string.Empty;
        public int BooksAdded { get; set; }
        public int Inquiries { get; set; }
        public int NewUsers { get; set; }
        public int ViewCount { get; set; }
    }

    public class LowStockBookDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? TitleArabic { get; set; }
        public string? AuthorName { get; set; }
        public string? CategoryName { get; set; }
        public int StockQuantity { get; set; }
        public bool IsAvailable { get; set; }
        public decimal? Price { get; set; }
    }

    public class UserStatsDto
    {
        public int TotalUsers { get; set; }
        public int AdminUsers { get; set; }
        public int LibrarianUsers { get; set; }
        public int CustomerUsers { get; set; }
        public int ActiveUsers { get; set; }
        public int InactiveUsers { get; set; }
        public List<MonthlyUserRegistrationDto> MonthlyRegistrations { get; set; } = new List<MonthlyUserRegistrationDto>();
    }

    public class MonthlyUserRegistrationDto
    {
        public string Month { get; set; } = string.Empty;
        public int Count { get; set; }
    }

    public class SystemSettingsDto
    {
        public string LibraryName { get; set; } = string.Empty;
        public string LibraryNameArabic { get; set; } = string.Empty;
        public string ContactEmail { get; set; } = string.Empty;
        public string ContactPhone { get; set; } = string.Empty;
        public string WhatsAppNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string AddressArabic { get; set; } = string.Empty;
        public string Website { get; set; } = string.Empty;
        public string FacebookUrl { get; set; } = string.Empty;
        public string TwitterUrl { get; set; } = string.Empty;
        public string InstagramUrl { get; set; } = string.Empty;
        public string Currency { get; set; } = string.Empty;
        public string CurrencyEnglish { get; set; } = string.Empty;
        public int LowStockThreshold { get; set; } = 5;
        public bool EnableWhatsAppIntegration { get; set; } = true;
        public bool EnableEmailNotifications { get; set; } = true;
        public string DefaultLanguage { get; set; } = "Arabic";
    }

    public class UpdateSystemSettingsDto
    {
        public string LibraryName { get; set; } = string.Empty;
        public string LibraryNameArabic { get; set; } = string.Empty;
        public string ContactEmail { get; set; } = string.Empty;
        public string ContactPhone { get; set; } = string.Empty;
        public string WhatsAppNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string AddressArabic { get; set; } = string.Empty;
        public string Website { get; set; } = string.Empty;
        public string FacebookUrl { get; set; } = string.Empty;
        public string TwitterUrl { get; set; } = string.Empty;
        public string InstagramUrl { get; set; } = string.Empty;
        public string Currency { get; set; } = string.Empty;
        public string CurrencyEnglish { get; set; } = string.Empty;
        public int LowStockThreshold { get; set; } = 5;
        public bool EnableWhatsAppIntegration { get; set; } = true;
        public bool EnableEmailNotifications { get; set; } = true;
        public string DefaultLanguage { get; set; } = "Arabic";
    }
}


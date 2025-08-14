using LibraryManagementAPI.DTOs;

namespace LibraryManagementAPI.Services
{
    public interface IDashboardService
    {
        Task<DashboardStatsDto> GetDashboardStatsAsync();
        Task<List<RecentActivityDto>> GetRecentActivitiesAsync(int count = 10);
        Task<List<PopularBookDto>> GetPopularBooksAsync(int count = 10);
        Task<List<CategoryStatsDto>> GetCategoryStatsAsync();
        Task<List<MonthlyStatsDto>> GetMonthlyStatsAsync(int months = 12);
        Task<List<LowStockBookDto>> GetLowStockBooksAsync();
        Task<UserStatsDto> GetUserStatsAsync();
        Task<SystemSettingsDto> GetSystemSettingsAsync();
        Task<bool> UpdateSystemSettingsAsync(UpdateSystemSettingsDto updateDto);
    }
}


using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LibraryManagementAPI.DTOs;
using LibraryManagementAPI.Services;

namespace LibraryManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,Librarian")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("stats")]
        public async Task<IActionResult> GetDashboardStats()
        {
            var stats = await _dashboardService.GetDashboardStatsAsync();
            return Ok(stats);
        }

        [HttpGet("recent-activities")]
        public async Task<IActionResult> GetRecentActivities([FromQuery] int count = 10)
        {
            var activities = await _dashboardService.GetRecentActivitiesAsync(count);
            return Ok(activities);
        }

        [HttpGet("popular-books")]
        public async Task<IActionResult> GetPopularBooks([FromQuery] int count = 10)
        {
            var books = await _dashboardService.GetPopularBooksAsync(count);
            return Ok(books);
        }

        [HttpGet("category-stats")]
        public async Task<IActionResult> GetCategoryStats()
        {
            var stats = await _dashboardService.GetCategoryStatsAsync();
            return Ok(stats);
        }

        [HttpGet("monthly-stats")]
        public async Task<IActionResult> GetMonthlyStats([FromQuery] int months = 12)
        {
            var stats = await _dashboardService.GetMonthlyStatsAsync(months);
            return Ok(stats);
        }

        [HttpGet("low-stock-books")]
        public async Task<IActionResult> GetLowStockBooks()
        {
            var books = await _dashboardService.GetLowStockBooksAsync();
            return Ok(books);
        }

        [HttpGet("user-stats")]
        public async Task<IActionResult> GetUserStats()
        {
            var stats = await _dashboardService.GetUserStatsAsync();
            return Ok(stats);
        }

        [HttpGet("system-settings")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetSystemSettings()
        {
            var settings = await _dashboardService.GetSystemSettingsAsync();
            return Ok(settings);
        }

        [HttpPut("system-settings")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateSystemSettings([FromBody] UpdateSystemSettingsDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _dashboardService.UpdateSystemSettingsAsync(updateDto);
            if (!result)
                return BadRequest(new { message = "Failed to update system settings" });

            return Ok(new { message = "System settings updated successfully" });
        }
    }
}


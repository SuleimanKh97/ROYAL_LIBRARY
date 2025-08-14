using LibraryManagementAPI.DTOs;

namespace LibraryManagementAPI.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto?> LoginAsync(LoginDto loginDto);
        Task<AuthResponseDto?> RegisterAsync(RegisterDto registerDto);
        Task<AuthResponseDto?> RegisterCustomerAsync(RegisterCustomerDto registerDto);
        Task<bool> ChangePasswordAsync(string userId, ChangePasswordDto changePasswordDto);
        Task<UserDto?> GetUserByIdAsync(string userId);
        Task<UserDto?> UpdateUserAsync(string userId, UpdateUserDto updateUserDto);
        Task<bool> DeactivateUserAsync(string userId);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        string GenerateJwtToken(UserDto user);
    }
}


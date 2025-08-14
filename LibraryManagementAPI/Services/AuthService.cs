using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LibraryManagementAPI.DTOs;
using LibraryManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthService(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<AuthResponseDto?> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null || !user.IsActive)
                return null;

            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!result)
                return null;

            var userRoles = await _userManager.GetRolesAsync(user);
            var userDto = new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email!,
                Username = user.UserName!,
                Role = userRoles.FirstOrDefault() ?? "Customer",
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt
            };

            var token = GenerateJwtToken(userDto);
            var expiration = DateTime.UtcNow.AddMinutes(
                _configuration.GetValue<int>("JwtSettings:ExpirationInMinutes", 60));

            return new AuthResponseDto
            {
                Token = token,
                Expiration = expiration,
                User = userDto
            };
        }

        public async Task<AuthResponseDto?> RegisterAsync(RegisterDto registerDto)
        {
            // Check if user already exists
            var existingUser = await _userManager.FindByEmailAsync(registerDto.Email);
            if (existingUser != null)
                return null;

            existingUser = await _userManager.FindByNameAsync(registerDto.Username);
            if (existingUser != null)
                return null;

            // Create new user
            var user = new User
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                UserName = registerDto.Username,
                EmailConfirmed = true, // For simplicity, auto-confirm emails
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
                return null;

            // Ensure role exists
            if (!await _roleManager.RoleExistsAsync(registerDto.Role))
            {
                await _roleManager.CreateAsync(new IdentityRole(registerDto.Role));
            }

            // Assign role to user
            await _userManager.AddToRoleAsync(user, registerDto.Role);

            var userDto = new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email!,
                Username = user.UserName!,
                Role = registerDto.Role,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt
            };

            var token = GenerateJwtToken(userDto);
            var expiration = DateTime.UtcNow.AddMinutes(
                _configuration.GetValue<int>("JwtSettings:ExpirationInMinutes", 60));

            return new AuthResponseDto
            {
                Token = token,
                Expiration = expiration,
                User = userDto
            };
        }

        public async Task<AuthResponseDto?> RegisterCustomerAsync(RegisterCustomerDto registerDto)
        {
            try
            {

                // Check if user already exists
                var existingUser = await _userManager.FindByEmailAsync(registerDto.Email);
                if (existingUser != null)
                    return null;

                existingUser = await _userManager.FindByNameAsync(registerDto.Username);
                if (existingUser != null)
                    return null;

                // Check if phone number already exists
                existingUser = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == registerDto.PhoneNumber);
                if (existingUser != null)
                    return null;

                // Create new user with Customer role only
                var user = new User
                {
                    FirstName = registerDto.FirstName.Trim(),
                    LastName = registerDto.LastName.Trim(),
                    Email = registerDto.Email.Trim().ToLower(),
                    UserName = registerDto.Username.Trim().ToLower(),
                    PhoneNumber = registerDto.PhoneNumber.Trim(),
                    EmailConfirmed = false, // Require email confirmation for security
                    PhoneNumberConfirmed = false,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                var result = await _userManager.CreateAsync(user, registerDto.Password);
                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    throw new InvalidOperationException($"فشل في إنشاء الحساب: {errors}");
                }

                // Ensure Customer role exists
                if (!await _roleManager.RoleExistsAsync("Customer"))
                {
                    await _roleManager.CreateAsync(new IdentityRole("Customer"));
                }

                // Assign Customer role only (no admin privileges)
                var roleResult = await _userManager.AddToRoleAsync(user, "Customer");
                if (!roleResult.Succeeded)
                {
                    // If role assignment fails, delete the user
                    await _userManager.DeleteAsync(user);
                    throw new InvalidOperationException("فشل في تعيين دور المستخدم");
                }

                var userDto = new UserDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email!,
                    Username = user.UserName!,
                    Role = "Customer", // Always Customer role
                    IsActive = user.IsActive,
                    CreatedAt = user.CreatedAt
                };

                var token = GenerateJwtToken(userDto);
                var expiration = DateTime.UtcNow.AddMinutes(
                    _configuration.GetValue<int>("JwtSettings:ExpirationInMinutes", 60));

                return new AuthResponseDto
                {
                    Token = token,
                    Expiration = expiration,
                    User = userDto
                };
            }
            catch (Exception ex)
            {
                // Log the error for debugging
                Console.WriteLine($"Error in RegisterCustomerAsync: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> ChangePasswordAsync(string userId, ChangePasswordDto changePasswordDto)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null || !user.IsActive)
                return false;

            var result = await _userManager.ChangePasswordAsync(
                user, changePasswordDto.CurrentPassword, changePasswordDto.NewPassword);

            if (result.Succeeded)
            {
                user.UpdatedAt = DateTime.UtcNow;
                await _userManager.UpdateAsync(user);
            }

            return result.Succeeded;
        }

        public async Task<UserDto?> GetUserByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return null;

            var userRoles = await _userManager.GetRolesAsync(user);

            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email!,
                Username = user.UserName!,
                Role = userRoles.FirstOrDefault() ?? "Customer",
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt
            };
        }

        public async Task<UserDto?> UpdateUserAsync(string userId, UpdateUserDto updateUserDto)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return null;

            // Check if email is already taken by another user
            var existingUser = await _userManager.FindByEmailAsync(updateUserDto.Email);
            if (existingUser != null && existingUser.Id != userId)
                return null;

            // Check if username is already taken by another user
            existingUser = await _userManager.FindByNameAsync(updateUserDto.Username);
            if (existingUser != null && existingUser.Id != userId)
                return null;

            user.FirstName = updateUserDto.FirstName;
            user.LastName = updateUserDto.LastName;
            user.Email = updateUserDto.Email;
            user.UserName = updateUserDto.Username;
            user.IsActive = updateUserDto.IsActive;
            user.UpdatedAt = DateTime.UtcNow;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return null;

            var userRoles = await _userManager.GetRolesAsync(user);

            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Username = user.UserName,
                Role = userRoles.FirstOrDefault() ?? "Customer",
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt
            };
        }

        public async Task<bool> DeactivateUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return false;

            user.IsActive = false;
            user.UpdatedAt = DateTime.UtcNow;

            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var userDtos = new List<UserDto>();

            foreach (var user in users)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                userDtos.Add(new UserDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email!,
                    Username = user.UserName!,
                    Role = userRoles.FirstOrDefault() ?? "Customer",
                    IsActive = user.IsActive,
                    CreatedAt = user.CreatedAt
                });
            }

            return userDtos;
        }

        public string GenerateJwtToken(UserDto user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"];
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];
            var expirationInMinutes = jwtSettings.GetValue<int>("ExpirationInMinutes", 60);

            var key = Encoding.ASCII.GetBytes(secretKey!);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.GivenName, user.FirstName),
                    new Claim(ClaimTypes.Surname, user.LastName),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim("IsActive", user.IsActive.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(expirationInMinutes),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}


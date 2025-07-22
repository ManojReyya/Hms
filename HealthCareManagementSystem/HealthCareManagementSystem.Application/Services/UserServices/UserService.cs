using HealthCareManagementSystem.Application.DTOs.UserDTOs;
using HealthCareManagementSystem.Domain.Entities;
using HealthCareManagementSystem.Domain.Enums;
using HealthCareManagementSystem.Infrastructure.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HealthCareManagementSystem.Application.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly IUserContract _userRepo;
        private readonly IConfiguration _configuration;

        public UserService(IUserContract userRepo, IConfiguration configuration)
        {
            _userRepo = userRepo;
            _configuration = configuration;
        }

        public async Task<IEnumerable<UserReadDTO>> GetAllUsersAsync()
        {
            var users = await _userRepo.GetAllUsersAsync();
            return users.Select(u => new UserReadDTO
            {
                UserId = u.UserId,
                Role = u.Role,
                CreatedAt = u.CreatedAt
            });
        }

        public async Task<UserReadDTO?> GetUserByIdAsync(string userId)
        {
            var user = await _userRepo.GetUserByIdAsync(userId);
            if (user == null) return null;

            return new UserReadDTO
            {
                UserId = user.UserId,
                Role = user.Role,
                CreatedAt = user.CreatedAt
            };
        }

        public async Task CreateUserAsync(UserCreateDTO dto)
        {
            if (dto.Role == UserRole.Admin)
                throw new InvalidOperationException("Creating Admin users is not allowed.");
            
            var user = new User
            {
                UserId = dto.UserId,
                Password = dto.Password,
                Role = dto.Role,
                CreatedAt = DateTime.UtcNow
            };
            await _userRepo.CreateUserAsync(user);
        }

        public async Task UpdateUserAsync(UserUpdateDTO dto)
        {
            var user = await _userRepo.GetUserByIdAsync(dto.UserId);
            if (user == null)
                throw new Exception("User not found");
            if (user.Role == UserRole.Admin)
                throw new InvalidOperationException("Admin users cannot be updated.");

            if (!string.IsNullOrWhiteSpace(dto.Password))
                user.Password = dto.Password;

            if (dto.Role.HasValue)
                user.Role = dto.Role.Value;

            await _userRepo.UpdateUserAsync(user);
        }

        public async Task DeleteUserAsync(string userId)
        {
            var user = await _userRepo.GetUserByIdAsync(userId);
            if (user == null)
                throw new KeyNotFoundException("User not found.");

            if (user.Role == UserRole.Admin)
                throw new InvalidOperationException("Admin users cannot be deleted.");

            await _userRepo.DeleteUserAsync(userId);
        }

        public async Task DeactivateUser(string userId)
        {
            var user = await _userRepo.GetUserByIdAsync(userId);
            if (user == null)
                throw new KeyNotFoundException("User not found.");

            if (user.Role == UserRole.Admin)
                throw new InvalidOperationException("Admin users cannot be deactivated.");

            await _userRepo.DeactivateUser(userId);
        }

        // Authentication methods
        public async Task RegisterAsync(User user)
        {
            user.CreatedAt = DateTime.UtcNow;
            user.IsActive = true;
            await _userRepo.Register(user);
        }

        public async Task<UserResponseDTO?> LoginAsync(LoginDTO loginDto)
        {
            if (loginDto == null || string.IsNullOrEmpty(loginDto.UserId) || string.IsNullOrEmpty(loginDto.Password))
            {
                return null;
            }

            var validatedUser = await _userRepo.Validate(loginDto.UserId, loginDto.Password);
            if (validatedUser == null)
            {
                return null;
            }

            var response = new UserResponseDTO
            {
                UserId = validatedUser.UserId,
                Role = validatedUser.Role.ToString(),
                Token = GetToken(validatedUser)
            };

            return response;
        }

        private string GetToken(User user)
        {
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

            // Create a signing key using the symmetric security key
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256
            );

            // Create claims for the user
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserId),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
            };

            // Set the expiration time for the token
            var expires = DateTime.UtcNow.AddMinutes(60); // 1 hour expiration

            // Create the JWT token
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expires,
                signingCredentials: signingCredentials
            );

            // Serialize the token to a string
            var tokenStr = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenStr;
        }
    }
}
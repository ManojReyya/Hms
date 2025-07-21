using HealthCareManagementSystem.Application.DTOs.UserDTOs;
using HealthCareManagementSystem.Domain.Entities;
using HealthCareManagementSystem.Domain.Enums;
using HealthCareManagementSystem.Infrastructure.Contracts;

namespace HealthCareManagementSystem.Application.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly IUserContract _userRepo;

        public UserService(IUserContract userRepo)
        {
            _userRepo = userRepo;
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
    }
}
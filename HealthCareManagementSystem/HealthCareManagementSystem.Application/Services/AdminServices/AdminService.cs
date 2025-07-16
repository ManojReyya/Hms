using HealthCareManagementSystem.Application.DTOs.AdminDTOs;
using HealthCareManagementSystem.Domain.Enums;
using HealthCareManagementSystem.Infrastructure.Contracts;

namespace HealthCareManagementSystem.Application.Services.AdminServices
{
    public class AdminService : IAdminService
    {
        private readonly IUserContract _userRepo;

        public AdminService(IUserContract userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<IEnumerable<AdminReadDTO>> GetAllAdminsAsync()
        {
            var users = await _userRepo.GetAllUsersAsync();
            return users
                .Where(u => u.Role == UserRole.Admin)
                .Select(u => new AdminReadDTO
                {
                    UserId = u.UserId,
                    CreatedAt = u.CreatedAt
                });
        }

        public async Task<AdminReadDTO?> GetAdminByIdAsync(string adminId)
        {
            var user = await _userRepo.GetUserByIdAsync(adminId);
            if (user == null || user.Role != UserRole.Admin)
                return null;

            return new AdminReadDTO
            {
                UserId = user.UserId,
                CreatedAt = user.CreatedAt
            };
        }
    }
}
using HealthCareManagementSystem.Domain.Entities;
using HealthCareManagementSystem.Infrastructure.Contracts;
using HealthCareManagementSystem.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace HealthCareManagementSystem.Infrastructure.Repositories
{
    public class AdminRepository : IAdminContract
    {
        private readonly HealthCareManagementSystemDbContext _dbContext;

        public AdminRepository(HealthCareManagementSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<User>> GetAllAdminsAsync()
        {
            return await _dbContext.Users
                .Where(u => u.Role == UserRole.Admin)
                .ToListAsync();
        }

        public async Task<User?> GetAdminByIdAsync(string adminId)
        {
            return await _dbContext.Users
                .FirstOrDefaultAsync(u => u.UserId == adminId && u.Role == UserRole.Admin);
        }
    }
}
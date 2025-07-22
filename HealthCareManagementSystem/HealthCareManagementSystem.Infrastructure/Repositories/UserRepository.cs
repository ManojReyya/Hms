using System.Collections.Generic;
using System.Threading.Tasks;
using HealthCareManagementSystem.Domain.Entities;
using HealthCareManagementSystem.Infrastructure.Contracts;
using Microsoft.EntityFrameworkCore;
using HealthCareManagementSystem.Infrastructure;

namespace HealthCareManagementSystem.Infrastructure.Repositories
{
    public class UserRepository : IUserContract
    {
        private readonly HealthCareManagementSystemDbContext _dbContext;

        public UserRepository(HealthCareManagementSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(string userId)
        {
            return await _dbContext.Users
                .FirstOrDefaultAsync(u => u.UserId == userId);
        }

        public async Task<User> CreateUserAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            var existingUser = await _dbContext.Users.FindAsync(user.UserId);
            if (existingUser == null) return null!;

            _dbContext.Entry(existingUser).CurrentValues.SetValues(user);
            await _dbContext.SaveChangesAsync();
            return existingUser;
        }

        public async Task<User> DeactivateUser(string userId)
        {
            var existingUser = await _dbContext.Users.FindAsync(userId);
            if (existingUser == null) return null!;
            existingUser.IsActive = false;
            await _dbContext.SaveChangesAsync();
            return existingUser;
        }

        public async Task<User> DeleteUserAsync(string userId)
        {
            var user = await _dbContext.Users.FindAsync(userId);
            if (user == null) return null!;

            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        // Authentication methods following JWT pattern
        public async Task Register(User user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<User> Validate(string userId, string password)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(u => u.UserId == userId && u.Password == password && u.IsActive);
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using HealthCareManagementSystem.Domain.Entities;

namespace HealthCareManagementSystem.Infrastructure.Contracts
{
    public interface IUserContract
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(string userId);
        Task<User> CreateUserAsync(User user);
        Task<User> UpdateUserAsync(User user);
        Task<User> DeleteUserAsync(string userId);
        Task<User> DeactivateUser(string userId);
        
        // Authentication methods following JWT pattern
        Task Register(User user);
        Task<User> Validate(string userId, string password);
    }
}

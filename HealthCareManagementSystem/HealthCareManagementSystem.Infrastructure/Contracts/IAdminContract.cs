using HealthCareManagementSystem.Domain.Entities;

namespace HealthCareManagementSystem.Infrastructure.Contracts
{
    public interface IAdminContract
    {
        Task<IEnumerable<User>> GetAllAdminsAsync();
        Task<User?> GetAdminByIdAsync(string userId);
    }
}

using HealthCareManagementSystem.Application.DTOs.AdminDTOs;

namespace HealthCareManagementSystem.Application.Services.AdminServices
{
    public interface IAdminService
    {
        Task<IEnumerable<AdminReadDTO>> GetAllAdminsAsync();
        Task<AdminReadDTO?> GetAdminByIdAsync(string adminId);
    }
}
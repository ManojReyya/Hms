using HealthCareManagementSystem.Application.DTOs.UserDTOs;

namespace HealthCareManagementSystem.Application.Services.UserServices
{
    public interface IUserService
    {
        Task<IEnumerable<UserReadDTO>> GetAllUsersAsync();
        Task<UserReadDTO?> GetUserByIdAsync(string userId);
        Task CreateUserAsync(UserCreateDTO dto);
        Task UpdateUserAsync(UserUpdateDTO dto);
        Task DeleteUserAsync(string userId);
    }
}
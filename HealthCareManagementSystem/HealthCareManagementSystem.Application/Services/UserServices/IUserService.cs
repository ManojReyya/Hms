using HealthCareManagementSystem.Application.DTOs.UserDTOs;
using HealthCareManagementSystem.Domain.Entities;

namespace HealthCareManagementSystem.Application.Services.UserServices
{
    public interface IUserService
    {
        Task<IEnumerable<UserReadDTO>> GetAllUsersAsync();
        Task<UserReadDTO?> GetUserByIdAsync(string userId);
        Task CreateUserAsync(UserCreateDTO dto);
        Task UpdateUserAsync(UserUpdateDTO dto);
        Task DeleteUserAsync(string userId);
        Task DeactivateUser(string userId);
        
        // Authentication methods
        Task RegisterAsync(User user);
        Task<UserResponseDTO?> LoginAsync(LoginDTO loginDto);
    }
}
using HealthCareManagementSystem.Domain.Enums;

namespace HealthCareManagementSystem.Application.DTOs.UserDTOs
{
    public class UserReadDTO
    {
        public string UserId { get; set; } = null!;
        public UserRole Role { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
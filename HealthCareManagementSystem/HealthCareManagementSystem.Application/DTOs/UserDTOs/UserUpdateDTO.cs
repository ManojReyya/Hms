using System.ComponentModel.DataAnnotations;
using HealthCareManagementSystem.Domain.Enums;

namespace HealthCareManagementSystem.Application.DTOs.UserDTOs
{
    public class UserUpdateDTO
    {
        [Required, StringLength(100)]
        public string UserId { get; set; } = null!;

        [Required, StringLength(255)]
        public string Password { get; set; } = null!;

        public UserRole? Role { get; set; }
    }
}
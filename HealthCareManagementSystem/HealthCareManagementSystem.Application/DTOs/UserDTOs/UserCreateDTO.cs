using System.ComponentModel.DataAnnotations;
using HealthCareManagementSystem.Domain.Enums;

namespace HealthCareManagementSystem.Application.DTOs.UserDTOs
{
    public class UserCreateDTO
    {
        [Required, StringLength(100)]
        public string UserId { get; set; } = null!;

        [Required, StringLength(255)]
        public string Password { get; set; } = null!;

        [Required]
        public UserRole Role { get; set; }
    }
}

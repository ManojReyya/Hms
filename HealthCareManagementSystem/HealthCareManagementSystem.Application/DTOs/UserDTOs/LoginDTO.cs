using System.ComponentModel.DataAnnotations;

namespace HealthCareManagementSystem.Application.DTOs.UserDTOs
{
    public class LoginDTO
    {
        [Required]
        public string UserId { get; set; } = null!;
        
        [Required]
        public string Password { get; set; } = null!;
    }
}
namespace HealthCareManagementSystem.Application.DTOs.UserDTOs
{
    public class UserResponseDTO
    {
        public string UserId { get; set; } = null!;
        public string Token { get; set; } = null!;
        public string Role { get; set; } = null!;
    }
}
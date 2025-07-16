namespace HealthCareManagementSystem.Application.DTOs.DoctorDTOs;

public class DoctorCreateDTO
{
    public string DoctorId { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string? Bio { get; set; }
    public string? Department { get; set; }
    public TimeSpan WorkStart { get; set; }
    public TimeSpan WorkEnd { get; set; }
    public string? Qualification { get; set; }
    public string Email { get; set; } = null!;
    public string ContactNum { get; set; } = null!;
    public int ExperienceYears { get; set; }
}

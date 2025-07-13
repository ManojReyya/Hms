namespace HealthCareManagementSystem.Application.DTOs
{
    public class DoctorDTO
    {
        public string DoctorId { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string? Department { get; set; }
        public string ContactNum { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Bio { get; set; }
        public string? Qualification { get; set; }
        public int ExperienceYears { get; set; }
        public int AppointmentsDone { get; set; }
    }

}

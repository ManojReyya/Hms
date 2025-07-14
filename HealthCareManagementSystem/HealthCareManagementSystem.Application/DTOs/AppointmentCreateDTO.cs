using HealthCareManagementSystem.Domain.Enums;

namespace HealthCareManagementSystem.Application.DTOs;

public class AppointmentCreateDTO
{
    public string DoctorId { get; set; } = null!;
    public string PatientId { get; set; } = null!;
    public DateTime AppointmentDate { get; set; }
    public TimeSpan SlotStart { get; set; }
    public TimeSpan SlotEnd { get; set; }
    public AppointmentStatus Status { get; set; }
}
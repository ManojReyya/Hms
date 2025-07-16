using HealthCareManagementSystem.Domain.Enums;

namespace HealthCareManagementSystem.Application.DTOs.AppointmentDTOs;

public class AppointmentUpdateDTO
{
    public int AppointmentId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public TimeSpan SlotStart { get; set; }
    public TimeSpan SlotEnd { get; set; }
    public AppointmentStatus Status { get; set; }
}
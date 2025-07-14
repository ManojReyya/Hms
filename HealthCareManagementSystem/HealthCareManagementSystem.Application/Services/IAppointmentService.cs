using HealthCareManagementSystem.Application.DTOs;

namespace HealthCareManagementSystem.Application.Services;

public interface IAppointmentService
{
    Task<IEnumerable<AppointmentReadDTO>> GetAllAsync();
    Task<AppointmentReadDTO?> GetByIdAsync(int id);
    Task<IEnumerable<AppointmentReadDTO>> GetAppointmentsByDoctorIdAsync(string doctorId);
    Task<IEnumerable<AppointmentReadDTO>> GetAppointmentsByPatientIdAsync(string patientId);
    Task<AppointmentReadDTO> CreateAsync(AppointmentCreateDTO dto);
    Task<AppointmentReadDTO?> UpdateAsync(AppointmentUpdateDTO dto);
    Task<AppointmentReadDTO?> DeleteAsync(int id);
}
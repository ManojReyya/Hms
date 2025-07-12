using HealthCareManagementSystem.Domain.Entities;

namespace HealthCareManagementSystem.Infrastructure.Contracts;

public interface IAppointmentContract
{
    Task<IEnumerable<Appointment>> GetAllAppointmentsAsync();
    Task<Appointment?> GetAppointmentByAppointmentIdAsync(int appointmentId);
    Task<IEnumerable<Appointment>> GetAppointmentsByDoctorIdAsync(string doctorId);
    Task<IEnumerable<Appointment>>  GetAppointmentsByPatientIdAsync(string patientId);
    Task<IEnumerable<Appointment>> GetAppointmentsByDateAsync(DateTime date);
    Task<Appointment> CreateAppointmentAsync(Appointment appointment);
    Task<Appointment> UpdateAppointmentAsync(Appointment appointment);
    Task<Appointment> DeleteAppointmentAsync(int appointmentId);
    
}
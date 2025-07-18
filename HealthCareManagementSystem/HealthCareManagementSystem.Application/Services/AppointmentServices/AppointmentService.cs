using HealthCareManagementSystem.Application;
using HealthCareManagementSystem.Application.DTOs.AppointmentDTOs;
using HealthCareManagementSystem.Domain.Entities;
using HealthCareManagementSystem.Infrastructure.Contracts;

namespace HealthCareManagementSystem.Application.Services.AppointmentServices;

public class AppointmentService: IAppointmentService
{
    private readonly IAppointmentContract _appointmentRepo;

    public AppointmentService(IAppointmentContract appointmentContract)
    {
        _appointmentRepo = appointmentContract;
    }
    
    public async Task<IEnumerable<AppointmentReadDTO>> GetAllAsync()
    {
        var appointments = await _appointmentRepo.GetAllAppointmentsAsync();
        return appointments.Select(a => new AppointmentReadDTO()
        {
            AppointmentId = a.AppointmentId,
            DoctorId = a.DoctorId,
            PatientId = a.PatientId,
            AppointmentDate = a.AppointmentDate,
            SlotStart = a.SlotStart,
            SlotEnd = a.SlotEnd,
            Status = a.Status
        });
    }

    public async Task<AppointmentReadDTO?> GetByIdAsync(int id)
    {
        var appointment = await _appointmentRepo.GetAppointmentByAppointmentIdAsync(id);
        return appointment == null ? null : new AppointmentReadDTO()
        {
            AppointmentId = appointment.AppointmentId,
            DoctorId = appointment.DoctorId,
            PatientId = appointment.PatientId,
            AppointmentDate = appointment.AppointmentDate,
            SlotStart = appointment.SlotStart,
            SlotEnd = appointment.SlotEnd,
            Status = appointment.Status
        };
    }

    public async Task<IEnumerable<AppointmentReadDTO>> GetAppointmentsByDoctorIdAsync(string doctorId)
    {
        var appointments = await _appointmentRepo.GetAppointmentsByDoctorIdAsync(doctorId);

        return appointments.Select(a => new AppointmentReadDTO
        {
            AppointmentId = a.AppointmentId,
            DoctorId = a.DoctorId,
            PatientId = a.PatientId,
            AppointmentDate = a.AppointmentDate,
            SlotStart = a.SlotStart,
            SlotEnd = a.SlotEnd,
            Status = a.Status
        });
    }

    public async Task<IEnumerable<AppointmentReadDTO>> GetAppointmentsByPatientIdAsync(string patientId)
    {
        var appointments = await _appointmentRepo.GetAppointmentsByPatientIdAsync(patientId);

        return appointments.Select(a => new AppointmentReadDTO
        {
            AppointmentId = a.AppointmentId,
            DoctorId = a.DoctorId,
            PatientId = a.PatientId,
            AppointmentDate = a.AppointmentDate,
            SlotStart = a.SlotStart,
            SlotEnd = a.SlotEnd,
            Status = a.Status
        });
    }

    public async Task<AppointmentReadDTO> CreateAsync(AppointmentCreateDTO dto)
    {
        var appointment = new Appointment()
        {
            DoctorId = dto.DoctorId,
            PatientId = dto.PatientId,
            AppointmentDate = dto.AppointmentDate,
            SlotStart = dto.SlotStart,
            SlotEnd = dto.SlotEnd,
            Status = dto.Status
        };
        
        var createdAppointment = await _appointmentRepo.CreateAppointmentAsync(appointment);
        
        return new AppointmentReadDTO()
        {
            AppointmentId = createdAppointment.AppointmentId,
            DoctorId = createdAppointment.DoctorId,
            PatientId = createdAppointment.PatientId,
            AppointmentDate = createdAppointment.AppointmentDate,
            SlotStart = createdAppointment.SlotStart,
            SlotEnd = createdAppointment.SlotEnd,
            Status = createdAppointment.Status
        };
    }

    public async Task<AppointmentReadDTO?> UpdateAsync(AppointmentUpdateDTO dto)
    {
        var existing = await _appointmentRepo.GetAppointmentByAppointmentIdAsync(dto.AppointmentId);
        if (existing == null) return null;

        existing.AppointmentDate = dto.AppointmentDate;
        existing.SlotStart = dto.SlotStart;
        existing.SlotEnd = dto.SlotEnd;
        existing.Status = dto.Status;

        var updated = await _appointmentRepo.UpdateAppointmentAsync(existing);
        
        if (updated == null) return null;

        return new AppointmentReadDTO()
        {
            AppointmentId = updated.AppointmentId,
            DoctorId = updated.DoctorId,
            PatientId = updated.PatientId,
            AppointmentDate = updated.AppointmentDate,
            SlotStart = updated.SlotStart,
            SlotEnd = updated.SlotEnd,
            Status = updated.Status
        };
    }

    public async Task<AppointmentReadDTO?> DeleteAsync(int id)
    {
        var deleted = await _appointmentRepo.DeleteAppointmentAsync(id);
        if (deleted == null) return null;
        return new AppointmentReadDTO
        {
            AppointmentId = deleted.AppointmentId,
            DoctorId = deleted.DoctorId,
            PatientId = deleted.PatientId,
            AppointmentDate = deleted.AppointmentDate,
            SlotStart = deleted.SlotStart,
            SlotEnd = deleted.SlotEnd,
            Status = deleted.Status
        };
    }
}
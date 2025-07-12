using HealthCareManagementSystem.Domain.Entities;
using HealthCareManagementSystem.Infrastructure.Contracts;
using Microsoft.EntityFrameworkCore;

namespace HealthCareManagementSystem.Infrastructure.Repositories;

public class AppointmentRepository: IAppointmentContract
{
    private readonly HealthCareManagementSystemDbContext _dbContext;
    
    public AppointmentRepository(HealthCareManagementSystemDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync()
    {
        return await _dbContext.Appointments
            .Include(a => a.Doctor)
            .Include(a => a.Patient)
            .ToListAsync();
    }

    public async Task<Appointment?> GetAppointmentByAppointmentIdAsync(int appointmentId)
    {
        return await _dbContext.Appointments
            .Include(a => a.Doctor)
            .Include(a => a.Patient)
            .FirstAsync(a => a.AppointmentId == appointmentId);
    }

    public async Task<IEnumerable<Appointment>> GetAppointmentsByDoctorIdAsync(string doctorId)
    {
        return await _dbContext.Appointments
            .Where(a => a.DoctorId == doctorId)
            .Include(a => a.Patient)
            .ToListAsync();
    }

    public async Task<IEnumerable<Appointment>> GetAppointmentsByPatientIdAsync(string patientId)
    {
        return await _dbContext.Appointments
            .Where(a => a.PatientId == patientId)
            .Include(a => a.Doctor)
            .ToListAsync();
    }

    public async Task<IEnumerable<Appointment>> GetAppointmentsByDateAsync(DateTime date)
    {
        return await _dbContext.Appointments
            .Where(a => a.AppointmentDate.Date == date.Date)
            .Include(a => a.Doctor)
            .Include(a => a.Patient)
            .ToListAsync();
    }

    public async Task<Appointment> CreateAppointmentAsync(Appointment appointment)
    {
        _dbContext.Appointments.Add(appointment);
        await _dbContext.SaveChangesAsync();
        return appointment;
    }

    public async Task<Appointment> UpdateAppointmentAsync(Appointment appointment)
    {
        var existing = await _dbContext.Appointments.FindAsync(appointment.AppointmentId);
        if (existing == null) return null!;

        _dbContext.Entry(existing).CurrentValues.SetValues(appointment);
        await _dbContext.SaveChangesAsync();
        return existing;
    }

    public async Task<Appointment> DeleteAppointmentAsync(int appointmentId)
    {
        var existing = _dbContext.Appointments.Find(appointmentId);
        if (existing == null) return null!;

        _dbContext.Appointments.Remove(existing);
        await _dbContext.SaveChangesAsync();
        
        return existing;
    }
}
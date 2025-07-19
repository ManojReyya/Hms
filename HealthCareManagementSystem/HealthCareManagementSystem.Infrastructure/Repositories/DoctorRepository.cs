using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCareManagementSystem.Domain.Entities;
using HealthCareManagementSystem.Infrastructure.Contracts;
using Microsoft.EntityFrameworkCore;

namespace HealthCareManagementSystem.Infrastructure.Repositories
{
    public class DoctorRepository : IDoctorContract
    {
        private readonly HealthCareManagementSystemDbContext _dbContext;

        public DoctorRepository(HealthCareManagementSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Doctor>> GetAllDoctorsAsync()
        {
            return await _dbContext.Doctors
                .Include(d => d.User)
                .Include(d => d.Appointments)
                .Include(d => d.MedicalRecords)
                .ToListAsync();
        }

        public async Task<Doctor?> GetDoctorByIdAsync(string doctorId)
        {
            return await _dbContext.Doctors
                .Include(d => d.User)
                .Include(d => d.Appointments)
                .Include(d => d.MedicalRecords)
                .FirstOrDefaultAsync(d => d.DoctorId == doctorId);
        }

        public async Task<Doctor> CreateDoctorAsync(Doctor doctor)
        {
            await _dbContext.Doctors.AddAsync(doctor);
            await _dbContext.SaveChangesAsync();
            return doctor;
        }

        public async Task<Doctor> UpdateDoctorAsync(Doctor doctor)
        {
            var existingDoctor = await _dbContext.Doctors.FindAsync(doctor.DoctorId);
            if (existingDoctor == null) return null!;

            _dbContext.Entry(existingDoctor).CurrentValues.SetValues(doctor);
            await _dbContext.SaveChangesAsync();
            return existingDoctor;
        }

        public async Task<Doctor> DeleteDoctorAsync(string doctorId)
        {
            var doctor = await _dbContext.Doctors.FindAsync(doctorId);
            if (doctor == null) return null!;

            _dbContext.Doctors.Remove(doctor);
            await _dbContext.SaveChangesAsync();
            return doctor;
        }

        public async Task<Doctor> ApproveDoctorAsync(string userId, Doctor doctorProfile)
        {
            var doctor = await _dbContext.Doctors.FirstOrDefaultAsync(d => d.DoctorId == userId);
            if (doctor == null) return null!;

            doctor.IsApproved = true;
            doctor.Bio = doctorProfile.Bio;
            doctor.Department = doctorProfile.Department;
            doctor.WorkStart = doctorProfile.WorkStart;
            doctor.WorkEnd = doctorProfile.WorkEnd;
            doctor.Qualification = doctorProfile.Qualification;

            await _dbContext.SaveChangesAsync();
            return doctor;
        }

        public async Task<IEnumerable<Doctor>> GetPendingDoctorsAsync()
        {
            return await _dbContext.Doctors
                .Where(d => d.IsApproved == false)
                .ToListAsync();
        }

        public async Task<IEnumerable<Doctor>> GetDoctorsByDepartmentAsync(string department)
        {
            return await _dbContext.Doctors
                .Where(d => d.Department != null && d.Department.ToLower() == department.ToLower())
                .ToListAsync();
        }

    }
}

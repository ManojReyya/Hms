using HealthCareManagementSystem.Application.DTOs;
using HealthCareManagementSystem.Domain.Entities;
using HealthCareManagementSystem.Infrastructure.Contracts;
using HealthCareManagementSystem.Infrastructure.Repositories;
using HealthCareManagementSystem.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCareManagementSystem.Application.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorContract _doctorRepository;

        public DoctorService(HealthCareManagementSystemDbContext dbContext)
        {
            _doctorRepository = new DoctorRepository(dbContext);
        }

        public async Task AddDoctorAsync(DoctorDTO doctor)
        {
            var doctorEntity = new Doctor
            {
                DoctorId = doctor.DoctorId,
                FullName = doctor.FullName,
                Department = doctor.Department,
                ContactNum = doctor.ContactNum,
                Email = doctor.Email,
                Bio = doctor.Bio,
                Qualification = doctor.Qualification,
                ExperienceYears = doctor.ExperienceYears,
                AppointmentsDone = doctor.AppointmentsDone,
                // Default values
                Available = true,
                IsApproved = false,
                WorkStart = new TimeSpan(9, 0, 0),
                WorkEnd = new TimeSpan(17, 0, 0)
            };

            await _doctorRepository.CreateDoctorAsync(doctorEntity);
        }

        public async Task DeleteDoctorAsync(string doctorId)
        {
            await _doctorRepository.DeleteDoctorAsync(doctorId);
        }

        public async Task<IEnumerable<DoctorDTO>> GetAllDoctorsAsync()
        {
            var doctors = await _doctorRepository.GetAllDoctorsAsync();
            return doctors.Select(d => new DoctorDTO
            {
                DoctorId = d.DoctorId,
                FullName = d.FullName,
                Department = d.Department ?? "N/A",
                ContactNum = d.ContactNum,
                Email = d.Email
            });
        }

        public async Task<DoctorDTO> GetDoctorByIdAsync(string doctorId)
        {
            var doctor = await _doctorRepository.GetDoctorByIdAsync(doctorId);
            if (doctor == null) return null;

            return new DoctorDTO
            {
                DoctorId = doctor.DoctorId,
                FullName = doctor.FullName,
                Department = doctor.Department ?? "N/A",
                ContactNum = doctor.ContactNum,
                Email = doctor.Email
            };
        }

        public async Task UpdateDoctorAsync(DoctorDTO doctor)
        {
            var existing = await _doctorRepository.GetDoctorByIdAsync(doctor.DoctorId);
            if (existing == null) return;

            existing.FullName = doctor.FullName;
            existing.Department = doctor.Department;
            existing.ContactNum = doctor.ContactNum;
            existing.Email = doctor.Email;
            existing.Bio = doctor.Bio;
            existing.Qualification = doctor.Qualification;
            existing.ExperienceYears = doctor.ExperienceYears;
            existing.AppointmentsDone = doctor.AppointmentsDone;

            await _doctorRepository.UpdateDoctorAsync(existing);
        }

    }
}

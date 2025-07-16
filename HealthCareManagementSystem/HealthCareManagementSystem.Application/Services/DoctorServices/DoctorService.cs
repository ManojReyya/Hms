using HealthCareManagementSystem.Application.DTOs.DoctorDTOs;
using HealthCareManagementSystem.Domain.Entities;
using HealthCareManagementSystem.Infrastructure.Contracts;
using HealthCareManagementSystem.Infrastructure.Repositories;
using HealthCareManagementSystem.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCareManagementSystem.Application.Services.DoctorServices
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorContract _doctorRepository;

        public DoctorService(HealthCareManagementSystemDbContext dbContext)
        {
            _doctorRepository = new DoctorRepository(dbContext);
        }

        public async Task AddDoctorAsync(DoctorCreateDTO doctor)
        {
            var doctorEntity = new Doctor
            {
                DoctorId = doctor.DoctorId,
                FullName = doctor.FullName,
                Bio = doctor.Bio,
                Department = doctor.Department,
                WorkStart = doctor.WorkStart,
                WorkEnd = doctor.WorkEnd,
                Qualification = doctor.Qualification,
                Email = doctor.Email,
                ContactNum = doctor.ContactNum,
                ExperienceYears = doctor.ExperienceYears,
                Available = false,
                IsApproved = false
            };

            await _doctorRepository.CreateDoctorAsync(doctorEntity);
        }

        public async Task UpdateDoctorAsync(DoctorUpdateDTO doctor)
        {
            var doctorEntity = new Doctor
            {
                DoctorId = doctor.DoctorId,
                FullName = doctor.FullName,
                Bio = doctor.Bio,
                Department = doctor.Department,
                WorkStart = doctor.WorkStart,
                WorkEnd = doctor.WorkEnd,
                Qualification = doctor.Qualification,
                Email = doctor.Email,
                ContactNum = doctor.ContactNum,
                ExperienceYears = doctor.ExperienceYears,
                Available = true, // optionally keep current value by fetching if needed
                IsApproved = doctor.IsApproved
            };

            await _doctorRepository.UpdateDoctorAsync(doctorEntity);
        }

        public async Task<DoctorReadDTO?> GetDoctorByIdAsync(string doctorId)
        {
            var doctor = await _doctorRepository.GetDoctorByIdAsync(doctorId);
            if (doctor == null) return null;

            return new DoctorReadDTO
            {
                DoctorId = doctor.DoctorId,
                FullName = doctor.FullName,
                Bio = doctor.Bio,
                Department = doctor.Department,
                WorkStart = doctor.WorkStart,
                WorkEnd = doctor.WorkEnd,
                Qualification = doctor.Qualification,
                Email = doctor.Email,
                ContactNum = doctor.ContactNum,
                ExperienceYears = doctor.ExperienceYears,
                IsApproved = doctor.IsApproved
            };
        }

        public async Task<IEnumerable<DoctorReadDTO>> GetAllDoctorsAsync()
        {
            var doctors = await _doctorRepository.GetAllDoctorsAsync();

            return doctors.Select(doctor => new DoctorReadDTO
            {
                DoctorId = doctor.DoctorId,
                FullName = doctor.FullName,
                Bio = doctor.Bio,
                Department = doctor.Department,
                WorkStart = doctor.WorkStart,
                WorkEnd = doctor.WorkEnd,
                Qualification = doctor.Qualification,
                Email = doctor.Email,
                ContactNum = doctor.ContactNum,
                ExperienceYears = doctor.ExperienceYears,
                IsApproved = doctor.IsApproved
            });
        }

        public async Task DeleteDoctorAsync(string doctorId)
        {
            await _doctorRepository.DeleteDoctorAsync(doctorId);
        }
    }
}

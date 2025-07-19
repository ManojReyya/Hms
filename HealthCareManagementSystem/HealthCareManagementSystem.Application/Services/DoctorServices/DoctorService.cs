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

        public async Task<DoctorReadDTO> AddDoctorAsync(DoctorCreateDTO doctor)
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

            var createdDoctor = await _doctorRepository.CreateDoctorAsync(doctorEntity);

            return new DoctorReadDTO
            {
                DoctorId = createdDoctor.DoctorId,
                FullName = createdDoctor.FullName,
                Bio = createdDoctor.Bio,
                Department = createdDoctor.Department,
                WorkStart = createdDoctor.WorkStart,
                WorkEnd = createdDoctor.WorkEnd,
                Qualification = createdDoctor.Qualification,
                Email = createdDoctor.Email,
                ContactNum = createdDoctor.ContactNum,
                ExperienceYears = createdDoctor.ExperienceYears,
                IsApproved = createdDoctor.IsApproved
            };
        }


        public async Task<DoctorReadDTO> UpdateDoctorAsync(DoctorUpdateDTO doctor)
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
                Available = true,
                IsApproved = doctor.IsApproved
            };

            var updatedDoctor = await _doctorRepository.UpdateDoctorAsync(doctorEntity);

            return new DoctorReadDTO
            {
                DoctorId = updatedDoctor.DoctorId,
                FullName = updatedDoctor.FullName,
                Bio = updatedDoctor.Bio,
                Department = updatedDoctor.Department,
                WorkStart = updatedDoctor.WorkStart,
                WorkEnd = updatedDoctor.WorkEnd,
                Qualification = updatedDoctor.Qualification,
                Email = updatedDoctor.Email,
                ContactNum = updatedDoctor.ContactNum,
                ExperienceYears = updatedDoctor.ExperienceYears,
                IsApproved = updatedDoctor.IsApproved
            };
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

        public async Task<DoctorReadDTO?> DeleteDoctorAsync(string doctorId)
        {
            var deletedDoctor = await _doctorRepository.DeleteDoctorAsync(doctorId);
            if (deletedDoctor == null) return null;

            return new DoctorReadDTO
            {
                DoctorId = deletedDoctor.DoctorId,
                FullName = deletedDoctor.FullName,
                Bio = deletedDoctor.Bio,
                Department = deletedDoctor.Department,
                WorkStart = deletedDoctor.WorkStart,
                WorkEnd = deletedDoctor.WorkEnd,
                Qualification = deletedDoctor.Qualification,
                Email = deletedDoctor.Email,
                ContactNum = deletedDoctor.ContactNum,
                ExperienceYears = deletedDoctor.ExperienceYears,
                IsApproved = deletedDoctor.IsApproved
            };
        }

        public async Task<DoctorReadDTO> ApproveDoctorAsync(string userId, DoctorUpdateDTO doctorProfile)
        {
            var doctorEntity = new Doctor
            {
                DoctorId = doctorProfile.DoctorId,
                FullName = doctorProfile.FullName,
                Bio = doctorProfile.Bio,
                Department = doctorProfile.Department,
                WorkStart = doctorProfile.WorkStart,
                WorkEnd = doctorProfile.WorkEnd,
                Qualification = doctorProfile.Qualification,
                Email = doctorProfile.Email,
                ContactNum = doctorProfile.ContactNum,
                ExperienceYears = doctorProfile.ExperienceYears,
                Available = true,
                IsApproved = true // because we are approving
            };

            var approvedDoctor = await _doctorRepository.ApproveDoctorAsync(userId, doctorEntity);

            return new DoctorReadDTO
            {
                DoctorId = approvedDoctor.DoctorId,
                FullName = approvedDoctor.FullName,
                Bio = approvedDoctor.Bio,
                Department = approvedDoctor.Department,
                WorkStart = approvedDoctor.WorkStart,
                WorkEnd = approvedDoctor.WorkEnd,
                Qualification = approvedDoctor.Qualification,
                Email = approvedDoctor.Email,
                ContactNum = approvedDoctor.ContactNum,
                ExperienceYears = approvedDoctor.ExperienceYears,
                IsApproved = approvedDoctor.IsApproved
            };
        }

        public async Task<IEnumerable<DoctorReadDTO>> GetPendingDoctorsAsync()
        {
            var pendingDoctors = await _doctorRepository.GetPendingDoctorsAsync();

            return pendingDoctors.Select(doctor => new DoctorReadDTO
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
        public async Task<IEnumerable<DoctorReadDTO>> GetDoctorsByDepartmentAsync(string department)
        {
            var doctors = await _doctorRepository.GetDoctorsByDepartmentAsync(department); 
            return doctors.Select(d => new DoctorReadDTO
            {
                DoctorId = d.DoctorId,
                FullName = d.FullName,
                Bio = d.Bio,
                Department = d.Department,
                WorkStart = d.WorkStart,
                WorkEnd = d.WorkEnd,
                Qualification = d.Qualification,
                Email = d.Email,
                ContactNum = d.ContactNum,
                ExperienceYears = d.ExperienceYears,
                IsApproved = d.IsApproved
            });
        }




    }
}

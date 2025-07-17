using HealthCareManagementSystem.Application.DTOs.PatientDTOs;
using HealthCareManagementSystem.Domain.Entities;
using HealthCareManagementSystem.Infrastructure;
using HealthCareManagementSystem.Infrastructure.Contracts;
using HealthCareManagementSystem.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareManagementSystem.Application.Services.PatientServices
{
    public class PatientService : IPatientService
    {
        private readonly IPatientContract _patientRepository;
        public PatientService(HealthCareManagementSystemDbContext dbContext)
        {
            _patientRepository = new PatientRepository(dbContext);
        }
        public async Task AddPatientAsync(PatientCreateDTO patient)
        {
            var patientEntity = new Patient
            {
                PatientId = patient.PatientId,
                FullName = patient.FullName,
                Gender = patient.Gender,
                Email = patient.Email,
                ContactNum = patient.ContactNum,
                Age = patient.Age
            };

            await _patientRepository.AddPatientAsync(patientEntity);
        }
        public async Task<IEnumerable<PatientReadDTO>> GetAllPatientsAsync()
        {
            var patients=await _patientRepository.GetAllPatientsAsync();
            return patients.Select(p => new PatientReadDTO
            {
                PatientId = p.PatientId,
                FullName = p.FullName,
                Gender = p.Gender,
                Email = p.Email,
                ContactNum = p.ContactNum,
                Age = p.Age
            });
        }
        public async Task<PatientReadDTO?> GetPatientByIdAsync(string patientId)
        {
            var p=await _patientRepository.GetPatientByIdAsync(patientId);
            if (p == null)
                return null;
            return new PatientReadDTO
            {
                PatientId = p.PatientId,
                FullName = p.FullName,
                Gender = p.Gender,
                Email = p.Email,
                ContactNum = p.ContactNum,
                Age = p.Age
            };
        }
        public async Task UpdatePatientAsync(PatientUpdateDTO patient)
        {
            var patientEntity = new Patient
            {
                PatientId = patient.PatientId,
                FullName = patient.FullName,
                Gender = patient.Gender,
                Email = patient.Email,
                ContactNum = patient.ContactNum,
                Age = patient.Age
            };
            await _patientRepository.UpdatePatientAsync(patientEntity);
        }
        public async Task DeletePatientAsync(string patientId)
        {
            await _patientRepository.DeletePatientAsync(patientId);
        }
    }
}

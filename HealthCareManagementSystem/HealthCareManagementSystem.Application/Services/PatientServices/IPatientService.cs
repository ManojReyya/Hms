using HealthCareManagementSystem.Application.DTOs.PatientDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareManagementSystem.Application.Services.PatientServices
{
    public interface IPatientService
    {
        Task<IEnumerable<PatientReadDTO>> GetAllPatientsAsync();
        Task<PatientReadDTO> GetPatientByIdAsync(string patientId);
        Task AddPatientAsync(PatientCreateDTO patient);
        Task UpdatePatientAsync(PatientUpdateDTO patient);
        Task DeletePatientAsync(string patientId);
    }
}

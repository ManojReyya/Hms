using System.Collections.Generic;
using System.Threading.Tasks;
using HealthCareManagementSystem.Application.DTOs;

namespace HealthCareManagementSystem.Application.Services
{
    public interface IDoctorService
    {
        Task<IEnumerable<DoctorDTO>> GetAllDoctorsAsync();
        Task<DoctorDTO> GetDoctorByIdAsync(string doctorId);
        Task AddDoctorAsync(DoctorDTO doctor);
        Task UpdateDoctorAsync(DoctorDTO doctor);
        Task DeleteDoctorAsync(string doctorId);
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using HealthCareManagementSystem.Application.DTOs.DoctorDTOs;

namespace HealthCareManagementSystem.Application.Services.DoctorServices
{
    public interface IDoctorService
    {
        Task<IEnumerable<DoctorReadDTO>> GetAllDoctorsAsync();
        Task<DoctorReadDTO?> GetDoctorByIdAsync(string doctorId);
        Task AddDoctorAsync(DoctorCreateDTO doctor);
        Task UpdateDoctorAsync(DoctorUpdateDTO doctor);
        Task DeleteDoctorAsync(string doctorId);
    }
}

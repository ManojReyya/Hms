using System.Collections.Generic;
using System.Threading.Tasks;
using HealthCareManagementSystem.Application.DTOs.DoctorDTOs;

namespace HealthCareManagementSystem.Application.Services.DoctorServices
{
    public interface IDoctorService
    {
        Task<IEnumerable<DoctorReadDTO>> GetAllDoctorsAsync();
        Task<DoctorReadDTO?> GetDoctorByIdAsync(string doctorId);
        Task<DoctorReadDTO> AddDoctorAsync(DoctorCreateDTO doctor);
        Task<DoctorReadDTO> UpdateDoctorAsync(DoctorUpdateDTO doctor);

        Task<DoctorReadDTO?> DeleteDoctorAsync(string doctorId);

        Task<DoctorReadDTO> ApproveDoctorAsync(string userId, DoctorUpdateDTO doctorProfile);
        Task<IEnumerable<DoctorReadDTO>> GetPendingDoctorsAsync();
        Task<IEnumerable<DoctorReadDTO>> GetDoctorsByDepartmentAsync(string department);

    }
}

using HealthCareManagementSystem.Domain.Entities;

namespace HealthCareManagementSystem.Infrastructure.Contracts
{
    public interface IDoctorContract
    {
        Task<IEnumerable<Doctor>> GetAllDoctorsAsync();
        Task<Doctor?> GetDoctorByIdAsync(string doctorId);
        Task<Doctor> CreateDoctorAsync(Doctor doctor);
        Task<Doctor> UpdateDoctorAsync(Doctor doctor);
        Task<Doctor> DeleteDoctorAsync(string doctorId);
        Task<Doctor> ApproveDoctorAsync(string userId, Doctor doctorProfile);
        Task<IEnumerable<Doctor>> GetPendingDoctorsAsync();


    }
}

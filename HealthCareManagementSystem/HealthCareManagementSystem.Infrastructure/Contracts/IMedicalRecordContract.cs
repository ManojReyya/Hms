using HealthCareManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareManagementSystem.Infrastructure.Contracts
{
    public interface IMedicalRecordContract
    {
        Task<MedicalRecord?> GetByMedicalRecordIdAsync(int medicalRecordId);
        Task<IEnumerable<MedicalRecord>> GetAllMedicalRecordAsync();
        Task AddMedicalRecordAsync(MedicalRecord record);
        Task UpdateMedicalRecordAsync(MedicalRecord record);
        Task DeleteMedicalRecordAsync(int medicalRecordId);
    }
}

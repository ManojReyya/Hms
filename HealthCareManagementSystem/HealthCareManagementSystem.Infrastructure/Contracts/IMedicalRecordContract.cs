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
        Task<IEnumerable<MedicalRecord>> GetAllMedicalRecordsAsync();
        Task<MedicalRecord?> GetMedicalRecordByIdAsync(int medicalRecordId);
        Task<IEnumerable<MedicalRecord>> GetMedicalRecordsByPatientIdAsync(string patientId);
        Task<IEnumerable<MedicalRecord>> GetMedicalRecordsByDoctorIdAsync(string doctorId);
        Task<IEnumerable<MedicalRecord>> GetMedicalRecordsByAppointmentIdAsync(int appointmentId);
        Task<IEnumerable<MedicalRecord>> GetMedicalRecordsByDateAsync(DateTime date);
        Task<MedicalRecord> CreateMedicalRecordAsync(MedicalRecord record);
        Task<MedicalRecord> UpdateMedicalRecordAsync(MedicalRecord record);
        Task<MedicalRecord> DeleteMedicalRecordAsync(int medicalRecordId);
    }
}

using HealthCareManagementSystem.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareManagementSystem.Application.Services
{
    public interface IMedicalRecordService
    {
        Task<IEnumerable<MedicalRecordReadDTO>> GetAllMedicalRecordsAsync();
        Task<MedicalRecordReadDTO?> GetMedicalRecordByIdAsync(int medicalRecordId);
        Task<IEnumerable<MedicalRecordReadDTO>> GetMedicalRecordsByPatientIdAsync(string patientId);
        Task<IEnumerable<MedicalRecordReadDTO>> GetMedicalRecordsByDoctorIdAsync(string doctorId);
        Task<IEnumerable<MedicalRecordReadDTO>> GetMedicalRecordsByAppointmentIdAsync(int appointmentId);
        Task<IEnumerable<MedicalRecordReadDTO>> GetMedicalRecordsByDateAsync(DateTime date);
        Task<MedicalRecordReadDTO> CreateMedicalRecordAsync(MedicalRecordCreateDTO medicalRecord);
        Task<MedicalRecordReadDTO?> UpdateMedicalRecordAsync(MedicalRecordUpdateDTO medicalRecord);
        Task<bool> DeleteMedicalRecordAsync(int medicalRecordId);
    }
}
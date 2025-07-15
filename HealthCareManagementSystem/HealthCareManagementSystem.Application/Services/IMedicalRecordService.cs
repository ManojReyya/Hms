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
        Task<IEnumerable<MedicalRecordReadDTO>> GetAllAsync();
        Task<MedicalRecordReadDTO?> GetByIdAsync(int id);
        Task<IEnumerable<MedicalRecordReadDTO>> GetMedicalRecordsByPatientIdAsync(string patientId);
        Task<IEnumerable<MedicalRecordReadDTO>> GetMedicalRecordsByDoctorIdAsync(string doctorId);
        Task<IEnumerable<MedicalRecordReadDTO>> GetMedicalRecordsByAppointmentIdAsync(int appointmentId);
        Task<IEnumerable<MedicalRecordReadDTO>> GetMedicalRecordsByDateAsync(DateTime date);
        Task<MedicalRecordReadDTO> CreateAsync(MedicalRecordCreateDTO medicalRecord);
        Task<MedicalRecordReadDTO?> UpdateAsync(MedicalRecordUpdateDTO medicalRecord);
        Task<MedicalRecordReadDTO?> DeleteAsync(int medicalRecordId);
    }

}
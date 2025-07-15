using HealthCareManagementSystem.Application.DTOs;
using HealthCareManagementSystem.Domain.Entities;
using HealthCareManagementSystem.Infrastructure.Contracts;

namespace HealthCareManagementSystem.Application.Services;

public class MedicalRecordService : IMedicalRecordService
{
    private readonly IMedicalRecordContract _medicalRecordRepo;

    public MedicalRecordService(IMedicalRecordContract medicalRecordContract)
    {
        _medicalRecordRepo = medicalRecordContract;
    }

    public async Task<IEnumerable<MedicalRecordReadDTO>> GetAllAsync()
    {
        var medicalRecords = await _medicalRecordRepo.GetAllMedicalRecordsAsync();
        return medicalRecords.Select(m => new MedicalRecordReadDTO()
        {
            MedicalRecordId = m.MedicalRecordId,
            PatientId = m.PatientId,
            PatientName = m.Patient?.FullName ?? string.Empty,
            DoctorId = m.DoctorId,
            DoctorName = m.Doctor?.FullName ?? string.Empty,
            AppointmentId = m.AppointmentId,
            Diagnosis = m.Diagnosis,
            Prescription = m.Prescription,
            Notes = m.Notes,
            RecordDate = m.RecordDate
        });
    }

    public async Task<MedicalRecordReadDTO?> GetByIdAsync(int id)
    {
        var medicalRecord = await _medicalRecordRepo.GetMedicalRecordByIdAsync(id);
        return medicalRecord == null ? null : new MedicalRecordReadDTO()
        {
            MedicalRecordId = medicalRecord.MedicalRecordId,
            PatientId = medicalRecord.PatientId,
            PatientName = medicalRecord.Patient?.FullName ?? string.Empty,
            DoctorId = medicalRecord.DoctorId,
            DoctorName = medicalRecord.Doctor?.FullName ?? string.Empty,
            AppointmentId = medicalRecord.AppointmentId,
            Diagnosis = medicalRecord.Diagnosis,
            Prescription = medicalRecord.Prescription,
            Notes = medicalRecord.Notes,
            RecordDate = medicalRecord.RecordDate
        };
    }

    public async Task<IEnumerable<MedicalRecordReadDTO>> GetMedicalRecordsByPatientIdAsync(string patientId)
    {
        var medicalRecords = await _medicalRecordRepo.GetMedicalRecordsByPatientIdAsync(patientId);
        return medicalRecords.Select(m => new MedicalRecordReadDTO
        {
            MedicalRecordId = m.MedicalRecordId,
            PatientId = m.PatientId,
            PatientName = m.Patient?.FullName ?? string.Empty,
            DoctorId = m.DoctorId,
            DoctorName = m.Doctor?.FullName ?? string.Empty,
            AppointmentId = m.AppointmentId,
            Diagnosis = m.Diagnosis,
            Prescription = m.Prescription,
            Notes = m.Notes,
            RecordDate = m.RecordDate
        });
    }

    public async Task<IEnumerable<MedicalRecordReadDTO>> GetMedicalRecordsByDoctorIdAsync(string doctorId)
    {
        var medicalRecords = await _medicalRecordRepo.GetMedicalRecordsByDoctorIdAsync(doctorId);
        return medicalRecords.Select(m => new MedicalRecordReadDTO
        {
            MedicalRecordId = m.MedicalRecordId,
            PatientId = m.PatientId,
            PatientName = m.Patient?.FullName ?? string.Empty,
            DoctorId = m.DoctorId,
            DoctorName = m.Doctor?.FullName ?? string.Empty,
            AppointmentId = m.AppointmentId,
            Diagnosis = m.Diagnosis,
            Prescription = m.Prescription,
            Notes = m.Notes,
            RecordDate = m.RecordDate
        });
    }

    public async Task<IEnumerable<MedicalRecordReadDTO>> GetMedicalRecordsByAppointmentIdAsync(int appointmentId)
    {
        var medicalRecords = await _medicalRecordRepo.GetMedicalRecordsByAppointmentIdAsync(appointmentId);
        return medicalRecords.Select(m => new MedicalRecordReadDTO
        {
            MedicalRecordId = m.MedicalRecordId,
            PatientId = m.PatientId,
            PatientName = m.Patient?.FullName ?? string.Empty,
            DoctorId = m.DoctorId,
            DoctorName = m.Doctor?.FullName ?? string.Empty,
            AppointmentId = m.AppointmentId,
            Diagnosis = m.Diagnosis,
            Prescription = m.Prescription,
            Notes = m.Notes,
            RecordDate = m.RecordDate
        });
    }

    public async Task<IEnumerable<MedicalRecordReadDTO>> GetMedicalRecordsByDateAsync(DateTime date)
    {
        var medicalRecords = await _medicalRecordRepo.GetMedicalRecordsByDateAsync(date);
        return medicalRecords.Select(m => new MedicalRecordReadDTO
        {
            MedicalRecordId = m.MedicalRecordId,
            PatientId = m.PatientId,
            PatientName = m.Patient?.FullName ?? string.Empty,
            DoctorId = m.DoctorId,
            DoctorName = m.Doctor?.FullName ?? string.Empty,
            AppointmentId = m.AppointmentId,
            Diagnosis = m.Diagnosis,
            Prescription = m.Prescription,
            Notes = m.Notes,
            RecordDate = m.RecordDate
        });
    }

    public async Task<MedicalRecordReadDTO> CreateAsync(MedicalRecordCreateDTO dto)
    {
        var medicalRecord = new MedicalRecord()
        {
            PatientId = dto.PatientId,
            DoctorId = dto.DoctorId,
            AppointmentId = dto.AppointmentId,
            Diagnosis = dto.Diagnosis,
            Prescription = dto.Prescription,
            Notes = dto.Notes,
            RecordDate = dto.RecordDate
        };

        var createdMedicalRecord = await _medicalRecordRepo.CreateMedicalRecordAsync(medicalRecord);

        // Fetch the created record with related entities to get patient and doctor names
        var recordWithRelatedData = await _medicalRecordRepo.GetMedicalRecordByIdAsync(createdMedicalRecord.MedicalRecordId);

        return new MedicalRecordReadDTO()
        {
            MedicalRecordId = recordWithRelatedData!.MedicalRecordId,
            PatientId = recordWithRelatedData.PatientId,
            PatientName = recordWithRelatedData.Patient?.FullName ?? string.Empty,
            DoctorId = recordWithRelatedData.DoctorId,
            DoctorName = recordWithRelatedData.Doctor?.FullName ?? string.Empty,
            AppointmentId = recordWithRelatedData.AppointmentId,
            Diagnosis = recordWithRelatedData.Diagnosis,
            Prescription = recordWithRelatedData.Prescription,
            Notes = recordWithRelatedData.Notes,
            RecordDate = recordWithRelatedData.RecordDate
        };
    }

    public async Task<MedicalRecordReadDTO?> UpdateAsync(MedicalRecordUpdateDTO dto)
    {
        var existing = await _medicalRecordRepo.GetMedicalRecordByIdAsync(dto.MedicalRecordId);
        if (existing == null) return null;

        existing.PatientId = dto.PatientId;
        existing.DoctorId = dto.DoctorId;
        existing.AppointmentId = dto.AppointmentId;
        existing.Diagnosis = dto.Diagnosis;
        existing.Prescription = dto.Prescription;
        existing.Notes = dto.Notes;
        existing.RecordDate = dto.RecordDate;

        var updated = await _medicalRecordRepo.UpdateMedicalRecordAsync(existing);

        // Fetch the updated record with related entities to get patient and doctor names
        var recordWithRelatedData = await _medicalRecordRepo.GetMedicalRecordByIdAsync(updated.MedicalRecordId);

        return new MedicalRecordReadDTO()
        {
            MedicalRecordId = recordWithRelatedData!.MedicalRecordId,
            PatientId = recordWithRelatedData.PatientId,
            PatientName = recordWithRelatedData.Patient?.FullName ?? string.Empty,
            DoctorId = recordWithRelatedData.DoctorId,
            DoctorName = recordWithRelatedData.Doctor?.FullName ?? string.Empty,
            AppointmentId = recordWithRelatedData.AppointmentId,
            Diagnosis = recordWithRelatedData.Diagnosis,
            Prescription = recordWithRelatedData.Prescription,
            Notes = recordWithRelatedData.Notes,
            RecordDate = recordWithRelatedData.RecordDate
        };
    }

    public async Task<MedicalRecordReadDTO?> DeleteAsync(int id)
    {
        var deleted = await _medicalRecordRepo.DeleteMedicalRecordAsync(id);
        if (deleted == null) return null;

        return new MedicalRecordReadDTO
        {
            MedicalRecordId = deleted.MedicalRecordId,
            PatientId = deleted.PatientId,
            PatientName = deleted.Patient?.FullName ?? string.Empty,
            DoctorId = deleted.DoctorId,
            DoctorName = deleted.Doctor?.FullName ?? string.Empty,
            AppointmentId = deleted.AppointmentId,
            Diagnosis = deleted.Diagnosis,
            Prescription = deleted.Prescription,
            Notes = deleted.Notes,
            RecordDate = deleted.RecordDate
        };
    }
}
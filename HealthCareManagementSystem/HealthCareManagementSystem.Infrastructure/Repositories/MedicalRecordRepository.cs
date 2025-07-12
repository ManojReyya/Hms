using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCareManagementSystem.Domain.Entities;
using HealthCareManagementSystem.Infrastructure.Contracts;
using Microsoft.EntityFrameworkCore;

namespace HealthCareManagementSystem.Infrastructure.Repositories
{
    public class MedicalRecordRepository : IMedicalRecordContract
    {
        private readonly HealthCareManagementSystemDbContext _dbContext;

        public MedicalRecordRepository(HealthCareManagementSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<MedicalRecord>> GetAllMedicalRecordsAsync()
        {
            return await _dbContext.MedicalRecords
                .Include(m => m.Patient)
                .Include(m => m.Doctor)
                .Include(m => m.Appointment)
                .ToListAsync();
        }

        public async Task<MedicalRecord?> GetMedicalRecordByIdAsync(int medicalRecordId)
        {
            return await _dbContext.MedicalRecords
                .Include(m => m.Patient)
                .Include(m => m.Doctor)
                .Include(m => m.Appointment)
                .FirstOrDefaultAsync(m => m.MedicalRecordId == medicalRecordId);
        }

        public async Task<IEnumerable<MedicalRecord>> GetMedicalRecordsByPatientIdAsync(string patientId)
        {
            return await _dbContext.MedicalRecords
                .Where(m => m.PatientId == patientId)
                .Include(m => m.Patient)
                .Include(m => m.Doctor)
                .Include(m => m.Appointment)
                .ToListAsync();
        }

        public async Task<IEnumerable<MedicalRecord>> GetMedicalRecordsByDoctorIdAsync(string doctorId)
        {
            return await _dbContext.MedicalRecords
                .Where(m => m.DoctorId == doctorId)
                .Include(m => m.Patient)
                .Include(m => m.Doctor)
                .Include(m => m.Appointment)
                .ToListAsync();
        }

        public async Task<IEnumerable<MedicalRecord>> GetMedicalRecordsByAppointmentIdAsync(int appointmentId)
        {
            return await _dbContext.MedicalRecords
                .Where(m => m.AppointmentId == appointmentId)
                .Include(m => m.Patient)
                .Include(m => m.Doctor)
                .Include(m => m.Appointment)
                .ToListAsync();
        }

        public async Task<IEnumerable<MedicalRecord>> GetMedicalRecordsByDateAsync(DateTime date)
        {
            return await _dbContext.MedicalRecords
                .Where(m => m.RecordDate.Date == date.Date)
                .Include(m => m.Patient)
                .Include(m => m.Doctor)
                .Include(m => m.Appointment)
                .ToListAsync();
        }

        public async Task<MedicalRecord> CreateMedicalRecordAsync(MedicalRecord record)
        {
            await _dbContext.MedicalRecords.AddAsync(record);
            await _dbContext.SaveChangesAsync();
            return record;
        }

        public async Task<MedicalRecord> UpdateMedicalRecordAsync(MedicalRecord record)
        {
            var existing = await _dbContext.MedicalRecords.FindAsync(record.MedicalRecordId);
            if (existing == null) return null!;

            _dbContext.Entry(existing).CurrentValues.SetValues(record);
            await _dbContext.SaveChangesAsync();
            return existing;
        }

        public async Task<MedicalRecord> DeleteMedicalRecordAsync(int medicalRecordId)
        {
            var record = await _dbContext.MedicalRecords.FindAsync(medicalRecordId);
            if (record == null) return null!;

            _dbContext.MedicalRecords.Remove(record);
            await _dbContext.SaveChangesAsync();
            return record;
        }
    }
}

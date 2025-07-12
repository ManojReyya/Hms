using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCareManagementSystem.Domain.Entities;
using HealthCareManagementSystem.Infrastructure.Contracts;
using Microsoft.EntityFrameworkCore;

namespace HealthCareManagementSystem.Infrastructure.Repositories
{
    internal class MedicalRecordRepository : IMedicalRecordContract
    {
        private readonly HealthCareManagementSystemDbContext _context;

        public MedicalRecordRepository(HealthCareManagementSystemDbContext context)
        {
            _context = context;
        }

        public async Task<MedicalRecord?> GetByMedicalRecordIdAsync(int medicalRecordId)
        {
            return await _context.MedicalRecords
                .Include(m => m.Patient)
                .Include(m => m.Doctor)
                .Include(m => m.Appointment)
                .FirstOrDefaultAsync(m => m.MedicalRecordId == medicalRecordId);
        }

        public async Task<IEnumerable<MedicalRecord>> GetAllMedicalRecordAsync()
        {
            return await _context.MedicalRecords
                .Include(m => m.Patient)
                .Include(m => m.Doctor)
                .Include(m => m.Appointment)
                .ToListAsync();
        }

        public async Task AddMedicalRecordAsync(MedicalRecord record)
        {
            await _context.MedicalRecords.AddAsync(record);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMedicalRecordAsync(MedicalRecord record)
        {
            _context.MedicalRecords.Update(record);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMedicalRecordAsync(int medicalRecordId)
        {
            var record = await _context.MedicalRecords.FindAsync(medicalRecordId);
            if (record != null)
            {
                _context.MedicalRecords.Remove(record);
                await _context.SaveChangesAsync();
            }
        }
    }
}



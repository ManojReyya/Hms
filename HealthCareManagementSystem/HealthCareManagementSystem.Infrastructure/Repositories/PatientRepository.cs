using HealthCareManagementSystem.Domain.Entities;
using HealthCareManagementSystem.Infrastructure.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareManagementSystem.Infrastructure.Repositories
{
    public class PatientRepository : IPatientContract
    {
        private readonly HealthCareManagementSystemDbContext _context;
        public PatientRepository(HealthCareManagementSystemDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Patient>> GetAllPatientsAsync()
        {
            return await _context.Patients
                .Include(p => p.User)
                .Include(p => p.Appointments)
                .Include(p => p.MedicalRecords)
                .ToListAsync();
        }
        public async Task<Patient?> GetPatientByIdAsync(string patientId)
        {
            return await _context.Patients
                .Include(p => p.User)
                .Include(p => p.Appointments)
                .Include(p => p.MedicalRecords)
                .FirstOrDefaultAsync(p => p.PatientId == patientId);
        }
        public async Task<Patient> AddPatientAsync(Patient patient)
        {
            await _context.Patients.AddAsync(patient);
            await _context.SaveChangesAsync();
            return patient;
        }
        public async Task<Patient> UpdatePatientAsync(Patient patient)
        {
            var existing=await _context.Patients.FindAsync(patient.PatientId);
            if (existing == null)
                return null!;

            _context.Entry(existing).CurrentValues.SetValues(patient);
            await _context.SaveChangesAsync();
            return existing;
        }
        public async Task<Patient> DeletePatientAsync(string patientId)
        {
            var patient = await _context.Patients.FindAsync(patientId);
            if (patient == null)
                return null!;

                _context.Patients.Remove(patient);
                await _context.SaveChangesAsync();
                return patient;
        }

    }
}

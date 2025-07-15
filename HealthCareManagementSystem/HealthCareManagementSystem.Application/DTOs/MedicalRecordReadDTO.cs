using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareManagementSystem.Application.DTOs
{
    public class MedicalRecordReadDTO
    {
        public int MedicalRecordId { get; set; }
        public string PatientId { get; set; } = null!;
        public string PatientName { get; set; } = null!;
        public string DoctorId { get; set; } = null!;
        public string DoctorName { get; set; } = null!;
        public int AppointmentId { get; set; }
        public string Diagnosis { get; set; } = null!;
        public string? Prescription { get; set; }
        public string? Notes { get; set; }
        public DateTime RecordDate { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareManagementSystem.Application.DTOs.MedicalRecordDTOs
{
    public class MedicalRecordCreateDTO
    {
        public string PatientId { get; set; } = null!;
        public string DoctorId { get; set; } = null!;
        public int AppointmentId { get; set; }
        public string Diagnosis { get; set; } = null!;
        public string? Prescription { get; set; }
        public string? Notes { get; set; }
        public DateTime RecordDate { get; set; }
    }
}
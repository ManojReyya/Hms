using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareManagementSystem.Application.DTOs.PatientDTOs
{
    public class PatientCreateDTO
    {
        public string PatientId { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string? Gender { get; set; }
        public string Email { get; set; } = null!;
        public string ContactNum { get; set; } = null!;
        public int Age { get; set; }
    }
}

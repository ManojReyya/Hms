using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareManagementSystem.Application.DTOs
{
    public class PatientUpdateDTO
    {
        public string PatientId { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string? Gender { get; set; }
        public string ContactNum { get; set; } = null!;
        public int Age { get; set; }

    }
}

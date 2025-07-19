using Microsoft.AspNetCore.Mvc;
using HealthCareManagementSystem.Application.DTOs.DoctorDTOs;
using HealthCareManagementSystem.Application.Services.DoctorServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthCareManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        // GET: api/Doctor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoctorReadDTO>>> GetAllDoctors()
        {
            var doctors = await _doctorService.GetAllDoctorsAsync();
            return Ok(doctors);
        }

        // GET: api/Doctor/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorReadDTO>> GetDoctorById(string id)
        {
            var doctor = await _doctorService.GetDoctorByIdAsync(id);
            if (doctor == null)
                return NotFound();
            return Ok(doctor);
        }

        // POST: api/Doctor
        [HttpPost]
        public async Task<ActionResult> CreateDoctor([FromBody] DoctorCreateDTO newDoctor)
        {
            var createdDoctor = await _doctorService.AddDoctorAsync(newDoctor);
            return Ok(new { message = "Doctor created successfully.", data = createdDoctor });
        }


        // PUT: api/Doctor
        [HttpPut]
        public async Task<ActionResult> UpdateDoctor([FromBody] DoctorUpdateDTO updatedDoctor)
        {
            var doctor = await _doctorService.UpdateDoctorAsync(updatedDoctor);
            return Ok(new { message = "Doctor updated successfully.", data = doctor });
        }


        // DELETE: api/Doctor/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDoctor(string id)
        {
            var deletedDoctor = await _doctorService.DeleteDoctorAsync(id);
            if (deletedDoctor == null)
                return NotFound(new { message = "Doctor not found." });

            return Ok(new
            {
                message = "Doctor deleted successfully.",
                data = deletedDoctor
            });
        }
        [HttpGet("department/{department}")]
        public async Task<ActionResult<IEnumerable<DoctorReadDTO>>> GetDoctorsByDepartment(string department)
        {
            var doctors = await _doctorService.GetDoctorsByDepartmentAsync(department);
            return Ok(doctors);
        }



    }
}

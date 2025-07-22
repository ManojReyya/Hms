using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using HealthCareManagementSystem.Application.DTOs.DoctorDTOs;
using HealthCareManagementSystem.Application.Services.DoctorServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims;

namespace HealthCareManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Require authentication for all doctor endpoints
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        // GET: api/Doctor
        [HttpGet]
        [Authorize(Roles = "Admin,Patient")] // Only admins and patients can view list of doctors
        public async Task<ActionResult<IEnumerable<DoctorReadDTO>>> GetAllDoctors()
        {
            var doctors = await _doctorService.GetAllDoctorsAsync();
            return Ok(doctors);
        }

        // GET: api/Doctor/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Doctor")] // Only admins and doctors can view doctor profiles
        public async Task<ActionResult<DoctorReadDTO>> GetDoctorById(string id)
        {
            // If user is a doctor, they can only view their own profile
            if (User.IsInRole("Doctor"))
            {
                var currentUserId = User.FindFirst("sub")?.Value ?? User.FindFirst("id")?.Value;
                if (currentUserId != id)
                {
                    return Forbid("Doctors can only view their own profile.");
                }
            }

            var doctor = await _doctorService.GetDoctorByIdAsync(id);
            if (doctor == null)
                return NotFound();
            return Ok(doctor);
        }

        // POST: api/Doctor
        [HttpPost]
        [Authorize(Roles = "Admin")] // Only admins can create doctors
        public async Task<ActionResult> CreateDoctor([FromBody] DoctorCreateDTO newDoctor)
        {
            var createdDoctor = await _doctorService.AddDoctorAsync(newDoctor);
            return Ok(new { message = "Doctor created successfully.", data = createdDoctor });
        }


        // PUT: api/Doctor
        [HttpPut]
        [Authorize(Roles = "Admin,Doctor")] // Admins and doctors can update doctor profiles
        public async Task<ActionResult> UpdateDoctor([FromBody] DoctorUpdateDTO updatedDoctor)
        {
            // If user is a doctor, they can only update their own profile
            if (User.IsInRole("Doctor"))
            {
                var currentUserId = User.FindFirst("sub")?.Value ?? User.FindFirst("id")?.Value;
                if (currentUserId != updatedDoctor.DoctorId)
                {
                    return Forbid("Doctors can only update their own profile.");
                }
            }

            var doctor = await _doctorService.UpdateDoctorAsync(updatedDoctor);
            return Ok(new { message = "Doctor updated successfully.", data = doctor });
        }


        // DELETE: api/Doctor/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] // Only admins can delete doctors
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
        [Authorize(Roles = "Admin,Patient")] // Only admins and patients can view list of doctors by department
        public async Task<ActionResult<IEnumerable<DoctorReadDTO>>> GetDoctorsByDepartment(string department)
        {
            var doctors = await _doctorService.GetDoctorsByDepartmentAsync(department);
            return Ok(doctors);
        }
    }
}

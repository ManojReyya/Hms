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
            await _doctorService.AddDoctorAsync(newDoctor);
            return Ok(new { message = "Doctor created successfully." });
        }

        // PUT: api/Doctor
        [HttpPut]
        public async Task<ActionResult> UpdateDoctor([FromBody] DoctorUpdateDTO updatedDoctor)
        {
            await _doctorService.UpdateDoctorAsync(updatedDoctor);
            return Ok(new { message = "Doctor updated successfully." });
        }

        // DELETE: api/Doctor/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDoctor(string id)
        {
            await _doctorService.DeleteDoctorAsync(id);
            return Ok(new { message = "Doctor deleted successfully." });
        }
    }
}

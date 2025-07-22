using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using HealthCareManagementSystem.Application.Services.PatientServices;
using HealthCareManagementSystem.Application.DTOs.PatientDTOs;

namespace HealthCareManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Require authentication for all patient endpoints
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly ILogger<PatientController> _logger;
        public PatientController(IPatientService patientService, ILogger<PatientController> logger)
        {
            _patientService = patientService;
            _logger = logger;
        }

        // GET: api/Patient
        [HttpGet]
        [Authorize(Roles = "Admin,Doctor")] // Only admins and doctors can view all patients
        public async Task<ActionResult<IEnumerable<PatientReadDTO>>> GetAllPatients()
        {
            try
            {
                var patients = await _patientService.GetAllPatientsAsync();
                return Ok(patients);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting patients");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/Patient/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PatientReadDTO>> GetPatientById(string id)
        {
            try
            {
                // Check authorization: Admin and Doctor can view any patient, Patient can only view their own data
                var currentUserId = User.Identity?.Name;
                var userRole = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
                
                if (userRole == "Patient" && currentUserId != id)
                {
                    return Forbid("Patients can only view their own information");
                }

                var patient = await _patientService.GetPatientByIdAsync(id);
                if (patient == null)
                    return NotFound();

                return Ok(patient);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting patient with id {id}");
                return StatusCode(500, "Internal server error");
            }
        }

        // POST: api/Patient
        [HttpPost]
        [Authorize(Roles = "Admin")] // Only admins can create patients
        public async Task<ActionResult<PatientReadDTO>> CreatePatient([FromBody] PatientCreateDTO patient)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _patientService.AddPatientAsync(patient);
                return CreatedAtAction(nameof(GetPatientById), new { id = patient.PatientId }, patient);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error creating patient");
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT: api/Patient
        [HttpPut("{id}")]
        public async Task<ActionResult<PatientReadDTO>> UpdatePatient(string id, [FromBody] PatientUpdateDTO patient)
        {
            if (id != patient.PatientId)
                return BadRequest("ID mismatch");

            try
            {
                // Check authorization: Admin and Doctor can update any patient, Patient can only update their own data
                var currentUserId = User.Identity?.Name;
                var userRole = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
                
                if (userRole == "Patient" && currentUserId != id)
                {
                    return Forbid("Patients can only update their own information");
                }

                await _patientService.UpdatePatientAsync(patient);
                return Ok($"Patient with ID {id} updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating patient with id {id}");
                return StatusCode(500, "Internal server error");
            }
        }

        // DELETE: api/Patient/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] // Only admins can delete patients
        public async Task<ActionResult<PatientReadDTO>> DeletePatient(string id)
        {
            try
            {
                await _patientService.DeletePatientAsync(id);
                return Ok($"Patient with ID {id} deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting patient with id {id}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}

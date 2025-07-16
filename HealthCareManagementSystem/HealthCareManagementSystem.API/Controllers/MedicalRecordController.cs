using Microsoft.AspNetCore.Mvc;
using HealthCareManagementSystem.Application.Services.MedicalRecordServices;
using HealthCareManagementSystem.Application.DTOs.MedicalRecordDTOs;

namespace HealthCareManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicalRecordController : ControllerBase
    {
        private readonly IMedicalRecordService _medicalRecordService;
        private readonly ILogger<MedicalRecordController> _logger;

        public MedicalRecordController(IMedicalRecordService medicalRecordService, ILogger<MedicalRecordController> logger)
        {
            _medicalRecordService = medicalRecordService;
            _logger = logger;
        }

        /// <summary>
        /// Get all medical records
        /// </summary>
        /// <returns>List of all medical records</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicalRecordReadDTO>>> GetAllMedicalRecords()
        {
            try
            {
                var medicalRecords = await _medicalRecordService.GetAllAsync();
                return Ok(medicalRecords);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all medical records");
                return StatusCode(500, "Internal server error occurred while fetching medical records");
            }
        }

        /// <summary>
        /// Get medical record by ID
        /// </summary>
        /// <param name="id">Medical record ID</param>
        /// <returns>Medical record details</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<MedicalRecordReadDTO>> GetMedicalRecordById(int id)
        {
            try
            {
                var medicalRecord = await _medicalRecordService.GetByIdAsync(id);
                if (medicalRecord == null)
                {
                    return NotFound($"Medical record with ID {id} not found");
                }
                return Ok(medicalRecord);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching medical record with ID {Id}", id);
                return StatusCode(500, "Internal server error occurred while fetching medical record");
            }
        }

        /// <summary>
        /// Get medical records by patient ID
        /// </summary>
        /// <param name="patientId">Patient ID</param>
        /// <returns>List of medical records for the patient</returns>
        [HttpGet("patient/{patientId}")]
        public async Task<ActionResult<IEnumerable<MedicalRecordReadDTO>>> GetMedicalRecordsByPatientId(string patientId)
        {
            try
            {
                var medicalRecords = await _medicalRecordService.GetMedicalRecordsByPatientIdAsync(patientId);
                return Ok(medicalRecords);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching medical records for patient {PatientId}", patientId);
                return StatusCode(500, "Internal server error occurred while fetching medical records");
            }
        }

        /// <summary>
        /// Get medical records by doctor ID
        /// </summary>
        /// <param name="doctorId">Doctor ID</param>
        /// <returns>List of medical records created by the doctor</returns>
        [HttpGet("doctor/{doctorId}")]
        public async Task<ActionResult<IEnumerable<MedicalRecordReadDTO>>> GetMedicalRecordsByDoctorId(string doctorId)
        {
            try
            {
                var medicalRecords = await _medicalRecordService.GetMedicalRecordsByDoctorIdAsync(doctorId);
                return Ok(medicalRecords);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching medical records for doctor {DoctorId}", doctorId);
                return StatusCode(500, "Internal server error occurred while fetching medical records");
            }
        }

        /// <summary>
        /// Get medical records by appointment ID
        /// </summary>
        /// <param name="appointmentId">Appointment ID</param>
        /// <returns>List of medical records for the appointment</returns>
        [HttpGet("appointment/{appointmentId}")]
        public async Task<ActionResult<IEnumerable<MedicalRecordReadDTO>>> GetMedicalRecordsByAppointmentId(int appointmentId)
        {
            try
            {
                var medicalRecords = await _medicalRecordService.GetMedicalRecordsByAppointmentIdAsync(appointmentId);
                return Ok(medicalRecords);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching medical records for appointment {AppointmentId}", appointmentId);
                return StatusCode(500, "Internal server error occurred while fetching medical records");
            }
        }

        /// <summary>
        /// Get medical records by date
        /// </summary>
        /// <param name="date">Date in YYYY-MM-DD format</param>
        /// <returns>List of medical records for the specified date</returns>
        [HttpGet("date/{date}")]
        public async Task<ActionResult<IEnumerable<MedicalRecordReadDTO>>> GetMedicalRecordsByDate(DateTime date)
        {
            try
            {
                var medicalRecords = await _medicalRecordService.GetMedicalRecordsByDateAsync(date);
                return Ok(medicalRecords);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching medical records for date {Date}", date);
                return StatusCode(500, "Internal server error occurred while fetching medical records");
            }
        }

        /// <summary>
        /// Create a new medical record
        /// </summary>
        /// <param name="medicalRecordDto">Medical record data</param>
        /// <returns>Created medical record</returns>
        [HttpPost]
        public async Task<ActionResult<MedicalRecordReadDTO>> CreateMedicalRecord([FromBody] MedicalRecordCreateDTO medicalRecordDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var createdMedicalRecord = await _medicalRecordService.CreateAsync(medicalRecordDto);
                return CreatedAtAction(
                    nameof(GetMedicalRecordById), 
                    new { id = createdMedicalRecord.MedicalRecordId }, 
                    createdMedicalRecord);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating medical record");
                return StatusCode(500, "Internal server error occurred while creating medical record");
            }
        }

        /// <summary>
        /// Update an existing medical record
        /// </summary>
        /// <param name="id">Medical record ID</param>
        /// <param name="medicalRecordDto">Updated medical record data</param>
        /// <returns>Updated medical record</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<MedicalRecordReadDTO>> UpdateMedicalRecord(int id, [FromBody] MedicalRecordUpdateDTO medicalRecordDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != medicalRecordDto.MedicalRecordId)
                {
                    return BadRequest("ID in URL does not match ID in request body");
                }

                var updatedMedicalRecord = await _medicalRecordService.UpdateAsync(medicalRecordDto);
                if (updatedMedicalRecord == null)
                {
                    return NotFound($"Medical record with ID {id} not found");
                }

                return Ok(updatedMedicalRecord);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating medical record with ID {Id}", id);
                return StatusCode(500, "Internal server error occurred while updating medical record");
            }
        }

        /// <summary>
        /// Delete a medical record
        /// </summary>
        /// <param name="id">Medical record ID</param>
        /// <returns>Deleted medical record</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<MedicalRecordReadDTO>> DeleteMedicalRecord(int id)
        {
            try
            {
                var deletedMedicalRecord = await _medicalRecordService.DeleteAsync(id);
                if (deletedMedicalRecord == null)
                {
                    return NotFound($"Medical record with ID {id} not found");
                }

                return Ok(deletedMedicalRecord);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting medical record with ID {Id}", id);
                return StatusCode(500, "Internal server error occurred while deleting medical record");
            }
        }
    }
}
using HealthCareManagementSystem.Application.DTOs.AppointmentDTOs;
using HealthCareManagementSystem.Application.Services.AppointmentServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace HealthCareManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Require authentication for all appointment endpoints
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;       
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Doctor")] // Only admins and doctors can view all appointments
        public async Task<IActionResult> GetAllAppointments()
        {
            var appointments = await _appointmentService.GetAllAsync();
            return Ok(appointments);       
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppointmentById(int id)
        {
            var appointment = await _appointmentService.GetByIdAsync(id);
            if (appointment == null)
                return NotFound();
            return Ok(appointment);       
        }
        
        [HttpGet("doctor/{doctorId}")]
        [Authorize(Roles = "Admin,Doctor")] // Only admins and doctors can view doctor appointments
        public async Task<IActionResult> GetAppointmentsByDoctorId(string doctorId)
        {
            var appointments = await _appointmentService.GetAppointmentsByDoctorIdAsync(doctorId);
            if (appointments.ToList().Count == 0)
            {
                return NotFound();
            }
            
            return Ok(appointments);
        }
        
        [HttpGet("patient/{patientId}")]
        [Authorize(Roles = "Admin,Doctor,Patient")] // Admins, doctors, and patients can view patient appointments
        public async Task<IActionResult> GetAppointmentsByPatientId(string patientId)
        {
            var appointments = await _appointmentService.GetAppointmentsByPatientIdAsync(patientId);
            if (appointments.ToList().Count == 0)
            {
                return NotFound();
            }
            
            return Ok(appointments);
        }
        
        // [HttpGet("date/{date}")]
        // public async Task<IActionResult> GetAppointmentsByDate(DateTime date)
        // {
        //     var appointments = await _appointmentService.GetAppointmentsByDateAsync(date);
        //     return Ok(appointments);
        // }
        
        [HttpPost]
        [Authorize(Roles = "Admin,Patient")] // Admins and patients can create appointments
        public async Task<IActionResult> CreateAppointment([FromBody] AppointmentCreateDTO appointment)
        {
            var created = await _appointmentService.CreateAsync(appointment);
            return CreatedAtAction(nameof(GetAppointmentById), new { id = created.AppointmentId }, created);
        }
        
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Doctor,Patient")] // Admins, doctors, and patients can update appointments
        public async Task<IActionResult> UpdateAppointment(int id, [FromBody] AppointmentUpdateDTO appointment)
        {
            if (id != appointment.AppointmentId)
                return BadRequest("Appointment ID mismatch");

            var updated = await _appointmentService.UpdateAsync(appointment);
            return Ok(updated);
        }
        
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] // Only admins can delete appointments
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var deleted = await _appointmentService.DeleteAsync(id);
            if (deleted == null)
            {
                return NotFound();           
            }
            return Ok(deleted);
        }

    }
}

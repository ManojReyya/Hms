using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HealthCareManagementSystem.Application.Services.AdminServices;
using HealthCareManagementSystem.Application.DTOs.AdminDTOs;

namespace HealthCareManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly ILogger<AdminController> _logger;

        public AdminController(IAdminService adminService, ILogger<AdminController> logger)
        {
            _adminService = adminService;
            _logger = logger;
        }

        // GET: api/Admin
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdminReadDTO>>> GetAllAdmins()
        {
            try
            {
                var admins = await _adminService.GetAllAdminsAsync();
                return Ok(admins);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving admin users");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/Admin/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<AdminReadDTO>> GetAdminById(string id)
        {
            try
            {
                var admin = await _adminService.GetAdminByIdAsync(id);
                if (admin == null)
                    return NotFound();

                return Ok(admin);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving admin user with ID {id}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
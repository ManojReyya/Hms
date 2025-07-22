using Microsoft.AspNetCore.Mvc;
using HealthCareManagementSystem.Application.Services.UserServices;
using HealthCareManagementSystem.Application.DTOs.UserDTOs;
using HealthCareManagementSystem.Domain.Entities;
using Microsoft.AspNetCore.Authorization;

namespace HealthCareManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Require authentication for all endpoints by default
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        // GET: api/User
        [HttpGet]
        [Authorize(Roles = "Admin")] // Only admins can view all users
        public async Task<ActionResult<IEnumerable<UserReadDTO>>> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving users");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/User/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<UserReadDTO>> GetUserById(string id)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(id);
                if (user == null)
                    return NotFound();

                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving user with ID {id}");
                return StatusCode(500, "Internal server error");
            }
        }

        // POST: api/User
        [HttpPost]
        [Authorize(Roles = "Admin")] // Only admins can create users
        public async Task<IActionResult> CreateUser([FromBody] UserCreateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _userService.CreateUserAsync(dto);
                return CreatedAtAction(nameof(GetUserById), new { id = dto.UserId }, dto);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Business rule violation while creating user");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user");
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT: api/User/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UserUpdateDTO dto)
        {
            if (id != dto.UserId)
                return BadRequest("ID mismatch");

            try
            {
                await _userService.UpdateUserAsync(dto);
                return Ok($"User with ID {id} updated successfully");
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Business rule violation while updating user");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating user with ID {id}");
                return StatusCode(500, "Internal server error");
            }
        }

        // DELETE: api/User/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] // Only admins can delete users
        public async Task<IActionResult> DeleteUser(string id)
        {
            try
            {
                await _userService.DeleteUserAsync(id);
                return Ok($"User with ID {id} deleted successfully");
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "User not found for deletion");
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Business rule violation while deleting user");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting user with ID {id}");
                return StatusCode(500, "Internal server error");
            }
        }

        // PATCH: api/User/{id}/deactivate
        [HttpPatch("{id}/deactivate")]
        [Authorize(Roles = "Admin")] // Only admins can deactivate users
        public async Task<IActionResult> DeactivateUser(string id)
        {
             try
            {
                await _userService.DeactivateUser(id);
                return Ok($"User with ID {id} deactivated successfully");
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "User not found for deactivation");
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Business rule violation while deactivating user");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deactivating user with ID {id}");
                return StatusCode(500, "Internal server error");
            }
        }

        // Authentication endpoints
        [HttpPost("Register")]
        [AllowAnonymous]// Allow anonymous access to the Register endpoint
        public async Task<IActionResult> Register([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("User cannot be null");
            }

            try
            {
                await _userService.RegisterAsync(user);
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error registering user");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("Login")]
        [AllowAnonymous] // Allow anonymous access to the Login endpoint
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            if (login == null || string.IsNullOrEmpty(login.UserId) || string.IsNullOrEmpty(login.Password))
            {
                return BadRequest("UserId and Password are required");
            }

            try
            {
                var response = await _userService.LoginAsync(login);
                if (response == null)
                {
                    return NotFound("Invalid User");
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login");
                return StatusCode(500, "Internal server error");
            }
        }

        // Test endpoint to verify JWT authentication
        [HttpGet("test-auth")]
        [Authorize] // Requires valid JWT token
        public IActionResult TestAuth()
        {
            var userId = User.Identity?.Name;
            var role = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
            
            return Ok(new { 
                Message = "Authentication successful", 
                UserId = userId, 
                Role = role,
                Timestamp = DateTime.UtcNow 
            });
        }

        // Test endpoint for admin-only access
        [HttpGet("test-admin")]
        [Authorize(Roles = "Admin")]
        public IActionResult TestAdminAuth()
        {
            return Ok(new { 
                Message = "Admin authentication successful", 
                Timestamp = DateTime.UtcNow 
            });
        }
    }
}

using System.ComponentModel.DataAnnotations;
using HealthCareManagementSystem.Domain.Enums;

namespace HealthCareManagementSystem.Domain.Entities;

public class User
{
    [Key]
    [StringLength(100)]
    public string UserId { get; set; } = null!;

    [Required]
    [StringLength(255)]
    public string Password { get; set; } = null!;

    [Required]
    public UserRole Role { get; set; }

    [Required]
    public Boolean IsActive { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    // Navigation Properties
    // one-to-one
    public Doctor? Doctor { get; set; }
    public Patient? Patient { get; set; }
}
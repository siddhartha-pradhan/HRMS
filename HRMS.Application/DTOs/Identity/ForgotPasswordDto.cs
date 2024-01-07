using System.ComponentModel.DataAnnotations;

namespace HRMS.Application.DTOs.Identity;

public class ForgotPasswordDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}
using System.ComponentModel.DataAnnotations;
using AuthenticationService.Api.Validation;

namespace AuthenticationService.Api.DTOs;

public sealed class RegisterRequest
{
    [Required]
    [EmailAddress]
    [StringLength(320)]
    public string Email { get; init; } = string.Empty;

    [Required]
    [BcryptPassword]
    public string Password { get; init; } = string.Empty;

    [Required]
    [Compare(nameof(Password), ErrorMessage = "Password and confirmation password must match.")]
    public string ConfirmPassword { get; init; } = string.Empty;
}

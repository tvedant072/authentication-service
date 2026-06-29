using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AuthenticationService.Api.Validation;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter)]
public sealed class BcryptPasswordAttribute : ValidationAttribute
{
    private const int MinimumLength = 12;
    private const int MaximumByteCount = 72;

    protected override ValidationResult? IsValid(
        object? value,
        ValidationContext validationContext)
    {
        if (value is not string password)
        {
            return ValidationResult.Success;
        }

        if (password.Length < MinimumLength)
        {
            return new ValidationResult(
                $"Password must be at least {MinimumLength} characters long.");
        }

        if (Encoding.UTF8.GetByteCount(password) > MaximumByteCount)
        {
            return new ValidationResult(
                $"Password must not exceed {MaximumByteCount} bytes when UTF-8 encoded.");
        }

        return ValidationResult.Success;
    }
}

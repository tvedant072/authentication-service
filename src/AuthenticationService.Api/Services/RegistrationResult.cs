using AuthenticationService.Api.DTOs;

namespace AuthenticationService.Api.Services;

public sealed record RegistrationResult(
    bool IsSuccess,
    RegisterResponse? User)
{
    public static RegistrationResult Success(RegisterResponse user) => new(true, user);

    public static RegistrationResult DuplicateEmail() => new(false, null);
}

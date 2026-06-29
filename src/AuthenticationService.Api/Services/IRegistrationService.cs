using AuthenticationService.Api.DTOs;

namespace AuthenticationService.Api.Services;

public interface IRegistrationService
{
    Task<RegistrationResult> RegisterAsync(
        RegisterRequest request,
        CancellationToken cancellationToken);
}

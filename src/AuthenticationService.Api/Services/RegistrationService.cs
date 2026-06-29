using AuthenticationService.Api.DTOs;
using AuthenticationService.Api.Entities;
using AuthenticationService.Api.Repositories;

namespace AuthenticationService.Api.Services;

public sealed class RegistrationService(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    TimeProvider timeProvider) : IRegistrationService
{
    public async Task<RegistrationResult> RegisterAsync(
        RegisterRequest request,
        CancellationToken cancellationToken)
    {
        var email = request.Email.Trim();
        var normalizedEmail = email.ToUpperInvariant();

        if (await userRepository.ExistsByNormalizedEmailAsync(
                normalizedEmail,
                cancellationToken))
        {
            return RegistrationResult.DuplicateEmail();
        }

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = email,
            NormalizedEmail = normalizedEmail,
            PasswordHash = passwordHasher.Hash(request.Password),
            CreatedAtUtc = timeProvider.GetUtcNow()
        };

        try
        {
            await userRepository.AddAsync(user, cancellationToken);
        }
        catch (DuplicateEmailException)
        {
            return RegistrationResult.DuplicateEmail();
        }

        return RegistrationResult.Success(
            new RegisterResponse(user.Id, user.Email, user.CreatedAtUtc));
    }
}

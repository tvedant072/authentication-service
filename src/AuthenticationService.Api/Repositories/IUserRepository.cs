using AuthenticationService.Api.Entities;

namespace AuthenticationService.Api.Repositories;

public interface IUserRepository
{
    Task<bool> ExistsByNormalizedEmailAsync(
        string normalizedEmail,
        CancellationToken cancellationToken);

    Task AddAsync(User user, CancellationToken cancellationToken);
}

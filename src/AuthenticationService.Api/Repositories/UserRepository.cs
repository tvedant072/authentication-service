using AuthenticationService.Api.Data;
using AuthenticationService.Api.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Api.Repositories;

public sealed class UserRepository(AuthDbContext dbContext) : IUserRepository
{
    public Task<bool> ExistsByNormalizedEmailAsync(
        string normalizedEmail,
        CancellationToken cancellationToken)
    {
        return dbContext.Users
            .AsNoTracking()
            .AnyAsync(user => user.NormalizedEmail == normalizedEmail, cancellationToken);
    }

    public async Task AddAsync(User user, CancellationToken cancellationToken)
    {
        dbContext.Users.Add(user);

        try
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException exception)
            when (exception.InnerException is SqlException { Number: 2601 or 2627 })
        {
            throw new DuplicateEmailException(exception);
        }
    }
}

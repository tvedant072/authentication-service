using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Api.Data;

public sealed class AuthDbContext(DbContextOptions<AuthDbContext> options) : DbContext(options)
{
}

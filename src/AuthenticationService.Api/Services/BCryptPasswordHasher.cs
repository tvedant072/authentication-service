using AuthenticationService.Api.Configuration;
using Microsoft.Extensions.Options;

namespace AuthenticationService.Api.Services;

public sealed class BCryptPasswordHasher(
    IOptions<PasswordHashingOptions> options) : IPasswordHasher
{
    public string Hash(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, options.Value.WorkFactor);
    }
}

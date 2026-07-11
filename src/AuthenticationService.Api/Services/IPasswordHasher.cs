namespace AuthenticationService.Api.Services;

public interface IPasswordHasher
{
    string Hash(string password);
}

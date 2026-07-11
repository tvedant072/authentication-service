namespace AuthenticationService.Api.Repositories;

public sealed class DuplicateEmailException : Exception
{
    public DuplicateEmailException(Exception innerException)
        : base("A user with this email address already exists.", innerException)
    {
    }
}

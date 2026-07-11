namespace AuthenticationService.Api.Entities;

public sealed class User
{
    public Guid Id { get; init; }

    public required string Email { get; init; }

    public required string NormalizedEmail { get; init; }

    public required string PasswordHash { get; init; }

    public DateTimeOffset CreatedAtUtc { get; init; }
}

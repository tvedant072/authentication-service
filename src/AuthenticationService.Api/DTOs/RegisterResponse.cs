namespace AuthenticationService.Api.DTOs;

public sealed record RegisterResponse(
    Guid Id,
    string Email,
    DateTimeOffset CreatedAtUtc);

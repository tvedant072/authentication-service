namespace AuthenticationService.Api.Configuration;

public sealed class PasswordHashingOptions
{
    public const string SectionName = "PasswordHashing";

    public int WorkFactor { get; init; } = 12;
}

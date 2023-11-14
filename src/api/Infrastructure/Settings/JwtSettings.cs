namespace Infrastructure.Settings;

public class JwtSettings
{
    public static string SectionName => nameof(JwtSettings);
    public string Issuer { get; init; } = string.Empty;
    public string Audience { get; init; } = string.Empty;
    public int ExpirationInDays { get; init; }
    public string SecretKey { get; init; } = string.Empty;
}

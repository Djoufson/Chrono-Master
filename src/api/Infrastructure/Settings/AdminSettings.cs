using Domain.Entities;

namespace Infrastructure.Settings;

public class AdminSettings
{
    public static string SectionName => nameof(AdminSettings);
    public string Id { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
}

namespace TaskManagement.Core.Domain.Settings;

/// <summary>
/// Represents settings related to JWT (JSON Web Token) configuration.
/// </summary>
public class JwtSettings
{
    /// <summary>
    /// Gets or sets the key used for signing JWT tokens.
    /// </summary>
    public string Key { get; set; } = null!;

    /// <summary>
    /// Gets or sets the issuer of the JWT tokens.
    /// </summary>
    public string Issuer { get; set; } = null!;

    /// <summary>
    /// Gets or sets the audience of the JWT tokens.
    /// </summary>
    public string Audience { get; set; } = null!;

    /// <summary>
    /// Gets or sets the duration in minutes for which JWT tokens are valid.
    /// </summary>
    public int DurationInMinutes { get; set; }
}
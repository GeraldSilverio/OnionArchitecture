namespace RealEstateApp.Core.Application.Dtos.Accounts
{
    /// <summary>
    /// Represents a refresh token used for renewing authentication sessions.
    /// </summary>
    public class RefreshToken
    {
        /// <summary>
        /// Gets or sets the unique identifier for the refresh token.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the refresh token string.
        /// </summary>
        public string Token { get; set; } = null!;

        /// <summary>
        /// Gets or sets the expiration date and time of the refresh token.
        /// </summary>
        public DateTime Expires { get; set; }

        /// <summary>
        /// Gets a value indicating whether the refresh token has expired.
        /// </summary>
        public bool IsExpired => DateTime.UtcNow >= Expires;

        /// <summary>
        /// Gets or sets the date and time when the refresh token was created.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the refresh token was revoked.
        /// </summary>
        public DateTime? Revoked { get; set; }

        /// <summary>
        /// Gets or sets the token that replaced this refresh token.
        /// </summary>
        public string? ReplacedByToken { get; set; }

        /// <summary>
        /// Gets a value indicating whether the refresh token is active.
        /// A token is active if it is not revoked and not expired.
        /// </summary>
        public bool IsActive => Revoked == null && !IsExpired;
    }
}

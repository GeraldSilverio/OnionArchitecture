using System.Text.Json.Serialization;

namespace TaskManagement.Core.Application.Dtos.Accounts
{
    /// <summary>
    /// Represents the response returned after a successful authentication.
    /// </summary>
    public class AuthenticationResponse
    {
        /// <summary>
        /// Gets or sets the user ID.
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// Gets or sets the first name of the user.
        /// </summary>
        public string? FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the user.
        /// </summary>
        public string? LastName { get; set; }

        /// <summary>
        /// Gets or sets the username of the user.
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// Gets or sets the email of the user.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Gets or sets the roles assigned to the user.
        /// </summary>
        public List<string>? Roles { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user is active.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether an error occurred during authentication.
        /// </summary>
        public bool HasError { get; set; }

        /// <summary>
        /// Gets or sets the list of errors that occurred during authentication.
        /// </summary>
        public List<string>? Error { get; set; }

        /// <summary>
        /// Gets or sets the JSON Web Token (JWT) for the authenticated session.
        /// </summary>
        public string? JwToken { get; set; }

        /// <summary>
        /// Gets or sets the refresh token for the authenticated session.
        /// This property is ignored during JSON serialization.
        /// </summary>
        [JsonIgnore]
        public string? RefreshToken { get; set; }
    }
}

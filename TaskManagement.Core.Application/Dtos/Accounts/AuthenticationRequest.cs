using Swashbuckle.AspNetCore.Annotations;

namespace TaskManagement.Core.Application.Dtos.Accounts
{
    /// <summary>
    /// Parameters for signing in.
    /// </summary>
    public class AuthenticationRequest
    {
        /// <summary>
        /// Gets or sets the username for signing in.
        /// </summary>
        [SwaggerParameter(Description = "UserName for Sign In")]
        public string UserName { get; set; } = null!;

        /// <summary>
        /// Gets or sets the password for signing in.
        /// </summary>
        [SwaggerParameter(Description = "Password for Sign In")]
        public string Password { get; set; } = null!;
    }
}

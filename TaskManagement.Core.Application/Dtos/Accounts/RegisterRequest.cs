using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Annotations;

namespace TaskManagement.Core.Application.Dtos.Accounts
{
    /// <summary>
    /// Parameters for registering a new user account.
    /// </summary>
    public class RegisterRequest
    {
        /// <summary>
        /// Gets or sets the first name of the person for whom the account is being created.
        /// </summary>
        [SwaggerParameter(Description = "Person's first name for whom the account will be created")]
        public string FirstName { get; set; } = null!;

        /// <summary>
        /// Gets or sets the last name of the person for whom the account is being created.
        /// </summary>
        [SwaggerParameter(Description = "Person's last name for whom the account will be created")]
        public string LastName { get; set; } = null!;

        /// <summary>
        /// Gets or sets the username of the person for whom the account is being created.
        /// </summary>
        [SwaggerParameter(Description = "Username of the person for whom the account will be created")]
        public string UserName { get; set; } = null!;

        /// <summary>
        /// Gets or sets the email address of the person for whom the account is being created.
        /// </summary>
        [SwaggerParameter(Description = "Email address of the person for whom the account will be created")]
        public string Email { get; set; } = null!;

        /// <summary>
        /// Gets or sets the password of the person for whom the account is being created.
        /// </summary>
        [SwaggerParameter(Description = "Password of the person for whom the account will be created")]
        public string Password { get; set; } = null!;

        /// <summary>
        /// Gets or sets the confirmation password of the person for whom the account is being created.
        /// </summary>
        [SwaggerParameter(Description = "Password confirmation of the person for whom the account will be created")]
        public string ConfirmPassword { get; set; } = null!;

        /// <summary>
        /// Gets or sets a value indicating whether the account is active.
        /// This property is ignored during JSON serialization.
        /// </summary>
        [JsonIgnore]
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the role selected for the account.
        /// This property is ignored during JSON serialization.
        /// </summary>
        [JsonIgnore]
        public int SelectRole { get; set; }
    }
}
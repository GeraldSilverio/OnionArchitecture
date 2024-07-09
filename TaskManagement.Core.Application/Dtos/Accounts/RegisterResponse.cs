
namespace TaskManagement.Core.Application.Dtos.Accounts
{
    // <summary>
    /// Represents the response after attempting to register a new user.
    /// </summary>
    public class RegisterResponse
    {
        /// <summary>
        /// Gets or sets the user ID if registration was successful.
        /// </summary>
        public string? IdUser { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether an error occurred during registration.
        /// </summary>
        public bool HasError { get; set; }

        /// <summary>
        /// Gets or sets the list of errors that occurred during registration, if any.
        /// </summary>
        public List<string>? Error { get; set; }
    }
}

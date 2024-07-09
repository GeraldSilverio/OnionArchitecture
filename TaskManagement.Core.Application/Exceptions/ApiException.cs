using System.Globalization;

namespace TaskManagement.Core.Application.Exceptions
{
    /// <summary>
    /// Custom exception class for API errors.
    /// </summary>
    public class ApiException : Exception
    {
        /// <summary>
        /// Gets or sets the error code associated with the exception.
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// Default constructor for the ApiException class.
        /// </summary>
        public ApiException() : base()
        {
        }

        /// <summary>
        /// Constructor for the ApiException class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public ApiException(string message) : base(message)
        {
        }

        /// <summary>
        /// Constructor for the ApiException class with a specified error message and error code.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="errorCode">The error code associated with the exception.</param>
        public ApiException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode; // Sets the error code for the exception.
        }

        /// <summary>
        /// Constructor for the ApiException class with a formatted error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="args">An array of objects to format the message.</param>
        public ApiException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}

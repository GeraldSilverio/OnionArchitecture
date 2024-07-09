namespace TaskManagement.Core.Application.Wrappers
{
    /// <summary>
    /// Class for the standard response for APIs.
    /// </summary>
    /// <typeparam name="T">The type of data returned in the response.</typeparam>
    public class Response<T>
    {
        /// <summary>
        /// Default constructor for the Response class.
        /// </summary>
        public Response()
        {
        }

        /// <summary>
        /// Constructor for a successful response.
        /// </summary>
        /// <param name="data">The data returned in the response.</param>
        /// <param name="message">An optional message providing additional information.</param>
        public Response(T data, string? message = null)
        {
            Succeeded = true; // Indicates that the response is successful.
            Message = message ?? ""; // Sets the message if provided, otherwise sets it to an empty string.
            Data = data; // Sets the data for the response.
        }

        /// <summary>
        /// Constructor for a failed response.
        /// </summary>
        /// <param name="message">The error message explaining the failure.</param>
        public Response(string message)
        {
            Succeeded = false; // Indicates that the response is not successful.
            Message = message; // Sets the error message.
        }

        /// <summary>
        /// Indicates whether the response is successful.
        /// </summary>
        public bool Succeeded { get; set; }

        /// <summary>
        /// Provides additional information or error messages.
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// A list of errors that occurred during the operation.
        /// </summary>
        public List<string>? Errors { get; set; }

        /// <summary>
        /// The data returned in the response.
        /// </summary>
        public T Data { get; set; }
    }

}

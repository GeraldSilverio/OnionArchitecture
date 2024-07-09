using TaskManagement.Core.Application.Dtos.Accounts;

namespace TaskManagement.Core.Application.Interfaces.Services;

/// <summary>
/// Service interface for managing user accounts.
/// </summary>
public interface IAccountService
{
    /// <summary>
    /// Registers a new user asynchronously.
    /// </summary>
    /// <param name="request">The registration request containing user information.</param>
    /// <param name="origin">The origin URL or context from which the registration request originated.</param>
    /// <returns>A response indicating the result of the registration attempt.</returns>
    Task<RegisterResponse> RegisterAsync(RegisterRequest request, string? origin);

    /// <summary>
    /// Authenticates a user asynchronously.
    /// </summary>
    /// <param name="request">The authentication request containing user credentials.</param>
    /// <returns>A response containing authentication details, including tokens and user information.</returns>
    Task<AuthenticationResponse> AuthenticationAsync(AuthenticationRequest request);

    /// <summary>
    /// Retrieves the ID of the authenticated user.
    /// </summary>
    /// <returns>The ID of the authenticated user.</returns>
    string GetIdUser();
}
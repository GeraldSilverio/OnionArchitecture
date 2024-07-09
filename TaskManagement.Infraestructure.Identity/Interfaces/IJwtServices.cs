using System.IdentityModel.Tokens.Jwt;
using RealEstateApp.Core.Application.Dtos.Accounts;

namespace TaskManagement.Infraestructure.Identity.Interfaces;

/// <summary>
/// Interface for JWT (JSON Web Token) related services.
/// </summary>
public interface IJwtServices
{
    /// <summary>
    /// Generates a JWT security token for the specified user.
    /// </summary>
    /// <param name="user">The user for whom the token is generated.</param>
    /// <returns>A task representing the asynchronous operation that returns a JWT security token.</returns>
    Task<JwtSecurityToken> GetSecurityToken(ApplicationUser user);

    /// <summary>
    /// Retrieves the ID of the authenticated user.
    /// </summary>
    /// <returns>The ID of the authenticated user.</returns>
    string GetIdUser();

    /// <summary>
    /// Generates a refresh token.
    /// </summary>
    /// <returns>A refresh token.</returns>
    RefreshToken GenerateRefreshToken();
}
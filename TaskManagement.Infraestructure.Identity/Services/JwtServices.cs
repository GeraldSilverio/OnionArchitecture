using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RealEstateApp.Core.Application.Dtos.Accounts;
using TaskManagement.Core.Domain.Settings;
using TaskManagement.Infraestructure.Identity.Interfaces;

namespace TaskManagement.Infraestructure.Identity.Services;

/// <summary>
/// Service class implementing <see cref="IJwtServices"/> for JWT generation and management.
/// </summary>
public class JwtServices : IJwtServices
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly JwtSettings _jwtSettings;
    private readonly IHttpContextAccessor _httpContextAccessor;

    /// <summary>
    /// Constructor for <see cref="JwtServices"/>.
    /// </summary>
    /// <param name="jwtSettings">The JWT settings injected via options.</param>
    /// <param name="userManager">The user manager instance.</param>
    /// <param name="httpContextAccessor">The HTTP context accessor for retrieving tokens.</param>
    public JwtServices(IOptions<JwtSettings> jwtSettings, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
    {
        _jwtSettings = jwtSettings.Value;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    /// Generates a JWT security token for the specified user.
    /// </summary>
    /// <param name="user">The user for whom the token is generated.</param>
    /// <returns>A task that represents the asynchronous operation and returns the JWT security token.</returns>
    public async Task<JwtSecurityToken> GetSecurityToken(ApplicationUser user)
    {
        // Retrieve user claims and roles
        var userClaims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);

        var roleClaims = new List<Claim>();

        // Add role claims for each role the user belongs to
        foreach (var role in roles)
        {
            roleClaims.Add(new Claim("roles", role));
        }

        // Combine user claims, role claims, and standard JWT claims
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("uid", user.Id)
        }
        .Union(userClaims)
        .Union(roleClaims);

        // Create symmetric security key and signing credentials
        SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        // Create JWT security token with issuer, audience, claims, expiration, and signing credentials
        JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
            signingCredentials: signingCredentials);

        return jwtSecurityToken;
    }

    /// <summary>
    /// Retrieves the ID of the authenticated user from the JWT token.
    /// </summary>
    /// <returns>The ID of the authenticated user.</returns>
    public string GetIdUser()
    {
        try
        {
            // Retrieve authorization header and extract JWT token
            string authorization = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            string tokenRequest = authorization.Replace("Bearer", "").Trim();
            JwtSecurityToken jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(tokenRequest);

            // Retrieve and return the 'uid' claim value from the JWT token
            Claim idUser = jwtToken.Claims.FirstOrDefault(x => x.Type == "uid");
            return idUser?.Value ?? throw new ApplicationException("User ID claim not found in token.");
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
    }

    /// <summary>
    /// Generates a refresh token.
    /// </summary>
    /// <returns>The generated refresh token.</returns>
    public RefreshToken GenerateRefreshToken()
    {
        return new RefreshToken
        {
            Token = RandomTokenString(),
            Expires = DateTime.UtcNow.AddDays(7),
            Created = DateTime.UtcNow
        };
    }

    /// <summary>
    /// Generates a random token string using cryptographic RNG.
    /// </summary>
    /// <returns>The generated random token string.</returns>
    private string RandomTokenString()
    {
        using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
        byte[] randomBytes = new byte[40];
        rngCryptoServiceProvider.GetBytes(randomBytes);

        return BitConverter.ToString(randomBytes).Replace("-", "");
    }
}
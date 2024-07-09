using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using TaskManagement.Core.Application;
using TaskManagement.Core.Application.Dtos.Accounts;
using TaskManagement.Core.Application.Interfaces.Services;
using TaskManagement.Infraestructure.Identity.Context;
using TaskManagement.Infraestructure.Identity.Interfaces;

namespace TaskManagement.Infraestructure.Identity.Services;

/// <summary>
/// Service class implementing <see cref="IAccountService"/> for user account management.
/// </summary>
public class AccountService : IAccountService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IdentityContext _identityContext;
    private readonly IJwtServices _jwtServices;

    /// <summary>
    /// Constructor for <see cref="AccountService"/>.
    /// </summary>
    /// <param name="userManager">The user manager instance.</param>
    /// <param name="signInManager">The sign-in manager instance.</param>
    /// <param name="identityContext">The Identity context.</param>
    /// <param name="jwtServices">The JWT services instance.</param>
    public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
        IdentityContext identityContext, IJwtServices jwtServices)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _identityContext = identityContext;
        _jwtServices = jwtServices;
    }

    /// <summary>
    /// Registers a new user asynchronously.
    /// </summary>
    /// <param name="request">The registration request.</param>
    /// <param name="origin">The origin of the registration request.</param>
    /// <returns>A task that represents the asynchronous operation and returns the registration response.</returns>
    public async Task<RegisterResponse> RegisterAsync(RegisterRequest request, string? origin)
    {
        try
        {
            var response = new RegisterResponse()
            {
                HasError = false,
                Error = new List<string>()
            };

            // Validate user inputs
            var validate = await ValidateUser(request, response);
            if (validate.HasError) return validate;

            var user = new ApplicationUser
            {
                UserName = request.UserName,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                IsActive = true,
                EmailConfirmed = true
            };

            // Attempt to create the user
            var result = await _userManager.CreateAsync(user, request.Password);

            // If user creation is successful, assign 'User' role and return response
            if (result.Succeeded)
            {
                response.IdUser = user.Id;
                await _userManager.AddToRoleAsync(user, Roles.User.ToString());
                return response;
            }

            // If user creation fails, collect errors and return response with errors
            foreach (var error in result.Errors)
            {
                response.Error.Add(error.Description);
            }

            response.HasError = true;
            return response;
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Error al intentar registrar un nuevo usuario: {ex.Message} {ex.StackTrace}", ex);
        }
    }

    /// <summary>
    /// Authenticates a user asynchronously.
    /// </summary>
    /// <param name="request">The authentication request.</param>
    /// <returns>A task that represents the asynchronous operation and returns the authentication response.</returns>
    public async Task<AuthenticationResponse> AuthenticationAsync(AuthenticationRequest request)
    {
        try
        {
            var response = new AuthenticationResponse()
            {
                Error = new List<string>(),
                HasError = false
            };

            // Validate if the user exists
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                SetError(response, $"El usuario {request.UserName} no existe");
                return response;
            }

            // Validate if the password matches the user's password in the database
            var checkPassword = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
            if (!checkPassword.Succeeded)
            {
                SetError(response, $"La contraseña para el usuario {request.UserName} es incorrecta");
                return response;
            }

            // Retrieve roles of the user
            var roles = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

            // Populate response with user details and JWT token
            response.Id = user.Id;
            response.Email = user.Email;
            response.UserName = user.UserName;
            response.FirstName = user.FirstName;
            response.LastName = user.LastName;
            response.IsActive = user.IsActive;
            response.Roles = roles.ToList();

            // Assign JWT token and refresh token
            var jwtSecutiryToken = await _jwtServices.GetSecurityToken(user);
            response.JwToken = new JwtSecurityTokenHandler().WriteToken(jwtSecutiryToken);
            response.RefreshToken = _jwtServices.GenerateRefreshToken().Token;

            return response;
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
    }

    /// <summary>
    /// Validates user inputs during registration.
    /// </summary>
    /// <param name="request">The registration request.</param>
    /// <param name="response">The registration response.</param>
    /// <returns>A task that represents the asynchronous operation and returns the registration response.</returns>
    private async Task<RegisterResponse> ValidateUser(RegisterRequest request, RegisterResponse response)
    {
        try
        {
            // Check if the username already exists
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user != null)
            {
                SetError(response, $"UserName: {request.UserName} was used.Try again with another UserName");
            }

            // Check if the email already exists
            var userEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userEmail != null)
            {
                SetError(response, $"Email: {request.Email} was used.Try again with another Email");
            }

            if(request.Password != request.ConfirmPassword)
            {
                SetError(response, $"Passwords don`t match.");
            }

            return response;
        }
        catch (Exception e)
        {
            throw new ApplicationException(e.Message, e);
        }
    }

    /// <summary>
    /// Sets error information in the response object.
    /// </summary>
    /// <param name="model">The response model to set errors on.</param>
    /// <param name="error">The error message to set.</param>
    /// <returns>The response model with error information set.</returns>
    private dynamic SetError(dynamic model, string error)
    {
        model.HasError = true;
        model.Error?.Add(error);
        return model;
    }

    /// <summary>
    /// Retrieves the ID of the authenticated user.
    /// </summary>
    /// <returns>The ID of the authenticated user.</returns>
    public string GetIdUser()
    {
        try
        {
            return _jwtServices.GetIdUser();
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
    }
}
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TaskManagement.Core.Application.Dtos.Accounts;
using TaskManagement.Core.Application.Interfaces.Services;
using TaskManagement.Infraestructure.Identity.Interfaces;
using RegisterRequest = TaskManagement.Core.Application.Dtos.Accounts.RegisterRequest;

namespace TaskManagement.Presentation.WebApi.Controllers;

[ApiController]
[SwaggerTag("Controlador para el login y registro de la API")]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly IJwtServices _jwtServices;

    public UserController(IAccountService accountService, IJwtServices jwtServices)
    {
        _accountService = accountService;
        _jwtServices = jwtServices;
    }
    
    [HttpPost("Register")]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerOperation(
        Summary ="Registro de un nuevo usuario",
        Description="Se envia los parametros necesarios para crear un usuario"
    )]
    public async Task<IActionResult> RegisterDeveloper([FromBody] RegisterRequest register)
    {
        try
        {
            var response = await _accountService.RegisterAsync(register, null);
            return response.HasError ? BadRequest(response.Error) : StatusCode(StatusCodes.Status201Created, "Usuario creado con existo");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPost("Authentication")]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerOperation(
        Summary = "Login de usuario",
        Description ="Incio de sesion para los usuarios del sistema"
    )]
    public async Task<IActionResult> Authentication([FromBody] AuthenticationRequest request)
    {
        try
        {
            var response = await _accountService.AuthenticationAsync(request);
            return StatusCode(response.HasError ? StatusCodes.Status400BadRequest : StatusCodes.Status200OK, response);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
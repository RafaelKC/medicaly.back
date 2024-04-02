using Medicaly.Application.Authentications;
using Medicaly.Application.Authentications.Dtos;
using Medicaly.Domain.Pacientes.Dtos;
using Medicaly.Domain.Users;
using Medicaly.Domain.Users.Enums;
using Medicaly.Infrastructure.Authentication;
using Medicaly.Infrastructure.Authentication.Dots;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Medicaly.Api.Authentication;

[Route("auth")]
[AllowAnonymous]
public class AuthenticationController: ControllerBase
{
    private readonly ISingUpService _singUpService;
    private readonly ISingInService _singInService;
    private readonly IUsuarioService _usuarioService;
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(ISingUpService singUpService, ISingInService singInService, IUsuarioService usuarioService, IAuthenticationService authenticationService)
    {
        _singUpService = singUpService;
        _singInService = singInService;
        _usuarioService = usuarioService;
        _authenticationService = authenticationService;
    }

    [HttpPost("paciente/register")]
    public async Task<ActionResult<CreateUserOutput>> RegisterPaciente(
        [FromBody] CreateUserInput<PacienteInput> pacienteInput)
    {
        var result = await _singUpService.SingUp(pacienteInput);
        if (result.Success) return Ok(result);
        return UnprocessableEntity();
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginOutput>> Login([FromQuery] UserTipo tipoUsuario, [FromBody] LoginInput loginInput)
    {
        var result = await _singInService.SingIn(tipoUsuario, loginInput);
        if (result.Success) return Ok(result);
        return UnprocessableEntity();
    }

    [HttpPost("logout")]
    [Authorize]
    public async Task LogOut()
    {
        await _authenticationService.Logout();
    }

    [HttpGet("current-user")]
    [Authorize]
    public ActionResult<User> GetCurrentUser()
    {
        var usuario = _usuarioService.GetCurrentUser();
        return usuario != null ? usuario : NotFound();
    }

}
using Medicaly.Application.Authentications;
using Medicaly.Application.Authentications.Dtos;
using Medicaly.Domain.Pacientes.Dtos;
using Medicaly.Domain.Users.Enums;
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

    public AuthenticationController(ISingUpService singUpService, ISingInService singInService)
    {
        _singUpService = singUpService;
        _singInService = singInService;
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

}
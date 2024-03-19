using Medicaly.Application.Authentications;
using Medicaly.Application.Authentications.Dtos;
using Medicaly.Domain.Pacientes.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Medicaly.Api.Authentication;

[Route("auth")]
[AllowAnonymous]
public class AuthenticationController: ControllerBase
{
    private readonly ISingUpService _singUpService;

    public AuthenticationController(ISingUpService singUpService)
    {
        _singUpService = singUpService;
    }

    [HttpPost("paciente/register")]
    public async Task<ActionResult<CreateUserOutput>> RegisterPaciente(
        [FromBody] CreateUserInput<PacienteInput> pacienteInput)
    {
        var result = await _singUpService.SingUp(pacienteInput);
        if (result.Success) return Ok(result);
        return UnprocessableEntity();
    }

}
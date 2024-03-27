using Medicaly.Application.Pacientes;
using Medicaly.Application.Transients;
using Medicaly.Domain.Users.Enums;
using Medicaly.Infrastructure.Authentication;
using Medicaly.Infrastructure.Authentication.Dots;
using Microsoft.AspNetCore.Mvc;

namespace Medicaly.Application.Authentications;

public interface ISingInService
{
    public Task<LoginOutput> SingIn([FromRoute] UserTipo tipoUsuario, [FromBody] LoginInput loginInput);
}

public class SingInService: ISingInService, IAutoTransient
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IPacienteService _pacienteService;

    public SingInService(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public async Task<LoginOutput> SingIn(UserTipo tipoUsuario, LoginInput loginInput)
    {
        if (tipoUsuario == UserTipo.Paciente)
        {
            var paciente = await _pacienteService.GetByEmail(loginInput.email);
            if (paciente is null)
                return new LoginOutput
                {
                    Success = false
                };
        }
        return await _authenticationService.Login(loginInput);
    }
}
using Medicaly.Application.Administradores;
using Medicaly.Application.Pacientes;
using Medicaly.Application.Profissionals;
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
    private readonly IProfissionalService _profissionalService;
    private readonly IAdministradorService _administradorService;

    public SingInService(
        IAuthenticationService authenticationService,
        IPacienteService pacienteService,
        IProfissionalService profissionalService,
        IAdministradorService administradorService)
    {
        _authenticationService = authenticationService;
        _pacienteService = pacienteService;
        _profissionalService = profissionalService;
        _administradorService = administradorService;
    }

    public async Task<LoginOutput> SingIn(UserTipo tipoUsuario, LoginInput loginInput)
    {
        switch (tipoUsuario)
        {
            case UserTipo.Administrador:
                var administrador = await _administradorService.GetByEmail(loginInput.email);
                if (administrador is null)
                    return new LoginOutput
                    {
                        Success = false
                    };
                break;
            case UserTipo.ProfissionalSaude:
                var profissional = await _profissionalService.GetByEmail(loginInput.email);
                if (profissional is null)
                    return new LoginOutput
                    {
                        Success = false
                    };
                break;
            case UserTipo.Paciente:
                var paciente = await _pacienteService.GetByEmail(loginInput.email);
                if (paciente is null)
                    return new LoginOutput
                    {
                        Success = false
                    };
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(tipoUsuario), tipoUsuario, null);
        }

        return await _authenticationService.Login(loginInput);
    }
}
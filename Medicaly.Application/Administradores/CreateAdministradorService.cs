using Medicaly.Application.Administradores.Dtos;
using Medicaly.Application.Enrecos;
using Medicaly.Application.Transients;
using Medicaly.Domain.Users;
using Medicaly.Domain.Users.Enums;
using Medicaly.Infrastructure.Authentication;

namespace Medicaly.Application.Administradores;

public interface ICreateAdministradorService
{
    public Task<Guid?> CreateUser(CreateAdministradorInput input);
}

public class CreateAdministradorService: ICreateAdministradorService, IAutoTransient
{
    private readonly IEnderecoService _enderecoService;
    private readonly IAdministradorService _AdministradorService;
    private readonly IAuthenticationService _authenticationService;
    public CreateAdministradorService(IEnderecoService enderecoService, IAuthenticationService authenticationService,IAdministradorService AdministradorService)
    {
        _enderecoService = enderecoService;
        _AdministradorService = AdministradorService;
        _authenticationService = authenticationService;
    }

    public async Task<Guid?> CreateUser(CreateAdministradorInput input)
    {
        input.Endereco.Id = input.Endereco.Id != Guid.Empty ? input.Endereco.Id : Guid.NewGuid();
        var enderecoCriado = await _enderecoService.Create(input.Endereco);
        if (enderecoCriado.HasValue)
        {
            input.Administrador.EnderecoId = input.Endereco.Id;
            var usuarioCriado = await _AdministradorService.Create(input.Administrador);
            if (usuarioCriado.HasValue)
            {
               var result =  await _authenticationService.RegisterAsync(input.Administrador.Email, input.Password, new User(input.Administrador, UserTipo.Administrador));
                if (result.Success)
                {
                    return usuarioCriado;
                }
            }
        }

        return null;
    }
}
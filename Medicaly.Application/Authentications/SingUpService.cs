using Medicaly.Application.Authentications.Dtos;
using Medicaly.Application.Enrecos;
using Medicaly.Application.Pacientes;
using Medicaly.Application.Transients;
using Medicaly.Domain.Pacientes.Dtos;
using Medicaly.Domain.Users;
using Medicaly.Domain.Users.Enums;
using Medicaly.Infrastructure.Authentication;

namespace Medicaly.Application.Authentications;

public interface ISingUpService
{
    public Task<CreateUserOutput> SingUp(CreateUserInput<PacienteInput> pacienteInput);
}

public class SingUpService: ISingUpService, IAutoTransient
{
    private readonly IPacienteService _pacienteService;
    private readonly IAuthenticationService _authenticationService;
    private readonly IEnderecoService _enderecoService;

    public SingUpService(
        IPacienteService pacienteService,
        IAuthenticationService authenticationService,
        IEnderecoService enderecoService)
    {
        _pacienteService = pacienteService;
        _authenticationService = authenticationService;
        _enderecoService = enderecoService;
    }


    public async Task<CreateUserOutput> SingUp(CreateUserInput<PacienteInput> pacienteInput)
    {
        var pacienteExistente = await _pacienteService.GetByCpf(pacienteInput.User.Cpf);

        pacienteInput.Endereco.Id = pacienteInput.Endereco.Id != Guid.Empty  ? pacienteInput.Endereco.Id : Guid.NewGuid();

        var userAtualizadoCriado = false;
        if (pacienteExistente != null)
        {
            if (pacienteExistente.EnderecoId.HasValue)
            {
                pacienteInput.Endereco.Id = pacienteExistente.EnderecoId.Value;
                await _enderecoService.Update(pacienteExistente.EnderecoId.Value, pacienteInput.Endereco);
            }
            else
            {
                await _enderecoService.Create(pacienteInput.Endereco);
                pacienteInput.User.EnderecoId = pacienteInput.Endereco.Id;
            }

            userAtualizadoCriado = await _pacienteService.Update(pacienteExistente.Id, pacienteInput.User);
        }
        else
        {
            await _enderecoService.Create(pacienteInput.Endereco);
            pacienteInput.User.EnderecoId = pacienteInput.Endereco.Id;
            var userId = await _pacienteService.Create(pacienteInput.User);
            userAtualizadoCriado = userId.HasValue;
            if (userAtualizadoCriado)
            {
                pacienteInput.User.Id = userId.Value;
            }
        }

        if (!userAtualizadoCriado)
        {
            return new CreateUserOutput
            {
                Success = false
            };
        }

        var result =
            await _authenticationService.RegisterAsync(pacienteInput.User.Email, pacienteInput.Password, new User(pacienteInput.User, UserTipo.Paciente));

        return new CreateUserOutput
        {
            Success = result.Success,
            RefreshToken = result.RefreshToken,
            Token = result.Token
        };
    }
}
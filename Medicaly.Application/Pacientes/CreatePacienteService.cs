using Medicaly.Application.Enrecos;
using Medicaly.Application.Pacientes.Dtos;
using Medicaly.Application.Transients;

namespace Medicaly.Application.Pacientes;

public interface ICreatePacienteService
{
    public Task<Guid?> CreateUser(CreateMedicoInput input);
}

public class CreatePacienteService: ICreatePacienteService, IAutoTransient
{
    private readonly IEnderecoService _enderecoService;
    private readonly IPacienteService _pacienteService;

    public CreatePacienteService(IEnderecoService enderecoService, IPacienteService pacienteService)
    {
        _enderecoService = enderecoService;
        _pacienteService = pacienteService;
    }

    public async Task<Guid?> CreateUser(CreateMedicoInput input)
    {
        input.Endereco.Id = input.Endereco.Id != Guid.Empty ? input.Endereco.Id : Guid.NewGuid();
        var enderecoCriado = await _enderecoService.Create(input.Endereco);
        if (enderecoCriado.HasValue)
        {
            input.Paciente.EnderecoId = input.Endereco.Id;
            return await _pacienteService.Create(input.Paciente);
        }

        return null;
    }
}
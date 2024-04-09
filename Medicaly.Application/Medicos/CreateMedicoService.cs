using Medicaly.Application.Enrecos;
using Medicaly.Application.Medicos.Dtos;
using Medicaly.Application.Transients;

namespace Medicaly.Application.Medicos;

public interface ICreateMedicoService
{
    public Task<Guid?> CreateUser(CreateMedicoInput input);
}

public class CreateMedicoService: ICreateMedicoService, IAutoTransient
{
    private readonly IEnderecoService _enderecoService;
    private readonly IMedicoService _MedicoService;

    public CreateMedicoService(IEnderecoService enderecoService, IMedicoService MedicoService)
    {
        _enderecoService = enderecoService;
        _MedicoService = MedicoService;
    }

    public async Task<Guid?> CreateUser(CreateMedicoInput input)
    {
        input.Endereco.Id = input.Endereco.Id != Guid.Empty ? input.Endereco.Id : Guid.NewGuid();
        var enderecoCriado = await _enderecoService.Create(input.Endereco);
        if (enderecoCriado.HasValue)
        {
            input.Medico.EnderecoId = input.Endereco.Id;
            return await _MedicoService.Create(input.Medico);
        }

        return null;
    }
}
using Medicaly.Application.Enrecos;
using Medicaly.Application.Profissionals.Dtos;
using Medicaly.Application.Transients;

namespace Medicaly.Application.Profissionals;

public interface ICreateProfissionalService
{
    public Task<Guid?> CreateUser(CreateProfissionalInput input);
}

public class CreateProfissionalService: ICreateProfissionalService, IAutoTransient
{
    private readonly IEnderecoService _enderecoService;
    private readonly IProfissionalService _ProfissionalService;

    public CreateProfissionalService(IEnderecoService enderecoService, IProfissionalService ProfissionalService)
    {
        _enderecoService = enderecoService;
        _ProfissionalService = ProfissionalService;
    }

    public async Task<Guid?> CreateUser(CreateProfissionalInput input)
    {
        input.Endereco.Id = input.Endereco.Id != Guid.Empty ? input.Endereco.Id : Guid.NewGuid();
        var enderecoCriado = await _enderecoService.Create(input.Endereco);
        if (enderecoCriado.HasValue)
        {
            input.Profissional.EnderecoId = input.Endereco.Id;
            return await _ProfissionalService.Create(input.Profissional);
        }

        return null;
    }
}
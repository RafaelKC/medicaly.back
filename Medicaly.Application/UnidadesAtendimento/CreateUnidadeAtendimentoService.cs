using Medicaly.Application.Enrecos;
using Medicaly.Application.Transients;
using Medicaly.Application.UnidadesAtendimento.Dtos;

namespace Medicaly.Application.UnidadesAtendimento;

public interface ICreateUnidadeAtendimentoService
{
    public Task<Guid?> CreateUnidadeAtendimento(CreateUnidadeAtendimentoInput input);
}

public class CreateUnidadeAtendimentoService: ICreateUnidadeAtendimentoService, IAutoTransient
{
    private readonly IEnderecoService _enderecoService;
    private readonly IUnidadeAtendimentoService _unidadeAtendimentoService;

    public CreateUnidadeAtendimentoService(IEnderecoService enderecoService, IUnidadeAtendimentoService unidadeAtendimentoService)
    {
        _enderecoService = enderecoService;
        _unidadeAtendimentoService = unidadeAtendimentoService;
    }

    public async Task<Guid?> CreateUnidadeAtendimento(CreateUnidadeAtendimentoInput input)
    {
        input.Endereco.Id = input.Endereco.Id != Guid.Empty ? input.Endereco.Id : Guid.NewGuid();
        var enderecoCriado = await _enderecoService.Create(input.Endereco);
        if (enderecoCriado.HasValue)
        {
            input.UnidadeAtendimento.EnderecoId = input.Endereco.Id;
            return await _unidadeAtendimentoService.Create(input.UnidadeAtendimento);
        }

        return null;
    }
}


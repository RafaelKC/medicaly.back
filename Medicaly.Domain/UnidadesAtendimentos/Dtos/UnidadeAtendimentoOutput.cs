using Medicaly.Domain.Communs;
using Medicaly.Domain.Enderecos;
using Medicaly.Domain.UnidadesAtendimentos.Enums;

namespace Medicaly.Domain.UnidadesAtendimentos.Dtos;

public class UnidadeAtendimentoOutput: EntityDto
{
    public string Nome { get; set; }
    public TipoUnidade TipoUnidade { get; set; }
    public Guid EnderecoId { get; set; }
    public Endereco Endereco { get; set; }

    public UnidadeAtendimentoOutput(UnidadeAtendimento input)
    {
        Id = input.Id;
        Nome = input.Nome;
        TipoUnidade = input.TipoUnidade;
        Endereco = input.Endereco;
        EnderecoId = input.EnderecoId;
    }
}
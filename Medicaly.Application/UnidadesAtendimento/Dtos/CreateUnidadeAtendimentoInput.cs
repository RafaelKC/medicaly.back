using Medicaly.Domain.Enderecos;
using Medicaly.Domain.Enderecos.Dtos;
using Medicaly.Domain.UnidadeAtendimento.Dtos;

namespace Medicaly.Application.UnidadesAtendimento.Dtos;

public class CreateUnidadeAtendimentoInput
{

    public UnidadeAtendimentoInput UnidadeAtendimento { get; set; }
    
    public EnderecoInput Endereco { get; set; }
    
}
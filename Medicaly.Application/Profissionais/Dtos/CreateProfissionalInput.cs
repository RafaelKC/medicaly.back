using Medicaly.Domain.Enderecos.Dtos;
using Medicaly.Domain.Profissionais.Dtos;

namespace Medicaly.Application.Profissionais.Dtos;

public class CreateProfissionalInput
{
    public ProfissionalInput Profissional { get; set; }
    public EnderecoInput Endereco { get; set; }

    public String Password { get; set; }
}
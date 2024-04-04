using Medicaly.Domain.Enderecos.Dtos;
using Medicaly.Domain.Pacientes.Dtos;

namespace Medicaly.Application.Pacientes.Dtos;

public class CreateMedicoInput
{
    public PacienteInput Paciente { get; set; }
    public EnderecoInput Endereco { get; set; }
}
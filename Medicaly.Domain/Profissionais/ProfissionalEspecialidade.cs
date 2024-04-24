using Medicaly.Domain.Especialidades;

namespace Medicaly.Domain.Profissionais;

public class ProfissionalEspecialidade
{
    public Guid ProfissionalId { get; set; }
    public Guid IdEspecialidade { get; set; }

    public Especialidade Especialidade { get; set; }
    public Profissional Profissional { get; set; }
}
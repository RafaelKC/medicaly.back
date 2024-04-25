using Medicaly.Domain.Communs;
using Medicaly.Domain.Especialidades;

namespace Medicaly.Domain.Profissionais;

public class ProfissionalAtuacao: Entity
{
    public Guid IdProsissional { get; set; }
    public Guid IdEspecialidade { get; set; }

    public Especialidade Especialidade { get; set; }
    public Profissional Profissional { get; set; }
}
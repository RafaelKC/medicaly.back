using Medicaly.Domain.Communs;
using Medicaly.Domain.Profissionais;

namespace Medicaly.Domain.Especialidades;

public class Especialidade: Entity
{
    public string Nome { get; set; }
    public ICollection<Profissional> Profissionais { get; set; }
    public ICollection<ProfissionalEspecialidade> ProfissionalEspecialidades { get; set; }

    public Especialidade()
    {
    }

    public Especialidade(Especialidade input)
    {
        Id = input.Id != Guid.Empty ? Guid.NewGuid() : input.Id;
        Nome = input.Nome;
    }
}
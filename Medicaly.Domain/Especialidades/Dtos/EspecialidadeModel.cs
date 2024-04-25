using Medicaly.Domain.Communs;

namespace Medicaly.Domain.Especialidades.Dtos;

public class EspecialidadeModel: EntityDto
{
    public string Nome { get; set; }

    public EspecialidadeModel()
    {
    }

    public EspecialidadeModel(Especialidade input)
    {
        Id = input.Id;
        Nome = input.Nome;
    }
}
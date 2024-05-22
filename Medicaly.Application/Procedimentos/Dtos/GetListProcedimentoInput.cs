using Medicaly.Application.Communs;

namespace Medicaly.Application.Procedimentos.Dtos;

public class GetListProcedimentoInput: PagedFilteredInput
{
 public Guid? ProfissionalId { get; set; }
 public Guid? PacienteId { get; set; }
}
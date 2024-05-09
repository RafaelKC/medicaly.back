using Medicaly.Application.Communs;

namespace Medicaly.Application.Procedimentos.Dtos;

public class GetListProcedimentoInput: PagedFilteredInput
{
 public Guid? ProfissionalId;
}
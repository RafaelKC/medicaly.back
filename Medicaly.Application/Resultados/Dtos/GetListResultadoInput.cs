using Medicaly.Application.Communs;

namespace Medicaly.Application.Resultados.Dtos;

public class GetListResultadoInput : PagedFilteredInput
{
    public Guid? ProcedimentoId { get; set; }

}
using Medicaly.Domain.Anexos;
using Medicaly.Domain.Resultados;

namespace Medicaly.Domain.ResultadoAnexos.Dtos;

public class ResultadoAnexoInput
{
    public Guid ProcedimentoId { get; set; }
    public Guid AnexoId { get; set; }
}
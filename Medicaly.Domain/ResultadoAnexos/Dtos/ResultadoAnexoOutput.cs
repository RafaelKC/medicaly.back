using Medicaly.Domain.Anexos;
using Medicaly.Domain.Resultados;

namespace Medicaly.Domain.ResultadoAnexos.Dtos;

public class ResultadoAnexoOutput
{
    public Guid ProcedimentoId { get; set; }
    public Guid AnexoId { get; set; }
    
    public Resultado Resultado { get; set; }
    public Anexo Anexo { get; set; }

    public ResultadoAnexoOutput(ResultadoAnexo input)
    {
        Resultado = input.Resultado;
        Anexo = input.Anexo;
        AnexoId = input.AnexoId;
        ProcedimentoId = input.ProcedimentoId;
    }
}
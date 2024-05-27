using Medicaly.Domain.Anexos;
using Medicaly.Domain.ResultadoAnexos.Dtos;
using Medicaly.Domain.Resultados;
using Medicaly.Domain.Resultados.Dtos;

namespace Medicaly.Domain.ResultadoAnexos;


public class ResultadoAnexo
{
    public Guid ProcedimentoId { get; set; }
    public Guid AnexoId { get; set; }
    
    public Resultado Resultado { get; set; }
    public Anexo Anexo { get; set; }

    public ResultadoAnexo()
    {
        
    }

    public ResultadoAnexo(ResultadoAnexoInput input)
    {
        AnexoId = input.AnexoId;
        ProcedimentoId = input.ProcedimentoId;
    }
}
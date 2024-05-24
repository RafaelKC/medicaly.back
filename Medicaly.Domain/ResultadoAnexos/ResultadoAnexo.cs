using Medicaly.Domain.Agendamentos;
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
        Resultado = input.Resultado;
        Anexo = input.Anexo;
        AnexoId = input.AnexoId;
        ProcedimentoId = input.ProcedimentoId;
    }
}
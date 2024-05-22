using Medicaly.Domain.Resultados;

namespace Medicaly.Domain.Resultados.Dtos;

public class ResultadoOutput
{
    public Guid ProcedimentoId { get; set; }
    public String Observacoes { get; set; }
    
    public ResultadoOutput(Resultado input)
    {
        ProcedimentoId = input.ProcedimentoId;
        Observacoes = input.Observacoes;
    }
}


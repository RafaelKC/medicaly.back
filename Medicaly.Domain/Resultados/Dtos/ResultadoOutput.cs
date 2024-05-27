using Medicaly.Domain.Anexos;
using Medicaly.Domain.Anexos.Dtos;
using Medicaly.Domain.Resultados;

namespace Medicaly.Domain.Resultados.Dtos;

public class ResultadoOutput
{
    public ResultadoOutput(Resultado resultado)
    {
        ProcedimentoId = resultado.ProcedimentoId;
        Observacoes = resultado.Observacoes;
    }

    public Guid ProcedimentoId { get; set; }
    public String Observacoes { get; set; }
}


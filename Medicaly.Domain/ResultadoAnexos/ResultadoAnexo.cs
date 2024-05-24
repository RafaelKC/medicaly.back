using Medicaly.Domain.Agendamentos;
using Medicaly.Domain.Anexos;
using Medicaly.Domain.Resultados;

namespace Medicaly.Domain.ResultadoAnexos;


public class ResultadoAnexo
{
    public Guid ProcedimentoId { get; set; }
    public Guid AnexoId { get; set; }
    
    public Resultado Resultado { get; set; }
    public Anexo Anexo { get; set; }
    
}
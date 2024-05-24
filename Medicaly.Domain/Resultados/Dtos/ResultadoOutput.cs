using Medicaly.Domain.Anexos;
using Medicaly.Domain.Anexos.Dtos;
using Medicaly.Domain.Resultados;

namespace Medicaly.Domain.Resultados.Dtos;

public class ResultadoOutput
{
    public Guid ProcedimentoId { get; set; }
    public String Observacoes { get; set; }
    
    public AnexoOutput Anexo { get; set; }
    
    
}


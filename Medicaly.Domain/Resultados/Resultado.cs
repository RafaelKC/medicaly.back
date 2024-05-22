using System.ComponentModel.DataAnnotations;
using Medicaly.Domain.Agendamentos.Dtos;
using Medicaly.Domain.Communs;
using Medicaly.Domain.Resultados.Dtos;

namespace Medicaly.Domain.Resultados;

public class Resultado
{
    [Key]
    public Guid ProcedimentoId { get; set; }
    public String Observacoes { get; set; }
    
    
    public Resultado() 
    {
    
    }
    
    public Resultado(ResultadoInput input)
    {
        ProcedimentoId = input.ProcedimentoId;
        Observacoes = input.Observacoes;
    }
    
    public void Update(ResultadoInput input)
    {
        Observacoes = input.Observacoes;
    }
}




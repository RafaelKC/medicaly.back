using System.ComponentModel.DataAnnotations;
using Medicaly.Domain.Anexos;
using Medicaly.Domain.Communs;
using Medicaly.Domain.ResultadoAnexos;
using Medicaly.Domain.Resultados.Dtos;

namespace Medicaly.Domain.Resultados;

public class Resultado
{
    [Key]
    public Guid ProcedimentoId { get; set; }
    public String Observacoes { get; set; }
    
    public ICollection<ResultadoAnexo>? ResultadosAnexos { get; set; }
    public ICollection<Anexo>? Anexos { get; set; }

    
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




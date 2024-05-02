using System.ComponentModel.DataAnnotations;
using Medicaly.Domain.Communs;
using Medicaly.Domain.UnidadesAtendimentos.Enums;

namespace Medicaly.Domain.UnidadesAtendimentos.Dtos;

public class UnidadeAtendimentoInput: EntityDto
{
    [Required]
    [MinLength(10)]
    [MaxLength(50)]
    public string Nome { get; set; }

    [Required] public TipoUnidade TipoUnidade { get; set; }
    
    [Required] public Guid EnderecoId { get; set; }
}
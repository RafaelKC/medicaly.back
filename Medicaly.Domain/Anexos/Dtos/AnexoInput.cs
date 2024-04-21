using System.ComponentModel.DataAnnotations;
using Medicaly.Domain.Communs;

namespace Medicaly.Domain.Anexos.Dtos;

public class AnexoInput: EntityDto
{
    [Required]
    public string ArquivoNome { get; set; }
}
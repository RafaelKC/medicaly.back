using System.ComponentModel.DataAnnotations;
using Medicaly.Domain.Communs;

namespace Medicaly.Domain.Enderecos.Dtos;

public class EnderecoInput: EntityDto
{
    [MaxLength(8)]
    [MinLength(8)]
    public string Cep { get; set; }

    [Required]
    public string Logradouro { get; set; }

    [Required]
    public int Numero { get; set; }

    [Required]
    public string Bairro { get; set; }

    [Required]
    public string Cidade { get; set; }

    [Required]
    public string CodigoIbgeCidade { get; set; }

    [Required]
    public string Complemento { get; set; }

    [Required]
    public string Observacao { get; set; }
}
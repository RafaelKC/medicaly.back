using System.ComponentModel.DataAnnotations;
using Medicaly.Domain.Communs;
using Medicaly.Domain.Enderecos.Dtos;

namespace Medicaly.Domain.Enderecos;

public class Endereco: Entity
{
    [MaxLength(8)]
    [MinLength(8)]
    public string Cep { get; set; }

    [MaxLength(2)]
    [MinLength(2)]
    public string Estado { get; set; }

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

    public Endereco()
    {
    }

    public Endereco(EnderecoInput input)
    {
        Id = input.Id != Guid.Empty ? input.Id : Guid.NewGuid();
        Cep = input.Cep;
        Logradouro = input.Logradouro;
        Numero = input.Numero;
        Bairro = input.Bairro;
        Cidade = input.Cidade;
        CodigoIbgeCidade = input.CodigoIbgeCidade;
        Complemento = input.Complemento;
        Observacao = input.Observacao;
        Estado = input.Estado;
    }

    public override bool Equals(object? obj)
    {
        var endereco = obj as Endereco;

        var temValor = obj != null && endereco != null;
        if (!temValor) return false;

        return endereco?.Cep == Cep
               && endereco?.Logradouro == Logradouro
               && endereco?.Numero == Numero
               && endereco?.CodigoIbgeCidade == CodigoIbgeCidade
               && endereco?.Estado == Estado;
    }

    public void Update(EnderecoInput input)
    {
        Cep = input.Cep;
        Logradouro = input.Logradouro;
        Numero = input.Numero;
        Bairro = input.Bairro;
        Cidade = input.Cidade;
        CodigoIbgeCidade = input.CodigoIbgeCidade;
        Complemento = input.Complemento;
        Observacao = input.Observacao;
    }
}
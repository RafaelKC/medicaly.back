using System.ComponentModel.DataAnnotations;
using Medicaly.Domain.Communs;

namespace Medicaly.Domain.Enderecos.Dtos;

public class EnderecoOutput : EntityDto
{
    public string Cep { get; set; }
    public string Logradouro { get; set; }
    public int Numero { get; set; }
    public string Bairro { get; set; }
    public string Cidade { get; set; }
    public string CodigoIbgeCidade { get; set; }
    public string Complemento { get; set; }
    public string Observacao { get; set; }
    public string Estado { get; set; }

    public EnderecoOutput(Endereco input)
    {
        Id = input.Id;
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
}
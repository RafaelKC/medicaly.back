using System.ComponentModel.DataAnnotations;
using Medicaly.Domain.Communs;
using Medicaly.Domain.Profissionais.Dtos;
using Medicaly.Domain.Profissionais.Enums;
using Medicaly.Domain.Users;
using Medicaly.Domain.Users.Enums;

namespace Medicaly.Domain.Profissionais;

public class Profissional: Entity, IUser
{
    [Required]
    public string Nome { get; set; }

    [Required]
    public string Sobrenome { get; set; }

    [MaxLength(11)]
    [MinLength(11)]
    public string Cpf { get; set; }

    [EmailAddress]
    public string Email { get; set; }

    [MaxLength(11)]
    [MinLength(10)]
    public string Telefone { get; set; }

    [Required]
    public DateTime DataNascimento { get; set; }

    [Required]
    public Genero Genero { get; set; }

    public Guid? EnderecoId { get; set; }

    [Required]
    public string CredencialDeSaude { get; set; }

    public string Atuacoes { get; set; }

    public string Especialidades { get; set; }

    [Required]
    public TipoProfissional Tipo { get; set; }

    [Required]
    public TimeSpan InicioExpediente { get; set; }

    [Required]
    public TimeSpan FimExpedienteExpediente { get; set; }

    public Profissional()
    {
    }

    public Profissional(ProfissionalInput input)
    {
        Id = input.Id;
        Nome = input.Nome;
        Sobrenome = input.Sobrenome;
        Cpf = input.Cpf;
        Email = input.Email;
        Telefone = input.Telefone;
        DataNascimento = input.DataNascimento;
        EnderecoId = input.EnderecoId;
        Genero = input.Genero;
        CredencialDeSaude = input.CredencialDeSaude;
        Atuacoes = input.Atuacoes;
        Especialidades = input.Especialidades;
        Tipo = input.Tipo;
        InicioExpediente = input.InicioExpediente;
        FimExpedienteExpediente = input.FimExpedienteExpediente;
    }
    
    public void Update(ProfissionalInput input)
    {
        Nome = input.Nome;
        Sobrenome = input.Sobrenome;
        Email = input.Email;
        Telefone = input.Telefone;
        DataNascimento = input.DataNascimento;
        EnderecoId = input.EnderecoId;
        Genero = input.Genero;
        CredencialDeSaude = input.CredencialDeSaude;
        Atuacoes = input.Atuacoes;
        Especialidades = input.Especialidades;
        Tipo = input.Tipo;
        InicioExpediente = input.InicioExpediente;
        FimExpedienteExpediente = input.FimExpedienteExpediente;
    }
}
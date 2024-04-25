using System.ComponentModel.DataAnnotations;
using Medicaly.Domain.Communs;
using Medicaly.Domain.Especialidades;
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

    [Required]
    public TipoProfissional Tipo { get; set; }

    [Required]
    public TimeSpan InicioExpediente { get; set; }

    [Required]
    public TimeSpan FimExpediente { get; set; }

    public DayOfWeek[] DiasAtendidos { get; set; }

    public ICollection<Especialidade> Especialidades { get; set; }
    public ICollection<ProfissionalEspecialidade> ProfissionalEspecialidades { get; set; }

    public ICollection<Especialidade> Atuacoes { get; set; }
    public ICollection<ProfissionalAtuacao> ProfissionalAtuacoes { get; set; }

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
        Tipo = input.Tipo;
        InicioExpediente = TimeSpan.FromMilliseconds(input.InicioExpediente);
        FimExpediente = TimeSpan.FromMilliseconds(input.FimExpediente);
        DiasAtendidos = input.DiasAtendidos;
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
        Tipo = input.Tipo;
        InicioExpediente = TimeSpan.FromMilliseconds(input.InicioExpediente);
        DiasAtendidos = input.DiasAtendidos;
        FimExpediente = TimeSpan.FromMilliseconds(input.FimExpediente);
    }
}
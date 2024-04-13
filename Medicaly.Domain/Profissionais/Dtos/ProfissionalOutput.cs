using Medicaly.Domain.Communs;
using Medicaly.Domain.Pacientes;
using Medicaly.Domain.Profissionais.Enums;
using Medicaly.Domain.Users;
using Medicaly.Domain.Users.Enums;

namespace Medicaly.Domain.Profissionais.Dtos;

public class ProfissionalOutput: EntityDto, IUser
{
    public string Nome { get; set; }
    public string Sobrenome { get; set; }
    public string Cpf { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public DateTime DataNascimento { get; set; }
    public Guid? EnderecoId { get; set; }
    public Genero Genero { get; set; }
    public string CredencialDeSaude { get; set; }
    public List<string> Atuacoes { get; set; }
    public List<string> Especialidades { get; set; }
    public TipoProfissional Tipo { get; set; }
    public double InicioExpediente { get; set; }
    public double FimExpediente { get; set; }
    public DayOfWeek[] DiasAtendidos { get; set; }
    
    public ProfissionalOutput(Profissional input)
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
        Atuacoes = string.IsNullOrWhiteSpace(input.Atuacoes) ? new List<string>() : input.Atuacoes.Split(",").ToList();
        Especialidades = string.IsNullOrWhiteSpace(input.Especialidades) ? new List<string>() : input.Especialidades.Split(",").ToList();
        Tipo = input.Tipo;
        InicioExpediente = input.InicioExpediente.TotalMilliseconds;
        FimExpediente = input.FimExpediente.TotalMilliseconds;
        DiasAtendidos = input.DiasAtendidos;
    }
}
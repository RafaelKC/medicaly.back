using Medicaly.Domain.Communs;
using Medicaly.Domain.Pacientes;
using Medicaly.Domain.Profissionais.Enums;
using Medicaly.Domain.Users.Enums;

namespace Medicaly.Domain.Profissionais.Dtos;

public class ProfissionalOutput: EntityDto
{
    public string Nome { get; set; }
    public string Sobrenome { get; set; }
    public string Cpf { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public DateOnly DataNascimento { get; set; }
    public Guid? EnderecoId { get; set; }
    public Genero Genero { get; set; }
    public string CredencialDeSaude { get; set; }
    public string Atuacoes { get; set; }
    public string Especialidades { get; set; }
    public TipoProfissional Tipo { get; set; }
    public TimeSpan InicioExpediente { get; set; }
    public TimeSpan FimExpedienteExpediente { get; set; }
    
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
        Atuacoes = input.Atuacoes;
        Especialidades = input.Especialidades;
        Tipo = input.Tipo;
        InicioExpediente = input.InicioExpediente;
        FimExpedienteExpediente = input.FimExpedienteExpediente;
    }
}
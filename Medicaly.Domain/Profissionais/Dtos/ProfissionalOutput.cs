using Medicaly.Domain.Communs;
using Medicaly.Domain.Especialidades;
using Medicaly.Domain.Pacientes;
using Medicaly.Domain.Profissionais.Enums;
using Medicaly.Domain.UnidadesAtendimentos;
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
    public List<Especialidade> Atuacoes { get; set; }
    public TipoProfissional Tipo { get; set; }
    public double InicioExpediente { get; set; }
    public double FimExpediente { get; set; }
    public DayOfWeek[] DiasAtendidos { get; set; }
    public List<Especialidade> Especialidades { get; set; }
    public Guid UnidadeId { get; set; }
    public UnidadeAtendimento Unidade { get; set; }

    public ProfissionalOutput()
    {

    }

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
        Tipo = input.Tipo;
        InicioExpediente = input.InicioExpediente.TotalMilliseconds;
        FimExpediente = input.FimExpediente.TotalMilliseconds;
        DiasAtendidos = input.DiasAtendidos;
        UnidadeId = input.UnidadeId;
        Unidade = input.Unidade;
    }
}
using System.ComponentModel.DataAnnotations;
using Medicaly.Domain.Communs;
using Medicaly.Domain.Enderecos;
using Medicaly.Domain.Pacientes.Dtos;
using Medicaly.Domain.Users;
using Medicaly.Domain.Users.Enums;

namespace Medicaly.Domain.Pacientes;

public class Paciente: Entity, IUser
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

    public DateTime DataNascimento { get; set; }

    [Required]
    public Genero Genero { get; set; }

    public Guid? EnderecoId { get; set; }

    public Endereco Endereco { get; set; }

    public Paciente()
    {
    }

    public Paciente(PacienteInput input)
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
    }
    
    public void Update(PacienteInput input)
    {
        Nome = input.Nome;
        Sobrenome = input.Sobrenome;
        Email = input.Email;
        Telefone = input.Telefone;
        DataNascimento = input.DataNascimento;
        EnderecoId = input.EnderecoId;
        Genero = input.Genero;
    }
}
using System.ComponentModel.DataAnnotations;
using Medicaly.Domain.Administradores.Dtos;
using Medicaly.Domain.Communs;
using Medicaly.Domain.Users;
using Medicaly.Domain.Users.Enums;

namespace Medicaly.Domain.Administradores;

public class Administrador: Entity, IUser
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

    public DateOnly DataNascimento { get; set; }

    [Required]
    public Genero Genero { get; set; }

    public Guid? EnderecoId { get; set; }


    public Administrador()
    {
    }

    public Administrador(AdministradorInput input)
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

    public void Update(AdministradorInput input)
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
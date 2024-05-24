using System.ComponentModel.DataAnnotations;
using Medicaly.Domain.Communs;
using Medicaly.Domain.Users;
using Medicaly.Domain.Users.Enums;

namespace Medicaly.Domain.Pacientes.Dtos;

public class PacienteInput: EntityDto, IUser
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

    public Guid? EnderecoId { get; set; }

    public Genero Genero { get; set; }
}
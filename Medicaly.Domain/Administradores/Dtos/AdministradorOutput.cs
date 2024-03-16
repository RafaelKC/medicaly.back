using Medicaly.Domain.Communs;
using Medicaly.Domain.Users.Enums;

namespace Medicaly.Domain.Administradores.Dtos;

public class AdministradorOutput: EntityDto
{
    public string Nome { get; set; }
    public string Sobrenome { get; set; }
    public string Cpf { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public DateOnly DataNascimento { get; set; }
    public Guid? EnderecoId { get; set; }
    public Genero Genero { get; set; }

    public AdministradorOutput(Administrador input)
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
}
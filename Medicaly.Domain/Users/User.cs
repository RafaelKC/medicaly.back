using Medicaly.Domain.Users.Enums;

namespace Medicaly.Domain.Users;

public interface IUser
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Sobrenome { get; set; }
    public string Telefone { get; set; }
    public string Email { get; set; }
    public DateOnly DataNascimento { get; set; }
    public Genero Genero { get; set; }
}

public class User: IUser
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Sobrenome { get; set; }
    public string Telefone { get; set; }
    public string Email { get; set; }
    public DateOnly DataNascimento { get; set; }
    public Genero Genero { get; set; }
    public UserTipo Tipo { get; set; }

    public User(IUser userBase, UserTipo tipo)
    {
        Id = userBase.Id;
        Nome = userBase.Nome;
        Nome = userBase.Nome;
        Sobrenome = userBase.Sobrenome;
        Telefone = userBase.Telefone;
        Email = userBase.Email;
        DataNascimento = userBase.DataNascimento;
        Genero = userBase.Genero;
        Tipo = tipo;
    }

    public User()
    {
    }
}
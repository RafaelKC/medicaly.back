using Medicaly.Domain.Communs;
using Medicaly.Domain.Enderecos.Dtos;
using Medicaly.Domain.Users;

namespace Medicaly.Application.Authentications.Dtos;

public class CreateUserInput<T> where T: EntityDto, IUser
{
    public EnderecoInput Endereco { get; set; }
    public string Password { get; set; }
    public T User { get; set; }
}
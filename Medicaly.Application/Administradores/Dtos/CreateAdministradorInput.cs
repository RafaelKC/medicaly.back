using Medicaly.Domain.Administradores.Dtos;
using Medicaly.Domain.Enderecos.Dtos;

namespace Medicaly.Application.Administradores.Dtos;

public class CreateAdministradorInput
{
    public AdministradorInput Administrador { get; set; }
    public EnderecoInput Endereco { get; set; }

    public String Password { get; set; }
}
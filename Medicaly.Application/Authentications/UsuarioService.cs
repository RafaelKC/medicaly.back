using Medicaly.Application.Transients;
using Medicaly.Domain.Users;
using Medicaly.Infrastructure.Supabse;

namespace Medicaly.Application.Authentications;

public interface IUsuarioService
{
    public User? GetCurrentUser();
}

public class UsuarioService: IUsuarioService, IAutoTransient
{
    private readonly ISupabseClient _supabseClient;

    public UsuarioService(ISupabseClient supabseClient)
    {
        _supabseClient = supabseClient;
    }

    public  User? GetCurrentUser()
    {
        var currentUser = _supabseClient.Auth.CurrentUser;
        var userData = currentUser?.UserMetadata["user"];
        return  userData as User;
    }
}
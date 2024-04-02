using Medicaly.Application.Transients;
using Medicaly.Domain.Users;
using Medicaly.Infrastructure.Supabse;
using Newtonsoft.Json;

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
        var userData = currentUser?.UserMetadata["user"] as string;
        if (string.IsNullOrWhiteSpace(userData)) return null;
        return  (User)JsonConvert.DeserializeObject(userData, typeof(User));
    }
}
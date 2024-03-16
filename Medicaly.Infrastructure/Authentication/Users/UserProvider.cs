using Medicaly.Domain.Users;
using Medicaly.Infrastructure.Authentication.Dots;
using Medicaly.Infrastructure.Supabse;

namespace Medicaly.Infrastructure.Authentication.Users;

public interface IUserProvider
{
    public User? GetCurrentUser();
}

public class UserProvider: IUserProvider
{
    private readonly ISupabseClient _supabseClient;

    public UserProvider(ISupabseClient supabseClient)
    {
        _supabseClient = supabseClient;
    }

    public User? GetCurrentUser()
    {
        return _supabseClient.Auth.CurrentUser?.UserMetadata["user"] as User;
    }
}
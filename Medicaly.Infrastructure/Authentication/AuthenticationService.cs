using Medicaly.Infrastructure.Authentication.Dots;
using Medicaly.Infrastructure.Supabse;
using Supabase.Gotrue;
using Supabase.Gotrue.Exceptions;
using User = Medicaly.Domain.Users.User;

namespace Medicaly.Infrastructure.Authentication;

public interface IAuthenticationService
{
    Task<Guid?> RegisterAsync(string email, string password, User input);
    Task<LoginOutput> Login(LoginInput input);
    Task Logout();
}

public class AuthenticationService: IAuthenticationService
{
    private readonly ISupabseClient _supabseClient;

    public AuthenticationService(ISupabseClient supabseClient)
    {
        _supabseClient = supabseClient;
    }


    public async Task<Guid?> RegisterAsync(string email, string password, User input)
    {
        try
        {
            var session = await _supabseClient.Auth.SignUp(email, password, new SignUpOptions
            {
                Data = new Dictionary<string, object>
                {
                    { "user", input },
                }
            });
            
            return session?.User is not null ? Guid.Parse(session.User.Id) : null;
        }
        catch (GotrueException e)
        {
            return null;
        }
    }

    public async Task<LoginOutput> Login(LoginInput input)
    {
        try
        {
            var session = await _supabseClient.Auth.SignIn(input.email, input.password);
            
            return new LoginOutput
            {
                Success = session.User is not null,
                Token = session.AccessToken,
                RefreshToken = session.RefreshToken
            };
        }
        catch (GotrueException e)
        {
            return new LoginOutput
            {
                Success = false
            };
        }
    }

    public async Task Logout()
    {
        var token = _supabseClient.Auth.CurrentSession?.AccessToken;
        var refresh = _supabseClient.Auth.CurrentSession?.RefreshToken;
        await _supabseClient.Auth.SignOut();
    }
}
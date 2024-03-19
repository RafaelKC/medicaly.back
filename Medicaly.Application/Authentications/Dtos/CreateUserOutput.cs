namespace Medicaly.Application.Authentications.Dtos;

public class CreateUserOutput
{
    public bool Success { get; set; }
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}
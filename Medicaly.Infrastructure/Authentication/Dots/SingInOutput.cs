namespace Medicaly.Infrastructure.Authentication.Dots;

public class SingInOutput
{
    public bool Success { get; set; }
    public Guid? UserId { get; set; }
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace Medicaly.Infrastructure.Authentication.Dots;

public class LoginInput
{
    [Required(AllowEmptyStrings = false)]
    public string email { get; set; }
    
    [Required(AllowEmptyStrings = false)]
    public string password { get; set; }
}
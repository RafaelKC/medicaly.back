using System.Text;
using Medicaly.Infrastructure.Authentication;
using Medicaly.Infrastructure.Consts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Medicaly.Infrastructure.Extensions;

public static class AddAuthenticationExtension
{
    public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddScoped<IAuthenticationService, AuthenticationService>()
            .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.Authority = configuration[AppConfig.AuthenticationAuthority];
                options.Audience = configuration[AppConfig.AuthenticationAudience];
                
                options.IncludeErrorDetails = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration[AppConfig.AuthenticationIssuer],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration[AppConfig.AuthenticationKey])
                    ),
                    
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidAudience = configuration[AppConfig.AuthenticationAudience],
                };
            });

        return services;
    }
}
namespace Medicaly.Infrastructure.Extensions;

public static class AddInfrastructureExtension
{
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
                services
                        .AddDatabase(configuration)
                        .AddSupabase(configuration)
                        .AddAuthentication(configuration);

                return services;
        }
}
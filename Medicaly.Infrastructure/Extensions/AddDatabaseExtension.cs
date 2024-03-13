using Medicaly.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Medicaly.Infrastructure.Extensions;

public static class AddDatabaseExtension
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddDbContext<MedicalyDbContext>(op =>
            {
                op.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            });
        
        return services;
    }
}
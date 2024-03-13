using Medicaly.Infrastructure.Consts;
using Medicaly.Infrastructure.Supabse;
using Supabase;

namespace Medicaly.Infrastructure.Extensions;


public static class AddSupabaseExtension
{
    public static IServiceCollection AddSupabase(this IServiceCollection services, IConfiguration configuration)
    {
        var supabaseUrl = configuration[AppConfig.SupabaseUrlKey];
        var supabaseKey = configuration[AppConfig.SupabaseKey];

        if (string.IsNullOrWhiteSpace(supabaseUrl) || string.IsNullOrWhiteSpace(supabaseKey))
        {
            throw new Exception("Supabse not set");
        }
                
        var options = new SupabaseOptions
        {
            AutoRefreshToken = true,
            AutoConnectRealtime = true,
        };

        var supabase = new SupabseClient(supabaseUrl, supabaseKey, options);

        services.AddSingleton<ISupabseClient>(supabase);
        
        return services;
    }
}
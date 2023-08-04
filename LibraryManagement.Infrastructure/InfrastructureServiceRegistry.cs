using LibraryManagement.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryManagement.Infrastructure;


/// <summary>
/// Class that registers all interfaces and implementations to DI
/// </summary>
public static class InfrastructureServiceRegistry
{
    /// <summary>
    /// Adds the infrastructure services.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configuration">The configuration.</param>
    public static void AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        AddPostgres(services, configuration);
        services.AddScoped<ILibraryDbContext>(provider => provider.GetService<LibraryDbContext>()!);
    }

    /// <summary>
    /// Adds configuration for using Postgress Server  
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    internal static void AddPostgres(IServiceCollection services, IConfiguration configuration)
    {
        services.AddEntityFrameworkNpgsql().AddDbContext<LibraryDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DbConnection")));
    }
}
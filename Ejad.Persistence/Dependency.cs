using Ejad.Domain.Repositories;
using Ejad.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ejad.Persistence;

public static class Dependency
{
    public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddContext(configuration);
        services.AddServices();
    }

    private static void AddContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default");

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
            options.UseStronglyTypeConverters();
        });
    }

    private static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IApplicantRepository, ApplicantRepository>();
    }
}
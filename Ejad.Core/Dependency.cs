using Ejad.Core.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Ejad.Core.Behaviours;

namespace Ejad.Core;

public static class Dependency
{
    public static void AddCore(this IServiceCollection services)
    {
        services.AddGenerics();
        services.AddServices();
    }

    private static void AddGenerics(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            configuration.AddOpenRequestPreProcessor(typeof(ValidationProcessor<>));
        });
    }

    private static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IFileService, FileService>();
    }
}
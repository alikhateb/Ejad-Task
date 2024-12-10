using Ejad.Ui.Services;
using FluentValidation;
using Microsoft.FluentUI.AspNetCore.Components;
using System.Reflection;

namespace Ejad.Ui;

public static class Dependency
{
    public static void AddDependency(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient(configuration);
        services.AddServices();
        services.AddGenerics();
    }

    private static void AddHttpClient(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(_ => new HttpClient { BaseAddress = new Uri("https://localhost:7292") });
    }

    private static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IHttpService, HttpService>();
        services.AddScoped<IApplicantsServices, ApplicantsServices>();
    }

    private static void AddGenerics(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddSingleton(new LibraryConfiguration
        {
            CollocatedJavaScriptQueryString = null,
        });
    }
}
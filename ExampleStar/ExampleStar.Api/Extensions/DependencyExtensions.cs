using ExampleStar.Core;
using ExampleStar.DataAccess;
using ExampleStar.Infrastructure.Interfaces.Managers;
using ExampleStar.Infrastructure.Interfaces.Repositories;

namespace ExampleStar.Api.Extensions;

internal static class DependencyExtensions
{
    internal static IServiceCollection AddVeloxDependencies(this IServiceCollection services)
    {
        // Managers
        services.AddScoped<IPatientManager, PatientManager>();
        
        // Repositories
        services.AddScoped<IPatientRepository, PatientRepositoryAllocator>()
            .AddLazyKeyedScoped<IPatientRepository, PatientRepositorySql>("NewPatientRepository")
            .AddLazyKeyedScoped<IPatientRepository, PatientRepositoryStoredProcedures>("OldPatientRepository");
        
        return services;
    }

    private static IServiceCollection AddLazyKeyedScoped<TService, TImplementation>(this IServiceCollection services, string key)
        where TService : class
        where TImplementation : class, TService
    {
        string baseKey = $"base{key}";
        services.AddKeyedScoped<TService, TImplementation>(baseKey);
        services.AddKeyedScoped<Lazy<TService>>(
            key,
            (provider, _) => new Lazy<TService>(() => provider.GetRequiredKeyedService<TService>(baseKey)));

        return services;
    }
}
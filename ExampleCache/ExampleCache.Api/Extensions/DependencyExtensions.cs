using ExampleCache.Core;
using ExampleCache.DataAccess;
using ExampleCache.Infrastructure.Interfaces.Managers;
using ExampleCache.Infrastructure.Interfaces.Repositories;

namespace ExampleCache.Api.Extensions;

internal static class DependencyExtensions
{
    internal static IServiceCollection AddVeloxDependencies(this IServiceCollection services)
    {
        // Managers
        services.AddScoped<IPatientManager, PatientManager>();

        // Repositories
        services.AddScoped<IPatientRepository, PatientRepositoryCache>();
        services.AddKeyedScoped<IPatientRepository, PatientRepository>("BasePatientRepository");

        return services;
    }
}
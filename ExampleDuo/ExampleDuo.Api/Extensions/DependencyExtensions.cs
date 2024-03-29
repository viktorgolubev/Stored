using ExampleDuo.Core;
using ExampleDuo.DataAccess;
using ExampleDuo.Infrastructure.Interfaces.Managers;
using ExampleDuo.Infrastructure.Interfaces.Repositories;

namespace ExampleDuo.Api.Extensions;

internal static class DependencyExtensions
{
    internal static IServiceCollection AddVeloxDependencies(this IServiceCollection services)
    {
        // Managers
        services.AddScoped<IPatientManager, PatientManager>();
        
        // Repositories
        services.AddScoped<IPatientRepository, PatientRepositoryAllocator>();
        services.AddKeyedScoped<IPatientRepository, PatientRepositorySql>("NewPatientRepository");
        services.AddKeyedScoped<IPatientRepository, PatientRepositoryStoredProcedures>("OldPatientRepository");
        
        return services;
    }
}
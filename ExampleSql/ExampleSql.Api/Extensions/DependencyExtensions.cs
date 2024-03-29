using ExampleSql.Core;
using ExampleSql.DataAccess;
using ExampleSql.Infrastructure.Interfaces.Managers;
using ExampleSql.Infrastructure.Interfaces.Repositories;

namespace ExampleSql.Api.Extensions;

internal static class DependencyExtensions
{
    internal static IServiceCollection AddVeloxDependencies(this IServiceCollection services)
    {
        // Managers
        services.AddScoped<IPatientManager, PatientManager>();

        // Repositories
        services.AddScoped<IPatientRepository, PatientRepository>();

        return services;
    }
}
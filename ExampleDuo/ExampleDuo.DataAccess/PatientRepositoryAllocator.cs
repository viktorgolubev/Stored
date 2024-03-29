using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ExampleDuo.Infrastructure.Interfaces.Repositories;
using ExampleDuo.Infrastructure.Models.Entities;
using ExampleDuo.Infrastructure.Models.Options;

namespace ExampleDuo.DataAccess;

public class PatientRepositoryAllocator(
    IOptions<CommonOptions> commonOptions,
    [FromKeyedServices("NewPatientRepository")] IPatientRepository patientRepositorySql,
    [FromKeyedServices("OldPatientRepository")] IPatientRepository patientRepositoryStoredProcedures)
    : IPatientRepository
{
    private readonly CommonOptions _commonOptions = commonOptions.Value;
    
    public async Task<PatientEntity?> GetPatientByIdAsync(int id, CancellationToken token)
    {
        return _commonOptions.UseSql
            ? await patientRepositorySql.GetPatientByIdAsync(id, token)
            : await patientRepositoryStoredProcedures.GetPatientByIdAsync(id, token);
    }

    public async Task<List<PatientEntity>> SearchPatientsAsync(string? firstName, string lastName, CancellationToken token)
    {
        return _commonOptions.UseSql
            ? await patientRepositorySql.SearchPatientsAsync(firstName, lastName, token)
            : await patientRepositoryStoredProcedures.SearchPatientsAsync(firstName, lastName, token);
    }
}
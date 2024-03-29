using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ExampleStar.Infrastructure.Interfaces.Repositories;
using ExampleStar.Infrastructure.Models.Entities;
using ExampleStar.Infrastructure.Models.Options;

namespace ExampleStar.DataAccess;

public class PatientRepositoryAllocator(
    IOptions<CommonOptions> commonOptions,
    [FromKeyedServices("NewPatientRepository")] Lazy<IPatientRepository> patientRepositorySql,
    [FromKeyedServices("OldPatientRepository")] Lazy<IPatientRepository> patientRepositoryStoredProcedures)
    : IPatientRepository
{
    private readonly CommonOptions _commonOptions = commonOptions.Value;
    
    public async Task<PatientEntity?> GetPatientByIdAsync(int id, CancellationToken token)
    {
        return _commonOptions.UseSql
            ? await patientRepositorySql.Value.GetPatientByIdAsync(id, token)
            : await patientRepositoryStoredProcedures.Value.GetPatientByIdAsync(id, token);
    }

    public async Task<List<PatientEntity>> SearchPatientsAsync(string? firstName, string lastName, CancellationToken token)
    {
        return _commonOptions.UseSql
            ? await patientRepositorySql.Value.SearchPatientsAsync(firstName, lastName, token)
            : await patientRepositoryStoredProcedures.Value.SearchPatientsAsync(firstName, lastName, token);
    }
}
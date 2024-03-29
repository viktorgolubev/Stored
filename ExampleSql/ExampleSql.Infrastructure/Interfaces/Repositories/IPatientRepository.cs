using ExampleSql.Infrastructure.Models.Entities;

namespace ExampleSql.Infrastructure.Interfaces.Repositories;

public interface IPatientRepository
{
    Task<PatientEntity?> GetPatientByIdAsync(int id, CancellationToken token);

    Task<List<PatientEntity>> SearchPatientsAsync(string? firstName, string lastName, CancellationToken token);
}
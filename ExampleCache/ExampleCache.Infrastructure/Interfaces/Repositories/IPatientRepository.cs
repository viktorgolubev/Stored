using ExampleCache.Infrastructure.Models.Entities;

namespace ExampleCache.Infrastructure.Interfaces.Repositories;

public interface IPatientRepository
{
    Task<PatientEntity?> GetPatientByIdAsync(int id, CancellationToken token);

    Task<List<PatientEntity>> SearchPatientsAsync(string? firstName, string lastName, CancellationToken token);
}
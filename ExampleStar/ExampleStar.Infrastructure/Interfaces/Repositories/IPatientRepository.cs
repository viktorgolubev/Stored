using ExampleStar.Infrastructure.Models.Entities;

namespace ExampleStar.Infrastructure.Interfaces.Repositories;

public interface IPatientRepository
{
    Task<PatientEntity?> GetPatientByIdAsync(int id, CancellationToken token);

    Task<List<PatientEntity>> SearchPatientsAsync(string? firstName, string lastName, CancellationToken token);
}
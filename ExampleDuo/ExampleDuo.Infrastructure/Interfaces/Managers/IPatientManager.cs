using ExampleDuo.Infrastructure.Models.Domains;

namespace ExampleDuo.Infrastructure.Interfaces.Managers;

public interface IPatientManager
{
    Task<Patient> GetPatientByIdAsync(int id, CancellationToken token);

    Task<List<Patient>> SearchPatientsAsync(string? firstName, string lastName, CancellationToken token);
}
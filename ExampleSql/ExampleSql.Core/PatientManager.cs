using ExampleSql.Core.Extensions;
using ExampleSql.Infrastructure.Interfaces.Managers;
using ExampleSql.Infrastructure.Interfaces.Repositories;
using ExampleSql.Infrastructure.Models.Domains;
using ExampleSql.Infrastructure.Models.Entities;
using ExampleSql.Infrastructure.Models.Exceptions;

namespace ExampleSql.Core;

public class PatientManager(
    IPatientRepository patientRepository)
    : IPatientManager
{
    public async Task<Patient> GetPatientByIdAsync(int id, CancellationToken token)
    {
        id.ThrowIfZeroOrLess(nameof(Patient));

        PatientEntity patientEntity = await patientRepository.GetPatientByIdAsync(id, token)
                                      ?? throw new NotFoundException($"Patient not found. Patient id: {id}");

        return patientEntity.ToPatient();
    }

    public async Task<List<Patient>> SearchPatientsAsync(string? firstName, string lastName, CancellationToken token)
    {
        if (string.IsNullOrWhiteSpace(lastName)) return [];

        List<PatientEntity> patientEntities = await patientRepository.SearchPatientsAsync(firstName, lastName, token);
        return patientEntities
            .Select(e => e.ToPatient())
            .ToList();
    }
}
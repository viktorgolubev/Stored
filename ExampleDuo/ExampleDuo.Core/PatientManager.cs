using ExampleDuo.Core.Extensions;
using ExampleDuo.Infrastructure.Interfaces.Managers;
using ExampleDuo.Infrastructure.Interfaces.Repositories;
using ExampleDuo.Infrastructure.Models.Domains;
using ExampleDuo.Infrastructure.Models.Entities;
using ExampleDuo.Infrastructure.Models.Exceptions;

namespace ExampleDuo.Core;

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
        if (string.IsNullOrWhiteSpace(lastName))
        {
            return [];
        }

        List<PatientEntity> patientEntities = await patientRepository.SearchPatientsAsync(firstName, lastName, token);
        return patientEntities
            .Select(e => e.ToPatient())
            .ToList();
    }
}
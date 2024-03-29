using ExampleStar.Core.Extensions;
using ExampleStar.Infrastructure.Interfaces.Managers;
using ExampleStar.Infrastructure.Interfaces.Repositories;
using ExampleStar.Infrastructure.Models.Domains;
using ExampleStar.Infrastructure.Models.Entities;
using ExampleStar.Infrastructure.Models.Exceptions;

namespace ExampleStar.Core;

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
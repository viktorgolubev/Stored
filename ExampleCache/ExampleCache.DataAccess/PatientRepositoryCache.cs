using ExampleCache.Infrastructure.Interfaces.Repositories;
using ExampleCache.Infrastructure.Models.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace ExampleCache.DataAccess;

public class PatientRepositoryCache(
    [FromKeyedServices("BasePatientRepository")]IPatientRepository basePatientRepository)
    : IPatientRepository
{
    private static readonly Dictionary<int, PatientEntity> Cache = [];
    
    public async Task<PatientEntity?> GetPatientByIdAsync(int id, CancellationToken token)
    {
        if (Cache.TryGetValue(id, out var patient))
        {
            return patient;
        }

        PatientEntity? patientEntity = await basePatientRepository.GetPatientByIdAsync(id, token);
        if (patientEntity == null)
        {
            return null;
        }

        Cache[id] = patientEntity;
        return patientEntity;
    }

    public Task<List<PatientEntity>> SearchPatientsAsync(string? firstName, string lastName, CancellationToken token)
    {
        return basePatientRepository.SearchPatientsAsync(firstName, lastName, token);
    }
    
    /*
     * public async Task UpdatePatient(int id, string firstName)
     * {
     *      await basePatientRepository.UpdatePatient(id, firstName);
     *      Cache.Remove(id);
     * }
     */
}
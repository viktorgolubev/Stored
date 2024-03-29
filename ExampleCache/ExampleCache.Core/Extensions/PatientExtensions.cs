using ExampleCache.Infrastructure.Models.Domains;
using ExampleCache.Infrastructure.Models.Entities;

namespace ExampleCache.Core.Extensions;

internal static class PatientExtensions
{
    internal static Patient ToPatient(this PatientEntity patientEntity)
    {
        return new Patient
        {
            Id = patientEntity.Id,
            FirstName = patientEntity.FirstName,
            LastName = patientEntity.LastName,
            Gender = patientEntity.Gender,
            DateOfBirth = patientEntity.DateOfBirth,
            ZipCode = patientEntity.ZipCode,
            City = patientEntity.City,
            State = patientEntity.State,
            Insurances = patientEntity.Hins
                ?.Select(h => h.ToHin())
                .ToList() ?? []
        };
    }
}
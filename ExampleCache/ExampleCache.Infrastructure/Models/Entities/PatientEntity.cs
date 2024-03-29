namespace ExampleCache.Infrastructure.Models.Entities;

public class PatientEntity
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string LastName { get; set; } = null!;

    public char Gender { get; set; }

    public DateTime DateOfBirth { get; set; }

    public string ZipCode { get; set; } = null!;

    public string State { get; set; } = null!;

    public string City { get; set; } = null!;

    public List<HinEntity> Hins { get; set; }
}
namespace ExampleStar.Infrastructure.Models.Domains;

public class Patient
{
    public int Id { get; set; }
    
    public string? FirstName { get; set; }

    public string LastName { get; set; } = null!;

    public char Gender { get; set; }

    public DateTime DateOfBirth { get; set; }

    public string ZipCode { get; set; } = null!;

    public string State { get; set; } = null!;

    public string City { get; set; } = null!;

    public List<Hin> Insurances { get; set; } = null!;
}
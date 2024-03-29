using System.ComponentModel.DataAnnotations;

namespace ExampleStar.Infrastructure.Models.Options;

public class CommonOptions
{
    [Required]
    public bool UseSql { get; set; }
}
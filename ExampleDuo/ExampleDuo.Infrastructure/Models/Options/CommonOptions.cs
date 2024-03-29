using System.ComponentModel.DataAnnotations;

namespace ExampleDuo.Infrastructure.Models.Options;

public class CommonOptions
{
    [Required]
    public bool UseSql { get; set; }
}
using ExampleStar.Infrastructure.Models.Domains;
using ExampleStar.Infrastructure.Models.Entities;

namespace ExampleStar.Core.Extensions;

internal static class HinExtensions
{
    internal static Hin ToHin(this HinEntity hinEntity)
    {
        return new Hin
        {
            Number = hinEntity.Number,
            Code = hinEntity.Code
        };
    }
}
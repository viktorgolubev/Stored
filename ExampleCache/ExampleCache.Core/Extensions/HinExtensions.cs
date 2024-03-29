using ExampleCache.Infrastructure.Models.Domains;
using ExampleCache.Infrastructure.Models.Entities;

namespace ExampleCache.Core.Extensions;

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
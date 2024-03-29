using ExampleDuo.Infrastructure.Models.Domains;
using ExampleDuo.Infrastructure.Models.Entities;

namespace ExampleDuo.Core.Extensions;

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
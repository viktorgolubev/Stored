using ExampleSql.Infrastructure.Models.Domains;
using ExampleSql.Infrastructure.Models.Entities;

namespace ExampleSql.Core.Extensions;

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
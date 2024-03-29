namespace ExampleCache.DataAccess.Extensions;

internal static class StringExtensions
{
    internal static string? ToNormalize(this string? input)
    {
        return input?.Trim().ToLower();
    }
}
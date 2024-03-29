namespace ExampleStar.DataAccess.Extensions;

internal static class StringExtensions
{
    internal static string? ToNormalize(this string? source)
    {
        return source?.Trim().ToLower();
    }
}
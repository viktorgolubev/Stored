using ExampleStar.Infrastructure.Models.Options;

namespace ExampleStar.Api.Extensions;

internal static class OptionsExtensions
{
    internal static IServiceCollection AddVeloxOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<CommonOptions>()
            .Bind(configuration.GetSection(nameof(CommonOptions)))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        return services;
    }
}
using ExampleDuo.Infrastructure.Models.Options;

namespace ExampleDuo.Api.Extensions;

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
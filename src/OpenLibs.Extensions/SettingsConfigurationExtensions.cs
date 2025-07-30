using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using OpenLibs.Extensions.Builders;

namespace OpenLibs.Extensions;

/// <summary>
/// Provides extension methods for configuring strongly-typed settings objects with various validation options.
/// </summary>
public static class SettingsConfigurationExtensions
{
    /// <summary>
    /// Creates a fluent configuration builder for settings of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of the settings class to configure. Must be a reference type.</typeparam>
    /// <param name="services">The service collection to add the configuration to.</param>
    /// <param name="configuration">The configuration instance to bind settings from.</param>
    /// <param name="sectionName">The name of the configuration section to bind to the settings class.</param>
    /// <returns>A <see cref="SettingsConfigurationBuilder{T}"/> instance for fluent configuration.</returns>
    /// <example>
    /// <code>
    /// services.ConfigureSettings&lt;MySettings&gt;(configuration, "MySection")
    ///     .WithDataAnnotationValidation()
    ///     .WithEagerValidation()
    ///     .Build();
    /// </code>
    /// </example>
    public static SettingsConfigurationBuilder<T> ConfigureSettings<T>(
         this IServiceCollection services,
         IConfiguration configuration,
         string sectionName
     ) where T : class
    {
        return new SettingsConfigurationBuilder<T>(services, configuration, sectionName);
    }

    /// <summary>
    /// Configures and builds a settings instance with data annotation validation and eager validation enabled.
    /// This is a convenience method that applies common validation patterns.
    /// </summary>
    /// <typeparam name="T">The type of the settings class to configure. Must be a reference type.</typeparam>
    /// <param name="services">The service collection to add the configuration to.</param>
    /// <param name="configuration">The configuration instance to bind settings from.</param>
    /// <param name="sectionName">The name of the configuration section to bind to the settings class.</param>
    /// <returns>A validated instance of type <typeparamref name="T"/> populated from the configuration section.</returns>
    /// <exception cref="OptionsValidationException">Thrown when validation fails during eager validation.</exception>
    /// <example>
    /// <code>
    /// var mySettings = services.ConfigureRequiredSettings&lt;MySettings&gt;(configuration, "MySection");
    /// </code>
    /// </example>
    public static T ConfigureRequiredSettings<T>(
        this IServiceCollection services,
        IConfiguration configuration,
        string sectionName
    ) where T : class
    {
        return services.ConfigureSettings<T>(configuration, sectionName)
            .WithDataAnnotationValidation()
            .WithEagerValidation()
            .Build();
    }

    /// <summary>
    /// Configures and registers a settings class in the service collection with data annotation validation and eager validation.
    /// The settings will be available for dependency injection as <see cref="IOptions{TOptions}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the settings class to configure. Must be a reference type.</typeparam>
    /// <param name="services">The service collection to register the settings with.</param>
    /// <param name="configuration">The configuration instance to bind settings from.</param>
    /// <param name="sectionName">The name of the configuration section to bind to the settings class.</param>
    /// <returns>The same <see cref="IServiceCollection"/> instance for method chaining.</returns>
    /// <exception cref="OptionsValidationException">Thrown when validation fails during eager validation.</exception>
    /// <example>
    /// <code>
    /// services.RegisterSettings&lt;MySettings&gt;(configuration, "MySection");
    /// 
    /// // Later in your application, inject the settings:
    /// public MyService(IOptions&lt;MySettings&gt; settings) { }
    /// </code>
    /// </example>
    public static IServiceCollection RegisterSettings<T>(
        this IServiceCollection services,
        IConfiguration configuration,
        string sectionName
    ) where T : class
    {
        return services.ConfigureSettings<T>(configuration, sectionName)
            .WithDataAnnotationValidation()
            .WithEagerValidation()
            .Register();
    }
}

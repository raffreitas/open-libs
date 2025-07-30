using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace OpenLibs.Extensions.Builders;

/// <summary>
/// Provides a fluent API for configuring settings objects with various validation and binding options.
/// </summary>
/// <typeparam name="T">The type of the settings class to configure. Must be a reference type.</typeparam>
/// <param name="services">The service collection to add the configuration to.</param>
/// <param name="configuration">The configuration instance to bind settings from.</param>
/// <param name="sectionName">The name of the configuration section to bind to the settings class.</param>
public sealed class SettingsConfigurationBuilder<T>(
    IServiceCollection services,
    IConfiguration configuration,
    string sectionName
) where T : class
{
    private readonly OptionsBuilder<T> _optionsBuilder = services.AddOptions<T>().BindConfiguration(sectionName);

    /// <summary>
    /// Enables Data Annotation validation for the settings object.
    /// This will validate properties decorated with validation attributes like [Required], [Range], etc.
    /// </summary>
    /// <returns>The current <see cref="SettingsConfigurationBuilder{T}"/> instance for method chaining.</returns>
    /// <example>
    /// <code>
    /// services.AddSettings&lt;MySettings&gt;(configuration, "MySection")
    ///     .WithDataAnnotationValidation();
    /// </code>
    /// </example>
    public SettingsConfigurationBuilder<T> WithDataAnnotationValidation()
    {
        _optionsBuilder.ValidateDataAnnotations();
        return this;
    }

    /// <summary>
    /// Enables eager validation that occurs at application startup rather than on first access.
    /// This ensures configuration errors are caught early during application initialization.
    /// </summary>
    /// <returns>The current <see cref="SettingsConfigurationBuilder{T}"/> instance for method chaining.</returns>
    /// <example>
    /// <code>
    /// services.AddSettings&lt;MySettings&gt;(configuration, "MySection")
    ///     .WithDataAnnotationValidation()
    ///     .WithEagerValidation();
    /// </code>
    /// </example>
    public SettingsConfigurationBuilder<T> WithEagerValidation()
    {
        _optionsBuilder.ValidateOnStart();
        return this;
    }

    /// <summary>
    /// Adds custom validation logic for the settings object.
    /// </summary>
    /// <param name="validation">A function that validates the settings object and returns true if valid.</param>
    /// <param name="failureMessage">The error message to display when validation fails. If null, a default message is used.</param>
    /// <returns>The current <see cref="SettingsConfigurationBuilder{T}"/> instance for method chaining.</returns>
    /// <example>
    /// <code>
    /// services.AddSettings&lt;MySettings&gt;(configuration, "MySection")
    ///     .WithCustomValidation(settings =&gt; settings.Port &gt; 0 &amp;&amp; settings.Port &lt; 65536, 
    ///                          "Port must be between 1 and 65535");
    /// </code>
    /// </example>
    public SettingsConfigurationBuilder<T> WithCustomValidation(Func<T, bool> validation, string? failureMessage = null)
    {
        _optionsBuilder.Validate(validation, failureMessage ?? $"Custom validation failed for {typeof(T).Name}");
        return this;
    }

    /// <summary>
    /// Builds and returns an instance of the configured settings object.
    /// This method retrieves the settings from the configuration section and applies any validation rules.
    /// </summary>
    /// <returns>An instance of type <typeparamref name="T"/> populated with values from the configuration section.</returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the configuration section is missing or cannot be bound to the specified type.
    /// </exception>
    /// <example>
    /// <code>
    /// var settings = services.AddSettings&lt;MySettings&gt;(configuration, "MySection")
    ///     .WithDataAnnotationValidation()
    ///     .Build();
    /// </code>
    /// </example>
    public T Build()
    {
        return configuration.GetSection(sectionName).Get<T>()
            ?? throw new InvalidOperationException($"Configuration section '{sectionName}' for type '{typeof(T).Name}' is missing or invalid.");
    }

    /// <summary>
    /// Registers the configured settings in the dependency injection container and returns the service collection.
    /// This method completes the configuration process and makes the settings available for dependency injection.
    /// </summary>
    /// <returns>The <see cref="IServiceCollection"/> for method chaining.</returns>
    /// <example>
    /// <code>
    /// services.AddSettings&lt;MySettings&gt;(configuration, "MySection")
    ///     .WithDataAnnotationValidation()
    ///     .Register()
    ///     .AddSingleton&lt;IMyService, MyService&gt;();
    /// </code>
    /// </example>
    public IServiceCollection Register()
    {
        return services;
    }
}

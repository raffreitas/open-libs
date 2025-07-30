using System.ComponentModel.DataAnnotations;

using Microsoft.Extensions.Configuration;

using Microsoft.Extensions.DependencyInjection;

using OpenLibs.Extensions.Builders;

namespace OpenLibs.Extensions.Tests.Builders.SettingsConfigurationBuilder;

public class SettingsConfigurationBuilderTests
{
    public class TestSettings
    {
        [Required]
        public string RequiredProperty { get; set; } = string.Empty;

        public int OptionalProperty { get; set; }

        [Range(1, 100)]
        public int RangeProperty { get; set; }
    }

    public class EmptySettings
    {
        public string Property { get; set; } = "default";
    }

    private static IConfiguration CreateConfiguration(Dictionary<string, string?> configData)
    {
        return new ConfigurationBuilder()
            .AddInMemoryCollection(configData)
            .Build();
    }

    private static ServiceCollection CreateServiceCollection()
    {
        return new ServiceCollection();
    }

    [Fact]
    public void Constructor_WithValidParameters_ShouldCreateInstance()
    {
        // Arrange
        var services = CreateServiceCollection();
        var configuration = CreateConfiguration([]);
        const string sectionName = "TestSection";

        // Act
        var builder = new SettingsConfigurationBuilder<TestSettings>(services, configuration, sectionName);

        // Assert
        Assert.NotNull(builder);
    }

    [Fact]
    public void WithDataAnnotationValidation_ShouldReturnSameInstance()
    {
        // Arrange
        var services = CreateServiceCollection();
        var configuration = CreateConfiguration(new Dictionary<string, string?>());
        var builder = new SettingsConfigurationBuilder<TestSettings>(services, configuration, "TestSection");

        // Act
        var result = builder.WithDataAnnotationValidation();

        // Assert
        Assert.Same(builder, result);
    }

    [Fact]
    public void WithEagerValidation_ShouldReturnSameInstance()
    {
        // Arrange
        var services = CreateServiceCollection();
        var configuration = CreateConfiguration(new Dictionary<string, string?>());
        var builder = new SettingsConfigurationBuilder<TestSettings>(services, configuration, "TestSection");

        // Act
        var result = builder.WithEagerValidation();

        // Assert
        Assert.Same(builder, result);
    }

    [Fact]
    public void WithCustomValidation_WithValidationFunction_ShouldReturnSameInstance()
    {
        // Arrange
        var services = CreateServiceCollection();
        var configuration = CreateConfiguration(new Dictionary<string, string?>());
        var builder = new SettingsConfigurationBuilder<TestSettings>(services, configuration, "TestSection");

        // Act
        var result = builder.WithCustomValidation(settings => settings.OptionalProperty > 0);

        // Assert
        Assert.Same(builder, result);
    }

    [Fact]
    public void WithCustomValidation_WithCustomMessage_ShouldReturnSameInstance()
    {
        // Arrange
        var services = CreateServiceCollection();
        var configuration = CreateConfiguration(new Dictionary<string, string?>());
        var builder = new SettingsConfigurationBuilder<TestSettings>(services, configuration, "TestSection");
        const string customMessage = "Custom validation failed";

        // Act
        var result = builder.WithCustomValidation(settings => settings.OptionalProperty > 0, customMessage);

        // Assert
        Assert.Same(builder, result);
    }

    [Fact]
    public void Build_WithValidConfiguration_ShouldReturnSettingsInstance()
    {
        // Arrange
        var configData = new Dictionary<string, string?>
        {
            ["TestSection:RequiredProperty"] = "Test Value",
            ["TestSection:OptionalProperty"] = "42",
            ["TestSection:RangeProperty"] = "50"
        };
        var configuration = CreateConfiguration(configData);
        var services = CreateServiceCollection();
        var builder = new SettingsConfigurationBuilder<TestSettings>(services, configuration, "TestSection");

        // Act
        var result = builder.Build();

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Test Value", result.RequiredProperty);
        Assert.Equal(42, result.OptionalProperty);
        Assert.Equal(50, result.RangeProperty);
    }

    [Fact]
    public void Build_WithMissingSection_ShouldThrowInvalidOperationException()
    {
        // Arrange
        var configuration = CreateConfiguration(new Dictionary<string, string?>());
        var services = CreateServiceCollection();
        var builder = new SettingsConfigurationBuilder<TestSettings>(services, configuration, "NonExistentSection");

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() => builder.Build());
        Assert.Contains("Configuration section 'NonExistentSection' for type 'TestSettings' is missing or invalid", exception.Message);
    }

    [Fact]
    public void Build_WithEmptySection_ShouldThrowInvalidOperationException()
    {
        // Arrange
        var configData = new Dictionary<string, string?>
        {
            ["OtherSection:Property"] = "value"
        };
        var configuration = CreateConfiguration(configData);
        var services = CreateServiceCollection();
        var builder = new SettingsConfigurationBuilder<TestSettings>(services, configuration, "EmptySection");

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() => builder.Build());
        Assert.Contains("Configuration section 'EmptySection' for type 'TestSettings' is missing or invalid", exception.Message);
    }

    [Fact]
    public void Register_ShouldReturnOriginalServiceCollection()
    {
        // Arrange
        var services = CreateServiceCollection();
        var configuration = CreateConfiguration(new Dictionary<string, string?>());
        var builder = new SettingsConfigurationBuilder<TestSettings>(services, configuration, "TestSection");

        // Act
        var result = builder.Register();

        // Assert
        Assert.Same(services, result);
    }

    [Fact]
    public void FluentChaining_ShouldWorkCorrectly()
    {
        // Arrange
        var configData = new Dictionary<string, string?>
        {
            ["TestSection:RequiredProperty"] = "Test Value",
            ["TestSection:OptionalProperty"] = "42",
            ["TestSection:RangeProperty"] = "50"
        };
        var configuration = CreateConfiguration(configData);
        var services = CreateServiceCollection();

        // Act
        var result = new SettingsConfigurationBuilder<TestSettings>(services, configuration, "TestSection")
            .WithDataAnnotationValidation()
            .WithEagerValidation()
            .WithCustomValidation(settings => settings.OptionalProperty > 0, "Optional property must be positive")
            .Build();

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Test Value", result.RequiredProperty);
        Assert.Equal(42, result.OptionalProperty);
        Assert.Equal(50, result.RangeProperty);
    }

    [Fact]
    public void Register_WithFluentChaining_ShouldReturnServiceCollection()
    {
        // Arrange
        var configData = new Dictionary<string, string?>
        {
            ["TestSection:RequiredProperty"] = "Test Value",
            ["TestSection:OptionalProperty"] = "42",
            ["TestSection:RangeProperty"] = "50"
        };
        var configuration = CreateConfiguration(configData);
        var services = CreateServiceCollection();

        // Act
        var result = new SettingsConfigurationBuilder<TestSettings>(services, configuration, "TestSection")
            .WithDataAnnotationValidation()
            .WithEagerValidation()
            .WithCustomValidation(settings => settings.OptionalProperty > 0)
            .Register();

        // Assert
        Assert.Same(services, result);
    }

    [Fact]
    public void Build_WithPartialConfiguration_ShouldReturnInstanceWithDefaults()
    {
        // Arrange
        var configData = new Dictionary<string, string?>
        {
            ["TestSection:RequiredProperty"] = "Only Required Set"
        };
        var configuration = CreateConfiguration(configData);
        var services = CreateServiceCollection();
        var builder = new SettingsConfigurationBuilder<TestSettings>(services, configuration, "TestSection");

        // Act
        var result = builder.Build();

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Only Required Set", result.RequiredProperty);
        Assert.Equal(0, result.OptionalProperty);
        Assert.Equal(0, result.RangeProperty);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("ValidSection")]
    public void Constructor_WithDifferentSectionNames_ShouldCreateInstance(string sectionName)
    {
        // Arrange
        var services = CreateServiceCollection();
        var configuration = CreateConfiguration(new Dictionary<string, string?>());

        // Act
        var builder = new SettingsConfigurationBuilder<EmptySettings>(services, configuration, sectionName);

        // Assert
        Assert.NotNull(builder);
    }

    [Fact]
    public void WithCustomValidation_WithNullMessage_ShouldUseDefaultMessage()
    {
        // Arrange
        var services = CreateServiceCollection();
        var configuration = CreateConfiguration(new Dictionary<string, string?>());
        var builder = new SettingsConfigurationBuilder<TestSettings>(services, configuration, "TestSection");

        // Act
        var result = builder.WithCustomValidation(settings => true, null);

        // Assert
        Assert.Same(builder, result);
    }
}
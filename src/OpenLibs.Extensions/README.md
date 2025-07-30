# OpenLibs.Extensions

[![NuGet](https://img.shields.io/nuget/v/OpenLibs.Extensions.svg)](https://www.nuget.org/packages/OpenLibs.Extensions/)
[![Downloads](https://img.shields.io/nuget/dt/OpenLibs.Extensions.svg)](https://www.nuget.org/packages/OpenLibs.Extensions/)

Useful extensions for strongly-typed configuration and dependency injection in .NET applications.

## 🚀 Installation

```bash
dotnet add package OpenLibs.Extensions
```

## 📋 Features

### SettingsConfigurationExtensions

Extensions to simplify strongly-typed settings configuration in .NET applications with built-in validation support.

#### Quick Usage Examples

```csharp
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        // Option 1: Register settings for dependency injection
        services.RegisterSettings<MySettings>(Configuration, "MyApp");
        
        // Option 2: Get validated settings instance immediately  
        var settings = services.ConfigureRequiredSettings<MySettings>(Configuration, "MyApp");
        
        // Option 3: Use fluent builder for advanced configuration
        services.ConfigureSettings<MySettings>(Configuration, "MyApp")
            .WithDataAnnotationValidation()
            .WithEagerValidation()
            .Register();
    }
}

[DataContract]
public class MySettings
{
    [Required]
    public string ApiUrl { get; set; } = string.Empty;
    
    [Range(1, 300)]
    public int TimeoutSeconds { get; set; }
}
```

### SettingsConfigurationBuilder

Fluent builder pattern for advanced settings configuration with validation options.

#### Builder Usage Example

```csharp
// Basic fluent configuration
services.ConfigureSettings<MySettings>(Configuration, "MyApp")
    .WithDataAnnotationValidation()  // Enable Data Annotations validation
    .WithEagerValidation()           // Validate at startup, not first access
    .Register();                     // Register in DI container

// Get settings instance directly
var settings = services.ConfigureSettings<MySettings>(Configuration, "MyApp")
    .WithDataAnnotationValidation()
    .Build();
```

## 🔧 Configuration

### appsettings.json

```json
{
  "MyApp": {
    "ApiUrl": "https://api.example.com",
    "TimeoutSeconds": 30
  }
}
```

### Usage in Your Services

```csharp
// Inject as IOptions<T>
public class MyService
{
    private readonly MySettings _settings;
    
    public MyService(IOptions<MySettings> settings)
    {
        _settings = settings.Value;
    }
}
```

## 📖 API Reference

### SettingsConfigurationExtensions

#### `RegisterSettings<T>(IServiceCollection, IConfiguration, string)`

Registers a configuration object in the DI container with validation.

**Parameters:**
- `services`: Service collection
- `configuration`: IConfiguration instance  
- `sectionName`: Configuration section name

**Returns:** `IServiceCollection` for method chaining

#### `ConfigureRequiredSettings<T>(IServiceCollection, IConfiguration, string)`

Gets a validated settings instance immediately.

**Returns:** Validated instance of type `T`

#### `ConfigureSettings<T>(IServiceCollection, IConfiguration, string)`

Creates a fluent configuration builder.

**Returns:** `SettingsConfigurationBuilder<T>` for fluent configuration

### SettingsConfigurationBuilder<T>

#### `WithDataAnnotationValidation()`

Enables automatic validation using Data Annotations attributes like `[Required]`, `[Range]`, etc.

#### `WithEagerValidation()`

Enables validation at application startup instead of first access.

#### `Build()`

Returns the configured settings instance.

#### `Register()`

Registers the settings in the DI container and returns the service collection.

## 🤝 Contributing

See the main project's [contribution guide](../../CONVENTIONAL_COMMITS.md).

## 📄 License

This project is licensed under the [MIT License](../../LICENSE).

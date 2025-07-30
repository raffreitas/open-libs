# OpenLibs.Extensions

[![NuGet](https://img.shields.io/nuget/v/OpenLibs.Extensions.svg)](https://www.nuget.org/packages/OpenLibs.Extensions/)
[![Downloads](https://img.shields.io/nuget/dt/OpenLibs.Extensions.svg)](https://www.nuget.org/packages/OpenLibs.Extensions/)

Useful extensions for configuration and dependency injection in .NET applications.

## 🚀 Installation

```bash
dotnet add package OpenLibs.Extensions
```

## 📋 Features

### SettingsConfigurationExtensions

Extensions to simplify settings configuration in .NET applications.

#### Usage Example

```csharp
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        // Configure settings automatically
        services.AddSettings<MySettings>(Configuration);
    }
}

public class MySettings
{
    public string ApiUrl { get; set; }
    public int TimeoutSeconds { get; set; }
}
```

### SettingsConfigurationBuilder

Builder pattern for advanced settings configuration.

#### Usage Example

```csharp
services.AddSettings<MySettings>(builder => 
    builder
        .FromSection("MyApp")
        .WithValidation()
        .WithReload());
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

## 📖 API Reference

### SettingsConfigurationExtensions

#### `AddSettings<T>(IServiceCollection, IConfiguration)`

Registers a configuration object in the DI container.

**Parameters:**
- `services`: Service collection
- `configuration`: IConfiguration instance

**Returns:** `IServiceCollection` for method chaining

### SettingsConfigurationBuilder

#### `FromSection(string sectionName)`

Defines the configuration file section to be used.

#### `WithValidation()`

Enables automatic validation using Data Annotations.

#### `WithReload()`

Enables automatic reload when the configuration file changes.

## 🤝 Contributing

See the main project's [contribution guide](../../CONVENTIONAL_COMMITS.md).

## 📄 License

This project is licensed under the [MIT License](../../LICENSE).

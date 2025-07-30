# OpenLibs

[![Build Status](https://github.com/raffreitas/open-libs/workflows/Release%20and%20Publish/badge.svg)](https://github.com/raffreitas/open-libs/actions)
[![NuGet](https://img.shields.io/nuget/v/OpenLibs.Extensions.svg)](https://www.nuget.org/packages/OpenLibs.Extensions/)
[![codecov](https://codecov.io/gh/raffreitas/open-libs/branch/main/graph/badge.svg)](https://codecov.io/gh/raffreitas/open-libs)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

A collection of useful .NET libraries for modern development.

## 📦 Available Packages

| Package | Version | Description |
|---------|---------|-------------|
| [OpenLibs.Extensions](https://www.nuget.org/packages/OpenLibs.Extensions/) | [![NuGet](https://img.shields.io/nuget/v/OpenLibs.Extensions.svg)](https://www.nuget.org/packages/OpenLibs.Extensions/) | Useful extensions for configuration and dependency injection |
| OpenLibs.SeedWork | Coming soon | Base implementations for Domain-Driven Design |

## 🚀 Installation

```bash
# Via Package Manager
Install-Package OpenLibs.Extensions

# Via .NET CLI
dotnet add package OpenLibs.Extensions

# Via PackageReference
<PackageReference Include="OpenLibs.Extensions" Version="1.0.0" />
```

## 📖 Documentation

Each package has its own documentation:

- [OpenLibs.Extensions](./src/OpenLibs.Extensions/README.md)

## 🤝 Contributing

This project uses [Conventional Commits](./CONVENTIONAL_COMMITS.md) for automatic semantic versioning.

### Development Process

1. Fork the project
2. Create a feature branch (`git checkout -b feat/amazing-feature`)
3. Make commits following the Conventional Commits standard
4. Push to the branch (`git push origin feat/amazing-feature`)
5. Open a Pull Request

### Commit Examples

```bash
feat(extensions): add JWT configuration extension
fix(extensions): fix dependency injection issue
docs: update API documentation
```

## 🔄 Versioning

This project uses **Automated Semantic Versioning**:

- **PATCH** (1.0.0 → 1.0.1): Bug fixes and minor improvements
- **MINOR** (1.0.0 → 1.1.0): New backward-compatible features
- **MAJOR** (1.0.0 → 2.0.0): Breaking changes

Versioning is automatically calculated based on commits using GitHub Actions.

## 📋 Requirements

- .NET 9.0+
- C# 12.0+

## 📄 License

This project is licensed under the [MIT License](./LICENSE).

## 👨‍💻 Author

**Rafael Freitas**
- GitHub: [@raffreitas](https://github.com/raffreitas)

---

⭐ If this project helped you, please consider giving it a star!

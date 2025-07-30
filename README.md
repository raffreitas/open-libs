# OpenLibs

A collection of useful .NET libraries for modern development.

## 🎯 Overview

**OpenLibs** is a monorepo containing high-quality .NET packages designed to accelerate development with clean, maintainable code:

Each package follows modern .NET practices, targets .NET 9.0+, and includes comprehensive documentation with practical examples.

## 📦 Available Packages

| Package | Version | Description |
|---------|---------|-------------|
| [OpenLibs.Extensions](https://www.nuget.org/packages/OpenLibs.Extensions/) | [![NuGet](https://img.shields.io/nuget/v/OpenLibs.Extensions.svg)](https://www.nuget.org/packages/OpenLibs.Extensions/) | Strongly-typed configuration and dependency injection extensions |
| [OpenLibs.SeedWork](https://www.nuget.org/packages/OpenLibs.SeedWork/) | [![NuGet](https://img.shields.io/nuget/v/OpenLibs.SeedWork.svg)](https://www.nuget.org/packages/OpenLibs.SeedWork/) | Base implementations for Domain-Driven Design (DDD) |

## 📖 Documentation

Each package has its own comprehensive documentation:

- [OpenLibs.Extensions](./src/OpenLibs.Extensions/README.md) - Configuration and dependency injection extensions
- [OpenLibs.SeedWork](./src/OpenLibs.SeedWork/README.md) - Domain-Driven Design building blocks

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
feat(seedwork): implement aggregate root with domain events
fix(extensions): fix dependency injection issue
fix(seedwork): resolve entity identity comparison
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

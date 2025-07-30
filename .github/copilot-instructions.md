# GitHub Copilot Instructions for OpenLibs Project

This file contains specific instructions for GitHub Copilot when working on the OpenLibs monorepo project.

## 🚀 Project Overview

This is a .NET monorepo containing multiple NuGet packages with automated semantic versioning via GitHub Actions. Each package has independent versioning and release cycles.

## 📦 Current Packages

- **OpenLibs.Extensions**: Strongly-typed configuration and dependency injection extensions
- **OpenLibs.SeedWork**: (Coming soon) Base implementations for Domain-Driven Design

## 🔄 Commit Standards

**MANDATORY**: This project uses [Conventional Commits](https://www.conventionalcommits.org/) specification for automatic semantic versioning. ALL commits must follow this format:

```
<type>(<scope>): <description>

[optional body]

[optional footer(s)]
```

### Commit Types

- **feat**: New features (triggers MINOR version bump)
- **fix**: Bug fixes (triggers PATCH version bump)
- **docs**: Documentation changes only
- **style**: Code style changes (formatting, semicolons, etc.)
- **refactor**: Code refactoring without feature changes
- **test**: Adding or updating tests
- **chore**: Maintenance tasks, dependency updates
- **ci**: CI/CD pipeline changes
- **build**: Build system or external dependencies changes
- **perf**: Performance improvements

### Breaking Changes

Add `!` after type/scope OR include `BREAKING CHANGE:` in footer for MAJOR version bumps:
```bash
feat(extensions)!: change configuration API structure
# OR
feat(extensions): add new configuration method

BREAKING CHANGE: ConfigureSettings method signature changed
```

### Scopes (Package Names)

- **extensions**: Changes to OpenLibs.Extensions package
- **seedwork**: Changes to OpenLibs.SeedWork package  
- **ci**: CI/CD pipeline changes
- **docs**: Documentation changes
- **root**: Root-level project changes

### Example Commits

```bash
# Feature additions
feat(extensions): add JWT configuration support
feat(seedwork): implement base entity class
feat(extensions): add validation attributes for settings

# Bug fixes  
fix(extensions): resolve dependency injection circular reference
fix(ci): correct test execution path in pipeline
fix(extensions): handle null configuration sections properly

# Documentation
docs(extensions): update README with validation examples
docs: add contributing guidelines
docs(seedwork): create initial API documentation

# CI/CD changes
ci(extensions): add automated NuGet publishing
ci: fix version calculation script
ci(extensions): update pipeline permissions

# Tests
test(extensions): add unit tests for configuration builder
test(seedwork): add integration tests for entities

# Refactoring
refactor(extensions): extract validation logic to separate class
refactor: organize project structure

# Chores
chore: update dependencies to latest versions
chore(extensions): clean up unused imports
```

## 🏗️ Code Patterns

### For OpenLibs.Extensions

- Use fluent builder patterns
- Implement comprehensive XML documentation
- Add Data Annotations validation support
- Follow Microsoft.Extensions.* patterns
- Include example usage in XML docs

### General Patterns

- Target .NET 9.0+ and C# 12.0+
- Use nullable reference types
- Implement proper error handling
- Follow Microsoft naming conventions
- Add comprehensive unit tests

## 🧪 Testing Requirements

- All new features must include unit tests
- Use meaningful test method names following pattern: `MethodName_Scenario_ExpectedResult`
- Test projects should be in `tests/` folder
- Use xUnit for testing framework

## 📁 Project Structure

```
OpenLibs/
├── src/
│   ├── OpenLibs.Extensions/
│   └── OpenLibs.SeedWork/
├── tests/
│   ├── OpenLibs.Extensions.Tests/
│   └── OpenLibs.SeedWork.Tests/
├── .github/workflows/
│   ├── extensions-release.yml
│   ├── seedwork-release.yml
│   └── general-ci.yml
└── docs/
```

## 🚀 Release Process

1. **Automatic**: Releases are triggered by pushes to `main` branch
2. **Path-based**: Each package has independent pipelines triggered by file changes
3. **Version Calculation**: Based on conventional commits since last release
4. **NuGet Publishing**: Automatic on successful build and tests

### Pipeline Triggers

- **Extensions**: Changes in `src/OpenLibs.Extensions/**` or `tests/OpenLibs.Extensions.Tests/**`
- **SeedWork**: Changes in `src/OpenLibs.SeedWork/**` or `tests/OpenLibs.SeedWork.Tests/**`

## ⚠️ Important Notes

1. **Never commit without conventional format** - it breaks semantic versioning
2. **Always include scope** for package-specific changes
3. **Test changes locally** before committing
4. **Update documentation** when adding new features
5. **Follow existing code patterns** in each package
6. **XML documentation** is required for all public APIs

## 🔍 Before Committing Checklist

- [ ] Commit message follows Conventional Commits format
- [ ] Appropriate scope is used (extensions, seedwork, ci, docs)
- [ ] Breaking changes are properly marked with `!` or `BREAKING CHANGE:`
- [ ] New features include XML documentation
- [ ] Tests are added/updated for new functionality
- [ ] README is updated if API changes
- [ ] Code follows project patterns and conventions

## 🎯 Semantic Versioning Impact

| Commit Type | Example | Version Impact |
|-------------|---------|----------------|
| `fix(extensions): ...` | Bug fix | 1.0.0 → 1.0.1 |
| `feat(extensions): ...` | New feature | 1.0.0 → 1.1.0 |
| `feat(extensions)!: ...` | Breaking change | 1.0.0 → 2.0.0 |
| `docs: ...` | Documentation | No release |
| `test: ...` | Tests only | No release |

Remember: Each package is versioned independently based on changes in its specific paths!

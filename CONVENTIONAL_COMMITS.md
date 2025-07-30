# Conventional Commits Guide

This project follows the [Conventional Commits](https://www.conventionalcommits.org/) specification for consistent commit messages and automatic versioning.

## Format

```
<type>[optional scope]: <description>

[optional body]

[optional footer(s)]
```

## Types and Version Impact

| Type | Description | Version Impact |
|------|-------------|----------------|
| `feat` | New feature | **MINOR** (1.0.0 → 1.1.0) |
| `fix` | Bug fix | **PATCH** (1.0.0 → 1.0.1) |
| `docs` | Documentation changes | **PATCH** |
| `style` | Formatting changes | **PATCH** |
| `refactor` | Code refactoring | **PATCH** |
| `perf` | Performance improvements | **PATCH** |
| `test` | Adding or fixing tests | **PATCH** |
| `build` | Build system changes | **PATCH** |
| `ci` | CI/CD changes | **PATCH** |
| `chore` | Other changes | **PATCH** |
| `revert` | Commit reversion | **PATCH** |

## Breaking Changes

To indicate a **BREAKING CHANGE** (MAJOR version bump):
- Add `!` after the type: `feat!: new incompatible API`
- Or include `BREAKING CHANGE:` in the commit footer

## Examples

```bash
# New feature (MINOR)
feat(extensions): add JWT configuration extension

# Bug fix (PATCH)
fix(extensions): fix dependency injection issue

# Breaking Change (MAJOR)
feat(extensions)!: change ISettingsBuilder interface

# With body and footer
feat(extensions): add JWT configuration support

Implements automatic JWT configuration using IConfiguration.
Adds extensions to simplify configuration in web projects.

Closes #123
```

## Useful Scripts

```bash
# Check commit format
git log --oneline -10

# Create commit following the pattern
git commit -m "feat(extensions): add new functionality"
```

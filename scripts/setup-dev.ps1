# OpenLibs Development Environment Setup
Write-Host "Setting up OpenLibs development environment..." -ForegroundColor Green

# Check if .NET SDK is installed
try {
    $dotnetVersion = dotnet --version
    Write-Host "✅ .NET SDK found: $dotnetVersion" -ForegroundColor Green
} catch {
    Write-Host "❌ .NET SDK not found. Please install .NET 9 SDK." -ForegroundColor Red
    exit 1
}

# Check if Git is installed
try {
    $gitVersion = git --version
    Write-Host "✅ Git found: $gitVersion" -ForegroundColor Green
} catch {
    Write-Host "❌ Git not found. Please install Git." -ForegroundColor Red
    exit 1
}

# Restore dependencies
Write-Host "Restoring dependencies..." -ForegroundColor Yellow
dotnet restore

# Build solution
Write-Host "Building solution..." -ForegroundColor Yellow
dotnet build --configuration Debug

# Run tests
Write-Host "Running tests..." -ForegroundColor Yellow
dotnet test --verbosity normal

Write-Host ""
Write-Host "🎉 Environment set up successfully!" -ForegroundColor Green
Write-Host ""
Write-Host "Useful commands:" -ForegroundColor Yellow
Write-Host "  dotnet build                     # Build solution"
Write-Host "  dotnet test                      # Run tests"
Write-Host "  dotnet test --collect:'XPlat Code Coverage' # Tests with coverage"
Write-Host "  .\scripts\new-project.ps1 -ProjectName 'NewPackage' # Create new package"
Write-Host ""
Write-Host "To contribute:" -ForegroundColor Yellow
Write-Host "  1. Read the guide: CONVENTIONAL_COMMITS.md"
Write-Host "  2. Use commits in format: 'feat(scope): description'"
Write-Host "  3. Versioning is automatic via GitHub Actions"
Write-Host ""
Write-Host "Commit example:" -ForegroundColor Cyan
Write-Host "  git commit -m 'feat(extensions): add JWT extension'"

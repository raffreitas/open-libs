# Script to create new project in monorepo
param(
    [Parameter(Mandatory=$true)]
    [string]$ProjectName,
    
    [Parameter(Mandatory=$false)]
    [string]$ProjectType = "classlib",
    
    [Parameter(Mandatory=$false)]
    [string]$Description = ".NET library from OpenLibs"
)

$SrcPath = "src/$ProjectName"
$TestPath = "tests/$ProjectName.Tests"

Write-Host "Creating project $ProjectName..." -ForegroundColor Green

# Create main project
dotnet new $ProjectType -n $ProjectName -o $SrcPath

# Create test project
dotnet new xunit -n "$ProjectName.Tests" -o $TestPath

# Add reference from test project to main project
dotnet add "$TestPath/$ProjectName.Tests.csproj" reference "$SrcPath/$ProjectName.csproj"

# Add projects to solution
dotnet sln add "$SrcPath/$ProjectName.csproj"
dotnet sln add "$TestPath/$ProjectName.Tests.csproj"

# Update project file with default settings
$ProjectContent = @"
<Project Sdk="Microsoft.NET.Sdk">

    <PropertyGroup>
        <!-- Project-specific settings -->
        <PackageId>OpenLibs.$ProjectName</PackageId>
        <Title>OpenLibs $ProjectName</Title>
        <Description>$Description</Description>
        <PackageTags>`$(PackageTags);$($ProjectName.ToLower())</PackageTags>
        <GeneratePackageOnBuild>false</GeneratePackageOnBuild>
    </PropertyGroup>

    <ItemGroup>
        <!-- Add your dependencies here -->
    </ItemGroup>

</Project>
"@

$ProjectContent | Out-File "$SrcPath/$ProjectName.csproj" -Encoding UTF8

# Create README for the project
$ReadmeContent = @"
# OpenLibs.$ProjectName

[![NuGet](https://img.shields.io/nuget/v/OpenLibs.$ProjectName.svg)](https://www.nuget.org/packages/OpenLibs.$ProjectName/)

$Description

## 🚀 Installation

``````bash
dotnet add package OpenLibs.$ProjectName
``````

## 📋 Features

[Document features here]

## 📖 API Reference

[Document API here]

## 🤝 Contributing

See the main project's [contribution guide](../../CONVENTIONAL_COMMITS.md).

## 📄 License

This project is licensed under the [MIT License](../../LICENSE).
"@

$ReadmeContent | Out-File "$SrcPath/README.md" -Encoding UTF8

Write-Host "Project $ProjectName created successfully!" -ForegroundColor Green
Write-Host ""
Write-Host "Next steps:" -ForegroundColor Yellow
Write-Host "1. Edit $SrcPath/$ProjectName.csproj to add dependencies"
Write-Host "2. Implement your library in $SrcPath/"
Write-Host "3. Write tests in $TestPath/"
Write-Host "4. Update README.md in $SrcPath/README.md"
Write-Host "5. Update the packages list in the main README.md"
Write-Host "6. Add the project to the GitHub Actions matrix (.github/workflows/release.yml)"

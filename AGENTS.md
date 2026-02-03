# Copilot Instructions for dotnet-framework-docker

This repository contains the official Docker images for .NET Framework, published to Microsoft Container Registry (mcr.microsoft.com).

## Build and Test Commands

```powershell
# Build and test all images for a specific .NET version
./build-and-test.ps1 -Version 4.8

# Build only (no tests) for specific version and OS
./build-and-test.ps1 -Version 4.8 -OS windowsservercore-ltsc2019 -Mode Build

# Test only
./build-and-test.ps1 -Version 4.8 -Mode Test

# Build/test specific image variants: runtime, sdk, aspnet, wcf
./build-and-test.ps1 -Repos sdk -Version 4.8.1

# Run a single test by filter
./tests/run-tests.ps1 -TestCategories sdk
dotnet test tests/Microsoft.DotNet.Framework.Docker.Tests --filter "Category=sdk"
```

## Architecture

### Image Hierarchy

Images build on each other: `runtime` → `aspnet` → `wcf`, with `sdk` building on `runtime`. Each variant supports multiple .NET Framework versions (4.8, 4.8.1) across Windows Server Core versions (ltsc2016, ltsc2019, ltsc2022, ltsc2025).

### Generated Files (Do Not Edit Directly)

- **Dockerfiles in `src/`**: Generated from [Cottle templates](https://cottle.readthedocs.io/en/stable/) in `eng/dockerfile-templates/`
- **README files**: Generated from templates in `eng/readme-templates/`

To modify Dockerfiles or READMEs, edit the templates and regenerate:

```powershell
# Regenerate Dockerfiles
./eng/dockerfile-templates/Get-GeneratedDockerfiles.ps1

# Regenerate READMEs
./eng/readme-templates/Get-GeneratedReadmes.ps1

# Validate without regenerating
./eng/dockerfile-templates/Get-GeneratedDockerfiles.ps1 -Validate
```

### Key Configuration Files

- `manifest.json`: Defines all images, tags, and build configuration for the infrastructure
- `manifest.versions.json`: Product versions (NuGet, Visual Studio, patches) referenced by Dockerfile templates
- `manifest.datestamps.json`: Date-based tags for image versioning

## Conventions

### Dockerfile Template Variables

Templates use Cottle syntax with variables from `manifest.versions.json`:

- `VARIABLES[cat("kb|", OS_VERSION_NUMBER, "|", PRODUCT_VERSION)]` - Windows KB patches
- `VARIABLES[cat(PRODUCT_VERSION, "|url")]` - .NET Framework installer URLs
- `OS_VERSION_NUMBER`, `PRODUCT_VERSION` - Context variables for the build

### Test Categories

Tests are categorized by image type: `runtime`, `sdk`, `aspnet`, `wcf`, `pre-build`. Tests validate both static image state (environment variables) and runtime scenarios.

### Updating Product Versions

Update `manifest.versions.json` then regenerate Dockerfiles. This is typically done by automation.

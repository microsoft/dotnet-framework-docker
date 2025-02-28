#!/usr/bin/env pwsh
param(
    [switch] $Validate
)

$ErrorActionPreference = 'Stop'

if (!$IsWindows) {
    $IsWindows = $PSVersionTable.PSEdition -eq "Desktop"
}

if ($Validate) {
    $customImageBuilderArgs = " --validate"
}

$repoRoot = (Get-Item "$PSScriptRoot").Parent.Parent.FullName

function CopyReadme([string]$containerName, [string]$readmeRelativePath) {
    $readmeDir = Split-Path $readmeRelativePath -Parent
    Exec "docker cp ${containerName}:/repo/$readmeRelativePath $repoRoot/$readmeDir"
}

$onDockerfilesGeneratedLinux = {
    param($ContainerName)

    if (-Not $Validate) {
        CopyReadme $ContainerName "README.aspnet.md"
        CopyReadme $ContainerName "README.md"
        CopyReadme $ContainerName "README.runtime.md"
        CopyReadme $ContainerName "README.samples.md"
        CopyReadme $ContainerName "README.sdk.md"
        CopyReadme $ContainerName "README.wcf.md"

        CopyReadme $ContainerName ".portal-docs/mar/README.aspnet.portal.md"
        CopyReadme $ContainerName ".portal-docs/mar/README.runtime.portal.md"
        CopyReadme $ContainerName ".portal-docs/mar/README.samples.portal.md"
        CopyReadme $ContainerName ".portal-docs/mar/README.sdk.portal.md"
        CopyReadme $ContainerName ".portal-docs/mar/README.wcf.portal.md"

        CopyReadme $ContainerName ".portal-docs/docker-hub/README.aspnet.md"
        CopyReadme $ContainerName ".portal-docs/docker-hub/README.runtime.md"
        CopyReadme $ContainerName ".portal-docs/docker-hub/README.samples.md"
        CopyReadme $ContainerName ".portal-docs/docker-hub/README.sdk.md"
        CopyReadme $ContainerName ".portal-docs/docker-hub/README.wcf.md"
    }
}

function Invoke-GenerateReadme {
    param ([string] $Manifest)

    $imageBuilderArgs = "generateReadmes --manifest $Manifest --source-branch 'main'$customImageBuilderArgs 'https://github.com/microsoft/dotnet-framework-docker'"

    if ($IsWindows) {
        & $PSScriptRoot/../common/Invoke-ImageBuilder.ps1 `
            -ImageBuilderArgs $imageBuilderArgs
    } else {
        & $PSScriptRoot/../common/Invoke-ImageBuilder.ps1 `
            -ImageBuilderArgs $imageBuilderArgs `
            -OnCommandExecuted $onDockerfilesGeneratedLinux
    }
}

Invoke-GenerateReadme "manifest.json"
Invoke-GenerateReadme "manifest.samples.json"

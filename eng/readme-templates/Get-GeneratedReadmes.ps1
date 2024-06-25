#!/usr/bin/env pwsh
param(
    [switch] $Validate
)

$ErrorActionPreference = 'Stop'

if ($Validate) {
    $customImageBuilderArgs = " --validate"
}

$repoRoot = (Get-Item "$PSScriptRoot").Parent.Parent.FullName

function CopyReadme([string]$containerName, [string]$readmeRelativePath) {
    $readmeDir = Split-Path $readmeRelativePath -Parent
    Exec "docker cp ${containerName}:/repo/$readmeRelativePath $repoRoot/$readmeDir"
}

$onDockerfilesGenerated = {
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

    & $PSScriptRoot/../common/Invoke-ImageBuilder.ps1 `
        -ImageBuilderArgs `
            "generateReadmes --manifest $Manifest --source-branch 'main'$customImageBuilderArgs 'https://github.com/microsoft/dotnet-framework-docker'" `
        -OnCommandExecuted $onDockerfilesGenerated `
}

Invoke-GenerateReadme "manifest.json"
Invoke-GenerateReadme "manifest.samples.json"

#!/usr/bin/env pwsh
param(
    [switch] $Validate
)

$ErrorActionPreference = 'Stop'

if ($Validate) {
    $customImageBuilderArgs = " --validate"
}

$repoRoot = (Get-Item "$PSScriptRoot").Parent.Parent.FullName

$onDockerfilesGenerated = {
    param($ContainerName)

    if (-Not $Validate) {
        Exec "docker cp ${ContainerName}:/repo/README.aspnet.md $repoRoot"
        Exec "docker cp ${ContainerName}:/repo/README.md $repoRoot"
        Exec "docker cp ${ContainerName}:/repo/README.runtime.md $repoRoot"
        Exec "docker cp ${ContainerName}:/repo/README.samples.md $repoRoot"
        Exec "docker cp ${ContainerName}:/repo/README.sdk.md $repoRoot"
        Exec "docker cp ${ContainerName}:/repo/README.wcf.md $repoRoot"
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

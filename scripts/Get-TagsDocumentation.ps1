#!/usr/bin/env pwsh
$ErrorActionPreference = 'Stop'
$repoRoot = Split-Path -Path "$PSScriptRoot" -Parent

function Log {
    param ([string] $Message)

    Write-Output $Message
}

function Exec {
    param ([string] $Cmd)

    Log "Executing: '$Cmd'"
    Invoke-Expression $Cmd
    if ($LASTEXITCODE -ne 0) {
        throw "Failed: '$Cmd'"
    }
}

function GenerateDoc {
    param (
        [string] $Repo,
        [string] $ReadmePath,
        [string] $Manifest,
        [switch] $ReuseImageBuilderImage
    )

    $onTagsGenerated = {
        param($ContainerName)
        Exec "docker cp ${ContainerName}:/repo/$ReadmePath $repoRoot/$ReadmePath"
    }

    $imageBuilderArgs = "generateTagsReadme" `
        + " --manifest $Manifest" `
        + " --repo $Repo" `
        + " https://github.com/microsoft/dotnet-framework-docker/blob/master"

    & "$PSScriptRoot/Invoke-ImageBuilder.ps1" `
        -ImageBuilderArgs $imageBuilderArgs `
        -ReuseImageBuilderImage:$ReuseImageBuilderImage `
        -OnCommandExecuted $onTagsGenerated
}

GenerateDoc dotnet/framework/runtime README.runtime.md manifest.json
GenerateDoc dotnet/framework/sdk README.sdk.md manifest.json -ReuseImageBuilderImage
GenerateDoc dotnet/framework/samples README.samples.md manifest.samples.json -ReuseImageBuilderImage

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
        [string] $Template,
        [string] $Repo,
        [string] $ReadmePath,
        [string] $Manifest,
        [string] $Branch,
        [switch] $ReuseImageBuilderImage
    )

    $onTagsGenerated = {
        param($ContainerName)
        Exec "docker cp ${ContainerName}:/repo/$ReadmePath $repoRoot/$ReadmePath"
    }

    $imageBuilderArgs = "generateTagsReadme" `
        + " --update-readme" `
        + " --manifest $Manifest" `
        + " --repo $Repo" `
        + " --template ./scripts/documentation-templates/$Template" `
        + " $skipValidationOption" `
        + " https://github.com/dotnet/dotnet-docker/blob/master"

    & "$PSScriptRoot/Invoke-ImageBuilder.ps1" `
        -ImageBuilderArgs $imageBuilderArgs `
        -ReuseImageBuilderImage:$ReuseImageBuilderImage `
        -OnCommandExecuted $onTagsGenerated
}

GenerateDoc runtime-tags.md dotnet/framework/runtime README.runtime.md manifest.json
GenerateDoc sdk-tags.md dotnet/framework/sdk README.sdk.md manifest.json -ReuseImageBuilderImage
GenerateDoc samples-tags.md dotnet/framework/samples README.samples.md manifest.samples.json -ReuseImageBuilderImage

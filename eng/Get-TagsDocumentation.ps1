#!/usr/bin/env pwsh
$ErrorActionPreference = 'Stop'

$gitRepo = "https://github.com/microsoft/dotnet-framework-docker"

& $PSScriptRoot/common/Invoke-ReadmeGeneration.ps1 `
    dotnet/framework/runtime README.runtime.md manifest.runtime-sdk.json $gitRepo
& $PSScriptRoot/common/Invoke-ReadmeGeneration.ps1 `
    dotnet/framework/sdk README.sdk.md manifest.runtime-sdk.json $gitRepo -ReuseImageBuilderImage
& $PSScriptRoot/common/Invoke-ReadmeGeneration.ps1 `
    dotnet/framework/wcf README.wcf.md manifest.wcf.json $gitRepo -ReuseImageBuilderImage
& $PSScriptRoot/common/Invoke-ReadmeGeneration.ps1 `
    dotnet/framework/samples README.samples.md manifest.samples.json $gitRepo -ReuseImageBuilderImage

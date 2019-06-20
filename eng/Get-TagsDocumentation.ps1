#!/usr/bin/env pwsh
$ErrorActionPreference = 'Stop'

$gitRepo = "https://github.com/microsoft/dotnet-framework-docker"

& $PSScriptRoot/common/Invoke-ReadmeGeneration.ps1 dotnet/framework/runtime README.runtime.md manifest.json master $gitRepo
& $PSScriptRoot/common/Invoke-ReadmeGeneration.ps1 dotnet/framework/sdk README.sdk.md manifest.json master $gitRepo -ReuseImageBuilderImage
& $PSScriptRoot/common/Invoke-ReadmeGeneration.ps1 dotnet/framework/samples README.samples.md manifest.samples.json master $gitRepo -ReuseImageBuilderImage

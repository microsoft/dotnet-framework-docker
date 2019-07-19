#
# Copyright (c) .NET Foundation and contributors. All rights reserved.
# Licensed under the MIT license. See LICENSE file in the project root for full license information.
#

[cmdletbinding()]
param(
    [string]$VersionFilter,
    [string]$OSFilter,
    [string]$Registry,
    [string]$RepoPrefix,
    [switch]$IsLocalRun
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

# Install the .NET Core SDK
$dotnetInstallDir = "$PSScriptRoot/../.dotnet"
if (!(Test-Path "$dotnetInstallDir")) {
    mkdir "$dotnetInstallDir" | Out-Null
}

$dotnetInstallScript = "dotnet-install.ps1";
$dotnetInstallScriptPath = "$dotnetInstallDir/$DotnetInstallScript"
if (!(Test-Path $dotnetInstallScriptPath)) {
    $dotnetInstallScriptUrl = "https://raw.githubusercontent.com/dotnet/cli/release/2.1/scripts/obtain/$dotnetInstallScript"
    Invoke-WebRequest $dotnetInstallScriptUrl -OutFile $dotnetInstallScriptPath
}

& $dotnetInstallScriptPath -Channel "2.1" -Version "latest" -Architecture x64 -InstallDir $dotnetInstallDir
if ($LASTEXITCODE -ne 0) { throw "Failed to install the .NET Core SDK" }

# Run Tests
$env:IMAGE_OS_FILTER = $OSFilter
$env:IMAGE_VERSION_FILTER = $VersionFilter
$env:REGISTRY = $Registry
$env:REPO_PREFIX = $RepoPrefix

if ($IsLocalRun) {
    $env:LOCAL_RUN = 1
}

& dotnet test --filter -c Release --logger:trx $PSScriptRoot/Microsoft.DotNet.Framework.Docker.Tests/Microsoft.DotNet.Framework.Docker.Tests.csproj
if ($LASTEXITCODE -ne 0) { throw "Tests Failed" }

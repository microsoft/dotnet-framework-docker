#
# Copyright (c) .NET Foundation and contributors. All rights reserved.
# Licensed under the MIT license. See LICENSE file in the project root for full license information.
#

[cmdletbinding()]
param(
    [string]$VersionFilter,
    [string]$OSFilter,
    [string]$RepoOwner
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
    $dotnetInstallScriptUrl = "https://raw.githubusercontent.com/dotnet/cli/release/2.0.0/scripts/obtain/$dotnetInstallScript"
    Invoke-WebRequest $dotnetInstallScriptUrl -OutFile $dotnetInstallScriptPath
}

& $dotnetInstallScriptPath -Channel "release-2.0.0" -Version "2.0.0" -Architecture x64 -InstallDir $dotnetInstallDir
if ($LASTEXITCODE -ne 0) { throw "Failed to install the .NET Core SDK" }

# Run Tests
$env:IMAGE_OS_FILTER = $OSFilter
$env:IMAGE_VERSION_FILTER = $VersionFilter
$env:REPO_OWNER = $RepoOwner

& dotnet test -c Release -v n $PSScriptRoot/Microsoft.DotNet.Framework.Docker.Tests/Microsoft.DotNet.Framework.Docker.Tests.csproj
if ($LASTEXITCODE -ne 0) { throw "Tests Failed" }

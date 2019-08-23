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
    [array]$TestCategories,
    [switch]$IsLocalRun,
    [string]$ImageInfoPath
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
$env:IMAGE_INFO_PATH = $ImageInfoPath

if ($IsLocalRun) {
    $env:LOCAL_RUN = 1
}
else {
    $env:LOCAL_RUN = $null
}

# Powershell treats each variable passed to the command line call as an individual argument.  But we're constructing the $testFilter variable
# which actually consists of multiple command line arguments.  This causes the parsing to not interpret the command correctly.  To solve this,
# we need to construct the parameters as an array rather than use variables in a direct call to the command line execution.
$testArgs = @()
$testFilter = ""
if ($TestCategories) {
    # Construct an expression that filters the test to each of the selected TestCategories (using an OR operator between each category)
    # See https://docs.microsoft.com/en-us/dotnet/core/testing/selective-unit-tests
    $TestCategories | foreach {
        if ($testFilter) {
            $testFilter += "|"
        }

        $testFilter += "Category=$_"
    }

    $testArgs += "--filter"
    $testArgs += "`"$testFilter`""
}

$testArgs += "-c"
$testArgs += "Release"
$testArgs += "--logger:trx"
$testArgs += "$PSScriptRoot/Microsoft.DotNet.Framework.Docker.Tests/Microsoft.DotNet.Framework.Docker.Tests.csproj"

& dotnet test $testArgs
if ($LASTEXITCODE -ne 0) { throw "Tests Failed" }

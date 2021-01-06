#
# Copyright (c) .NET Foundation and contributors. All rights reserved.
# Licensed under the MIT license. See LICENSE file in the project root for full license information.
#

[cmdletbinding()]
param(
    [string]$Version,
    [string]$Architecture,
    [string]$OS,
    [string]$Registry,
    [string]$RepoPrefix,
    [switch]$PullImages,
    [string]$ImageInfoPath,
    [ValidateSet('runtime', 'sdk', 'aspnet', 'wcf', 'pre-build')]
    [string[]]$TestCategories = @("runtime", "sdk", "aspnet", "wcf")
)

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

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

# Install the .NET Core SDK
$DotnetInstallDir = "$PSScriptRoot/../.dotnet"
& $PSScriptRoot/../eng/common/Install-DotNetSdk.ps1 $dotnetInstallDir

# Run Tests
$env:IMAGE_OS = $OS
$env:IMAGE_VERSION = $Version
$env:REGISTRY = $Registry
$env:REPO_PREFIX = $RepoPrefix
$env:IMAGE_INFO_PATH = $ImageInfoPath
$env:SOURCE_REPO_ROOT = (Get-Item "$PSScriptRoot").Parent.FullName

if ($PullImages) {
    $env:PULL_IMAGES = 1
}
else {
    $env:PULL_IMAGES = $null
}

$testFilter = ""
if ($TestCategories) {
    # Construct an expression that filters the test to each of the
    # selected TestCategories (using an OR operator between each category).
    # See https://docs.microsoft.com/en-us/dotnet/core/testing/selective-unit-tests
    $TestCategories | foreach {
        # Skip pre-build tests on Windows because of missing pre-reqs (https://github.com/dotnet/dotnet-docker/issues/2261)
        if ($_ -eq "pre-build" -and $activeOS -eq "windows") {
            Write-Warning "Skipping pre-build tests for Windows containers"
        } else {
            if ($testFilter) {
                $testFilter += "|"
            }

            $testFilter += "Category=$_"
        }
    }

    if (-not $testFilter) {
        exit;
    }

    $testFilter = "--filter `"$testFilter`""
}

Exec "$DotnetInstallDir/dotnet test $testFilter -c Release --logger:trx $PSScriptRoot/Microsoft.DotNet.Framework.Docker.Tests/Microsoft.DotNet.Framework.Docker.Tests.csproj"

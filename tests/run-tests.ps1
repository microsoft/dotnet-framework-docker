#
# Copyright (c) .NET Foundation and contributors. All rights reserved.
# Licensed under the MIT license. See LICENSE file in the project root for full license information.
#

[cmdletbinding()]
param(
    [Parameter(ParameterSetName = "Version")]
    [string]$Version = "*",

    [Parameter(ParameterSetName = "Paths")]
    [string[]]$Paths = @(),

    [string]$Architecture,
    
    [string[]]$OSVersions = @(),
    
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

function GetPath {
    param ([string] $osVersion)

    return "src/*/$Version/$osVersion"
}

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

# Install the .NET Core SDK
$DotnetInstallDir = "$PSScriptRoot/../.dotnet"
& $PSScriptRoot/../eng/common/Install-DotNetSdk.ps1 $dotnetInstallDir

$activeOS = docker version -f "{{ .Server.Os }}"

if ($OSVersions.Count -gt 1) {
    throw "Multiple OS versions are not supported"
}

# Run Tests
$env:IMAGE_OS = "*"
if ($OSVersions.Count -gt 0) {
    $env:IMAGE_OS = $OSVersions[0]
}
$env:REGISTRY = $Registry
$env:REPO_PREFIX = $RepoPrefix
$env:IMAGE_INFO_PATH = $ImageInfoPath
$env:SOURCE_REPO_ROOT = (Get-Item "$PSScriptRoot").Parent.FullName

if ($PSCmdlet.ParameterSetName -eq "Version") {
    if ($OSVersions -and $OSVersions.Count -gt 0) {
        foreach ($osVersion in $OSVersions) {
            $Paths += $(GetPath $osVersion)
        }
    }
    else {
        $Paths += GetPath "*"
    }
}

$env:DOCKERFILE_PATHS = $($Paths -Join ",")

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
    # See https://docs.microsoft.com/dotnet/core/testing/selective-unit-tests
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

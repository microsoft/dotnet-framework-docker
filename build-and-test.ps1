#!/usr/bin/env pwsh
[cmdletbinding()]
param(
    [string]$VersionFilter = "*",
    [string]$OSFilter = "*",
    [string]$ImageBuilderCustomArgs,
    [switch]$CleanupDocker,
    [switch]$SkipTesting = $false
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

function Invoke-CleanupDocker()
{
    if ($CleanupDocker) {
        ./scripts/Invoke-CleanupDocker.ps1
    }
}

pushd $PSScriptRoot
try {
    Invoke-CleanupDocker
    ./scripts/Invoke-ImageBuilder.ps1 "build --path '$VersionFilter/*/$OSFilter' $ImageBuilderCustomArgs"

    if (-not $SkipTesting) {
        ./tests/run-tests.ps1 -VersionFilter $VersionFilter -OSFilter $OSFilter -IsLocalRun
    }
}
finally {
    Invoke-CleanupDocker
    popd
}

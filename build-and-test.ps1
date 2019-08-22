#!/usr/bin/env pwsh
[cmdletbinding()]
param(
    [ValidateSet('*', 'runtime-sdk', 'aspnet', 'wcf')]
    [string]$ImageTypeFilter = '*',
    [string]$VersionFilter = "*",
    [string]$OSFilter = "*",
    [string]$OptionalImageBuilderArgs,
    [switch]$SkipTesting = $false
)

if ($ImageTypeFilter -eq "*") {
    $PathFilters = $null
}
elseif ($ImageTypeFilter -eq "runtime-sdk") {
    $PathFilters = "--path '$VersionFilter/runtime/$OSFilter' --path '$VersionFilter/sdk/$OSFilter'"
}
else {
    $PathFilters = "--path '$VersionFilter/$ImageTypeFilter/$OSFilter'"
}

$OptionalImageBuilderArgs += " --manifest manifest.json"

& ./eng/common/build-and-test.ps1 `
    -VersionFilter $VersionFilter `
    -OSFilter $OSFilter `
    -PathFilters $PathFilters `
    -OptionalImageBuilderArgs $OptionalImageBuilderArgs `
    -SkipTesting:$SkipTesting `
    -ExcludeArchitecture

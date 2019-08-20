#!/usr/bin/env pwsh
[cmdletbinding()]
param(
    [ValidateSet('default', 'samples')]
    [string]$ManifestType = 'default',
    [string]$VersionFilter = "*",
    [string]$OSFilter = "*",
    [string]$OptionalImageBuilderArgs,
    [switch]$SkipTesting = $false
)

if ($ManifestType -eq 'default') {
    $manifest = 'manifest.json'
}
elseif ($ManifestType -eq 'samples') {
    $manifest = 'manifest.samples.json'
}

$OptionalImageBuilderArgs += " --manifest $manifest"

& ./eng/common/build-and-test.ps1 `
    -VersionFilter $VersionFilter `
    -OSFilter $OSFilter `
    -OptionalImageBuilderArgs $OptionalImageBuilderArgs `
    -SkipTesting:$SkipTesting `
    -ExcludeArchitecture

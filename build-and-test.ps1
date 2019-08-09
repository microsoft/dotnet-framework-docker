#!/usr/bin/env pwsh
[cmdletbinding()]
param(
    [ValidateSet('aspnet','wcf','samples')]
    [string]$ManifestType,
    [string]$VersionFilter = "*",
    [string]$OSFilter = "*",
    [string]$OptionalImageBuilderArgs,
    [switch]$SkipTesting = $false
)

$manifestTypeExt = $ManifestType
if (-not [string]::IsNullOrEmpty($manifestType)) {
    $manifestTypeExt += '.'
    $testCategory = $ManifestType
}
else {
    $testCategory = "runtime"
}

$OptionalImageBuilderArgs += " --manifest manifest.${manifestTypeExt}json"

& ./eng/common/build-and-test.ps1 `
    -VersionFilter $VersionFilter `
    -OSFilter $OSFilter `
    -OptionalImageBuilderArgs $OptionalImageBuilderArgs `
    -OptionalTestArgs "-TestCategory $testCategory" `
    -SkipTesting:$SkipTesting `
    -ExcludeArchitecture

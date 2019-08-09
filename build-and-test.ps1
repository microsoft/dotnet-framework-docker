#!/usr/bin/env pwsh
[cmdletbinding()]
param(
    [ValidateSet('runtime-sdk', 'aspnet', 'wcf', 'samples')]
    [string]$ManifestType = 'runtime-sdk',
    [string]$VersionFilter = "*",
    [string]$OSFilter = "*",
    [string]$OptionalImageBuilderArgs,
    [switch]$SkipTesting = $false
)

$OptionalImageBuilderArgs += " --manifest manifest.${ManifestType}.json"

& ./eng/common/build-and-test.ps1 `
    -VersionFilter $VersionFilter `
    -OSFilter $OSFilter `
    -OptionalImageBuilderArgs $OptionalImageBuilderArgs `
    -OptionalTestArgs "-TestCategory $ManifestType" `
    -SkipTesting:$SkipTesting `
    -ExcludeArchitecture

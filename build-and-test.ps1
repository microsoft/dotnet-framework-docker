#!/usr/bin/env pwsh
[cmdletbinding(
    DefaultParameterSetName = "BuildAndTest"
)]
param(
    [ValidateSet("runtime", "sdk", "aspnet", "wcf")]
    [string[]]$Repos = @(),
    [string]$Version = "*",
    [string]$OS = "*",
    [Parameter(ParameterSetName = "Build")]
    [switch]$BuildOnly,
    [Parameter(ParameterSetName = "Test")]
    [switch]$TestOnly,
    [Parameter(ParameterSetName = "Build")]
    [Parameter(ParameterSetName = "BuildAndTest")]
    [string]$OptionalImageBuilderArgs
)

if ($PSCmdlet.ParameterSetName -eq "BuildAndTest") {
    $build = $true
    $test = $true
}
else {
    $build = $BuildOnly
    $test = $TestOnly
}

if ($Repos.Count -eq 0) {
    $Path = $null
    $testCategories = @()
}
else {
    $Path = ""
    $Repos | foreach {
        $Path += " --path '$Version/$_/$OS'"
    }
    $testCategories = $Repos
}

if ($build) {
    & ./eng/common/build.ps1 `
        -Version $Version `
        -OS $OS `
        -Path $Path `
        -OptionalImageBuilderArgs $OptionalImageBuilderArgs
}
if ($test) {
    & ./tests/run-tests.ps1 `
        -Version $Version `
        -OS $OS `
        -TestCategories $testCategories
}

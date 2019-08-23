#!/usr/bin/env pwsh
[cmdletbinding()]
param(
    [ValidateSet('runtime', 'sdk', 'aspnet', 'wcf')]
    [string[]]$RepoFilter = @(),
    [string]$VersionFilter = "*",
    [string]$OSFilter = "*",
    [string]$OptionalImageBuilderArgs,
    [switch]$SkipTesting = $false
)

if ($RepoFilter.Count -eq 0) {
    $PathFilters = $null
    $optionalTestArgs = ""
}
else {
    $PathFilters = ""
    $RepoFilter | foreach {
        $PathFilters += " --path '$VersionFilter/$_/$OSFilter'"
    }
    # Convert the array to a comma-delimited string of the repos with each repo value in quotes
    # (e.g. "runtime", "sdk")
    $repoList = ($RepoFilter | foreach { "`"$_`"" }) -join ", "
    $optionalTestArgs = "-TestCategories @($repoList)"
}

$OptionalImageBuilderArgs += " --manifest manifest.json"

& ./eng/common/build-and-test.ps1 `
    -VersionFilter $VersionFilter `
    -OSFilter $OSFilter `
    -PathFilters $PathFilters `
    -OptionalImageBuilderArgs $OptionalImageBuilderArgs `
    -OptionalTestArgs $optionalTestArgs `
    -SkipTesting:$SkipTesting `
    -ExcludeArchitecture

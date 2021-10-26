#!/usr/bin/env pwsh
param(
    # Docker repositories (image variants) to filter by
    [ValidateSet("runtime", "sdk", "aspnet", "wcf")]
    [string[]]$Repos = @(),

    # Version of .NET Fx to filter by
    [string]$Version = "*",

    # Name of OS to filter by
    [string]$OS = "*",

    # Additional args to pass to ImageBuilder
    [string]$OptionalImageBuilderArgs,

    # Execution mode of the script
    [ValidateSet("BuildAndTest", "Build", "Test")]
    [string]$Mode = "BuildAndTest"
)

if ($Repos.Count -eq 0) {
    $Path = $null
}
else {
    $Path = @()
    $Repos | foreach {
        $Path += "src/$_/$Version/$OS"
    }
    $testCategories = $Repos
}

if ($Mode -eq "BuildAndTest" -or $Mode -eq "Build") {
    & ./eng/common/build.ps1 `
        -Version $Version `
        -OS $OS `
        -Path $Path `
        -OptionalImageBuilderArgs $OptionalImageBuilderArgs
}

if ($Mode -eq "BuildAndTest" -or $Mode -eq "Test") {
    $testArgs = @{
        Version = $Version;
        OS = $OS;
    }

    if ($testCategories) {
        $testArgs.Add("TestCategories", $testCategories)
    }

    & ./tests/run-tests.ps1 @testArgs
}

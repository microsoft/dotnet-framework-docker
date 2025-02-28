#!/usr/bin/env pwsh
param(
    [switch]$Validate
)

if (!$IsWindows) {
    $IsWindows = $PSVersionTable.PSEdition -eq "Desktop"
}

$imageBuilderArgs = "generateDockerfiles --optional-templates"
if ($Validate) {
    $imageBuilderArgs += " --validate"
}

$repoRoot = (Get-Item "$PSScriptRoot").Parent.Parent.FullName
$onDockerfilesGeneratedLinux = {
    param($ContainerName)

    if (-Not $Validate) {
        Exec "docker cp ${ContainerName}:/repo/src $repoRoot"
    }
}

if ($IsWindows) {
    & $PSScriptRoot/../common/Invoke-ImageBuilder.ps1 `
        -ImageBuilderArgs $imageBuilderArgs
} else {
    & $PSScriptRoot/../common/Invoke-ImageBuilder.ps1 `
        -ImageBuilderArgs $imageBuilderArgs `
        -OnCommandExecuted $onDockerfilesGeneratedLinux
}

#!/usr/bin/env pwsh
param(
    [switch]$Validate
)

$dockerOs = docker version -f "{{ .Server.Os }}"

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

# On Windows, ImageBuilder is run locally due to limitations with running Docker client within a container.
# Remove when https://github.com/dotnet/docker-tools/issues/159 is resolved
if ($dockerOs -eq "windows") {
    & $PSScriptRoot/../common/Invoke-ImageBuilder.ps1 `
        -ImageBuilderArgs $imageBuilderArgs
} else {
    & $PSScriptRoot/../common/Invoke-ImageBuilder.ps1 `
        -ImageBuilderArgs $imageBuilderArgs `
        -OnCommandExecuted $onDockerfilesGeneratedLinux
}

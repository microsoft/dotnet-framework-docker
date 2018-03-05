param(
    [string]$Branch='master',
    [string]$ImageBuilderImageName='microsoft/dotnet-buildtools-prereqs:image-builder-debian-20180228165057'
)

$ErrorActionPreference = 'Stop'
$repoRoot = Split-Path -Path "$PSScriptRoot" -Parent

& docker pull $ImageBuilderImageName

& docker run --rm `
    -v /var/run/docker.sock:/var/run/docker.sock `
    -v "${repoRoot}:/repo" `
    -w /repo `
    $ImageBuilderImageName `
    generateTagsReadme --update-readme "https://github.com/Microsoft/dotnet-framework-docker/blob/${Branch}"

param(
    [string]$Branch='master',
    [string]$Manifest='manifest.json',
    [string]$ImageBuilderImageName='microsoft/dotnet-buildtools-prereqs:image-builder-debian-20180312170813'
)

$ErrorActionPreference = 'Stop'
$repoRoot = Split-Path -Path "$PSScriptRoot" -Parent

& docker pull $ImageBuilderImageName

& docker run --rm `
    -v /var/run/docker.sock:/var/run/docker.sock `
    -v "${repoRoot}:/repo" `
    -w /repo `
    $ImageBuilderImageName `
    generateTagsReadme --update-readme --manifest $Manifest "https://github.com/Microsoft/dotnet-framework-docker/blob/${Branch}"

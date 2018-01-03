[cmdletbinding()]
param(
    [string]$VersionFilter,
    [string]$OSFilter,
    [switch]$IsDryRun
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$(docker version) | ForEach-Object { Write-Host "$_" }

$buildFilter = "*"
if (-not [string]::IsNullOrEmpty($VersionFilter))
{
    $buildFilter = "$VersionFilter-$buildFilter"
}
if (-not [string]::IsNullOrEmpty($OSFilter))
{
    $buildFilter = "$buildFilter$OSFilter/*"
}

$imageBuilderFolder = ".Microsoft.DotNet.ImageBuilder"
if (-not (Test-Path -Path "$imageBuilderFolder" -PathType Container))
{
    New-Item -Path "$imageBuilderFolder" -ItemType Directory -Force
    $imageBuilderImageName = 'microsoft/dotnet-buildtools-prereqs:image-builder-nanoserver-20180103080524'
    $imageBuilderContainerName = "ImageBuilder-$(Get-Date -Format yyyyMMddhhmmss)"

    Invoke-Expression "docker pull $imageBuilderImageName"
    Invoke-Expression "docker create --name $imageBuilderContainerName $imageBuilderImageName"
    Invoke-Expression "docker cp ${imageBuilderContainerName}:/image-builder $imageBuilderFolder"
    Invoke-Expression "docker rmi -f $imageBuilderImageName"
}

$imageBuilderExe = [System.IO.Path]::Combine($imageBuilderFolder, "image-builder", "Microsoft.DotNet.ImageBuilder.exe")
$imageBuilderArgs = 'build --repo microsoft/dotnet-framework --path "$buildFilter"'
if ($IsDryRun)
{
    $imageBuilderArgs += " --dry-run"
}

Invoke-Expression "$imageBuilderExe $imageBuilderArgs"
if ($LastExitCode -ne 0) {
    throw "Failed executing $imageBuilderExe $imageBuilderArgs"
}

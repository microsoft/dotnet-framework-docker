[cmdletbinding()]
param(
    [string]$VersionFilter = "*",
    [string]$OSFilter = "*",
    [string]$ImageBuilderCustomArgs
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$buildFilter = "$VersionFilter-$OSFilter/*"

function ExecuteWithRetry {
    param(
        [Parameter(Mandatory=$true)]
        [string]$Command
    )

    $attempt = 0
    $maxRetries = 5
    $waitFactor = 6
    while ($attempt -lt $maxRetries) {
        try {
            Invoke-Expression -Command "$Command"
            break
        }
        catch {
            Write-Warning "$_"
        }

        $attempt++
        if ($attempt -ne $maxRetries) {
            $waitTime = $attempt * $waitFactor
            Write-Host "Retry ${attempt}/${maxRetries} failed, retrying in $waitTime seconds..."
            Start-Sleep -Seconds ($waitTime)
            Remove-Item -Path "$imageBuilderFolder" -Recurse -Force -ErrorAction Continue
        }
        else {
            throw "Retry ${attempt}/${maxRetries} failed, no more retries left."
        }
    }
}

function Get-ImageBuilder() {
    $imageBuilderImageName = 'microsoft/dotnet-buildtools-prereqs:image-builder-nanoserver-20180111160403'
    $imageBuilderContainerName = "ImageBuilder-$(Get-Date -Format yyyyMMddhhmmss)"
    New-Item -Path "$imageBuilderFolder" -ItemType Directory -Force | Out-Null
    ExecuteWithRetry -Command "docker pull $imageBuilderImageName"
    Invoke-Expression "docker create --name $imageBuilderContainerName $imageBuilderImageName"
    # Copying the 'image-builder' folder and not just the content due to https://github.com/moby/moby/issues/34638
    Invoke-Expression "docker cp ${imageBuilderContainerName}:/image-builder $imageBuilderFolder"
    Invoke-Expression "docker rmi -f $imageBuilderImageName"
}

$imageBuilderFolder = [System.IO.Path]::Combine("$PSScriptRoot", ".Microsoft.DotNet.ImageBuilder")
$imageBuilderExe = [System.IO.Path]::Combine("$imageBuilderFolder", "image-builder", "Microsoft.DotNet.ImageBuilder.exe")
if (-not (Test-Path -Path "$imageBuilderExe" -PathType Leaf)) {
    Get-ImageBuilder
}

$imageBuilderArgs = "build --path $buildFilter --var VersionFilter=$VersionFilter --var OSFilter=$OSFilter"
if (-not [string]::IsNullOrWhiteSpace($ImageBuilderCustomArgs)) {
    $imageBuilderArgs += " $ImageBuilderCustomArgs"
}

Invoke-Expression "$imageBuilderExe $imageBuilderArgs"
if ($LastExitCode -ne 0) {
    throw "Failed executing $imageBuilderExe $imageBuilderArgs"
}

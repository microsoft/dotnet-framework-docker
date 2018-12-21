param(
    [string]$Branch='master',
    [string]$ImageBuilderImageName='microsoft/dotnet-buildtools-prereqs:image-builder-debian-20181221161902'
)

$ErrorActionPreference = 'Stop'
$repoRoot = Split-Path -Path "$PSScriptRoot" -Parent

function GenerateDoc {
    param ([string] $Manifest, [string] $Repo, [string] $ReadmePath, [string] $Template)

    & docker run --rm `
        -v /var/run/docker.sock:/var/run/docker.sock `
        -v "${repoRoot}:/repo" `
        -w /repo `
        $ImageBuilderImageName `
            generateTagsReadme `
            --manifest $Manifest `
            --repo $Repo `
            --update-readme `
            --readme-path $ReadmePath `
            --template $Template `
            "https://github.com/Microsoft/dotnet-framework-docker/blob/${Branch}"
}

& docker pull $ImageBuilderImageName

GenerateDoc `
    -Manifest manifest.json `
    -Repo microsoft/dotnet-framework `
    -ReadmePath ./README.md `
    -Template ./scripts/ReadmeTagsDocumentationTemplate.md

GenerateDoc `
    -Manifest manifest.samples.json `
    -Repo microsoft/dotnet-framework-samples `
    -ReadmePath ./samples/README.DockerHub.md `
    -Template ./scripts/SamplesReadmeTagsDocumentationTemplate.md

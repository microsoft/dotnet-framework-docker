# Patch Tuesday Release

## Tasks

1. - [ ] Ensure all build agent scale sets have the appropriate number of machines allocated for a build
1. - [ ] Merge any pending PRs or commits from `dev` branch:
      - [ ] &lt;placeholder link&gt;
1. - [ ] Look up the latest recommended [NuGet CLI version](https://www.nuget.org/downloads) and set the `nuget|version` variable to that version in the [manifest.versions.json](https://github.com/microsoft/dotnet-framework-docker/blob/main/manifest.versions.json) file.
1. - [ ] Wait for latest cumulative updates (LCUs) to be released (typically at 10 AM PST on Patch Tuesday).
1. - [ ] Gather list of KB numbers for the .NET Framework updates from the .NET Release team.
1. - [ ] Look up the download URL for each of the KB numbers in [Microsoft Update Catalog](https://www.catalog.update.microsoft.com/) and input them into the [manifest.versions.json](https://github.com/microsoft/dotnet-framework-docker/blob/main/manifest.versions.json) file. If this is the first Patch Tuesday after the release of a new Windows version, you'll need to do the following for that version:
      - [ ] Add a new entry to the [manifest.versions.json](https://github.com/microsoft/dotnet-framework-docker/blob/main/manifest.versions.json) file to associate a URL for the new Windows version.
1. - [ ] Update the datestamp variables in the [manifest.json file](https://github.com/microsoft/dotnet-framework-docker/blob/main/manifest.json). The datestamp for a particular image's tag should only be updated if any of the following conditions are met:
      * The version of .NET Framework in the image has been serviced since the last release. (This includes servicing directly within the Dockerfile or from the base Windows image, see https://github.com/microsoft/dotnet-framework-docker/issues/783.)
      * Changes within the .NET Framework Dockerfile have caused the content of the image to be materially changed since the last release (e.g. new version of VS Build Tools, new version of NuGet CLI).
1. - [ ] Run the `update-dependencies` tool to update all the necessary files:
      - [ ] `dotnet run --project .\eng\update-dependencies`
1. - [ ] Inspect generated changes for correctness
1. - [ ] Commit generated changes
1. - [ ] Create PR
1. - [ ] Get PR signoff
1. - [ ] Merge PR
1. - [ ] Wait for changes to be mirrored to internal [dotnet-framework-docker repo](https://dev.azure.com/dnceng/internal/_git/Microsoft-dotnet-framework-docker) (internal MSFT link)
1. - [ ] Run the [`Get-BaseImageStatus.ps1`](https://github.com/microsoft/dotnet-framework-docker/blob/main/eng/common/Get-BaseImageStatus.ps1) script and wait until the Windows images have been updated as part of the Windows Patch Tuesday release process. This script will display when the dependent Windows images were last updated. Wait until all the images show that they have been recently updated. "Recently updated" amounts to be having been updated within the past week or so; images from a month ago should be considered to be the old version.

          ./eng/common/Get-BaseImageStatus.ps1 -Continuous
1. - [ ] Queue build of [dotnet-framework-docker pipeline](https://dev.azure.com/dnceng/internal/_build?definitionId=372) (internal MSFT link)
1. - [ ] Confirm READMEs have been updated in Docker Hub for [microsoft-dotnet-framework](https://hub.docker.com/_/microsoft-dotnet-framework)
1. - [ ] Confirm build for [dotnet-docker-framework-samples](https://dev.azure.com/dnceng/internal/_build?definitionId=374) (internal MSFT link) was queued. This will be queued automatically by [dotnet-docker-tools-check-base-image-updates](https://dev.azure.com/dnceng/internal/_build?definitionId=536) when it detects that the product images have been updated (detection runs on a schedule). Alternatively, you can manually queue the samples build.
1. - [ ] Confirm `Last Modified` field has been updated in Docker Hub for [microsoft-dotnet-framework-samples](https://hub.docker.com/_/microsoft-dotnet-framework-samples/)
1. - [ ] Reply to .NET Release team with a status update email
1. - [ ] Deallocate any extra scale set build agent machines if necessary

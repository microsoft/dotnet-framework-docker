# Patch Tuesday Release

## Tasks

1. - [ ] Ensure all build agent scale sets have the appropriate number of machines allocated for a build
1. - [ ] Merge any pending PRs or commits from `dev` branch:
      - [ ] &lt;placeholder link&gt;
1. - [ ] Look up the latest [NuGet CLI versions](https://www.nuget.org/downloads) and update the NuGet entries of the [manifest.versions.json](https://github.com/microsoft/dotnet-framework-docker/blob/master/manifest.versions.json) file with the latest patch of each major/minor version listed in the file.
1. - [ ] Wait for latest cumulative updates (LCUs) to be released (typically at 10 AM PST on Patch Tuesday).
1. - [ ] Gather list of KB numbers for the .NET Framework updates from the .NET Release team.
1. - [ ] Look up the download URL for each of the KB numbers in [Microsoft Update Catalog](https://www.catalog.update.microsoft.com/) and input them into the [manifest.versions.json](https://github.com/microsoft/dotnet-framework-docker/blob/master/manifest.versions.json) file. If this is the first Patch Tuesday after the release of a new Windows version, you'll need to do the following for that version:
      - [ ] Add a new entry to the [manifest.versions.json](https://github.com/microsoft/dotnet-framework-docker/blob/master/manifest.versions.json) file to associate a URL for the new Windows version.
1. - [ ] Run the `update-dependencies` tool to update all the necessary files:
      - [ ] `dotnet run --project .\eng\update-dependencies --datestamp-all <YYYYMMDD>`
1. - [ ] Inspect generated changes for correctness
1. - [ ] Commit generated changes
1. - [ ] Create PR
1. - [ ] Get PR signoff
1. - [ ] Merge PR
1. - [ ] Wait for changes to be mirrored to internal [dotnet-framework-docker repo](https://dev.azure.com/dnceng/internal/_git/Microsoft-dotnet-framework-docker) (internal MSFT link)
1. - [ ] Run the [`Get-BaseImageStatus.ps1`](https://github.com/microsoft/dotnet-framework-docker/blob/master/eng/common/Get-BaseImageStatus.ps1) script and wait until the Windows images have been updated as part of the Windows Patch Tuesday release process. This script will display when the dependent Windows images were last updated. Wait until all the images show that they have been recently updated. "Recently updated" amounts to be having been updated within the past week or so; images from a month ago should be considered to be the old version.

          ./eng/common/Get-BaseImageStatus.ps1 -Continuous
1. - [ ] Queue build of [dotnet-framework-docker pipeline](https://dev.azure.com/dnceng/internal/_build?definitionId=372) (internal MSFT link)
1. - [ ] Confirm images have been ingested by MCR
1. - [ ] Confirm READMEs have been updated in Docker Hub for [microsoft-dotnet-framework](https://hub.docker.com/_/microsoft-dotnet-framework)
1. - [ ] Confirm build for [dotnet-docker-framework-samples](https://dev.azure.com/dnceng/internal/_build?definitionId=374) (internal MSFT link) was queued. This will be queued automatically by [dotnet-docker-tools-check-base-image-updates](https://dev.azure.com/dnceng/internal/_build?definitionId=536) when it detects that the product images have been updated (detection runs on a schedule). Alternatively, you can manually queue the samples build.
1. - [ ] Confirm sample images have been ingested by MCR
1. - [ ] Confirm `Last Modified` field has been updated in Docker Hub for [microsoft-dotnet-framework-samples](https://hub.docker.com/_/microsoft-dotnet-framework-samples/)
1. - [ ] Reply to .NET Release team with a status update email
1. - [ ] Deallocate any extra scale set build agent machines if necessary

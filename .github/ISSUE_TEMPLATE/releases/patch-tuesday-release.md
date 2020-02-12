# Patch Tuesday Release

## Tasks

1. - [ ] Merge any pending PRs or commits from `dev` branch:
      - [ ] &lt;placeholder link&gt;
1. - [ ] Look up the latest [NuGet CLI versions](https://www.nuget.org/downloads) and update the [nuget-info.json](https://github.com/microsoft/dotnet-framework-docker/blob/master/eng/nuget-info.json) file with the latest patch of each major/minor version listed in the file.
1. - [ ] Wait for latest cumulative updates (LCUs) to be released (typically at 10 AM PST on Patch Tuesday).
1. - [ ] Gather list of KB numbers for the .NET Framework updates from the .NET Release team.
1. - [ ] Look up the download URL for each of the KB numbers in [Microsoft Update Catalog](https://www.catalog.update.microsoft.com/) and input them into the [lcu-info.json](https://github.com/microsoft/dotnet-framework-docker/blob/master/eng/lcu-info.json) file. If this is the first Patch Tuesday after the release of a new Windows version, you'll need to do the following extra steps for that version:
      - [ ] Add a new entry to the [lcu-info.json](https://github.com/microsoft/dotnet-framework-docker/blob/master/eng/lcu-info.json) file to associate a URL for the new Windows version and `3.5` runtime version.
      - [ ] Update the `3.5/runtime` Dockerfile for the new Windows version so that the patch is applied (this can be copied from another `3.5/runtime` Dockerfile in a section labeled `Apply latest patch`)
1. - [ ] Run the `update-dependencies` tool to update all the necessary files:
      - [ ] `dotnet run --project .\eng\update-dependencies --datestamp-all <YYYYMMDD>`
1. - [ ] Inspect generated changes for correctness
1. - [ ] Commit generated changes
1. - [ ] Create PR
1. - [ ] Get PR signoff
1. - [ ] Merge PR
1. - [ ] Wait for changes to be mirrored to internal [dotnet-framework-docker repo](https://dev.azure.com/dnceng/internal/_git/Microsoft-dotnet-framework-docker) (internal MSFT link)
1. - [ ] Wait for the following Windows images to have been updated as part of the Windows Patch Tuesday release process (this begins at 10 AM PST on Patch Tuesday):
      - [ ] `mcr.microsoft.com/windows/servercore:1903`
      - [ ] `mcr.microsoft.com/windows/servercore:1909`
      - [ ] `mcr.microsoft.com/windows/servercore:ltsc2016`
      - [ ] `mcr.microsoft.com/windows/servercore:ltsc2019`
1. - [ ] Queue build of [dotnet-framework-docker pipeline](https://dev.azure.com/dnceng/internal/_build?definitionId=372) (internal MSFT link)
1. - [ ] Confirm images have been ingested by MCR
1. - [ ] Confirm READMEs have been updated in Docker Hub for [microsoft-dotnet-framework](https://hub.docker.com/_/microsoft-dotnet-framework)
1. - [ ] Confirm build for [dotnet-docker-framework-samples](https://dev.azure.com/dnceng/internal/_build?definitionId=374) (internal MSFT link) was queued. This will be queued automatically by [dotnet-docker-tools-check-base-image-updates](https://dev.azure.com/dnceng/internal/_build?definitionId=536) when it detects that the product images have been updated (detection runs on a schedule). Alternatively, you can manually queue the samples build.
1. - [ ] Confirm sample images have been ingested by MCR
1. - [ ] Confirm `Last Modified` field has been updated in Docker Hub for [microsoft-dotnet-framework-samples](https://hub.docker.com/_/microsoft-dotnet-framework-samples/)
1. - [ ] Reply to .NET Release team with a status update email

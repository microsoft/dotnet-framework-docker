# VS Tools Release

VS Version: &lt;version&gt;

## Tasks

_There is overlap between the tasks here and those for a [Patch Tuesday release](patch-tuesday-release.md). In the case where a VS release is on the same day as Patch Tuesday, the tasks should be combined into one workflow. Tasks specific to only a VS release have been **bolded**._

1. - [ ] Ensure all build agent scale sets have the appropriate number of machines allocated for a build
1. - [ ] Merge any pending PRs or commits from `dev` branch:
      - [ ] &lt;placeholder link&gt;
1. - [ ] Look up the latest recommended [NuGet CLI version](https://www.nuget.org/downloads) and set the `nuget|version` variable to that version in the [manifest.versions.json](https://github.com/microsoft/dotnet-framework-docker/blob/main/manifest.versions.json) file.
1. - [ ] **Update the `vs|version` entry of the [manifest.versions.json](https://github.com/microsoft/dotnet-framework-docker/blob/main/manifest.versions.json) to be the new VS version being released**
1. - [ ] **Wait for VS to be released**
1. - [ ] **Gather the Build Tools and Test Agent download URLs by navigating to https://visualstudio.microsoft.com/downloads. Download links can be found under the `Tools for Visual Studio` section. Prior to clicking the download button, open a network profiler like your browser's dev tools or Fiddler so that you can capture the URL that gets downloaded. For `Build Tools for Visual Studio` look for a file named `vs_BuildTools.exe` being downloaded. For `Agents for Visual Studio`, ensure the radio button is defaulted to `Agent` and look for a file named `vs_TestAgent.exe`. Copy those two URLs to the `vs|buildToolsUrl` and `vs|testAgentUrl` entries of the [manifest.versions.json](https://github.com/microsoft/dotnet-framework-docker/blob/main/manifest.versions.json).**
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
1. - [ ] Queue build of [dotnet-framework-docker pipeline](https://dev.azure.com/dnceng/internal/_build?definitionId=372) (internal MSFT link) with variables:

          imageBuilder.pathArgs: --path 'src/*/3.5/*' --path 'src/*/4.8/*' --path 'src/*/4.8.1/*'

1. - [ ] Confirm images have been ingested by MCR
1. - [ ] Confirm READMEs have been updated in Docker Hub for [microsoft-dotnet-framework](https://hub.docker.com/_/microsoft-dotnet-framework)
1. - [ ] Confirm build for [dotnet-docker-framework-samples](https://dev.azure.com/dnceng/internal/_build?definitionId=374) (internal MSFT link) was queued. This will be queued automatically by [dotnet-docker-tools-check-base-image-updates](https://dev.azure.com/dnceng/internal/_build?definitionId=536) when it detects that the product images have been updated (detection runs on a schedule). Alternatively, you can manually queue the samples build.
1. - [ ] Confirm `Last Modified` field has been updated in Docker Hub for [microsoft-dotnet-framework-samples](https://hub.docker.com/_/microsoft-dotnet-framework-samples/)
1. - [ ] Deallocate any extra scale set build agent machines if necessary

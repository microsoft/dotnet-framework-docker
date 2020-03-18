# VS Tools Release

VS Version: &lt;version&gt;

## Tasks

_There is overlap between the tasks here and those for a [Patch Tuesday release](patch-tuesday-release.md). In the case where a VS release is on the same day as Patch Tuesday, the tasks should be combined into one workflow. Tasks specific to only a VS release have been **bolded**._

1. - [ ] Merge any pending PRs or commits from `dev` branch:
      - [ ] &lt;placeholder link&gt;
1. - [ ] Look up the latest [NuGet CLI versions](https://www.nuget.org/downloads) and update the [nuget-info.json](https://github.com/microsoft/dotnet-framework-docker/blob/master/eng/nuget-info.json) file with the latest patch of each major/minor version listed in the file
1. - [ ] **Update the `vsVersion` in [vs-info.json](https://github.com/microsoft/dotnet-framework-docker/blob/master/eng/vs-info.json) to be the new VS version being released**
1. - [ ] **Wait for VS to be released**
1. - [ ] **Install or update the new version of VS on your local machine**
1. - [ ] **Create a zip file named `MSBuild.Microsoft.VisualStudio.Web.targets.<YYYY>.<MM>.zip` that contains the `MSBuild\Microsoft\VisualStudio\v16.0\Web` and `MSBuild\Microsoft\VisualStudio\v16.0\WebApplications` folders from the VS installation location**
1. - [ ] **Upload the zip file to https://dotnetbinaries.blob.core.windows.net/dockerassets**
1. - [ ] **Update the `webTargetsUrl` in [vs-info.json](https://github.com/microsoft/dotnet-framework-docker/blob/master/eng/vs-info.json) to reference the new zip file name**
1. - [ ] **Gather the Build Tools and Test Agent download URLs by navigating to https://visualstudio.microsoft.com/downloads. Download links can be found under the `Tools for Visual Studio` section. Prior to clicking the download button, open a network profiler like your browser's dev tools or Fiddler so that you can capture the URL that gets downloaded. For `Build Tools for Visual Studio` look for a file named `vs_BuildTools.exe` being downloaded. For `Agents for Visual Studio`, ensure the radio button is defaulted to `Agent` and look for a file named `vs_TestAgent.exe`. Copy those two URLs and paste them into [vs-info.json](https://github.com/microsoft/dotnet-framework-docker/blob/master/eng/vs-info.json).**
1. - [ ] Run the `update-dependencies` tool to update all the necessary files:
      - [ ] **`dotnet run --project .\eng\update-dependencies --datestamp-sdk <YYYYMMDD>` _Note that this uses a different option than a [Patch Tuesday release](patch-tuesday-release.md) because only the SDK images should be updated here. If VS and Patch Tuesday releases align, then `--datestamp-all` should be used._**
1. - [ ] Inspect generated changes for correctness
1. - [ ] Commit generated changes
1. - [ ] Create PR
1. - [ ] Get PR signoff
1. - [ ] Merge PR
1. - [ ] Wait for changes to be mirrored to internal [dotnet-framework-docker repo](https://dev.azure.com/dnceng/internal/_git/Microsoft-dotnet-framework-docker) (internal MSFT link)
1. - [ ] Queue build of [dotnet-framework-docker pipeline](https://dev.azure.com/dnceng/internal/_build?definitionId=372) (internal MSFT link)
1. - [ ] Confirm images have been ingested by MCR
1. - [ ] Confirm READMEs have been updated in Docker Hub for [microsoft-dotnet-framework](https://hub.docker.com/_/microsoft-dotnet-framework)
1. - [ ] Confirm build for [dotnet-docker-framework-samples](https://dev.azure.com/dnceng/internal/_build?definitionId=374) (internal MSFT link) was queued. This will be queued automatically by [dotnet-docker-tools-check-base-image-updates](https://dev.azure.com/dnceng/internal/_build?definitionId=536) when it detects that the product images have been updated (detection runs on a schedule). Alternatively, you can manually queue the samples build.
1. - [ ] Confirm sample images have been ingested by MCR
1. - [ ] Confirm `Last Modified` field has been updated in Docker Hub for [microsoft-dotnet-framework-samples](https://hub.docker.com/_/microsoft-dotnet-framework-samples/)

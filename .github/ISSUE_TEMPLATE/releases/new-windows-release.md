# New Windows Release

Windows version: &lt;version&gt;

## Prep Tasks

1. - [ ] Ensure a ["New Windows Release" issue](https://github.com/dotnet/docker-tools/blob/.github/ISSUE_TEMPLATE/releases/new-windows-release.md) exists for docker-tools repo
1. - [ ] Update [manifest.json](https://github.com/microsoft/dotnet-framework-docker/blob/master/manifest.json) to include entries for the new images
1. - [ ] Update [manifest.samples.json](https://github.com/microsoft/dotnet-framework-docker/blob/master/manifest.samples.json) to include the new Windows version for each of the samples
1. - [ ] Update the test data for each of the [test classes](https://github.com/microsoft/dotnet-framework-docker/tree/master/tests/Microsoft.DotNet.Framework.Docker.Tests) to include the new Windows version
1. - [ ] Look up the recommended latest [NuGet CLI version](https://www.nuget.org/downloads) and update the NuGet entries of the [manifest.versions.json](https://github.com/microsoft/dotnet-framework-docker/blob/master/manifest.versions.json) file to associate that NuGet version with the new version of Windows
1. - [ ] Run the `update-dependencies` tool to generate the new Dockerfiles and update all the necessary files:
      - [ ] `dotnet run --project .\eng\update-dependencies`
1. - [ ] Inspect generated changes for correctness
1. - [ ] Test the images
      1. - [ ] Create a local VM of the new Windows version
      1. - [ ] Clone this repo with the above changes onto the VM
      1. - [ ] Run `.\build-and-test.ps1 -OS windowsservercore-<VERSION>` to build and test your changes
1. - [ ] Revert any modifications that were made to the 3.5/runtime Dockerfile to support local testing due to the lack of the https://dotnetbinaries.blob.core.windows.net/dockerassets/microsoft-windows.netfx3-<VERSION>.zip file.
1. - [ ] Commit generated changes
1. - [ ] Create PR
1. - [ ] Get PR signoff. **Don't merge PR to master.**

## Release Day Tasks

1. - [ ] Create a [`microsoft-windows.netfx3-<VERSION>.zip](https://github.com/microsoft/dotnet-framework-docker/blob/1c3dd6638c6b827b81ffb13386b924f6dcdee533/3.5/runtime/windowsservercore-1909/Dockerfile#L11) file containing the .NET Fx 3.5 cab installer file and upload it to the [blob storage location](https://dotnetbinaries.blob.core.windows.net/dockerassets).
1. - [ ] Follow [Patch Tuesday release](patch-tuesday-release.md) process
1. - [ ] Create an announcement (example: [Windows Server, version 1909](https://github.com/microsoft/dotnet-framework-docker/issues/448))

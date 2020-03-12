# New Windows Release

Windows version: &lt;version&gt;

## Tasks

1. - [ ] Ensure a ["New Windows Release" issue](https://github.com/dotnet/docker-tools/blob/.github/ISSUE_TEMPLATE/releases/new-windows-release.md) exists for docker-tools repo
1. - [ ] Copy the Dockerfiles of the most recent published Windows version for `3.5` and `4.8` Dockerkfiles and place them in a version-specific folder under their respective variants (`runtime`, `sdk`, `aspnet`, `wcf`)
1. - [ ] Modify the Dockerfiles as appropriate for any specific changes related to the new Windows version
      - [ ] The `3.5/runtime` Dockerfile will need to have the `Apply latest patch` section removed since there is no patch to apply for a new Windows version
1. - [ ] Update [manifest.json](https://github.com/microsoft/dotnet-framework-docker/blob/master/manifest.json) to reference the new set of Dockerfiles with the appropriate tags
1. - [ ] Update [manifest.samples.json](https://github.com/microsoft/dotnet-framework-docker/blob/master/manifest.samples.json) to include the new Windows version for each of the samples
1. - [ ] Update the test data for each of the [test classes](https://github.com/microsoft/dotnet-framework-docker/tree/master/tests/Microsoft.DotNet.Framework.Docker.Tests) to include the new Windows version
1. - [ ] Look up the recommended latest [NuGet CLI version](https://www.nuget.org/downloads) and update the [nuget-info.json](https://github.com/microsoft/dotnet-framework-docker/blob/master/eng/nuget-info.json) file to associate that NuGet version with the new version of Windows
1. - [ ] Run the `update-dependencies` tool to update all the necessary files:
      - [ ] `dotnet run --project .\eng\update-dependencies\update-dependencies.csproj`
1. - [ ] Inspect generated changes for correctness
1. - [ ] Test the images
      1. - [ ] Create a local VM of the new Windows version
      1. - [ ] Clone this repo with the above changes onto the VM
      1. - [ ] Run `.\build-and-test.ps1 -OS windowsservercore-<VERSION>` to build and test your changes
1. - [ ] Commit generated changes
1. - [ ] Create PR
1. - [ ] Get PR signoff. **Don't merge PR to master.**
1. - [ ] On release day, follow [Patch Tuesday release](patch-tuesday-release.md) process
1. - [ ] Create an announcement (example: [Windows Server, version 1909](https://github.com/microsoft/dotnet-framework-docker/issues/448))

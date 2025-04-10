---
name: ".NET Framework Docker Release - New Windows Version"
about: "Checklist for releasing .NET Framework container images for new Windows major versions"
title: ".NET Framework Container Images Release - New Windows Version - <new Windows version>"
---

## Prep Tasks

1. - [ ] Ensure a ["New Windows Release" issue](https://github.com/dotnet/docker-tools/blob/main/.github/ISSUE_TEMPLATE/releases/new-windows-release.md) exists for docker-tools repo. Follow that checklist until build images for the new Windows version are available.
1. - [ ] Update [manifest.json](https://github.com/microsoft/dotnet-framework-docker/blob/main/manifest.json) to include entries for the new images
1. - [ ] Update [manifest.versions.json](https://github.com/microsoft/dotnet-framework-docker/blob/main/manifest.versions.json) and [manifest.datestamps.json](https://github.com/microsoft/dotnet-framework-docker/blob/main/manifest.datestamps.json) and add any variables that are necessary for the new Windows version
1. - [ ] Update [mcr-tags-metadata-templates](https://github.com/microsoft/dotnet-framework-docker/blob/main/eng/mcr-tags-metadata-templates) to include the new Windows version
1. - [ ] Update the test data for each of the [test classes](https://github.com/microsoft/dotnet-framework-docker/tree/main/tests/Microsoft.DotNet.Framework.Docker.Tests) to include the new Windows version
1. - [ ] Run the `update-dependencies` tool to generate the new Dockerfiles and update all the necessary files:
      - [ ] `dotnet run --project .\eng\update-dependencies`
1. - [ ] Inspect generated changes for correctness
1. - [ ] Test the images
      1. - [ ] Create a local VM of the new Windows version
      1. - [ ] Clone this repo with the above changes onto the VM
      1. - [ ] Run `.\build-and-test.ps1 -OS windowsservercore-<VERSION>` to build and test your changes
1. - [ ] Commit generated changes
1. - [ ] Create PR
1. - [ ] Get PR signoff. **Don't merge PR to main.**

## Samples

1. - [ ] Add samples for the new Windows version
1. - [ ] Update [manifest.samples.json](https://github.com/microsoft/dotnet-framework-docker/blob/main/manifest.samples.json) to include the new Windows version for each of the samples
1. - [ ] Update [mcr-tags-metadata-templates](https://github.com/microsoft/dotnet-framework-docker/blob/main/eng/mcr-tags-metadata-templates) to include the new samples images

## Release Day Tasks

1. - [ ] Release the images by following the [Servicing Release Checklist](https://github.com/dotnet/release/blob/main/.github/ISSUE_TEMPLATE/dotnet-fx-docker-servicing-release.md)
1. - [ ] Create an announcement (example: [Windows Server, version 1909](https://github.com/microsoft/dotnet-framework-docker/issues/448))

# Guiding Principles

These are the guiding principles for the content, tagging and production of the official .NET Docker images.  They establish a framework for the behavior customers can expect and the decision-making process when making changes to the images.  They are not hard rules which are stringently enforced as there will be occasions where it is sensible to make special exceptions.

## Image Content

1. Images are intended to satisfy the common usage scenarios.  They are not intended to satisfy every possible usage scenario.  As a result of this, decisions will be made (e.g. components excluded, configurations made, etc.) in order to keep the image size manageable.  It is expected there will be scenarios in which customers will need to create derived images that add their required components/settings.

1. Components installed within the images are required to have the same or longer support lifecycle as [.NET Framework](https://dotnet.microsoft.com/platform/support/policy/dotnet-framework).  Components are expected to be serviced over their lifetime as appropriate.

1. Breaking changes are not allowed within a release.  This includes changes such as adding/removing components, adding/removing ENVs, and major/minor version changes to included components.

1. There should be parity within the supported image matrix.  Examples include:
    1. If support for a new version of Windows is added for a particular .NET Framework version, then support will be added across all `runtime`, `aspnet`, `wcf`, and `sdk` image variants.
    1. If a new component is added, it should be available across all supported OS versions.

1. Windows Server Core is the only Windows SKU supported by the official .NET Framework images.  Windows Server Core is the best Windows SKU to run .NET Framework apps from a performance perspective.  Windows Server Core doesn't have support for every scenario.  For these cases, it is expected that consumers will need to manage their own custom .NET Framework images based on the [Windows](https://hub.docker.com/_/microsoft-windows) base image.

## Image Tagging

The .NET Framework image tags strive to align with the tagging practices utilized by the [Official Images on Docker Hub](https://hub.docker.com/search?q=&type=image&image_filter=official).  The [Supported Tags](supported-tags.md) document describe this in detail.

## Engineering

1. Images will be included as part of the .NET release process.  The Docker images will be released at the same time as the core product.

1. Images will be rebuilt within hours of base image changes. For example, suppose a particular version of Windows is patched.  The .NET Framework images based on this version of Windows will be rebuilt with this new base image within hours of its release.

1. Images will never be deleted from the [official Docker repositories](https://hub.docker.com/_/microsoft-dotnet-framework/).

1. The [Dockerfiles](https://github.com/microsoft/dotnet-framework-docker/search?q=filename%3ADockerfile) used to produce all of the images will be publicly available. Customers will be able to take the Dockerfiles and build them to produce their own equivalent images.  No special build steps or permissions should be needed to build the Dockerfiles.

1. If a change is ever made to the tagging patterns, all of the old tags will be serviced appropriately through its original lifetime.  All old tags will no longer be documented within the tag details section of the readme.

1. No experimental Docker features will be utilized within the infrastructure used to produce the images.  Utilizing experimental features can negatively affect the reliability of image production and introduces a risk to the integrity of the resulting artifacts.

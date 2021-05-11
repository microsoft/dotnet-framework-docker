# Image Tagging Guidelines

This document describes the tagging practices used with the official .NET Framework Docker images.

The .NET image tags strive to align with the tagging practices utilized by the [Official Images on Docker Hub](https://hub.docker.com/search?q=&type=image&image_filter=official).

## Simple Tags

1. `<.NET Version>-<TimeStamp>-<OS>`

    **Examples**

    * `4.8-20210511-windowsservercore-20H2`
    * `4.7.2-20200310-windowsservercore-ltsc2019`
    * `3.5-20200414-windowsservercore-ltsc2016`

1. `<.NET Version>-<OS>`

    **Examples**

    * `4.8-windowsservercore-20H2`
    * `4.7.2-windowsservercore-ltsc2019`
    * `3.5-windowsservercore-ltsc2016`

## Shared Tags

1. `<.NET Version>`

    **Examples**

    * `4.8`
    * `4.7.2`
    * `3.5`

1. `latest`

    * References the most recent .NET version.

All shared tags [support multiple platforms](https://blog.docker.com/2017/09/docker-official-images-now-multi-platform/) and have the following characteristics:

1. Include entries for each supported Windows version.

    1. New entries will be added as new versions of Windows are released.

    1. Entries will be removed as versions of Windows reach EOL.

## Tag Parts

* `<.NET Version>` - The .NET version number included in the image.

* `<TimeStamp>` - The timestamp of when the .NET components of the image were last changed.  The timestamp does not change when only the base OS is updated.

* `<OS>` - The name of the OS release and variant the image is based upon.  The image the tag references is updated whenever a new OS patch is released.  The OS release name does support pinning to specific OS patches.  If OS patch pinning is required then the image digest should be used (e.g. `mcr.microsoft.com/dotnet/framework/runtime@sha256:c2310f61b429d6e9780c56068e4e9d35ab8a36deae03eff0e5b3d276f707e5b8`).

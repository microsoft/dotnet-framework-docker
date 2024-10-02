# Supported Tags

This document describes the tagging patterns and policies that are used for the official .NET Framework container images.
.NET tags are intended to closely match the tagging patterns used by [Official Images on Docker Hub](https://hub.docker.com/search?q=&type=image&image_filter=official).
 Please [log an issue](https://github.com/dotnet/dotnet-docker/issues/new/choose) if you encounter problems using .NET images or applying these tagging patterns.

Complete tag lists:

* [sdk](../README.sdk.md#full-tag-listing): .NET Framework SDK
* [aspnet](../README.aspnet.md#full-tag-listing): ASP.NET Web Forms and MVC
* [runtime](../README.runtime.md#full-tag-listing): .NET Framework Runtime
* [wcf](../README.wcf.md#full-tag-listing): Windows Communication Foundation (WCF)
* [samples](../README.samples.md#full-tag-listing): .NET Framework, ASP.NET and WCF Samples
* [Microsoft Artifact Registry](https://mcr.microsoft.com/en-us/catalog?search=dotnet/framework)

## Simple Tags

1. `<.NET Version>-<TimeStamp>-<OS>`

    **Examples**

    * `4.7.2-20200310-windowsservercore-ltsc2019`
    * `3.5-20200414-windowsservercore-ltsc2016`

1. `<.NET Version>-<OS>`

    **Examples**

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

## Tag Lifecycle

Each tag will be supported for the lifetime of the .NET Framework and Windows Server version referenced by the tag. Once either of these reaches EOL, the tag will be considered unsupported, will no longer be updated and will be removed from the [Tag Listing](#tag-listing). There are two exceptions:

* Tags containing a timestamp are only supported until a newer timestamp is published for that same .NET Framework and Windows Server version. The timeframe for this is typically on a monthly basis on ["Patch Tuesday"](https://www.microsoft.com/msrc/faqs-security-update-guide).
* The `latest` tag will always reference the latest version of .NET Framework.

Unsupported tags will be preserved to prevent breaking any references to it.

### Examples

* `4.8` - Will be supported for the lifetime of the .NET Framework 4.8 release.
* `4.8-windowservercore-ltsc2022` - Will be supported for the lifetime of .NET Framework 4.8 and Windows Server Core LTSC 2022 releases, whichever is shorter.
* `4.8-20220412-windowsservercore-ltsc2022` - Will be supported only until a newer timestamp is published for that same .NET Framework and Windows Server version.

## Policy Changes

In the event that a change is needed to the tagging patterns used, all tags for the previous pattern will continue to be supported for their original lifetime. They will however be removed from the documentation. [Announcements](https://github.com/microsoft/dotnet-framework-docker/discussions/categories/announcements) will be posted when any tagging policy changes are made.

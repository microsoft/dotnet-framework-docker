# Featured tags

* `4.8.1`
  * `docker pull mcr.microsoft.com/dotnet/framework/runtime:4.8.1`
* `4.8`
  * `docker pull mcr.microsoft.com/dotnet/framework/runtime:4.8`
* `3.5`
  * `docker pull mcr.microsoft.com/dotnet/framework/runtime:3.5`

# About

> **NOTICE**: .NET Framework 3.5 images for Windows Server 2025 will no longer be published after May 13th.
> Guidance for installing .NET Framework 3.5 in your own container images can be found [here](https://github.com/microsoft/dotnet-framework-docker/blob/main/documentation/install-netfx3.md#windows-server-core-2022-and-later).
> See the [announcement](https://github.com/dotnet/announcements/issues/349) for more details.

This image contains the .NET Framework runtimes and libraries and is optimized for running .NET Framework apps in production.

Watch [discussions](https://github.com/microsoft/dotnet-framework-docker/discussions/categories/announcements) for Docker-related .NET announcements.

# Usage

The [.NET Framework Docker samples](https://github.com/microsoft/dotnet-framework-docker/blob/main/samples/README.md) show various ways to use .NET Framework and Docker together.

## Container sample: Run a simple application

You can quickly run a container with a pre-built [.NET Framework Docker image](https://hub.docker.com/r/microsoft/dotnet-framework-samples/), based on the [.NET Framework console sample](https://github.com/microsoft/dotnet-framework-docker/blob/main/samples/dotnetapp/README.md).

Type the following command to run a sample console application:

```console
docker run --rm mcr.microsoft.com/dotnet/framework/samples:dotnetapp
```

## Version Compatibility

Version Tag | OS Version | Supported .NET Versions
-- | -- | --
4.8.1 | windowsservercore-ltsc2022 | 4.8.1
4.8 | windowsservercore-ltsc2022, windowsservercore-ltsc2019, windowsservercore-ltsc2016 | 4.8
4.7.2 | windowsservercore-ltsc2019, windowsservercore-ltsc2016 | 4.7.2
4.7.1 | windowsservercore-ltsc2016 | 4.7.1
4.7 | windowsservercore-ltsc2016 | 4.7
4.6.2 | windowsservercore-ltsc2016 | 4.6.2
3.5 | windowsservercore-ltsc2022 | 4.7.2, 3.5, 3.0, 2.5
3.5 | windowsservercore-ltsc2019 | 4.7.2, 3.5, 3.0, 2.5
3.5 | windowsservercore-ltsc2016 | 4.6.2, 3.5, 3.0, 2.5

# Related repositories

.NET Framework:

* [dotnet/framework](https://hub.docker.com/r/microsoft/dotnet-framework/): .NET Framework
* [dotnet/framework/sdk](https://hub.docker.com/r/microsoft/dotnet-framework-sdk/): .NET Framework SDK
* [dotnet/framework/aspnet](https://hub.docker.com/r/microsoft/dotnet-framework-aspnet/): ASP.NET Web Forms and MVC
* [dotnet/framework/wcf](https://hub.docker.com/r/microsoft/dotnet-framework-wcf/): Windows Communication Foundation (WCF)
* [dotnet/framework/samples](https://hub.docker.com/r/microsoft/dotnet-framework-samples/): .NET Framework, ASP.NET and WCF Samples

.NET:

* [dotnet](https://hub.docker.com/r/microsoft/dotnet/): .NET
* [dotnet/samples](https://hub.docker.com/r/microsoft/dotnet-samples/): .NET Samples

# Full Tag Listing

View the current tags at the [Microsoft Artifact Registry portal](https://mcr.microsoft.com/product/dotnet/framework/runtime/tags) or on [GitHub](https://github.com/microsoft/dotnet-framework-docker/blob/main/README.runtime.md#full-tag-listing).

# Support

## Lifecycle

* [.NET Framework Lifecycle FAQ](https://support.microsoft.com/help/17455/lifecycle-faq-net-framework)
* [Supported Tags Policy](https://github.com/microsoft/dotnet-framework-docker/blob/main/documentation/supported-tags.md)

## Image Update Policy

* We update the supported .NET Framework images within 12 hours of any updates to their base images (e.g. windows/servercore:ltsc2019, windows/servercore:ltsc2022, etc.).
* We publish .NET Framework images as part of releasing new versions of .NET Framework including major/minor and servicing.

## Feedback

* [File an issue](https://github.com/microsoft/dotnet-framework-docker/issues/new/choose)
* [Contact Microsoft Support](https://support.microsoft.com/contactus/)

# License

* [Microsoft Container Images Legal Notice](https://aka.ms/mcr/osslegalnotice): applies to all [.NET Framework container images](https://hub.docker.com/r/microsoft/dotnet-framework/)
* [Windows Base Image License](https://learn.microsoft.com/virtualization/windowscontainers/images-eula): applies to all [.NET Framework container images](https://hub.docker.com/r/microsoft/dotnet-framework/)
* [Visual Studio Tools License](https://visualstudio.microsoft.com/license-terms/mlt031519/): applies to all [.NET Framework SDK container images](https://hub.docker.com/r/microsoft/dotnet-framework-sdk/)

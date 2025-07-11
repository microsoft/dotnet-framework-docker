# Featured tags

* `4.8.1`
  * `docker pull mcr.microsoft.com/dotnet/framework/aspnet:4.8.1`
* `4.8`
  * `docker pull mcr.microsoft.com/dotnet/framework/aspnet:4.8`
* `3.5`
  * `docker pull mcr.microsoft.com/dotnet/framework/aspnet:3.5`

# About

ASP.NET is a high productivity framework for building Web Applications using Web Forms, MVC, Web API and SignalR.

This image contains:

* Windows Server Core as the base OS
* IIS 10 as Web Server
* .NET Framework (multiple versions available)
* .NET Extensibility for IIS

Watch [discussions](https://github.com/microsoft/dotnet-framework-docker/discussions/categories/announcements) for Docker-related .NET announcements.

# Usage

The [.NET Framework Docker samples](https://github.com/microsoft/dotnet-framework-docker/blob/main/samples/README.md) show various ways to use .NET Framework and Docker together.

## Container sample: Run an ASP.NET application

You can quickly run a container with a pre-built [sample ASP.NET Docker image](https://hub.docker.com/r/microsoft/dotnet-framework-samples/), based on the [ASP.NET Docker sample].

Type the following [Docker](https://www.docker.com/products/docker) command:

```console
docker run --name aspnet_sample --rm -it -p 8000:80 mcr.microsoft.com/dotnet/framework/samples:aspnetapp
```

After the application starts, navigate to `http://localhost:8000` in your web browser. You need to navigate to the application via IP address instead of `localhost` for earlier Windows versions, which is demonstrated in [View the ASP.NET app in a running container on Windows](https://github.com/microsoft/dotnet-framework-docker/blob/main/samples/aspnetapp/README.md#view-the-aspnet-app-in-a-running-container-on-windows).

## Version Compatibility

If you created your app using an earlier version of .NET Framework, you can generally upgrade it to .NET Framework 4.8+ easily.
Additionally, .NET Framework 4.8 and 4.8.1 can run apps that were built targeting any version of .NET Framework 4.

- [.NET Framework migration guide](https://learn.microsoft.com/en-us/dotnet/framework/migration-guide/)
- [Application compatibility in .NET Framework](https://learn.microsoft.com/dotnet/framework/migration-guide/application-compatibility).
- [Version compatibility in .NET Framework](https://learn.microsoft.com/dotnet/framework/migration-guide/version-compatibility)

# Related repositories

.NET Framework:

* [dotnet/framework](https://hub.docker.com/r/microsoft/dotnet-framework/): .NET Framework
* [dotnet/framework/sdk](https://hub.docker.com/r/microsoft/dotnet-framework-sdk/): .NET Framework SDK
* [dotnet/framework/runtime](https://hub.docker.com/r/microsoft/dotnet-framework-runtime/): .NET Framework Runtime
* [dotnet/framework/wcf](https://hub.docker.com/r/microsoft/dotnet-framework-wcf/): Windows Communication Foundation (WCF)
* [dotnet/framework/samples](https://hub.docker.com/r/microsoft/dotnet-framework-samples/): .NET Framework, ASP.NET and WCF Samples

.NET:

* [dotnet](https://hub.docker.com/r/microsoft/dotnet/): .NET
* [dotnet/samples](https://hub.docker.com/r/microsoft/dotnet-samples/): .NET Samples

# Full Tag Listing

View the current tags at the [Microsoft Artifact Registry portal](https://mcr.microsoft.com/product/dotnet/framework/aspnet/tags) or on [GitHub](https://github.com/microsoft/dotnet-framework-docker/blob/main/README.aspnet.md#full-tag-listing).

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

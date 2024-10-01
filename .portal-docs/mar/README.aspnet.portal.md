## About

ASP.NET is a high productivity framework for building Web Applications using Web Forms, MVC, Web API and SignalR.

This image contains:

* Windows Server Core as the base OS
* IIS 10 as Web Server
* .NET Framework (multiple versions available)
* .NET Extensibility for IIS

Watch [discussions](https://github.com/microsoft/dotnet-framework-docker/discussions/categories/announcements) for Docker-related .NET announcements.

## Featured tags

* `4.8.1`
  * `docker pull mcr.microsoft.com/dotnet/framework/aspnet:4.8.1`
* `4.8`
  * `docker pull mcr.microsoft.com/dotnet/framework/aspnet:4.8`
* `3.5`
  * `docker pull mcr.microsoft.com/dotnet/framework/aspnet:3.5`

## Related repositories

.NET Framework:

* [dotnet/framework](https://mcr.microsoft.com/catalog?search=dotnet/framework): .NET Framework
* [dotnet/framework/sdk](https://mcr.microsoft.com/product/dotnet/framework/sdk/about): .NET Framework SDK
* [dotnet/framework/runtime](https://mcr.microsoft.com/product/dotnet/framework/runtime/about): .NET Framework Runtime
* [dotnet/framework/wcf](https://mcr.microsoft.com/product/dotnet/framework/wcf/about): Windows Communication Foundation (WCF)
* [dotnet/framework/samples](https://mcr.microsoft.com/product/dotnet/framework/samples/about): .NET Framework, ASP.NET and WCF Samples

.NET:

* [dotnet](https://mcr.microsoft.com/catalog?search=dotnet): .NET
* [dotnet/samples](https://mcr.microsoft.com/product/dotnet/samples/about): .NET Samples

## Usage

The [.NET Framework Docker samples](https://github.com/microsoft/dotnet-framework-docker/blob/main/samples/README.md) show various ways to use .NET Framework and Docker together.

### Container sample: Run an ASP.NET application

You can quickly run a container with a pre-built [sample ASP.NET Docker image](https://mcr.microsoft.com/product/dotnet/framework/samples/about), based on the [ASP.NET Docker sample].

Type the following [Docker](https://www.docker.com/products/docker) command:

```console
docker run --name aspnet_sample --rm -it -p 8000:80 mcr.microsoft.com/dotnet/framework/samples:aspnetapp
```

After the application starts, navigate to `http://localhost:8000` in your web browser. You need to navigate to the application via IP address instead of `localhost` for earlier Windows versions, which is demonstrated in [View the ASP.NET app in a running container on Windows](https://github.com/microsoft/dotnet-framework-docker/blob/main/samples/aspnetapp/README.md#view-the-aspnet-app-in-a-running-container-on-windows).

### Version Compatibility

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

## Support

### Lifecycle

* [.NET Framework Lifecycle FAQ](https://support.microsoft.com/help/17455/lifecycle-faq-net-framework)
* [Supported Tags Policy](https://github.com/microsoft/dotnet-framework-docker/blob/main/documentation/supported-tags.md)

### Image Update Policy

* We update the supported .NET Framework images within 12 hours of any updates to their base images (e.g. windows/servercore:ltsc2019, windows/servercore:ltsc2022, etc.).
* We publish .NET Framework images as part of releasing new versions of .NET Framework including major/minor and servicing.

### Feedback

* [File an issue](https://github.com/microsoft/dotnet-framework-docker/issues/new/choose)
* [Contact Microsoft Support](https://support.microsoft.com/contactus/)

## License

* [Microsoft Container Images Legal Notice](https://aka.ms/mcr/osslegalnotice): applies to all [.NET Framework container images](https://mcr.microsoft.com/catalog?search=dotnet/framework)
* [Microsoft Software Supplemental License for Windows Container Base Image](https://mcr.microsoft.com/product/windows/servercore/about): applies to all [.NET Framework container images](https://mcr.microsoft.com/catalog?search=dotnet/framework)
* [Visual Studio Tools License](https://visualstudio.microsoft.com/license-terms/mlt031519/): applies to all [.NET Framework SDK container images](https://mcr.microsoft.com/product/dotnet/framework/sdk/about)

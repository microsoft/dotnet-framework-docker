## About

The Windows Communication Foundation (WCF) is a framework for building service-oriented applications. Using WCF, you can send data as asynchronous messages from one service endpoint to another. A service endpoint can be part of a continuously available service hosted by IIS, or it can be a service hosted in an application.

Watch [discussions](https://github.com/microsoft/dotnet-framework-docker/discussions/categories/announcements) for Docker-related .NET announcements.

## Featured tags

* `4.8.1`
  * `docker pull mcr.microsoft.com/dotnet/framework/wcf:4.8.1`
* `4.8`
  * `docker pull mcr.microsoft.com/dotnet/framework/wcf:4.8`

## Related repositories

.NET Framework:

* [dotnet/framework](https://mcr.microsoft.com/catalog?search=dotnet/framework/): .NET Framework
* [dotnet/framework/sdk](https://mcr.microsoft.com/product/dotnet/framework/sdk/about/): .NET Framework SDK
* [dotnet/framework/aspnet](https://mcr.microsoft.com/product/dotnet/framework/aspnet/about/): ASP.NET Web Forms and MVC
* [dotnet/framework/runtime](https://mcr.microsoft.com/product/dotnet/framework/runtime/about/): .NET Framework Runtime
* [dotnet/framework/samples](https://mcr.microsoft.com/product/dotnet/framework/samples/about/): .NET Framework, ASP.NET and WCF Samples

.NET:

* [dotnet](https://mcr.microsoft.com/catalog?search=dotnet/): .NET
* [dotnet/samples](https://mcr.microsoft.com/product/dotnet/samples/about/): .NET Samples

## Usage

The [.NET Framework Docker samples](https://github.com/microsoft/dotnet-framework-docker/blob/main/samples/README.md) show various ways to use .NET Framework and Docker together.

### Container sample: Run a WCF application
You can quickly run a container with a pre-built [sample WCF Docker image](https://mcr.microsoft.com/product/dotnet/framework/samples/about/), based on the WCF Docker sample.

Type the following [Docker](https://www.docker.com/products/docker) command to start a WCF service container:

```console
docker run --name wcfservicesample --rm -it mcr.microsoft.com/dotnet/framework/samples:wcfservice
```

Find the IP address of the container instance.

```console
docker inspect --format="{{.NetworkSettings.Networks.nat.IPAddress}}" wcfservicesample
172.26.236.119
```

Type the following Docker command to start a WCF client container, set environment variable HOST to the IP address of the wcfservicesample container:

```console
docker run --name wcfclientsample --rm -it -e HOST=172.26.236.119 mcr.microsoft.com/dotnet/framework/samples:wcfclient
```

### Version Compatibility

Version Tag | OS Version | Supported .NET Versions
-- | -- | --
4.8.1 | windowsservercore-ltsc2022 | 4.8.1
4.8 | windowsservercore-ltsc2022, windowsservercore-ltsc2019, windowsservercore-ltsc2016 | 4.8
4.7.2 | windowsservercore-ltsc2019, windowsservercore-ltsc2016 | 4.7.2
4.7.1 | windowsservercore-ltsc2016 | 4.7.1
4.7 | windowsservercore-ltsc2016 | 4.7
4.6.2 | windowsservercore-ltsc2016 | 4.6.2

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

* [Microsoft Container Images Legal Notice](https://aka.ms/mcr/osslegalnotice): applies to all [.NET Framework container images](https://mcr.microsoft.com/catalog?search=dotnet/framework/)
* [Microsoft Software Supplemental License for Windows Container Base Image](https://mcr.microsoft.com/product/windows/servercore/about/): applies to all [.NET Framework container images](https://mcr.microsoft.com/catalog?search=dotnet/framework/)
* [Visual Studio Tools License](https://visualstudio.microsoft.com/license-terms/mlt031519/): applies to all [.NET Framework SDK container images](https://mcr.microsoft.com/product/dotnet/framework/sdk/about/)

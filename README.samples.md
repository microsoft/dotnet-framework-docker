# .NET Framework, ASP.NET and WCF Samples

## Featured tags

* `dotnetapp`
  * `docker pull mcr.microsoft.com/dotnet/framework/samples:dotnetapp`
* `aspnetapp`
  * `docker pull mcr.microsoft.com/dotnet/framework/samples:aspnetapp`
* `wcfservice`
  * `docker pull mcr.microsoft.com/dotnet/framework/samples:wcfservice`
* `wcfclient`
  * `docker pull mcr.microsoft.com/dotnet/framework/samples:wcfclient`

## About

These images contain sample .NET Framework, ASP.NET and WCF applications.

Watch [discussions](https://github.com/microsoft/dotnet-framework-docker/discussions/categories/announcements) for Docker-related .NET announcements.

## Usage

The [.NET Framework Docker samples](https://github.com/microsoft/dotnet-framework-docker/blob/main/samples/README.md) show various ways to use .NET Framework and Docker together.

### Container sample: Run a simple application

Type the following command to run a sample console application with Docker:

```console
docker run --rm mcr.microsoft.com/dotnet/framework/samples:dotnetapp
```

### Container sample: Run a web application

Type the following command to run a sample web application with Docker:

```console
docker run -it --rm -p 8000:80 --name aspnet_sample mcr.microsoft.com/dotnet/framework/samples:aspnetapp
```

After the application starts, navigate to `http://localhost:8000` in your web browser. You need to navigate to the application via IP address instead of `localhost` for earlier Windows versions, which is demonstrated in [View the ASP.NET app in a running container on Windows](https://github.com/microsoft/dotnet-framework-docker/blob/main/samples/aspnetapp/README.md#view-the-aspnet-app-in-a-running-container-on-windows).

### Container sample: Run WCF service and client applications

Type the following command to run a sample WCF service application with Docker:

```console
docker run -it --rm --name wcfservice_sample mcr.microsoft.com/dotnet/framework/samples:wcfservice
```

After the container starts, find the IP address of the container instance:

```console
docker inspect --format="{{.NetworkSettings.Networks.nat.IPAddress}}" wcfservice_sample
172.26.236.119
```

Type the following Docker command to start a WCF client container, set environment variable HOST to the IP address of the wcfservice_sample container:

```console
docker run --name wcfclient_sample --rm -it -e HOST=172.26.236.119 mcr.microsoft.com/dotnet/framework/samples:wcfclient
```

## Related repositories

.NET Framework:

* [dotnet/framework](https://github.com/microsoft/dotnet-framework-docker/blob/main/README.md): .NET Framework
* [dotnet/framework/sdk](https://github.com/microsoft/dotnet-framework-docker/blob/main/README.sdk.md): .NET Framework SDK
* [dotnet/framework/aspnet](https://github.com/microsoft/dotnet-framework-docker/blob/main/README.aspnet.md): ASP.NET Web Forms and MVC
* [dotnet/framework/runtime](https://github.com/microsoft/dotnet-framework-docker/blob/main/README.runtime.md): .NET Framework Runtime
* [dotnet/framework/wcf](https://github.com/microsoft/dotnet-framework-docker/blob/main/README.wcf.md): Windows Communication Foundation (WCF)

.NET:

* [dotnet](https://github.com/dotnet/dotnet-docker/blob/main/README.md): .NET
* [dotnet/samples](https://github.com/dotnet/dotnet-docker/blob/main/README.samples.md): .NET Samples

## Full Tag Listing

### Windows Server Core 2022 amd64 Tags

Tag | Dockerfile
---------| ---------------
dotnetapp-windowsservercore-ltsc2022, dotnetapp, latest | [Dockerfile](samples/dotnetapp/Dockerfile)
aspnetapp-windowsservercore-ltsc2022, aspnetapp | [Dockerfile](samples/aspnetapp/Dockerfile)
wcfservice-windowsservercore-ltsc2022, wcfservice | [Dockerfile](samples/wcfapp/Dockerfile.web)
wcfclient-windowsservercore-ltsc2022, wcfclient | [Dockerfile](samples/wcfapp/Dockerfile.client)
<!--End of generated tags-->

*Tags not listed in the table above are not supported. See the [Supported Tags Policy](https://github.com/dotnet/dotnet-docker/blob/main/documentation/supported-tags.md).
See the [full list of tags](https://mcr.microsoft.com/v2/dotnet/framework/samples/tags/list) for all supported and unsupported tags.*

## Support

These sample images are not intended for production use and may be subject to breaking changes or removal at any time. They are provided as a starting point for developers to experiment with and learn about .NET Framework in a containerized environment.

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

* [Microsoft Container Images Legal Notice](https://aka.ms/mcr/osslegalnotice): applies to all [.NET Framework container images](https://github.com/microsoft/dotnet-framework-docker/blob/main/README.md)
* [Windows Base Image License](https://learn.microsoft.com/virtualization/windowscontainers/images-eula): applies to all [.NET Framework container images](https://github.com/microsoft/dotnet-framework-docker/blob/main/README.md)
* [Visual Studio Tools License](https://visualstudio.microsoft.com/license-terms/mlt031519/): applies to all [.NET Framework SDK container images](https://github.com/microsoft/dotnet-framework-docker/blob/main/README.sdk.md)

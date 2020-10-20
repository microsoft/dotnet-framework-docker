# Featured Tags

* `dotnetapp`
  * `docker pull mcr.microsoft.com/dotnet/framework/samples:dotnetapp`
* `aspnetapp`
  * `docker pull mcr.microsoft.com/dotnet/framework/samples:aspnetapp`
* `wcfservice`
  * `docker pull mcr.microsoft.com/dotnet/framework/samples:wcfservice`
* `wcfclient`
  * `docker pull mcr.microsoft.com/dotnet/framework/samples:wcfclient`

# About This Image

These images contain sample .NET Framework, ASP.NET and WCF applications.

Watch [dotnet/announcements](https://github.com/dotnet/announcements/labels/Docker) for Docker-related .NET announcements.

# How to Use the Image

The [.NET Framework Docker samples](https://github.com/microsoft/dotnet-framework-docker/blob/master/samples/README.md) show various ways to use .NET Framework and Docker together.

## Container sample: Run a simple application

Type the following command to run a sample console application with Docker:

```console
docker run --rm mcr.microsoft.com/dotnet/framework/samples:dotnetapp
```

## Container sample: Run a web application

Type the following command to run a sample web application with Docker:

```console
docker run -it --rm -p 8000:80 --name aspnet_sample mcr.microsoft.com/dotnet/framework/samples:aspnetapp
```

After the application starts, navigate to `http://localhost:8000` in your web browser. You need to navigate to the application via IP address instead of `localhost` for earlier Windows versions, which is demonstrated in [View the ASP.NET app in a running container on Windows](https://github.com/microsoft/dotnet-framework-docker/blob/master/samples/aspnetapp/README.md#view-the-aspnet-app-in-a-running-container-on-windows).

## Container sample: Run WCF service and client applications

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

# Related Repos

.NET Framework:

* [dotnet/framework/sdk](https://hub.docker.com/_/microsoft-dotnet-framework-sdk/): .NET Framework SDK
* [dotnet/framework/aspnet](https://hub.docker.com/_/microsoft-dotnet-framework-aspnet/): ASP.NET Web Forms and MVC
* [dotnet/framework/runtime](https://hub.docker.com/_/microsoft-dotnet-framework-runtime/): .NET Framework Runtime
* [dotnet/framework/wcf](https://hub.docker.com/_/microsoft-dotnet-framework-wcf/): Windows Communication Foundation (WCF)

.NET Core:

* [dotnet/core](https://hub.docker.com/_/microsoft-dotnet-core/): .NET Core
* [dotnet/core-nightly](https://hub.docker.com/_/microsoft-dotnet-core-nightly/): .NET Core (Preview)
* [dotnet/core/samples](https://hub.docker.com/_/microsoft-dotnet-core-samples/): .NET Core Samples

# Full Tag Listing

## Windows Server, version 2004 amd64 Tags
Tag | Dockerfile
---------| ---------------
dotnetapp-windowsservercore-2004, dotnetapp, latest | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/master/samples/dotnetapp/Dockerfile)
aspnetapp-windowsservercore-2004, aspnetapp | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/master/samples/aspnetapp/Dockerfile)
wcfservice-windowsservercore-2004, wcfservice | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/master/samples/wcfapp/Dockerfile.web)
wcfclient-windowsservercore-2004, wcfclient | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/master/samples/wcfapp/Dockerfile.client)

## Windows Server, version 1909 amd64 Tags
Tag | Dockerfile
---------| ---------------
dotnetapp-windowsservercore-1909, dotnetapp, latest | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/master/samples/dotnetapp/Dockerfile)
aspnetapp-windowsservercore-1909, aspnetapp | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/master/samples/aspnetapp/Dockerfile)
wcfservice-windowsservercore-1909, wcfservice | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/master/samples/wcfapp/Dockerfile.web)
wcfclient-windowsservercore-1909, wcfclient | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/master/samples/wcfapp/Dockerfile.client)

## Windows Server, version 1903 amd64 Tags
Tag | Dockerfile
---------| ---------------
dotnetapp-windowsservercore-1903, dotnetapp, latest | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/master/samples/dotnetapp/Dockerfile)
aspnetapp-windowsservercore-1903, aspnetapp | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/master/samples/aspnetapp/Dockerfile)
wcfservice-windowsservercore-1903, wcfservice | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/master/samples/wcfapp/Dockerfile.web)
wcfclient-windowsservercore-1903, wcfclient | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/master/samples/wcfapp/Dockerfile.client)

## Windows Server 2019 amd64 Tags
Tag | Dockerfile
---------| ---------------
dotnetapp-windowsservercore-ltsc2019, dotnetapp, latest | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/master/samples/dotnetapp/Dockerfile)
aspnetapp-windowsservercore-ltsc2019, aspnetapp | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/master/samples/aspnetapp/Dockerfile)
wcfservice-windowsservercore-ltsc2019, wcfservice | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/master/samples/wcfapp/Dockerfile.web)
wcfclient-windowsservercore-ltsc2019, wcfclient | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/master/samples/wcfapp/Dockerfile.client)

## Windows Server 2016 amd64 Tags
Tag | Dockerfile
---------| ---------------
dotnetapp-windowsservercore-ltsc2016, dotnetapp, latest | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/master/samples/dotnetapp/Dockerfile)
aspnetapp-windowsservercore-ltsc2016, aspnetapp | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/master/samples/aspnetapp/Dockerfile)
wcfservice-windowsservercore-ltsc2016, wcfservice | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/master/samples/wcfapp/Dockerfile.web)
wcfclient-windowsservercore-ltsc2016, wcfclient | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/master/samples/wcfapp/Dockerfile.client)

You can retrieve a list of all available tags for dotnet/framework/samples at https://mcr.microsoft.com/v2/dotnet/framework/samples/tags/list.

# Support

See the [.NET Framework Lifecycle FAQ](https://support.microsoft.com/help/17455/lifecycle-faq-net-framework)

# Image Update Policy

* We update the supported .NET Framework images within 12 hours of any updates to their base images (e.g. windows/servercore:1909, windows/servercore:ltsc2019, etc.).
* We publish .NET Framework images as part of releasing new versions of .NET Framework including major/minor and servicing.

# Feedback

* [File an issue](https://github.com/microsoft/dotnet-framework-docker/issues/new/choose)
* [Contact Microsoft Support](https://support.microsoft.com/contactus/)

# License

* Legal Notice: [Container License Information](https://aka.ms/mcr/osslegalnotice)

The .NET Framework images use the same license as the [Windows Server Core base image](https://hub.docker.com/_/microsoft-windows-servercore/).

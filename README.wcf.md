# Featured Tags

* `4.8`
  * `docker pull mcr.microsoft.com/dotnet/framework/wcf:4.8`

## About This Image
The Windows Communication Foundation (WCF) is a framework for building service-oriented applications. Using WCF, you can send data as asynchronous messages from one service endpoint to another. A service endpoint can be part of a continuously available service hosted by IIS, or it can be a service hosted in an application.

Watch [dotnet/announcements](https://github.com/dotnet/announcements/labels/Docker) for Docker-related .NET announcements.

# How to Use the Image

The [WCF Docker samples](https://github.com/Microsoft/dotnet-framework-docker/tree/master/samples/wcfapp) show various ways to use WCF and Docker together.

## Container sample: Run a WCF application
You can quickly run a container with a pre-built [sample WCF Docker image](https://hub.docker.com/_/microsoft-dotnet-framework-samples/), based on the WCF Docker sample.

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

# Related Repos

.NET Framework:

* [dotnet/framework/runtime](https://hub.docker.com/_/microsoft-dotnet-framework-runtime/): .NET Framework Runtime
* [dotnet/framework/aspnet](https://hub.docker.com/_/microsoft-dotnet-framework-aspnet/): ASP.NET Web Forms and MVC
* [dotnet/framework/sdk](https://hub.docker.com/_/microsoft-dotnet-framework-sdk/): .NET Framework SDK
* [dotnet/framework/samples](https://hub.docker.com/_/microsoft-dotnet-framework-samples/): .NET Framework, ASP.NET and WCF Samples

.NET Core:

* [microsoft/dotnet](https://hub.docker.com/r/microsoft/dotnet/) for .NET Core images.
* [dotnet/core-nightly](https://hub.docker.com/_/microsoft-dotnet-core-nightly/): .NET Core (Preview)
* [dotnet/core/samples](https://hub.docker.com/_/microsoft-dotnet-core-samples/): .NET Core Samples

# Full Tag Listing

## Windows Server, version 2004 amd64 Tags
Tag | Dockerfile
---------| ---------------
4.8-20200714-windowsservercore-2004, 4.8-windowsservercore-2004, 4.8, latest | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/master/src/wcf/4.8/windowsservercore-2004/Dockerfile)

## Windows Server, version 1909 amd64 Tags
Tag | Dockerfile
---------| ---------------
4.8-20200714-windowsservercore-1909, 4.8-windowsservercore-1909, 4.8, latest | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/master/src/wcf/4.8/windowsservercore-1909/Dockerfile)

## Windows Server, version 1903 amd64 Tags
Tag | Dockerfile
---------| ---------------
4.8-20200714-windowsservercore-1903, 4.8-windowsservercore-1903, 4.8, latest | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/master/src/wcf/4.8/windowsservercore-1903/Dockerfile)

## Windows Server 2019 amd64 Tags
Tag | Dockerfile
---------| ---------------
4.8-20200714-windowsservercore-ltsc2019, 4.8-windowsservercore-ltsc2019, 4.8, latest | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/master/src/wcf/4.8/windowsservercore-ltsc2019/Dockerfile)
4.7.2-20200714-windowsservercore-ltsc2019, 4.7.2-windowsservercore-ltsc2019, 4.7.2 | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/master/src/wcf/4.7.2/windowsservercore-ltsc2019/Dockerfile)

## Windows Server 2016 amd64 Tags
Tag | Dockerfile
---------| ---------------
4.8-20200714-windowsservercore-ltsc2016, 4.8-windowsservercore-ltsc2016, 4.8, latest | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/master/src/wcf/4.8/windowsservercore-ltsc2016/Dockerfile)
4.7.2-20200714-windowsservercore-ltsc2016, 4.7.2-windowsservercore-ltsc2016, 4.7.2 | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/master/src/wcf/4.7.2/windowsservercore-ltsc2016/Dockerfile)
4.7.1-20200714-windowsservercore-ltsc2016, 4.7.1-windowsservercore-ltsc2016, 4.7.1 | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/master/src/wcf/4.7.1/windowsservercore-ltsc2016/Dockerfile)
4.7-20200714-windowsservercore-ltsc2016, 4.7-windowsservercore-ltsc2016, 4.7 | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/master/src/wcf/4.7/windowsservercore-ltsc2016/Dockerfile)
4.6.2-20200714-windowsservercore-ltsc2016, 4.6.2-windowsservercore-ltsc2016, 4.6.2 | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/master/src/wcf/4.6.2/windowsservercore-ltsc2016/Dockerfile)

You can retrieve a list of all available tags for dotnet/framework/wcf at https://mcr.microsoft.com/v2/dotnet/framework/wcf/tags/list.

# Support

See the [.NET Framework Lifecycle FAQ](https://support.microsoft.com/en-us/help/17455/lifecycle-faq-net-framework)

# Image Update Policy

* We update the supported .NET Framework images within 12 hours of any updates to their base images (e.g. windows/servercore:1909, windows/servercore:ltsc2019, etc.).
* We publish .NET Framework images as part of releasing new versions of .NET Framework including major/minor and servicing.

# Feedback

* [File a WCF Docker issue](https://github.com/microsoft/dotnet-framework-docker/issues)
* [Report a WCF problem](https://developercommunity.visualstudio.com/spaces/61/index.html)
* [File a Visual Studio Docker Tools issue](https://github.com/microsoft/dockertools/issues)
* [File a Microsoft Container Registry (MCR) issue](https://github.com/microsoft/containerregistry/issues)
* [Ask on Stack Overflow](https://stackoverflow.com/questions/tagged/.net)
* [Contact Microsoft Support](https://support.microsoft.com/contactus/)

# License

* Legal Notice: [Container License Information](https://aka.ms/mcr/osslegalnotice)

View [license information](https://www.microsoft.com/net/dotnet_library_license.htm) for the software contained in this image.

Windows Container images use the same license as the [Windows Server Core base image](https://hub.docker.com/_/microsoft-windows-servercore/).

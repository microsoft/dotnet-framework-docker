# Featured tags

* `4.8.1`
  * `docker pull mcr.microsoft.com/dotnet/framework/wcf:4.8.1`
* `4.8`
  * `docker pull mcr.microsoft.com/dotnet/framework/wcf:4.8`

# About

The Windows Communication Foundation (WCF) is a framework for building service-oriented applications. Using WCF, you can send data as asynchronous messages from one service endpoint to another. A service endpoint can be part of a continuously available service hosted by IIS, or it can be a service hosted in an application.

Watch [discussions](https://github.com/microsoft/dotnet-framework-docker/discussions/categories/announcements) for Docker-related .NET announcements.

# Usage

The [.NET Framework Docker samples](https://github.com/microsoft/dotnet-framework-docker/blob/main/samples/README.md) show various ways to use .NET Framework and Docker together.

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

## Version Compatibility

Version Tag | OS Version | Supported .NET Versions
-- | -- | --
4.8.1 | windowsservercore-ltsc2022 | 4.8.1
4.8 | windowsservercore-ltsc2022, windowsservercore-ltsc2019, windowsservercore-ltsc2016 | 4.8
4.7.2 | windowsservercore-ltsc2019, windowsservercore-ltsc2016 | 4.7.2
4.7.1 | windowsservercore-ltsc2016 | 4.7.1
4.7 | windowsservercore-ltsc2016 | 4.7
4.6.2 | windowsservercore-ltsc2016 | 4.6.2

# Related repositories

.NET Framework:

* [dotnet/framework/sdk](https://hub.docker.com/_/microsoft-dotnet-framework-sdk/): .NET Framework SDK
* [dotnet/framework/aspnet](https://hub.docker.com/_/microsoft-dotnet-framework-aspnet/): ASP.NET Web Forms and MVC
* [dotnet/framework/runtime](https://hub.docker.com/_/microsoft-dotnet-framework-runtime/): .NET Framework Runtime
* [dotnet/framework/samples](https://hub.docker.com/_/microsoft-dotnet-framework-samples/): .NET Framework, ASP.NET and WCF Samples

.NET:

* [dotnet](https://hub.docker.com/_/microsoft-dotnet/): .NET
* [dotnet-nightly](https://hub.docker.com/_/microsoft-dotnet-nightly/): .NET (Preview)
* [dotnet/samples](https://hub.docker.com/_/microsoft-dotnet-samples/): .NET Samples

# Full Tag Listing

## Windows Server Core 2022 amd64 Tags
Tag | Dockerfile
---------| ---------------
4.8.1-20230214-windowsservercore-ltsc2022, 4.8.1-windowsservercore-ltsc2022, 4.8.1 | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/main/src/wcf/4.8.1/windowsservercore-ltsc2022/Dockerfile)
4.8-20230214-windowsservercore-ltsc2022, 4.8-windowsservercore-ltsc2022, 4.8, latest | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/main/src/wcf/4.8/windowsservercore-ltsc2022/Dockerfile)

## Windows Server Core 2019 amd64 Tags
Tag | Dockerfile
---------| ---------------
4.8-20230214-windowsservercore-ltsc2019, 4.8-windowsservercore-ltsc2019, 4.8, latest | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/main/src/wcf/4.8/windowsservercore-ltsc2019/Dockerfile)
4.7.2-20230214-windowsservercore-ltsc2019, 4.7.2-windowsservercore-ltsc2019, 4.7.2 | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/main/src/wcf/4.7.2/windowsservercore-ltsc2019/Dockerfile)

## Windows Server Core 2016 amd64 Tags
Tag | Dockerfile
---------| ---------------
4.8-20230411-windowsservercore-ltsc2016, 4.8-windowsservercore-ltsc2016, 4.8, latest | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/main/src/wcf/4.8/windowsservercore-ltsc2016/Dockerfile)
4.7.2-20230509-windowsservercore-ltsc2016, 4.7.2-windowsservercore-ltsc2016, 4.7.2 | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/main/src/wcf/4.7.2/windowsservercore-ltsc2016/Dockerfile)
4.7.1-20230509-windowsservercore-ltsc2016, 4.7.1-windowsservercore-ltsc2016, 4.7.1 | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/main/src/wcf/4.7.1/windowsservercore-ltsc2016/Dockerfile)
4.7-20230509-windowsservercore-ltsc2016, 4.7-windowsservercore-ltsc2016, 4.7 | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/main/src/wcf/4.7/windowsservercore-ltsc2016/Dockerfile)
4.6.2-20230509-windowsservercore-ltsc2016, 4.6.2-windowsservercore-ltsc2016, 4.6.2 | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/main/src/wcf/4.6.2/windowsservercore-ltsc2016/Dockerfile)

You can retrieve a list of all available tags for dotnet/framework/wcf at https://mcr.microsoft.com/v2/dotnet/framework/wcf/tags/list.
<!--End of generated tags-->

*Tags not listed in the table above are not supported. See the [Supported Tags Policy](https://github.com/microsoft/dotnet-framework-docker/blob/main/documentation/supported-tags.md).*

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

* [Microsoft Container Images Legal Notice](https://aka.ms/mcr/osslegalnotice): applies to all [.NET Framework container images](https://hub.docker.com/_/microsoft-dotnet-framework/)
* [Microsoft Software Supplemental License for Windows Container Base Image](https://hub.docker.com/_/microsoft-windows-servercore/): applies to all [.NET Framework container images](https://hub.docker.com/_/microsoft-dotnet-framework/)
* [Visual Studio Tools License](https://visualstudio.microsoft.com/license-terms/mlt031519/): applies to all [.NET Framework SDK container images](https://hub.docker.com/_/microsoft-dotnet-framework-sdk/)

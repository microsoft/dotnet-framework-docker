# Featured Tags

* `4.8`
  * `docker pull mcr.microsoft.com/dotnet/framework/aspnet:4.8`
* `3.5`
  * `docker pull mcr.microsoft.com/dotnet/framework/aspnet:3.5`

# About This Image

ASP.NET is a high productivity framework for building Web Applications using Web Forms, MVC, Web API and SignalR.

This image contains:

* Windows Server Core as the base OS
* IIS 10 as Web Server
* .NET Framework (multiple versions available)
* .NET Extensibility for IIS

Watch [dotnet/announcements](https://github.com/dotnet/announcements/labels/Docker) for Docker-related .NET announcements.

# How to Use the Image

The [.NET Framework Docker samples](https://github.com/microsoft/dotnet-framework-docker/blob/main/samples/README.md) show various ways to use .NET Framework and Docker together.

## Container sample: Run an ASP.NET application
You can quickly run a container with a pre-built [sample ASP.NET Docker image](https://hub.docker.com/_/microsoft-dotnet-framework-samples/), based on the [ASP.NET Docker sample].

Type the following [Docker](https://www.docker.com/products/docker) command:

```console
docker run --name aspnet_sample --rm -it -p 8000:80 mcr.microsoft.com/dotnet/framework/samples:aspnetapp
```

After the application starts, navigate to `http://localhost:8000` in your web browser. You need to navigate to the application via IP address instead of `localhost` for earlier Windows versions, which is demonstrated in [View the ASP.NET app in a running container on Windows](https://github.com/microsoft/dotnet-framework-docker/blob/main/samples/aspnetapp/README.md#view-the-aspnet-app-in-a-running-container-on-windows).

# Related Repos

.NET Framework:

* [dotnet/framework/sdk](https://hub.docker.com/_/microsoft-dotnet-framework-sdk/): .NET Framework SDK
* [dotnet/framework/runtime](https://hub.docker.com/_/microsoft-dotnet-framework-runtime/): .NET Framework Runtime
* [dotnet/framework/wcf](https://hub.docker.com/_/microsoft-dotnet-framework-wcf/): Windows Communication Foundation (WCF)
* [dotnet/framework/samples](https://hub.docker.com/_/microsoft-dotnet-framework-samples/): .NET Framework, ASP.NET and WCF Samples

.NET:

* [dotnet](https://hub.docker.com/_/microsoft-dotnet/): .NET
* [dotnet-nightly](https://hub.docker.com/_/microsoft-dotnet-nightly/): .NET (Preview)
* [dotnet/samples](https://hub.docker.com/_/microsoft-dotnet-samples/): .NET Samples

# Full Tag Listing

## Windows Server Core 2022 amd64 Tags
Tag | Dockerfile
---------| ---------------
4.8-20210818-windowsservercore-ltsc2022, 4.8-windowsservercore-ltsc2022, 4.8, latest | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/main/src/aspnet/4.8/windowsservercore-ltsc2022/Dockerfile)
3.5-20210818-windowsservercore-ltsc2022, 3.5-windowsservercore-ltsc2022, 3.5 | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/main/src/aspnet/3.5/windowsservercore-ltsc2022/Dockerfile)

## Windows Server Core, version 20H2 amd64 Tags
Tag | Dockerfile
---------| ---------------
4.8-20210713-windowsservercore-20H2, 4.8-windowsservercore-20H2, 4.8, latest | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/main/src/aspnet/4.8/windowsservercore-20H2/Dockerfile)
3.5-20210209-windowsservercore-20H2, 3.5-windowsservercore-20H2, 3.5 | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/main/src/aspnet/3.5/windowsservercore-20H2/Dockerfile)

## Windows Server Core, version 2004 amd64 Tags
Tag | Dockerfile
---------| ---------------
4.8-20210713-windowsservercore-2004, 4.8-windowsservercore-2004, 4.8, latest | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/main/src/aspnet/4.8/windowsservercore-2004/Dockerfile)
3.5-20210209-windowsservercore-2004, 3.5-windowsservercore-2004, 3.5 | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/main/src/aspnet/3.5/windowsservercore-2004/Dockerfile)

## Windows Server Core 2019 amd64 Tags
Tag | Dockerfile
---------| ---------------
4.8-20210713-windowsservercore-ltsc2019, 4.8-windowsservercore-ltsc2019, 4.8, latest | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/main/src/aspnet/4.8/windowsservercore-ltsc2019/Dockerfile)
4.7.2-20210713-windowsservercore-ltsc2019, 4.7.2-windowsservercore-ltsc2019, 4.7.2 | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/main/src/aspnet/4.7.2/windowsservercore-ltsc2019/Dockerfile)
3.5-20210209-windowsservercore-ltsc2019, 3.5-windowsservercore-ltsc2019, 3.5 | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/main/src/aspnet/3.5/windowsservercore-ltsc2019/Dockerfile)

## Windows Server Core 2016 amd64 Tags
Tag | Dockerfile
---------| ---------------
4.8-20210713-windowsservercore-ltsc2016, 4.8-windowsservercore-ltsc2016, 4.8, latest | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/main/src/aspnet/4.8/windowsservercore-ltsc2016/Dockerfile)
4.7.2-20210914-windowsservercore-ltsc2016, 4.7.2-windowsservercore-ltsc2016, 4.7.2 | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/main/src/aspnet/4.7.2/windowsservercore-ltsc2016/Dockerfile)
4.7.1-20210914-windowsservercore-ltsc2016, 4.7.1-windowsservercore-ltsc2016, 4.7.1 | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/main/src/aspnet/4.7.1/windowsservercore-ltsc2016/Dockerfile)
4.7-20210914-windowsservercore-ltsc2016, 4.7-windowsservercore-ltsc2016, 4.7 | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/main/src/aspnet/4.7/windowsservercore-ltsc2016/Dockerfile)
4.6.2-20210914-windowsservercore-ltsc2016, 4.6.2-windowsservercore-ltsc2016, 4.6.2 | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/main/src/aspnet/4.6.2/windowsservercore-ltsc2016/Dockerfile)
3.5-20210914-windowsservercore-ltsc2016, 3.5-windowsservercore-ltsc2016, 3.5 | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/main/src/aspnet/3.5/windowsservercore-ltsc2016/Dockerfile)

You can retrieve a list of all available tags for dotnet/framework/aspnet at https://mcr.microsoft.com/v2/dotnet/framework/aspnet/tags/list.

# Version Compatibility

Version Tag | OS Version | Supported .NET Versions
-- | -- | --
4.8 | windowsservercore-20H2, windowsservercore-2004, windowsservercore-ltsc2019, windowsservercore-ltsc2016 | 4.8
4.7.2 | windowsservercore-ltsc2019, windowsservercore-ltsc2016 | 4.7.2
4.7.1 | windowsservercore-ltsc2016 | 4.7.1
4.7 | windowsservercore-ltsc2016 | 4.7
4.6.2 | windowsservercore-ltsc2016 | 4.6.2
3.5 | windowsservercore-20H2, windowsservercore-2004 | 4.8, 3.5, 3.0, 2.5
3.5 | windowsservercore-ltsc2019 | 4.7.2, 3.5, 3.0, 2.5
3.5 | windowsservercore-ltsc2016 | 4.6.2, 3.5, 3.0, 2.5

# Support

See the [.NET Framework Lifecycle FAQ](https://support.microsoft.com/help/17455/lifecycle-faq-net-framework)

# Image Update Policy

* We update the supported .NET Framework images within 12 hours of any updates to their base images (e.g. windows/servercore:20H2, windows/servercore:ltsc2019, etc.).
* We publish .NET Framework images as part of releasing new versions of .NET Framework including major/minor and servicing.

# Feedback

* [File an issue](https://github.com/microsoft/dotnet-framework-docker/issues/new/choose)
* [Contact Microsoft Support](https://support.microsoft.com/contactus/)

# License

* [Microsoft Container Images Legal Notice](https://aka.ms/mcr/osslegalnotice): applies to all [.NET Framework container images](https://hub.docker.com/_/microsoft-dotnet-framework/)
* [Microsoft Software Supplemental License for Windows Container Base Image](https://hub.docker.com/_/microsoft-windows-servercore/): applies to all [.NET Framework container images](https://hub.docker.com/_/microsoft-dotnet-framework/)
* [Visual Studio Tools License](https://visualstudio.microsoft.com/license-terms/mlt031519/): applies to all [.NET Framework SDK container images](https://hub.docker.com/_/microsoft-dotnet-framework-sdk/)

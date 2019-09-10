# Featured Tags

* `4.8`
  * `docker pull mcr.microsoft.com/dotnet/framework/aspnet:4.8`
* `3.5`
  * `docker pull mcr.microsoft.com/dotnet/framework/aspnet:3.5`

# About This Image

ASP.NET is a high productivity framework for building Web Applications using Web Forms, MVC, Web API and SignalR.

This image contains:
- Windows Server Core as the base OS
- IIS 10 as Web Server
- .NET Framework (multiple versions available)
- .NET Extensibility for IIS

Watch [dotnet/announcements](https://github.com/dotnet/announcements/labels/Docker) for Docker-related .NET announcements.

# How to use these Images

The [.NET Framework Docker samples](https://github.com/microsoft/dotnet-framework-docker/blob/master/samples/README.md) show various ways to use .NET Framework and Docker together.

## Container sample: Run an ASP.NET application
You can quickly run a container with a pre-built [sample ASP.NET Docker image](https://hub.docker.com/_/microsoft-dotnet-framework-samples/), based on the [ASP.NET Docker sample].

Type the following [Docker](https://www.docker.com/products/docker) command:

```console
docker run --name aspnet_sample --rm -it -p 8000:80 mcr.microsoft.com/dotnet/framework/samples:aspnetapp
```

After the application starts, navigate to `http://localhost:8000` in your web browser. You need to navigate to the application via IP address instead of `localhost` for earlier Windows versions, which is demonstrated in [View the ASP.NET app in a running container on Windows](https://github.com/microsoft/dotnet-framework-docker/blob/master/samples/aspnetapp/README.md#view-the-aspnet-app-in-a-running-container-on-windows).

# Related Repos

.NET Framework:

* [dotnet/framework/sdk](https://hub.docker.com/_/microsoft-dotnet-framework-sdk/): .NET Framework SDK
* [dotnet/framework/runtime](https://hub.docker.com/_/microsoft-dotnet-framework-runtime/): .NET Framework Runtime
* [dotnet/framework/wcf](https://hub.docker.com/_/microsoft-dotnet-framework-wcf/): Windows Communication Framework (WCF)
* [dotnet/framework/samples](https://hub.docker.com/_/microsoft-dotnet-framework-samples/): .NET Framework, ASP.NET and WCF Samples

.NET Core:

* [dotnet/core](https://hub.docker.com/_/microsoft-dotnet-core/): .NET Core
* [dotnet/core-nightly](https://hub.docker.com/_/microsoft-dotnet-core-nightly/): .NET Core (Preview)
* [dotnet/core/samples](https://hub.docker.com/_/microsoft-dotnet-core-samples/): .NET Core Samples

# Full Tag Listing

## Windows Server, version 1903 amd64 Tags
Tag | Dockerfile
---------| ---------------
4.8-20190910-windowsservercore-1903, 4.8-windowsservercore-1903, 4.8, latest | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/master/4.8/aspnet/windowsservercore-1903/Dockerfile)
3.5-20190910-windowsservercore-1903, 3.5-windowsservercore-1903, 3.5 | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/master/3.5/aspnet/windowsservercore-1903/Dockerfile)

## Windows Server 2019 amd64 Tags
Tag | Dockerfile
---------| ---------------
4.8-20190910-windowsservercore-ltsc2019, 4.8-windowsservercore-ltsc2019, 4.8, latest | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/master/4.8/aspnet/windowsservercore-ltsc2019/Dockerfile)
4.7.2-20190910-windowsservercore-ltsc2019, 4.7.2-windowsservercore-ltsc2019, 4.7.2 | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/master/4.7.2/aspnet/windowsservercore-ltsc2019/Dockerfile)
3.5-20190910-windowsservercore-ltsc2019, 3.5-windowsservercore-ltsc2019, 3.5 | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/master/3.5/aspnet/windowsservercore-ltsc2019/Dockerfile)

## Windows Server, version 1803 amd64 Tags
Tag | Dockerfile
---------| ---------------
4.8-20190910-windowsservercore-1803, 4.8-windowsservercore-1803, 4.8, latest | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/master/4.8/aspnet/windowsservercore-1803/Dockerfile)
4.7.2-20190910-windowsservercore-1803, 4.7.2-windowsservercore-1803, 4.7.2 | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/master/4.7.2/aspnet/windowsservercore-1803/Dockerfile)
3.5-20190910-windowsservercore-1803, 3.5-windowsservercore-1803, 3.5 | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/master/3.5/aspnet/windowsservercore-1803/Dockerfile)

## Windows Server 2016 amd64 Tags
Tag | Dockerfile
---------| ---------------
4.8-20190910-windowsservercore-ltsc2016, 4.8-windowsservercore-ltsc2016, 4.8, latest | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/master/4.8/aspnet/windowsservercore-ltsc2016/Dockerfile)
4.7.2-20190910-windowsservercore-ltsc2016, 4.7.2-windowsservercore-ltsc2016, 4.7.2 | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/master/4.7.2/aspnet/windowsservercore-ltsc2016/Dockerfile)
4.7.1-20190910-windowsservercore-ltsc2016, 4.7.1-windowsservercore-ltsc2016, 4.7.1 | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/master/4.7.1/aspnet/windowsservercore-ltsc2016/Dockerfile)
4.7-20190910-windowsservercore-ltsc2016, 4.7-windowsservercore-ltsc2016, 4.7 | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/master/4.7/aspnet/windowsservercore-ltsc2016/Dockerfile)
4.6.2-20190910-windowsservercore-ltsc2016, 4.6.2-windowsservercore-ltsc2016, 4.6.2 | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/master/4.6.2/aspnet/windowsservercore-ltsc2016/Dockerfile)
3.5-20190910-windowsservercore-ltsc2016, 3.5-windowsservercore-ltsc2016, 3.5 | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/master/3.5/aspnet/windowsservercore-ltsc2016/Dockerfile)

You can retrieve a list of all available tags for dotnet/framework/aspnet at https://mcr.microsoft.com/v2/dotnet/framework/aspnet/tags/list.

# Support

See the [.NET Framework Lifecycle FAQ](https://support.microsoft.com/en-us/help/17455/lifecycle-faq-net-framework)

# Feedback

* [File an ASP.NET Docker issue](https://github.com/microsoft/dotnet-framework-docker/issues)
* [Report an ASP.NET problem](https://developercommunity.visualstudio.com/spaces/61/index.html)
* [Ask on Stack Overflow](https://stackoverflow.com/questions/tagged/.net)
* [Contact Microsoft Support](https://support.microsoft.com/contactus/)

# License

View [license information](https://www.microsoft.com/net/dotnet_library_license.htm) for the software contained in this image. 

Windows Container images use the same license as the [Windows Server Core base image](https://hub.docker.com/_/microsoft-windows-servercore/).


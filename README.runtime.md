# Featured Tags

* `4.7.2`
  * `docker pull mcr.microsoft.com/dotnet/framework/runtime:4.7.2`
* `3.5`
  * `docker pull mcr.microsoft.com/dotnet/framework/runtime:3.5`

# About This Image

This image contains the .NET Framework runtimes and libraries and is optimized for running .NET Framework apps in production.

Watch [dotnet/announcements](https://github.com/dotnet/announcements/labels/Docker) for Docker-related .NET announcements.

# How to Use the Image

The [.NET Framework Docker samples](https://github.com/microsoft/dotnet-framework-docker/blob/master/samples/README.md) show various ways to use .NET Framework and Docker together.

## Container sample: Run a simple application

You can quickly run a container with a pre-built [.NET Framework Docker image](https://hub.docker.com/_/microsoft-dotnet-framework-samples/), based on the [.NET Framework console sample](https://github.com/microsoft/dotnet-framework-docker/blob/master/samples/dotnetapp/README.md).

Type the following command to run a sample console application:

```console
docker run --rm mcr.microsoft.com/dotnet/framework/samples:dotnetapp
```

# Related Repos

.NET Framework:

* [dotnet/framework/sdk](https://hub.docker.com/_/microsoft-dotnet-framework-sdk/): .NET Framework SDK
* [dotnet/framework/aspnet](https://hub.docker.com/_/microsoft-dotnet-framework-aspnet/): ASP.NET Web Forms and MVC
* [dotnet/framework/wcf](https://hub.docker.com/_/microsoft-dotnet-framework-wcf/): Windows Communication Framework (WCF)
* [dotnet/framework/samples](https://hub.docker.com/_/microsoft-dotnet-framework-samples/): .NET Framework, ASP.NET and WCF Samples

.NET Core:

* [dotnet/core](https://hub.docker.com/_/microsoft-dotnet-core/): .NET Core
* [dotnet/core-nightly](https://hub.docker.com/_/microsoft-dotnet-core-nightly/): .NET Core (Preview)
* [dotnet/core/samples](https://hub.docker.com/_/microsoft-dotnet-core-samples/): .NET Core Samples

# Full Tag Listing

## Windows Server 2019 amd64 tags

- [`4.7.2-20190312-windowsservercore-ltsc2019`, `4.7.2-windowsservercore-ltsc2019`, `4.7.2`, `latest` (*Dockerfile*)](https://github.com/Microsoft/dotnet-framework-docker/blob/master/4.7.2/runtime/windowsservercore-ltsc2019/Dockerfile)
- [`3.5-20190312-windowsservercore-ltsc2019`, `3.5-windowsservercore-ltsc2019`, `3.5` (*Dockerfile*)](https://github.com/Microsoft/dotnet-framework-docker/blob/master/3.5/runtime/windowsservercore-ltsc2019/Dockerfile)

## Windows Server, version 1803 amd64 tags

- [`4.7.2-20190312-windowsservercore-1803`, `4.7.2-windowsservercore-1803`, `4.7.2`, `latest` (*Dockerfile*)](https://github.com/Microsoft/dotnet-framework-docker/blob/master/4.7.2/runtime/windowsservercore-1803/Dockerfile)
- [`3.5-20190312-windowsservercore-1803`, `3.5-windowsservercore-1803`, `3.5` (*Dockerfile*)](https://github.com/Microsoft/dotnet-framework-docker/blob/master/3.5/runtime/windowsservercore-1803/Dockerfile)

## Windows Server 2016 amd64 tags

- [`4.7.2-20190312-windowsservercore-ltsc2016`, `4.7.2-windowsservercore-ltsc2016`, `4.7.2`, `latest` (*Dockerfile*)](https://github.com/Microsoft/dotnet-framework-docker/blob/master/4.7.2/runtime/windowsservercore-ltsc2016/Dockerfile)
- [`4.7.1-20190312-windowsservercore-ltsc2016`, `4.7.1-windowsservercore-ltsc2016`, `4.7.1` (*Dockerfile*)](https://github.com/Microsoft/dotnet-framework-docker/blob/master/4.7.1/runtime/windowsservercore-ltsc2016/Dockerfile)
- [`4.7-20190312-windowsservercore-ltsc2016`, `4.7-windowsservercore-ltsc2016`, `4.7` (*Dockerfile*)](https://github.com/Microsoft/dotnet-framework-docker/blob/master/4.7/runtime/windowsservercore-ltsc2016/Dockerfile)
- [`4.6.2-20190312-windowsservercore-ltsc2016`, `4.6.2-windowsservercore-ltsc2016`, `4.6.2` (*Dockerfile*)](https://github.com/Microsoft/dotnet-framework-docker/blob/master/4.6.2/runtime/windowsservercore-ltsc2016/Dockerfile)
- [`3.5-20190312-windowsservercore-ltsc2016`, `3.5-windowsservercore-ltsc2016`, `3.5` (*Dockerfile*)](https://github.com/Microsoft/dotnet-framework-docker/blob/master/3.5/runtime/windowsservercore-ltsc2016/Dockerfile)

For more information about these images and their history, please see [(`microsoft/dotnet-framework-docker`)](https://github.com/Microsoft/dotnet-framework-docker). These images are updated via [pull requests to the `Microsoft/dotnet-framework-docker` GitHub repo](https://github.com/Microsoft/dotnet-framework-docker/pulls).

# Support

See the [.NET Framework Lifecycle FAQ](https://support.microsoft.com/en-us/help/17455/lifecycle-faq-net-framework)

# Feedback

* [File a .NET Framework Docker issue](https://github.com/microsoft/dotnet-framework-docker/issues)
* [Report a .NET Framework problem](https://developercommunity.visualstudio.com/spaces/61/index.html)
* [Ask on Stack Overflow](https://stackoverflow.com/questions/tagged/.net)
* [Contact Microsoft Support](https://support.microsoft.com/contactus/)

# License

The .NET Framework images use the same license as the [Windows Server Core base image](https://hub.docker.com/_/microsoft-windows-servercore/).
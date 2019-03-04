# Latest Version of Common Tags

The following tags are the latest stable versions of the most commonly used images. The complete set of tags is listed further down.

- [`dotnetapp`](https://github.com/Microsoft/dotnet-framework-docker/blob/master/samples/dotnetapp/Dockerfile)
- [`aspnetapp`](https://github.com/Microsoft/dotnet-framework-docker/blob/master/samples/aspnetapp/Dockerfile)
- [`wcfservice`](https://github.com/Microsoft/dotnet-framework-docker/blob/master/samples/wcfapp/Dockerfile.web)
- [`wcfclient`](https://github.com/Microsoft/dotnet-framework-docker/blob/master/samples/wcfapp/Dockerfile.client)

The [.NET Framework Docker samples](https://github.com/Microsoft/dotnet-framework-docker/blob/master/samples/README.md) show various ways to use .NET Framework and Docker together. See [Building Docker Images for .NET Framework Applications](https://docs.microsoft.com/dotnet/framework/docker/) to learn more.

Watch [dotnet/announcements](https://github.com/dotnet/announcements/labels/Docker) for Docker-related .NET announcements.

## Container sample: Run a simple application

Type the following command to run a sample console application with Docker:

```console
docker run --rm microsoft/dotnet-framework-samples
```

## Container sample: Run a web application

Type the following command to run a sample web application with Docker:

```console
docker run -it --rm -p 8000:80 --name aspnet_sample microsoft/dotnet-framework-samples:aspnetapp
```

After the application starts, navigate to `http://localhost:8000` in your web browser. You need to navigate to the application via IP address instead of `localhost` for earlier Windows versions, which is demonstrated in [View the ASP.NET app in a running container on Windows](https://github.com/microsoft/dotnet-framework-docker/blob/master/samples/aspnetapp/README.md#view-the-aspnet-app-in-a-running-container-on-windows).

## Container sample: Run WCF service and client applications

Type the following command to run a sample WCF service application with Docker:

```console
docker run -it --rm --name wcfservice_sample microsoft/dotnet-framework-samples:wcfservie
```
After the container starts, find the IP address of the container instance:
```console
docker inspect --format="{{.NetworkSettings.Networks.nat.IPAddress}}" wcfservice_sample
172.26.236.119
```
Type the following Docker command to start a WCF client container, set environment variable HOST to the IP address of the wcfservice_sample container:
```console
docker run --name wcfclient_sample --rm -it -e HOST=172.26.236.119 microsoft/dotnet-framework-samples:wcfclient
```

# Complete set of Tags

## Windows Server 2019 amd64 tags

- [`dotnetapp-windowsservercore-ltsc2019`, `dotnetapp`, `latest` (*Dockerfile*)](https://github.com/Microsoft/dotnet-framework-docker/blob/master/samples/dotnetapp/Dockerfile)
- [`aspnetapp-windowsservercore-ltsc2019`, `aspnetapp` (*Dockerfile*)](https://github.com/Microsoft/dotnet-framework-docker/blob/master/samples/aspnetapp/Dockerfile)
- [`wcfservice-windowsservercore-ltsc2019`, `wcfservice` (*Dockerfile*)](https://github.com/Microsoft/dotnet-framework-docker/blob/master/samples/wcfapp/Dockerfile.web)
- [`wcfclient-windowsservercore-ltsc2019`, `wcfclient` (*Dockerfile*)](https://github.com/Microsoft/dotnet-framework-docker/blob/master/samples/wcfapp/Dockerfile.client)

## Windows Server, version 1803 amd64 tags

- [`dotnetapp-windowsservercore-1803`, `dotnetapp`, `latest` (*Dockerfile*)](https://github.com/Microsoft/dotnet-framework-docker/blob/master/samples/dotnetapp/Dockerfile)
- [`aspnetapp-windowsservercore-1803`, `aspnetapp` (*Dockerfile*)](https://github.com/Microsoft/dotnet-framework-docker/blob/master/samples/aspnetapp/Dockerfile)
- [`wcfservice-windowsservercore-1803`, `wcfservice` (*Dockerfile*)](https://github.com/Microsoft/dotnet-framework-docker/blob/master/samples/wcfapp/Dockerfile.web)
- [`wcfclient-windowsservercore-1803`, `wcfclient` (*Dockerfile*)](https://github.com/Microsoft/dotnet-framework-docker/blob/master/samples/wcfapp/Dockerfile.client)

## Windows Server, version 1709 amd64 tags

- [`dotnetapp-windowsservercore-1709`, `dotnetapp`, `latest` (*Dockerfile*)](https://github.com/Microsoft/dotnet-framework-docker/blob/master/samples/dotnetapp/Dockerfile)
- [`aspnetapp-windowsservercore-1709`, `aspnetapp` (*Dockerfile*)](https://github.com/Microsoft/dotnet-framework-docker/blob/master/samples/aspnetapp/Dockerfile)
- [`wcfservice-windowsservercore-1709`, `wcfservice` (*Dockerfile*)](https://github.com/Microsoft/dotnet-framework-docker/blob/master/samples/wcfapp/Dockerfile.web)
- [`wcfclient-windowsservercore-1709`, `wcfclient` (*Dockerfile*)](https://github.com/Microsoft/dotnet-framework-docker/blob/master/samples/wcfapp/Dockerfile.client)

## Windows Server 2016 amd64 tags

- [`dotnetapp-windowsservercore-ltsc2016`, `dotnetapp`, `latest` (*Dockerfile*)](https://github.com/Microsoft/dotnet-framework-docker/blob/master/samples/dotnetapp/Dockerfile)
- [`aspnetapp-windowsservercore-ltsc2016`, `aspnetapp` (*Dockerfile*)](https://github.com/Microsoft/dotnet-framework-docker/blob/master/samples/aspnetapp/Dockerfile)
- [`wcfservice-windowsservercore-ltsc2016`, `wcfservice` (*Dockerfile*)](https://github.com/Microsoft/dotnet-framework-docker/blob/master/samples/wcfapp/Dockerfile.web)
- [`wcfclient-windowsservercore-ltsc2016`, `wcfclient` (*Dockerfile*)](https://github.com/Microsoft/dotnet-framework-docker/blob/master/samples/wcfapp/Dockerfile.client)

# What is the .NET Framework?

The [.NET Framework](https://www.microsoft.com/net/framework) is a general purpose development platform maintained by Microsoft. It is the most popular way to build client and server applications for Windows and Windows Server. It is included with Windows, Windows Server and Windows Server Core. It includes server technologies such as ASP.NET Web Forms, ASP.NET MVC and Windows Communication Framework (WCF) applications, which you can run in Docker containers.

.NET has several capabilities that make development easier, including automatic memory management, (runtime) generic types, reflection, asynchrony, concurrency, and native interop. Millions of developers take advantage of these capabilities to efficiently build high-quality web and client applications.

You can use C#, F# and VB to write .NET Framework apps. C# is simple, powerful, type-safe, and object-oriented while retaining the expressiveness and elegance of C-style languages. F# is a multi-paradigm programming language, enabling both functional and object-oriented patterns and practices. VB is a rapid development programming language with the deepest integration between the language and Visual Studio, providing the fastest path to a working app.   

The .NET Framework was first released by Microsoft in 2001. The latest version is [.NET Framework 4.7.2](https://www.microsoft.com/net/framework).

> https://docs.microsoft.com/dotnet/framework/

![dotnet-icon](https://cloud.githubusercontent.com/assets/2608468/19951790/a0458278-a11d-11e6-86e4-660aaa22aa3c.png)

# Issues

If you have any problems with or questions about this image, please contact us through a [GitHub issue](https://github.com/microsoft/dotnet-framework-docker/issues).

# Licenses

* [Windows Server Core license](https://hub.docker.com/r/microsoft/windowsservercore/)

# Related Repos

.NET Framework:

* [dotnet/framework/aspnet](https://hub.docker.com/_/microsoft-dotnet-framework-aspnet/): ASP.NET Web Forms and MVC
* [microsoft/dotnet-framework](https://hub.docker.com/r/microsoft/dotnet-framework/): .NET Framework
* [dotnet/framework/wcf](https://hub.docker.com/_/microsoft-dotnet-framework-wcf/): WCF

.NET Core:

* [dotnet/core](https://hub.docker.com/_/microsoft-dotnet-core/): .NET Core
* [dotnet/core/samples](https://hub.docker.com/_/microsoft-dotnet-core-samples/): .NET Core Samples
* [dotnet/core-nightly](https://hub.docker.com/_/microsoft-dotnet-core-nightly/): .NET Core (Preview)

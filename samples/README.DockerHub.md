# Latest Version of Common Tags

The following tags are the latest stable versions of the most commonly used images. The complete set of tags is listed further down.

- [`dotnetapp`](https://github.com/Microsoft/dotnet-framework-docker/blob/master/samples/dotnetapp/Dockerfile)
- [`aspnetapp`](https://github.com/Microsoft/dotnet-framework-docker/blob/master/samples/aspnetapp/Dockerfile)
- [`wcfservice`](https://github.com/Microsoft/dotnet-framework-docker/blob/master/samples/wcfapp/Dockerfile.web)
- [`wcfclient`](https://github.com/Microsoft/dotnet-framework-docker/blob/master/samples/wcfapp/Dockerfile.client)

The [.NET Framework Docker samples](https://github.com/Microsoft/dotnet-framework-docker/blob/master/samples/README.md) show various ways to use .NET Framework and Docker together. See [Building Docker Images for .NET Framework Applications](https://docs.microsoft.com/dotnet/framework/docker/) to learn more.

Watch [dotnet/announcements](https://github.com/dotnet/announcements/labels/Docker) for Docker-related .NET announcements.

### Container sample: Run a simple application

Type the following command to run a sample console application with Docker:

```console
docker run --rm microsoft/dotnet-framework-samples
```

### Container sample: Run a web application

Type the following command to run a sample web application with Docker:

```console
docker run -it --rm -p 8000:80 --name aspnet_sample microsoft/dotnet-framework-samples:aspnetapp
```

After the application starts, navigate to `http://localhost:8000` in your web browser. You need to navigate to the application via IP address instead of `localhost` for earlier Windows versions, which is demonstrated in [View the ASP.NET app in a running container on Windows](https://github.com/microsoft/dotnet-framework-docker/blob/master/samples/aspnetapp/README.md#view-the-aspnet-app-in-a-running-container-on-windows).

### Container sample: Run a WCF service application

Type the following command to run a sample WCF service application with Docker:

```console
docker run -it --rm --name wcfservice_sample microsoft/dotnet-framework-samples:wcfservie
```
After the container starts, you can find IP address of the container and then hookup the client application to the service. See [Try pre-built WCF Docker Images](https://github.com/microsoft/dotnet-framework-docker/blob/master/samples/wcfapp/README.md#try-pre-built-wcf-docker-images) for more details.

## Complete set of Tags

# Windows Server, version 1803 tags

- [`dotnetapp-windowsservercore-1803`, `dotnetapp`, `latest` (*samples/dotnetapp/Dockerfile*)](https://github.com/Microsoft/dotnet-framework-docker/blob/master/samples/dotnetapp/Dockerfile)
- [`aspnetapp-windowsservercore-1803`, `aspnetapp` (*samples/aspnetapp/Dockerfile*)](https://github.com/Microsoft/dotnet-framework-docker/blob/master/samples/aspnetapp/Dockerfile)
- [`wcfservice-windowsservercore-1803`, `wcfservice` (*samples/wcfapp/Dockerfile.web*)](https://github.com/Microsoft/dotnet-framework-docker/blob/master/samples/wcfapp/Dockerfile.web)
- [`wcfclient-windowsservercore-1803`, `wcfclient` (*samples/wcfapp/Dockerfile.client*)](https://github.com/Microsoft/dotnet-framework-docker/blob/master/samples/wcfapp/Dockerfile.client)

# Windows Server, version 1709 amd64 tags

- [`dotnetapp-windowsservercore-1709`, `dotnetapp`, `latest` (*samples/dotnetapp/Dockerfile*)](https://github.com/Microsoft/dotnet-framework-docker/blob/master/samples/dotnetapp/Dockerfile)
- [`aspnetapp-windowsservercore-1709`, `aspnetapp` (*samples/aspnetapp/Dockerfile*)](https://github.com/Microsoft/dotnet-framework-docker/blob/master/samples/aspnetapp/Dockerfile)
- [`wcfservice-windowsservercore-1709`, `wcfservice` (*samples/wcfapp/Dockerfile.web*)](https://github.com/Microsoft/dotnet-framework-docker/blob/master/samples/wcfapp/Dockerfile.web)
- [`wcfclient-windowsservercore-1709`, `wcfclient` (*samples/wcfapp/Dockerfile.client*)](https://github.com/Microsoft/dotnet-framework-docker/blob/master/samples/wcfapp/Dockerfile.client)

# Windows Server 2016 amd64 tags

- [`dotnetapp-windowsservercore-ltsc2016`, `dotnetapp`, `latest` (*samples/dotnetapp/Dockerfile*)](https://github.com/Microsoft/dotnet-framework-docker/blob/master/samples/dotnetapp/Dockerfile)
- [`aspnetapp-windowsservercore-ltsc2016`, `aspnetapp` (*samples/aspnetapp/Dockerfile*)](https://github.com/Microsoft/dotnet-framework-docker/blob/master/samples/aspnetapp/Dockerfile)
- [`wcfservice-windowsservercore-ltsc2016`, `wcfservice` (*samples/wcfapp/Dockerfile.web*)](https://github.com/Microsoft/dotnet-framework-docker/blob/master/samples/wcfapp/Dockerfile.web)
- [`wcfclient-windowsservercore-ltsc2016`, `wcfclient` (*samples/wcfapp/Dockerfile.client*)](https://github.com/Microsoft/dotnet-framework-docker/blob/master/samples/wcfapp/Dockerfile.client)

# What is the .NET Framework?

The [.NET Framework](https://www.microsoft.com/net/framework) is a general purpose development platform maintained by Microsoft. It is the most popular way to build client and server applications for Windows and Windows Server. It is included with Windows, Windows Server and Windows Server Core. It includes server technologies such as ASP.NET Web Forms, ASP.NET MVC and Windows Communication Framework (WCF) applications, which you can run in Docker containers.

.NET has several capabilities that make development easier, including automatic memory management, (runtime) generic types, reflection, asynchrony, concurrency, and native interop. Millions of developers take advantage of these capabilities to efficiently build high-quality web and client applications.

You can use C#, F# and VB to write .NET Framework apps. C# is simple, powerful, type-safe, and object-oriented while retaining the expressiveness and elegance of C-style languages. F# is a multi-paradigm programming language, enabling both functional and object-oriented patterns and practices. VB is a rapid development programming language with the deepest integration between the language and Visual Studio, providing the fastest path to a working app.   

The .NET Framework was first released by Microsoft in 2001. The latest version is [.NET Framework 4.7.2](https://www.microsoft.com/net/framework).

> https://docs.microsoft.com/dotnet/framework/

![dotnet-icon](https://cloud.githubusercontent.com/assets/2608468/19951790/a0458278-a11d-11e6-86e4-660aaa22aa3c.png)

## Issues

If you have any problems with or questions about this image, please contact us through a [GitHub issue](https://github.com/microsoft/dotnet-framework-docker/issues).

## Licenses

* [Windows Server Core license](https://hub.docker.com/r/microsoft/windowsservercore/)

## Related Repos

See the following related repos for other application types:

* [microsoft/dotnet](https://hub.docker.com/r/microsoft/dotnet/) for .NET Core images.
* [microsoft/aspnet](https://hub.docker.com/r/microsoft/aspnet/) for ASP.NET Web Forms and MVC images.
* [microsoft/dotnet-framework](https://hub.docker.com/r/microsoft/dotnet-framework/) for .NET Framework images (for web applications, see microsoft/aspnet).
* [microsoft/wcf](https://hub.docker.com/r/microsoft/wcf/) for WCF images.

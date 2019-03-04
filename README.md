# Latest Version of Common Tags

The following tags are the latest stable versions of the most commonly used images. The complete set of tags is listed further down.

- [`4.7.2-runtime`](https://github.com/Microsoft/dotnet-framework-docker/blob/master/4.7.2/runtime/windowsservercore-ltsc2016/Dockerfile)
- [`4.7.2-sdk`](https://github.com/Microsoft/dotnet-framework-docker/blob/master/4.7.2/sdk/windowsservercore-ltsc2016/Dockerfile)
- [`3.5-runtime`](https://github.com/Microsoft/dotnet-framework-docker/blob/master/3.5/runtime/windowsservercore-ltsc2016/Dockerfile)
- [`3.5-sdk`](https://github.com/Microsoft/dotnet-framework-docker/blob/master/3.5/sdk/windowsservercore-ltsc2016/Dockerfile)

The [.NET Framework Docker samples](https://github.com/Microsoft/dotnet-framework-docker/tree/master/samples/README.md) show various ways to use .NET Framework and Docker together.

Watch [dotnet/announcements](https://github.com/dotnet/announcements/labels/Docker) for Docker-related .NET announcements.

## Container sample: Run a simple application

Type the following command to run a sample console application:

```console
docker run --rm microsoft/dotnet-framework-samples
```

## Container sample: Run a web application

Type the following command to run a sample web application:

```console
docker run -it --rm -p 8000:80 --name aspnet_sample microsoft/dotnet-framework-samples:aspnetapp
```

After the application starts, navigate to `http://localhost:8000` in your web browser. You need to navigate to the application via IP address instead of `localhost` for earlier Windows versions, which is demonstrated in [View the ASP.NET app in a running container on Windows](https://github.com/microsoft/dotnet-framework-docker/blob/master/samples/aspnetapp/README.md#view-the-aspnet-app-in-a-running-container-on-windows).

# Complete set of Tags

## Windows Server 2019 amd64 tags

- [`4.7.2-runtime-20190212-windowsservercore-ltsc2019`, `4.7.2-runtime-windowsservercore-ltsc2019`, `4.7.2-runtime`, `runtime`, `latest` (*Dockerfile*)](https://github.com/Microsoft/dotnet-framework-docker/blob/master/4.7.2/runtime/windowsservercore-ltsc2019/Dockerfile)
- [`4.7.2-sdk-20190212-windowsservercore-ltsc2019`, `4.7.2-sdk-windowsservercore-ltsc2019`, `4.7.2-sdk`, `sdk` (*Dockerfile*)](https://github.com/Microsoft/dotnet-framework-docker/blob/master/4.7.2/sdk/windowsservercore-ltsc2019/Dockerfile)
- [`3.5-runtime-20190212-windowsservercore-ltsc2019`, `3.5-runtime-windowsservercore-ltsc2019`, `3.5-runtime` (*Dockerfile*)](https://github.com/Microsoft/dotnet-framework-docker/blob/master/3.5/runtime/windowsservercore-ltsc2019/Dockerfile)
- [`3.5-sdk-20190212-windowsservercore-ltsc2019`, `3.5-sdk-windowsservercore-ltsc2019`, `3.5-sdk` (*Dockerfile*)](https://github.com/Microsoft/dotnet-framework-docker/blob/master/3.5/sdk/windowsservercore-ltsc2019/Dockerfile)

## Windows Server, version 1803 amd64 tags

- [`4.7.2-runtime-20190212-windowsservercore-1803`, `4.7.2-runtime-windowsservercore-1803`, `4.7.2-runtime`, `runtime`, `latest` (*Dockerfile*)](https://github.com/Microsoft/dotnet-framework-docker/blob/master/4.7.2/runtime/windowsservercore-1803/Dockerfile)
- [`4.7.2-sdk-20190212-windowsservercore-1803`, `4.7.2-sdk-windowsservercore-1803`, `4.7.2-sdk`, `sdk` (*Dockerfile*)](https://github.com/Microsoft/dotnet-framework-docker/blob/master/4.7.2/sdk/windowsservercore-1803/Dockerfile)
- [`3.5-runtime-20190212-windowsservercore-1803`, `3.5-runtime-windowsservercore-1803`, `3.5-runtime` (*Dockerfile*)](https://github.com/Microsoft/dotnet-framework-docker/blob/master/3.5/runtime/windowsservercore-1803/Dockerfile)
- [`3.5-sdk-20190212-windowsservercore-1803`, `3.5-sdk-windowsservercore-1803`, `3.5-sdk` (*Dockerfile*)](https://github.com/Microsoft/dotnet-framework-docker/blob/master/3.5/sdk/windowsservercore-1803/Dockerfile)

## Windows Server, version 1709 amd64 tags

- [`4.7.2-runtime-20190212-windowsservercore-1709`, `4.7.2-runtime-windowsservercore-1709`, `4.7.2-runtime`, `runtime`, `latest` (*Dockerfile*)](https://github.com/Microsoft/dotnet-framework-docker/blob/master/4.7.2/runtime/windowsservercore-1709/Dockerfile)
- [`4.7.2-sdk-20190212-windowsservercore-1709`, `4.7.2-sdk-windowsservercore-1709`, `4.7.2-sdk`, `sdk` (*Dockerfile*)](https://github.com/Microsoft/dotnet-framework-docker/blob/master/4.7.2/sdk/windowsservercore-1709/Dockerfile)
- [`4.7.1-runtime-20190212-windowsservercore-1709`, `4.7.1-runtime-windowsservercore-1709`, `4.7.1-runtime` (*Dockerfile*)](https://github.com/Microsoft/dotnet-framework-docker/blob/master/4.7.1/runtime/windowsservercore-1709/Dockerfile)
- [`4.7.1-sdk-20190212-windowsservercore-1709`, `4.7.1-sdk-windowsservercore-1709`, `4.7.1-sdk` (*Dockerfile*)](https://github.com/Microsoft/dotnet-framework-docker/blob/master/4.7.1/sdk/windowsservercore-1709/Dockerfile)
- [`3.5-runtime-20190212-windowsservercore-1709`, `3.5-runtime-windowsservercore-1709`, `3.5-runtime` (*Dockerfile*)](https://github.com/Microsoft/dotnet-framework-docker/blob/master/3.5/runtime/windowsservercore-1709/Dockerfile)
- [`3.5-sdk-20190212-windowsservercore-1709`, `3.5-sdk-windowsservercore-1709`, `3.5-sdk` (*Dockerfile*)](https://github.com/Microsoft/dotnet-framework-docker/blob/master/3.5/sdk/windowsservercore-1709/Dockerfile)

## Windows Server 2016 amd64 tags

- [`4.7.2-runtime-20190212-windowsservercore-ltsc2016`, `4.7.2-runtime-windowsservercore-ltsc2016`, `4.7.2-runtime`, `runtime`, `latest` (*Dockerfile*)](https://github.com/Microsoft/dotnet-framework-docker/blob/master/4.7.2/runtime/windowsservercore-ltsc2016/Dockerfile)
- [`4.7.2-sdk-20190212-windowsservercore-ltsc2016`, `4.7.2-sdk-windowsservercore-ltsc2016`, `4.7.2-sdk`, `sdk` (*Dockerfile*)](https://github.com/Microsoft/dotnet-framework-docker/blob/master/4.7.2/sdk/windowsservercore-ltsc2016/Dockerfile)
- [`4.7.1-runtime-20190212-windowsservercore-ltsc2016`, `4.7.1-runtime-windowsservercore-ltsc2016`, `4.7.1-runtime` (*Dockerfile*)](https://github.com/Microsoft/dotnet-framework-docker/blob/master/4.7.1/runtime/windowsservercore-ltsc2016/Dockerfile)
- [`4.7.1-sdk-20190212-windowsservercore-ltsc2016`, `4.7.1-sdk-windowsservercore-ltsc2016`, `4.7.1-sdk` (*Dockerfile*)](https://github.com/Microsoft/dotnet-framework-docker/blob/master/4.7.1/sdk/windowsservercore-ltsc2016/Dockerfile)
- [`4.7-runtime-20190212-windowsservercore-ltsc2016`, `4.7-runtime-windowsservercore-ltsc2016`, `4.7-runtime` (*Dockerfile*)](https://github.com/Microsoft/dotnet-framework-docker/blob/master/4.7/runtime/windowsservercore-ltsc2016/Dockerfile)
- [`4.6.2-runtime-20190212-windowsservercore-ltsc2016`, `4.6.2-runtime-windowsservercore-ltsc2016`, `4.6.2-runtime` (*Dockerfile*)](https://github.com/Microsoft/dotnet-framework-docker/blob/master/4.6.2/runtime/windowsservercore-ltsc2016/Dockerfile)
- [`3.5-runtime-20190212-windowsservercore-ltsc2016`, `3.5-runtime-windowsservercore-ltsc2016`, `3.5-runtime` (*Dockerfile*)](https://github.com/Microsoft/dotnet-framework-docker/blob/master/3.5/runtime/windowsservercore-ltsc2016/Dockerfile)
- [`3.5-sdk-20190212-windowsservercore-ltsc2016`, `3.5-sdk-windowsservercore-ltsc2016`, `3.5-sdk` (*Dockerfile*)](https://github.com/Microsoft/dotnet-framework-docker/blob/master/3.5/sdk/windowsservercore-ltsc2016/Dockerfile)

For more information about these images and their history, please see [(`microsoft/dotnet-framework-docker`)](https://github.com/Microsoft/dotnet-framework-docker). These images are updated via [pull requests to the `Microsoft/dotnet-framework-docker` GitHub repo](https://github.com/Microsoft/dotnet-framework-docker/pulls).

# What is the .NET Framework?

The [.NET Framework](https://www.microsoft.com/net/framework) is a general purpose development platform maintained by Microsoft. It is the most popular way to build client and server applications for Windows and Windows Server. It is included with Windows, Windows Server and Windows Server Core. It includes server technologies such as ASP.NET Web Forms, ASP.NET MVC and Windows Communication Framework (WCF) applications, which you can run in Docker containers.

.NET has several capabilities that make development easier, including automatic memory management, (runtime) generic types, reflection, asynchrony, concurrency, and native interop. Millions of developers take advantage of these capabilities to efficiently build high-quality web and client applications.

You can use C#, F# and VB to write .NET Framework apps. C# is simple, powerful, type-safe, and object-oriented while retaining the expressiveness and elegance of C-style languages. F# is a multi-paradigm programming language, enabling both functional and object-oriented patterns and practices. VB is a rapid development programming language with the deepest integration between the language and Visual Studio, providing the fastest path to a working app.   

The .NET Framework was first released by Microsoft in 2001. The latest version is [.NET Framework 4.7.2](https://www.microsoft.com/net/framework).

> https://docs.microsoft.com/dotnet/framework/

![dotnet-icon](https://cloud.githubusercontent.com/assets/2608468/19951790/a0458278-a11d-11e6-86e4-660aaa22aa3c.png)

# .NET Framework Docker Samples

The [.NET Framework Docker samples](https://github.com/Microsoft/dotnet-framework-docker/tree/master/samples/README.md) show various ways to use .NET Framework and Docker together.

## Building .NET Framework Apps with Docker

* [.NET Framework Console Docker Sample](https://github.com/Microsoft/dotnet-framework-docker/tree/master/samples/dotnetapp/README.md) - This [sample](https://github.com/Microsoft/dotnet-framework-docker/tree/master/samples/dotnetapp/Dockerfile) builds, tests, and runs the sample. It includes and builds multiple projects.
* [ASP.NET Web Forms Docker Sample](https://github.com/Microsoft/dotnet-framework-docker/tree/master/samples/aspnetapp/README.md) - This [sample](https://github.com/Microsoft/dotnet-framework-docker/tree/master/samples/aspnetapp/Dockerfile) demonstrates using Docker with an ASP.NET Web Forms app.
* [ASP.NET MVC Docker Sample](https://github.com/Microsoft/dotnet-framework-docker/tree/master/samples/aspnetmvcapp/README.md) - This [sample](https://github.com/Microsoft/dotnet-framework-docker/tree/master/samples/aspnetmvcapp/Dockerfile) demonstrates using Docker with an ASP.NET MVC app.

# Image variants

The `microsoft/dotnet-framework` images come in different flavors, each designed for a specific use case.

## `4.7.2`

This is the primary image. If you are unsure about what your needs are, you probably want to use this one.

This image is for .NET Framework 4.0 and later version applications. It is based on the [Windows Server Core image](https://hub.docker.com/r/microsoft/windowsservercore/).

## `4.7`, `4.6.2`, `4.7.1`

These images are for applications that need a specific .NET Framework version and have not been tested with .NET Framework 4.7.2. It is based on the [Windows Server Core image](https://hub.docker.com/r/microsoft/windowsservercore/).

## `3.5`

This image is for .NET Framework 3.5 and earlier version applications.  It is based on the [Windows Server Core image](https://hub.docker.com/r/microsoft/windowsservercore/).

# Issues

If you have any problems with or questions about this image, please contact us through a [GitHub issue](https://github.com/microsoft/dotnet-framework-docker/issues).

# Licenses

The .NET Framework images use the same license as the [Windows Server Core base image](https://hub.docker.com/r/microsoft/windowsservercore/).

# Related Repos

.NET Framework:

* [dotnet/framework/aspnet](https://hub.docker.com/_/microsoft-dotnet-framework-aspnet/): ASP.NET Web Forms and MVC
* [dotnet/framework/wcf](https://hub.docker.com/_/microsoft-dotnet-framework-wcf/): WCF
* [microsoft/dotnet-framework-samples](https://hub.docker.com/r/microsoft/dotnet-framework-samples/): .NET Framework, ASP.NET and WCF Samples

.NET Core:

* [dotnet/core](https://hub.docker.com/_/microsoft-dotnet-core/): .NET Core
* [dotnet/core/samples](https://hub.docker.com/_/microsoft-dotnet-core-samples/): .NET Core Samples
* [dotnet/core-nightly](https://hub.docker.com/_/microsoft-dotnet-core-nightly/): .NET Core (Preview)

# Supported Windows Server, version 1709 amd64 tags

- [`4.7.1-windowsservercore-1709`, `4.7.1`, `latest` (*4.7.1-windowsservercore-1709/runtime/Dockerfile*)](https://github.com/Microsoft/dotnet-framework-docker/blob/master/4.7.1-windowsservercore-1709/runtime/Dockerfile)
- [`3.5-windowsservercore-1709`, `3.5` (*3.5-windowsservercore-1709/runtime/Dockerfile*)](https://github.com/Microsoft/dotnet-framework-docker/blob/master/3.5-windowsservercore-1709/runtime/Dockerfile)

# Supported Windows Server 2016 amd64 tags

- [`4.7.1-windowsservercore-ltsc2016`, `4.7.1`, `latest` (*4.7.1-windowsservercore-ltsc2016/runtime/Dockerfile*)](https://github.com/Microsoft/dotnet-framework-docker/blob/master/4.7.1-windowsservercore-ltsc2016/runtime/Dockerfile)
- [`4.7-windowsservercore-ltsc2016`, `4.7` (*4.7-windowsservercore-ltsc2016/runtime/Dockerfile*)](https://github.com/Microsoft/dotnet-framework-docker/blob/master/4.7-windowsservercore-ltsc2016/runtime/Dockerfile)
- [`4.6.2-windowsservercore-ltsc2016`, `4.6.2` (*4.6.2-windowsservercore-ltsc2016/runtime/Dockerfile*)](https://github.com/Microsoft/dotnet-framework-docker/blob/master/4.6.2-windowsservercore-ltsc2016/runtime/Dockerfile)
- [`3.5-windowsservercore-ltsc2016`, `3.5` (*3.5-windowsservercore-ltsc2016/runtime/Dockerfile*)](https://github.com/Microsoft/dotnet-framework-docker/blob/master/3.5-windowsservercore-ltsc2016/runtime/Dockerfile)

For more information about these images and their history, please see [(`microsoft/dotnet-framework-docker`)](https://github.com/Microsoft/dotnet-framework-docker). Watch [dotnet/announcements](https://github.com/dotnet/announcements/labels/Docker) for Docker-related .NET announcements.

# What is the .NET Framework?

The [.NET Framework](https://www.microsoft.com/net/framework) is a general purpose development platform maintained by Microsoft. It is the most popular way to build client and server applications for Windows and Windows Server. It is included with Windows, Windows Server and Windows Server Core. It includes server technologies such as ASP.NET Web Forms, ASP.NET MVC and Windows Communication Framework (WCF) applications, which you can run in Docker containers.

.NET has several capabilities that make development easier, including automatic memory management, (runtime) generic types, reflection, asynchrony, concurrency, and native interop. Millions of developers take advantage of these capabilities to efficiently build high-quality web and client applications.

You can use C#, F# and VB to write .NET Framework apps. C# is simple, powerful, type-safe, and object-oriented while retaining the expressiveness and elegance of C-style languages. F# is a multi-paradigm programming language, enabling both functional and object-oriented patterns and practices. VB is a rapid development programming language with the deepest integration between the language and Visual Studio, providing the fastest path to a working app.   

The .NET Framework was first released by Microsoft in 2001. The latest version is [.NET Framework 4.7.1](https://www.microsoft.com/net/framework).

> https://docs.microsoft.com/dotnet/framework/

![dotnet-icon](https://cloud.githubusercontent.com/assets/2608468/19951790/a0458278-a11d-11e6-86e4-660aaa22aa3c.png)

# How to use these Images

These images are based on [Windows Containers][win-containers]. You need to be Windows 10 or Windows Server 2016 to use them.

## Deploying a .NET Framework 4.x application with Docker

It is easy to create a Docker image for a .NET Framework 4.x application. You can try the instructions below or check out the [.NET Framework 4.7 Docker sample](https://github.com/Microsoft/dotnet-framework-docker-samples/tree/master/dotnetapp-4.7) if you want to try a pre-made version that's ready go.

.NET Framework Docker images can utilize the [multi-stage build feature](https://docs.docker.com/engine/userguide/eng-image/multistage-build/). This feature allows multiple FROM instructions to be used in one Dockerfile. Using this feature, you can build a .NET Framework app using an build image and then copy the published app into a lighter weight runtime image within a single Dockerfile.

1. Add a `Dockerfile` file with the following content to your project. The `Dockerfile` and the Docker commands assume that your application is called `dotnetapp.exe`. Please update your `Dockerfile` as appropriate.

    ```dockerfile
    FROM microsoft/dotnet-framework-build:4.7.1 AS build-env
    
    WORKDIR /app
    COPY . .
    
    RUN msbuild.exe /t:Build /p:Configuration=Release /p:OutputPath=out
    
    FROM microsoft/dotnet-framework:4.7.1
    WORKDIR /app
    COPY --from=build-env /app/out ./
    ENTRYPOINT ["dotnetapp.exe"]
    ```

2. Type the following Docker commands at the command line, within your project directory (beside Program.cs). You can change the tag name (`dotnetapp`) to your own string, as you like.

    ```console
    docker build -t dotnetapp .
    docker run --rm dotnetapp
    ```

The Docker runtime image used in this example includes the .NET Framework 4.7.1, however, your application does not need to explicity target the .NET Framework 4.7.1. Applications that target .NET Framework 4.0 or later should work correctly with this image.

## Deploying a .NET Framework 3.5 application with Docker

It is easy to create a Docker image for a .NET Framework 3.5 application. You can try the instructions below or check out the [.NET Framework 3.5 Docker sample](https://github.com/Microsoft/dotnet-framework-docker-samples/tree/master/dotnetapp-3.5) if you want to try a pre-made version that's ready go.

1. Build your application in Visual Studio or at the command line. 
2. Add a `Dockerfile` file with the following content to your project. The Dockerfile assumes that your app is built to the `bin\Release` directory and that your app name is `dotnetapp.exe`. Please update your `Dockerfile` as appropriate.

    ```Dockerfile
    FROM microsoft/dotnet-framework:3.5
    WORKDIR \app
    COPY bin\Release .
    ENTRYPOINT ["dotnetapp.exe"]
    ```

3. Type the following Docker commands at the command line, within your project directory (beside Program.cs). You can change the tag name (`dotnetapp`) to your own string, as you like.

    ```console
    docker build -t dotnetapp .
    docker run dotnetapp
    ```

The Docker image includes the .NET Framework 3.5, however, your application does not need to explicity target the .NET Framework 3.5. Applications that target .NET Framework 1.0 through 3.5 should work correctly with this image.

## Image variants

The `microsoft/dotnet-framework` images come in different flavors, each designed for a specific use case.

### `4.7.1`

This is the primary image. If you are unsure about what your needs are, you probably want to use this one.

This image is for .NET Framework 4.0 and later version applications. It is based on the [Windows Server Core image](https://hub.docker.com/r/microsoft/windowsservercore/).

### `4.7`, `4.6.2`

These images are for applications that need a specific .NET Framework version and have not been tested with .NET Framework 4.7.1. It is based on the [Windows Server Core image](https://hub.docker.com/r/microsoft/windowsservercore/).

### `3.5`

This image is for .NET Framework 3.5 and earlier version applications.  It is based on the [Windows Server Core image](https://hub.docker.com/r/microsoft/windowsservercore/).

## Related Repos and Examples

See [.NET Framework Docker samples](https://github.com/Microsoft/dotnet-framework-docker-samples) to get started with pre-made samples.

See [.NET Framework and Docker](https://docs.microsoft.com/dotnet/framework/docker/) to learn more about using .NET Framework with Docker.

See the following related repos for other application types:

- [microsoft/dotnet-framework-build](https://hub.docker.com/r/microsoft/dotnet-framework-build/) for .NET Framework build images.
- [microsoft/aspnet](https://hub.docker.com/r/microsoft/aspnet/) for ASP.NET Web Forms and MVC images.
- [microsoft/dotnet](https://hub.docker.com/r/microsoft/dotnet/) for .NET Core images.
- [microsoft/aspnetcore](https://hub.docker.com/r/microsoft/aspnetcore/) for ASP.NET Core images.

You can read more about [Windows Containers][win-containers] to learn how to use Docker with Windows.

# License

The .NET Framework images use the same license as the [Windows Server Core base image](https://hub.docker.com/r/microsoft/windowsservercore/), as follows:

MICROSOFT SOFTWARE SUPPLEMENTAL LICENSE TERMS

CONTAINER OS IMAGE

Microsoft Corporation (or based on where you live, one of its affiliates) (referenced as “us,” “we,” or “Microsoft”) licenses this Container OS Image supplement to you (“Supplement”). You are licensed to use this Supplement in conjunction with the underlying host operating system software (“Host Software”) solely to assist running the containers feature in the Host Software. The Host Software license terms apply to your use of the Supplement. You may not use it if you do not have a license for the Host Software. You may use this Supplement with each validly licensed copy of the Host Software.

# Supported Docker versions

This image is officially supported on Docker version 1.12.2.

Please see [Getting Started with Docker for Windows](https://docs.docker.com/docker-for-windows/) for details on how to install or upgrade Docker to use Windows Containers.

# User Feedback

## Issues and Contributing

You are invited to report issues or request features by creating a [GitHub issue](https://github.com/microsoft/dotnet-framework-docker/issues).

## Documentation

You can read documentation for using the .NET Framework with Docker usage in the [.NET docs](https://docs.microsoft.com/dotnet/framework/docker). The docs are [open source on GitHub](https://github.com/dotnet/docs). Contributions are welcome!

[win-containers]: http://aka.ms/windowscontainers

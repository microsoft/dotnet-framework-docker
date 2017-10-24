# Supported Windows Server 2016 Version 1709 (Fall Creators Update) amd64 tags

- [`4.7.1-1-windowsservercore-1709`, `4.7.1-windowsservercore-1709` (*4.7.1-windowsservercore-1709/build/Dockerfile*)](https://github.com/Microsoft/dotnet-framework-docker/blob/master/4.7.1-windowsservercore-1709/build/Dockerfile)

# Supported Windows Server 2016 amd64 tags

- [`4.7.1-1-windowsservercore-10.0.14393`, `4.7.1-windowsservercore-10.0.14393`, `4.7.1-1`, `4.7.1`, `latest` (*4.7.1/build/Dockerfile*)](https://github.com/Microsoft/dotnet-framework-docker/blob/master/4.7.1/build/Dockerfile)

For more information about these images and their history, please see [the relevent Dockerfile (`microsoft/dotnet-framework-docker`)](https://github.com/microsoft/dotnet-framework-docker/search?utf8=%E2%9C%93&q=FROM&type=Code). These images are updated via [pull requests to the `microsoft/dotnet-framework-docker` GitHub repo](https://github.com/microsoft/dotnet-framework-docker/pulls?utf8=%E2%9C%93&q=).

# What is the .NET Framework?

The [.NET Framework](https://www.microsoft.com/net/framework) is a general purpose development platform maintained by Microsoft. It is the most popular way to build client and server applications for Windows and Windows Server. It is included with Windows, Windows Server and Windows Server Core. It includes server technologies such as ASP.NET Web Forms, ASP.NET MVC and Windows Communication Framework (WCF) applications, which you can run in Docker containers.

.NET has several capabilities that make development easier, including automatic memory management, (runtime) generic types, reflection, asynchrony, concurrency, and native interop. Millions of developers take advantage of these capabilities to efficiently build high-quality web and client applications.

You can use C#, F# and VB to write .NET Framework apps. C# is simple, powerful, type-safe, and object-oriented while retaining the expressiveness and elegance of C-style languages. F# is a multi-paradigm programming language, enabling both functional and object-oriented patterns and practices. VB is a rapid development programming language with the deepest integration between the language and Visual Studio, providing the fastest path to a working app.   

The .NET Framework was first released by Microsoft in 2001. The latest version is [.NET Framework 4.7](https://www.microsoft.com/net/framework).

> https://docs.microsoft.com/dotnet/framework/

![dotnet-icon](https://cloud.githubusercontent.com/assets/2608468/19951790/a0458278-a11d-11e6-86e4-660aaa22aa3c.png)

# How to use these Images

These images are based on [Windows Containers][win-containers]. You need to be Windows 10 or Windows Server 2016 to use them.

## Building a .NET Framework 4.7.1 application with a multi-stage Dockerfile

.NET Framework Docker images can utilize the [multi-stage build feature](https://docs.docker.com/engine/userguide/eng-image/multistage-build/). This feature allows multiple FROM instructions to be used in one Dockerfile. Using this feature, you can build a .NET Framework app using an build image and then copy the published app into a lighter weight runtime image within a single Dockerfile.

Add a `Dockerfile` to your .NET Framework project with the following:

```dockerfile
FROM microsoft/dotnet-framework-build:4.7.1 AS build-env

WORKDIR /app
COPY . .

RUN msbuild.exe /t:Build /p:Configuration=Release /p:OutputPath=out

FROM microsoft/dotnet-framework:4.7.1
WORKDIR /app
COPY --from=build-env /app/out ./
ENTRYPOINT ["DotNetApp.exe"]

```

Build and run the Docker image:

```console
docker build -t dotnetapp .
docker run --rm dotnetapp
```

The `Dockerfile` and the Docker commands assumes that your application is called `DotNetApp`. You can change the `Dockerfile` and the commands, as needed.

## Image variants

The `microsoft/dotnet-framework-build` images come in different flavors, each designed for a specific use case.

### `4.7.1`

This image is for building .NET Framework 4.7.1 version applications. It is based on the [Windows Server Core image](https://hub.docker.com/r/microsoft/windowsservercore/).

## Related Repos and Examples

See [.NET Framework Docker samples](https://github.com/Microsoft/dotnet-framework-docker-samples) to get started with pre-made samples.

See [.NET Framework and Docker](https://docs.microsoft.com/dotnet/framework/docker/) to learn more about using .NET Framework with Docker.

See the following related repos for other application types:

- [microsoft/dotnet-framework](https://hub.docker.com/r/microsoft/dotnet-framework/) for .NET Framework runtime images.
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

# Supported tags and respective `Dockerfile` links

-       [`4.6.2`, `latest` (*4.6.2/Dockerfile*)](4.6.2/Dockerfile)
-       [`3.5` (*3.5/Dockerfile*)](3.5/Dockerfile)

For more information about these images and their history, please see [the relevent Dockerfile (`microsoft/dotnet-framework-docker`)](https://github.com/microsoft/dotnet-framework-docker/search?utf8=%E2%9C%93&q=FROM&type=Code). These images are updated via [pull requests to the `microsoft/dotnet-framework-docker` GitHub repo](https://github.com/microsoft/dotnet-framework-docker/pulls?utf8=%E2%9C%93&q=).

[![Downloads from Docker Hub](https://img.shields.io/docker/pulls/microsoft/dotnet-framework.svg)](https://hub.docker.com/r/microsoft/dotnet-framework)
[![Stars on Docker Hub](https://img.shields.io/docker/stars/microsoft/dotnet-framework.svg)](https://hub.docker.com/r/microsoft/dotnet-framework)

# What is the .NET Framework?

The .NET Framework is a general purpose development platform maintained by Microsoft. It is the most popular way to build client and server applications for Windows and Windows Server. It is included with Windows, Windows Server and Windows Server Core. It includes server technologies such as ASP.NET Web Forms, ASP.NET MVC and Windows Communication Framework (WCF) applications, which you can run in Docker containers.

.NET has several capabilities that make development easier, including automatic memory management, (runtime) generic types, reflection, asynchrony, concurrency, and native interop. Millions of developers take advantage of these capabilities to efficiently build high-quality web and client applications.

You can use C#, F# and VB to write .NET Framework apps. C# is simple, powerful, type-safe, and object-oriented while retaining the expressiveness and elegance of C-style languages. F# is a multi-paradigm programming language, enabling both functional and object-oriented patterns and practices. VB is a rapid development programming language with the deepest integration between the language and Visual Studio, providing the fastest path to a working app.   

The .NET Framework was first released by Microsoft in 2001. The latest version is .NET Framework 4.6.2.

> https://docs.microsoft.com/dotnet/articles/framework/

![logo](https://avatars0.githubusercontent.com/u/9141961?v=3&amp;s=100)

# How to use these Images

These images are based on [Windows Containers][win-containers]. You need to be Windows 10 or Windows Server 2016 to use them.

## Deploying a .NET Framework 4.x application with Docker

It is easy to create a Docker image for a .NET Framework 4.x application. You can try the instructions below or check out the [.NET Framework 4.6.2 Docker sample](https://github.com/Microsoft/dotnet-framework-docker-samples/tree/master/dotnetapp-4.6.2) if you want to try a pre-made version that's ready go.

1. Build your application in Visual Studio or at the command line. 
2. Add a `Dockerfile` file with the following content to your project. The Dockerfile assumes that your app is built to the `bin\Release` directory and that your app name is `dotnetapp.exe`. Please update your `Dockerfile` as appropriate. 

```Dockerfile
FROM microsoft/dotnet-framework:4.6.2
WORKDIR \app
COPY bin\Release .
ENTRYPOINT ["dotnetapp.exe"]
```

3. Type the following Docker commands at the command line, within your project directory (beside Program.cs). You can change the tag name (`dotnetapp`) to your own string, as you like.

```console
docker build -t dotnetapp .
docker run dotnetapp
```

The Docker image includes the .NET Framework 4.6.2, however, your application does not need to explicity target the .NET Framework 4.6.2. Applications that target .NET Framework 4.0 or later should work correctly with this image.

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

### `4.6.2`

This is the defacto image. If you are unsure about what your needs are, you probably want to use this one. It is designed to be used both as a throw away container (mount your source code and start the container to start your app), as well as the base to build other images off of.

This image is for .NET Framework 4.0 and later version applications. It is based on the [Windows Server Core image](https://hub.docker.com/r/microsoft/windowsservercore/).

### `3.5`

This image is for .NET Framework 3.5 and earlier version applications.  It is based on the [Windows Server Core image](https://hub.docker.com/r/microsoft/windowsservercore/).

## Related Repos and Examples

See [.NET Framework Docker samples](https://github.com/Microsoft/dotnet-framework-docker-samples) to get started with pre-made samples.

See [.NET Framework and Docker](https://docs.microsoft.com/dotnet/articles/framework/docker/) to learn more about using .NET Framework with Docker.

See the following related repos for other application types:

- [microsoft/aspnet](https://hub.docker.com/r/microsoft/aspnet/) for ASP.NET Web Forms and MVC applications.
- [microsoft/dotnet](https://hub.docker.com/r/microsoft/dotnet/) for .NET Core applications.
- [microsoft/aspnetcore](https://hub.docker.com/r/microsoft/aspnetcore/) for ASP.NET Core applications.

You can read more about [Windows Containers][win-containers] to learn how to use Docker with Windows.

# License

The .NET Framework images use the same license as the [Windows Server Core base image](https://hub.docker.com/r/microsoft/windowsservercore/), as follows:

MICROSOFT SOFTWARE SUPPLEMENTAL LICENSE TERMS

CONTAINER OS IMAGE

Microsoft Corporation (or based on where you live, one of its affiliates) (referenced as “us,” “we,” or “Microsoft”) licenses this Container OS Image supplement to you (“Supplement”). You are licensed to use this Supplement in conjunction with the underlying host operating system software (“Host Software”) solely to assist running the containers feature in the Host Software. The Host Software license terms apply to your use of the Supplement. You may not use it if you do not have a license for the Host Software. You may use this Supplement with each validly licensed copy of the Host Software.

# Supported Docker versions

This image is officially supported on Docker version 1.12.2.

Please see [the Docker installation documentation](https://docs.docker.com/installation/) for details on how to upgrade your Docker daemon.

# User Feedback

## Issues and Contributing

You are invited to report issues or request features by creating a [GitHub issue](https://github.com/microsoft/dotnet-framework-docker/issues).

## Documentation

You can read documentation for using the .NET Framework with Docker usage in the [.NET docs](https://docs.microsoft.com/dotnet/articles/framework/docker). The docs are also [open source on GitHub](https://github.com/dotnet/docs). Contributions are welcome!

[win-containers]: https://msdn.microsoft.com/virtualization/windowscontainers/about/about_overview

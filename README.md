# Supported tags and respective `Dockerfile` links

-       [`4.6.2`, `latest` (*4.6.2/Dockerfile*)](https://github.com/Microsoft/dotnet-framework-docker/blob/master/4.6.2/Dockerfile)
-       [`3.5` (*3.5/Dockerfile*)](https://github.com/Microsoft/dotnet-framework-docker/blob/master/3.5/Dockerfile)

For more information about these images and their history, please see [the relevent Dockerfile (`microsoft/dotnet-framework-docker`)](https://github.com/microsoft/dotnet-framework-docker/search?utf8=%E2%9C%93&q=FROM&type=Code). These images are updated via [pull requests to the `microsoft/dotnet-framework-docker` GitHub repo](https://github.com/dotnet/dotnet-docker/pulls?utf8=%E2%9C%93&q=).

[![Downloads from Docker Hub](https://img.shields.io/docker/pulls/microsoft/dotnet-framework.svg)](https://hub.docker.com/r/microsoft/dotnet-framework)
[![Stars on Docker Hub](https://img.shields.io/docker/stars/microsoft/dotnet-framework.svg)](https://hub.docker.com/r/microsoft/dotnet-framework)

# What is the .NET Framework?

The .NET Framework is a general purpose development platform maintained by Microsoft. It is a popular choice for building applications for Windows and Windows Server. It is included with Windows, Windows Server and Windows Server Core.

.NET has several capabilities that make development easier, including automatic memory management, (runtime) generic types, reflection, asynchrony, concurrency, and native interop. Millions of developers take advantage of these capabilities to efficiently build high-quality web and client applications.

You can use C# to write .NET Core apps. C# is simple, powerful, type-safe, and object-oriented while retaining the expressiveness and elegance of C-style languages. Anyone familiar with C and similar languages will find it straightforward to write in C#.

The .NET Framework was first released by Microsoft in 2001. The latest version is .NET Framework 4.6.2.

> https://docs.microsoft.com/en-us/dotnet/articles/core/

![logo](https://avatars0.githubusercontent.com/u/9141961?v=3&amp;s=100)
The official Docker images for the .NET Framework on Windows Server Core.

# .NET Framework Containers

There are multiple flavors of the .NET Framework Docker images available, for both Web and Console application workloads.

## .NET Framework 4.6.2

The .NET Framework 4.6.2 is included in the [Windows Server Core base image](https://hub.docker.com/r/microsoft/windowsservercore/). You can pull/FROM microsoft/windowsservercore to use .NET Framework 4.6.2.

To make discovery easier, we have also provided the same image (with a FROM wrapper) in this repo, enabling you to pull `microsoft/dotnet-framework`.

- Optional tags: `latest`, `4.6.2`
- Dockerfile: [4.6.2/Dockerfile](https://github.com/Microsoft/dotnet-framework-docker/blob/master/4.6.2/Dockerfile)

## .NET Framework 3.5

.NET Framework 3.5 is an optional component in Windows Server Core. The official Docker image for the .NET Framework 3.5 is included in this Docker repository and is in preview. You can pull/FROM microsoft/dotnet-framework:3.5 to use .NET Framework 3.5.

- Required tag: `3.5`
- Dockerfile: [3.5/Dockerfile](https://github.com/Microsoft/dotnet-framework-docker/blob/master/3.5/Dockerfile)

## .NET Framework 4.6 and 3.5 and ASP.NET

You should use the ASP.NET Docker images if you want to package a .NET Framework ASP.NET app with Docker. The following Docker images are available in the [microsoft/aspnet](https://hub.docker.com/r/microsoft/aspnet/) Dockerhub repo.

- microsoft/aspnet:4.6.2 - For .NET Framework 4.x ASP.NET applications.
- microsoft/aspnet:3.5 - For .NET Framework 3.5 ASP.NET applications.

Note: The .NET Framework 3.5 image also contains the .NET Framework 4.6.2. You may want to standardize on this image if you intend to host both .NET Framework 3.5 and 4.x web sites in the same environment.

## .NET Core

You can get .NET Core images from the [microsoft/dotnet](https://hub.docker.com/r/microsoft/dotnet/) Dockerhub repo.

# Learn more

There are multiple resources that you can look at to learn more.

## .NET Framework Containers

Check out [Docker on .NET Framework](https://docs.microsoft.com/dotnet/articles/framework/docker) to learn more about running .NET Framework applications in Windows Containers. 

## Windows Containers

.NET Framework Docker images rely on the Container feature in Windows. You can read about [Windows Containers](https://msdn.microsoft.com/virtualization/windowscontainers/about/about_overview) to learn everything you need to know about using Docker with Windows.

## What is the .NET Framework?
The .NET Framework can be used to build Windows and Windows Server applications. It includes technologies such as ASP.NET Web Forms, ASP.NET MVC and Windows Communication Framework (WCF) applications. 

## How to use the .NET Framework 3.5 image?

Create a Dockerfile for your application

```
FROM microsoft/dotnet-framework:3.5
ADD MyDotNet35App.exe /MyDotNet35App.exe
ENTRYPOINT MyDotNet35App.exe
```

You can then build and run the Docker image:

```
$ docker build -t my-dotnet35-app .
$ docker run my-dotnet35-app
```

## Supported Docker Versions

This image has been tested on Docker Versions 1.12.1-beta26 or higher.

## License of this repository

MIT

## License of .NET Framework Docker images 

MICROSOFT SOFTWARE SUPPLEMENTAL LICENSE TERMS

CONTAINER OS IMAGE

Microsoft Corporation (or based on where you live, one of its affiliates) (referenced as “us,” “we,” or “Microsoft”) licenses this Container OS Image supplement to you (“Supplement”). You are licensed to use this Supplement in conjunction with the underlying host operating system software (“Host Software”) solely to assist running the containers feature in the Host Software. The Host Software license terms apply to your use of the Supplement. You may not use it if you do not have a license for the Host Software. You may use this Supplement with each validly licensed copy of the Host Software.

## User Feedback

Please reach out to the team with any issues or concerns through a [GitHub issue](https://github.com/Microsoft/dotnet-framework-docker/issues/new).

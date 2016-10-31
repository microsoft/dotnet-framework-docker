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

# How to use these Images

## Deploying a .NET Framework 4.x application with Docker

The .NET Framework can be used to build Windows and Windows Server applications. It includes technologies such as ASP.NET Web Forms, ASP.NET MVC and Windows Communication Framework (WCF) applications. 

## Deploying a .NET Framework 3.5 application with Docker

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

## More Examples using these Images

You can learn more about using .NET Core with Docker with [.NET Docker samples](https://github.com/dotnet/dotnet-docker-samples):

- [ONBUILD](https://github.com/dotnet/dotnet-docker-samples/tree/master/dotnetapp-onbuild) sample using the `1.0.0-preview2-onbuild` .NET Core SDK image.
- [Development](https://github.com/dotnet/dotnet-docker-samples/tree/master/dotnetapp-dev) sample using the `1.0.0-preview2-sdk` .NET Core SDK image.
- [Production](https://github.com/dotnet/dotnet-docker-samples/tree/master/dotnetapp-prod) sample using the `1.0.1-core` .NET Core image.
- [Self-contained](https://github.com/dotnet/dotnet-docker-samples/tree/master/dotnetapp-selfcontained) sample using the `1.0.1-core-deps` base OS image (with native dependencies added).

Windows Container variants are provided at the same locations, above, and use slightly different image tags (for example, `1.0.0-preview2-nanoserver-onbuild`). You can read more about [Windows Containers](https://msdn.microsoft.com/virtualization/windowscontainers/about/about_overview) to learn how to use Docker with Windows.

See [Building Docker Images for .NET Core Applications](https://docs.microsoft.com/dotnet/articles/core/docker/building-net-docker-images) to learn more about the various Docker images and when to use each for them.

See the following related repos for other application types:

- [microsoft/dotnet](https://hub.docker.com/r/microsoft/dotnet/) for .NET Core applications.
- [microsoft/aspnetcore](https://hub.docker.com/r/microsoft/aspnetcore/) for ASP.NET Core applications.
- [microsoft/aspnet](https://hub.docker.com/r/microsoft/aspnet/) for ASP.NET Web Forms and MVC applications.

## Image variants

The `microsoft/dotnet-framework` images come in different flavors, each designed for a specific use case.

### `4.6.2`

This is the defacto image. If you are unsure about what your needs are, you probably want to use this one. It is designed to be used both as a throw away container (mount your source code and start the container to start your app), as well as the base to build other images off of.

This image is for .NET Framework 4.x applications.

### `3.5`

This image is for .NET Framework 3.5 applications.

# License

MICROSOFT SOFTWARE SUPPLEMENTAL LICENSE TERMS

CONTAINER OS IMAGE

Microsoft Corporation (or based on where you live, one of its affiliates) (referenced as “us,” “we,” or “Microsoft”) licenses this Container OS Image supplement to you (“Supplement”). You are licensed to use this Supplement in conjunction with the underlying host operating system software (“Host Software”) solely to assist running the containers feature in the Host Software. The Host Software license terms apply to your use of the Supplement. You may not use it if you do not have a license for the Host Software. You may use this Supplement with each validly licensed copy of the Host Software.

# Supported Docker versions

This image is officially supported on Docker version 1.12.2.

Please see [the Docker installation documentation](https://docs.docker.com/installation/) for details on how to upgrade your Docker daemon.

# User Feedback

## Issues

If you have any problems with or questions about this image, please contact us through a [GitHub issue](https://github.com/microsoft/dotnet-framework/issues).

## Contributing

You are invited to report issues or request features at [microsoft/dotnet-framework-docker](https://github.com/Microsoft/dotnet-framework-docker).

## Documentation

You can read documentation for .NET Framework, including Docker usage in the [.NET Core docs](https://docs.microsoft.com/en-us/dotnet/articles/framework/). The docs are also [open source on GitHub](https://github.com/dotnet/core-docs). Contributions are welcome!

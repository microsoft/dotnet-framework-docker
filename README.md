![3.5 .NET Framework Docker Image](https://avatars2.githubusercontent.com/u/6154722?v=3&s=200)

# .NET Framework 4.6.2

The .NET Framework 4.6.2 is included in the [Windows Server Core base image](https://hub.docker.com/r/microsoft/windowsservercore/). You can pull/FROM microsoft/windowsservercore to use .NET Framework 4.6.2.

# .NET Framework 3.5

.NET Framework 3.5 is an optional component in Windows Server Core. The official Docker image for the .NET Framework 3.5 is included in this Docker repository and is in preview. You can pull/FROM microsoft/dotnet-framework:3.5 to use .NET Framework 3.5.

The following tags can be used for the .NET Framework 3.5 Docker image:

* servercore, latest, 3.5 [servercore/Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/master/servercore/Dockerfile)

# ASP.NET

You should use the ASP.NET Docker images if you want to package an .NET Framework ASP.NET app with Docker. The following Docker images are available in the [microsoft/aspnet](https://hub.docker.com/r/microsoft/aspnet/) Dockerhub repo.

- microsoft/aspnet:4.x - For .NET Framework 4.x ASP.NET applications.
- microsoft/aspnet:3.5 - For .NET Frameowkr 3.5 ASP.NET applications.

Note: The .NET Framework 3.5 image also contains the .NET Framework 4.6.2. You may want to standardize on this image if you intend to host both .NET Framework 3.5 and 4.x web sites in the same environment.

# .NET Framework Containers

Check out [Docker on .NET Framework](https://docs.microsoft.com/dotnet/articles/framework/docker) to learn more about running .NET Framework applications in Windows Containers. 

# Windows Containers

.NET Framework Docker images rely on the Container feature in Windows. You can read about [Windows Containers](https://msdn.microsoft.com/virtualization/windowscontainers/about/about_overview) to learn everything you need to know about using Docker with Windows.

## What is .NET Framework?
The .NET Framework can be used to build Windows and Windows Server applications. It includes technologies such as ASP.NET Web Forms, ASP.NET MVC and Windows Communication Framework (WCF) applications. 

## How to use this image?
### Create a Dockerfile with your application
```
FROM microsoft/dotnet-framework:3.5
ADD MyDotNet35App.exe /MyDotNet35App.exe
ENTRYPOINT MyDotNet35App.exe
```
You can then build and run the Docker image:
```
$ docker build -t my-dotnet35-app .
$ docker run -it --rm --name my-running-app my-dotnet35-app
```

## Supported Docker Versions
This image has been tested on Docker Versions 1.12.1-beta26 or higher.

## License
MICROSOFT SOFTWARE SUPPLEMENTAL LICENSE TERMS

CONTAINER OS IMAGE

Microsoft Corporation (or based on where you live, one of its affiliates) (referenced as “us,” “we,” or “Microsoft”) licenses this Container OS Image supplement to you (“Supplement”). You are licensed to use this Supplement in conjunction with the underlying host operating system software (“Host Software”) solely to assist running the containers feature in the Host Software. The Host Software license terms apply to your use of the Supplement. You may not use it if you do not have a license for the Host Software. You may use this Supplement with each validly licensed copy of the Host Software.

## User Feedback
If you have any issues or concerns, reach out to us through a [GitHub issue](https://github.com/Microsoft/dotnet-framework-docker/issues/new).
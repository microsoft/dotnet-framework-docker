# .NET Framework Docker Sample

This [sample](Dockerfile) demonstrates how to use .NET Core and Docker together. It builds multiple projects and executes unit tests in a container. The sample can also be used without Docker.

The sample builds the application in a container based on the larger [.NET Framework SDK Docker image](https://hub.docker.com/r/microsoft/dotnet-framework-build/). It builds and [tests](dotnet-docker-unit-testing.md) the application and then copies the final build result into a Docker image based on the smaller [.NET Framework Runtime Docker image](https://hub.docker.com/r/microsoft/dotnet-framework/). It uses Docker [multi-stage build](https://github.com/dotnet/announcements/issues/18) and [multi-arch tags](https://github.com/dotnet/announcements/issues/14).

This sample requires [Docker 17.06](https://docs.docker.com/release-notes/docker-ce) or later of the [Docker client](https://store.docker.com/editions/community/docker-ce-desktop-windows).

## Try a pre-built .NET Framework Docker Image

You can quickly try a pre-built [sample .NET Framework Docker image](https://hub.docker.com/r/microsoft/dotnet-framework-samples/), based on this sample.

Type the following command to run a sample with [Docker](https://store.docker.com/editions/community/docker-ce-desktop-windows):

```console
docker run --rm microsoft/dotnet-framework-samples
```

## Getting the sample

The easiest way to get the sample is by cloning the samples repository with [git](https://git-scm.com/downloads), using the following instructions.

```console
git clone https://github.com/microsoft/dotnet-framework-docker/
```

You can also [download the repository as a zip](https://github.com/microsoft/dotnet-framework-docker/archive/master.zip).

## Build and run the sample with Docker

You can build and run the sample in Docker using the following commands. The instructions assume that you are in the root of the repository.

```console
cd samples
cd dotnetapp
docker build --pull -t dotnetapp .
docker run --rm dotnetapp
```

The commands above run unit tests as part `docker build`. You can also [run .NET unit tests as part of `docker run`](dotnet-docker-unit-testing.md). The following instructions provide you with the simplest way of doing that.

```console
docker build --target testrunner -t dotnetapp:test .
docker run --rm -it dotnetapp:test
```

You can mount a volume while running the image in order to save the test results to your local disk. The instructions to do that are provided in [Running Unit Tests with Docker](dotnet-docker-unit-testing.md)

Multiple variations of this sample have been provided, as follows. Some of these example Dockerfiles are demonstrated later. Specify an alternate Dockerfile via the `-f` argument.

* [Multi-arch sample with build and unit testing](Dockerfile)
* [Multi-arch basic sample](Dockerfile.basic)

## Develop ASP.NET Core Applications in a container

You can develop applications without a .NET Core installation on your machine with the [Develop .NET Core applications in a container](dotnet-docker-dev-in-container.md) instructions. These instructions are also useful if your development and production environments do not match.

## Run Docker Image on Another Device

You can push the image to a container registry so that you can pull and run it on another device. Straightforward instructions are provided for pushing to both Azure Container Registry and DockerHub.

* [Push Docker Images to Azure Container Registry](push-image-to-acr.md)
* [Push Docker Images to DockerHub](push-image-to-dockerhub.md)

## Build and run the sample locally

You can build and run the sample locally with the [.NET Core 2.0 SDK](https://www.microsoft.com/net/download/core) using the following instructions. The instructions assume that you are in the root of the repository.

You must have the [.NET Framework 4.7.1 or later](https://www.microsoft.com/net/download/Windows/run) installed and the .NET Framework 4.7.1 targeting pack (easiest to install with [Visual Studio 2017](https://www.microsoft.com/net/download/Windows/build)).

```console
cd samples
cd dotnetapp
dotnet run
```

You can produce an application that is ready to deploy to production using the following command:

```console
dotnet publish -c release -o out
```

You can run the published application using the following command.

```console
out\dotnetapp.exe
```

Note: The `-c release` argument builds the application in release mode (the default is debug mode). See the [dotnet run reference](https://docs.microsoft.com/dotnet/core/tools/dotnet-run) for more information on commandline parameters.

## .NET Resources

More Samples

* [.NET Core Docker Samples](../README.md)
* [.NET Framework Docker Samples](https://github.com/microsoft/dotnet-framework-docker-samples/)

Docs and More Information:

* [.NET Docs](https://docs.microsoft.com/dotnet/)
* [ASP.NET Docs](https://docs.microsoft.com/aspnet/)
* [dotnet/core](https://github.com/dotnet/core) for starting with .NET Core on GitHub.
* [dotnet/announcements](https://github.com/dotnet/announcements/issues) for .NET announcements.

## Related Repositories

.NET Core Docker Hub repos:

* [microsoft/aspnetcore](https://hub.docker.com/r/microsoft/aspnetcore/) for ASP.NET Core images.
* [microsoft/dotnet](https://hub.docker.com/r/microsoft/dotnet/) for .NET Core images.
* [microsoft/dotnet-nightly](https://hub.docker.com/r/microsoft/dotnet-nightly/) for .NET Core preview images.
* [microsoft/dotnet-samples](https://hub.docker.com/r/microsoft/dotnet-samples/) for .NET Core sample images.

.NET Framework Docker Hub repos:

* [microsoft/aspnet](https://hub.docker.com/r/microsoft/aspnet/) for ASP.NET Web Forms and MVC images.
* [microsoft/dotnet-framework](https://hub.docker.com/r/microsoft/dotnet-framework/) for .NET Framework images.
* [microsoft/dotnet-framework-build](https://hub.docker.com/r/microsoft/dotnet-framework-build/) for building .NET Framework applications with Docker.
* [microsoft/dotnet-framework-samples](https://hub.docker.com/r/microsoft/dotnet-framework-samples/) for .NET Framework and ASP.NET sample images.

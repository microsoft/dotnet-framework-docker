# .NET Framework Docker Sample

This [sample](Dockerfile) demonstrates how to use .NET Core and Docker together. It builds multiple projects and executes unit tests in a container. The sample can also be used without Docker.

The sample builds the application in a container based on the larger [.NET Framework SDK Docker image](https://hub.docker.com/_/microsoft-dotnet-framework-sdk/). It builds and [tests](dotnet-docker-unit-testing.md) the application and then copies the final build result into a Docker image based on the smaller [.NET Framework Runtime Docker image](https://hub.docker.com/_/microsoft-dotnet-framework-runtime/). It uses Docker [multi-stage build](https://github.com/dotnet/announcements/issues/18) and [multi-arch tags](https://github.com/dotnet/announcements/issues/14).

This sample requires [Docker 17.06](https://docs.docker.com/release-notes/docker-ce) or later of the [Docker client](https://store.docker.com/editions/community/docker-ce-desktop-windows).

## Try a pre-built .NET Framework Docker Image

You can quickly run a container with a pre-built [.NET Framework Docker image](https://hub.docker.com/_/microsoft-dotnet-framework-samples/), based on the [.NET Framework console sample](dotnetapp/README.md).

Type the following [Docker](https://www.docker.com/products/docker) command:

```console
docker run --rm mcr.microsoft.com/dotnet/framework/samples:dotnetapp
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

* [Sample with build and unit testing](Dockerfile)
* [Sample with basic build](Dockerfile.basic)

## Run Docker Image on Another Device

You can push the image to a container registry so that you can pull and run it on another device. Straightforward instructions are provided for pushing to both Azure Container Registry and DockerHub.

* [Push Docker Images to Azure Container Registry](push-image-to-acr.md)
* [Push Docker Images to DockerHub](push-image-to-dockerhub.md)

## Build and run the sample locally with the .NET Core SDK

You can build this [.NET Framework 4.8](https://www.microsoft.com/net/download/dotnet-framework-runtime/net48) application locally with the [.NET Core 2.0 SDK](https://www.microsoft.com/net/download/core) using the following instructions. The instructions assume that you are in the root of the repository.

You must have the [.NET Framework 4.8 targeting pack](http://go.microsoft.com/fwlink/?LinkId=2085167) installed. It is easiest to install with [Visual Studio 2019](https://visualstudio.microsoft.com/vs/) and the the Visual Studio Installer.

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

Note: The `-c release` argument builds the application in release mode (the default is debug mode). See the [dotnet publish reference](https://docs.microsoft.com/dotnet/core/tools/dotnet-publish) for more information on commandline parameters.

## Build and run the sample locally with MSBuild

You can build this [.NET Framework 4.8](https://www.microsoft.com/net/download/dotnet-framework-runtime/net48) application locally with MSBuild using the following instructions. The instructions assume that you are in the root of the repository and using the `Developer Command Prompt for VS 2019`.

You must have the [.NET Framework 4.8 targeting pack](https://go.microsoft.com/fwlink/?LinkId=2085167) installed. It is easiest to install with [Visual Studio 2019](https://visualstudio.microsoft.com/vs/) and the the Visual Studio Installer.

```console
cd samples
cd dotnetapp
msbuild /t:restore
msbuild /p:Configuration=Release
dotnetapp\bin\Release\net48\dotnetapp.exe
```

Note: The `/p:Configuration=Release` argument builds the application in release mode (the default is debug mode). See the [MSBuild Command-Line reference](https://msdn.microsoft.com/en-us/library/ms164311.aspx) for more information on commandline parameters.

## .NET Resources

More Samples

* [.NET Framework Docker Samples](../README.md)
* [.NET Core Docker Samples](https://github.com/dotnet/dotnet-docker/blob/master/samples/README.md)

Docs and More Information:

* [.NET Docs](https://docs.microsoft.com/dotnet/)
* [ASP.NET Docs](https://docs.microsoft.com/aspnet/)
* [dotnet/core](https://github.com/dotnet/core) for starting with .NET Core on GitHub.
* [dotnet/announcements](https://github.com/dotnet/announcements/issues) for .NET announcements.

## Related Docker Hub Repos

.NET Framework:

* [dotnet/framework/sdk](https://hub.docker.com/_/microsoft-dotnet-framework-sdk/): .NET Framework SDK
* [dotnet/framework/aspnet](https://hub.docker.com/_/microsoft-dotnet-framework-aspnet/): ASP.NET Web Forms and MVC
* [dotnet/framework/runtime](https://hub.docker.com/_/microsoft-dotnet-framework-runtime/): .NET Framework Runtime
* [dotnet/framework/wcf](https://hub.docker.com/_/microsoft-dotnet-framework-wcf/): Windows Communication Framework (WCF)
* [dotnet/framework/samples](https://hub.docker.com/_/microsoft-dotnet-framework-samples/): .NET Framework, ASP.NET and WCF Samples

.NET Core:

* [dotnet/core](https://hub.docker.com/_/microsoft-dotnet-core/): .NET Core
* [dotnet/core/samples](https://hub.docker.com/_/microsoft-dotnet-core-samples/): .NET Core Samples
* [dotnet/core-nightly](https://hub.docker.com/_/microsoft-dotnet-core-nightly/): .NET Core (Preview)


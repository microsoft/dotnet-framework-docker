# Develop .NET Framework Applications in a Container

You can use containers to establish a .NET Framework development environment with only Docker and optionally a code editor installed on your machine. The environment can be made to match your local machine, production or both. If you support multiple Windows versions, then this approach might become a key part of your development process.

A common use case of Docker is to [containerize an application](README.md). You can define the environment necessary to run the application and even build the application itself within a Dockerfile. This document describes a much more iterative and dynamic use of Docker, defining the environment and running .NET Core SDK commands within containers via the commandline.

See [Develop ASP.NET Core Applications in a Container](../aspnetapp/aspnet-docker-dev-in-container.md) for ASP.NET Core-specific instructions.

## Getting the sample

The easiest way to get the sample is by cloning the samples repository with [git](https://git-scm.com/downloads), using the following instructions:

```console
git clone https://github.com/microsoft/dotnet-framework-docker/
```

You can also [download the repository as a zip](https://github.com/microsoft/dotnet-framework-docker/archive/master.zip).

## Requirements

The instructions below use .NET Framework 4.7.2 SDK images.

This approach relies on [volume mounting](https://docs.docker.com/engine/admin/volumes/volumes/) (that's the `-v` argument in the following commands) to mount source into the container (without using a Dockerfile). You may need to [Enable shared drives](https://docs.docker.com/docker-for-windows/#shared-drives).

To avoid conflicts between container usage and your local environment, you need to use a different set of `obj` and `bin` folders for each environment.

 Make this change with the following steps:

 1. Delete your existing obj and bin folders manually or use `dotnet clean`.
 2. Add a [Directory.Build.props](Directory.Build.props) file to your project to use a different set of `obj` and `bin` folders for your local and container environments.

## Run your application in a container while you Develop

You can rerun your application in a container with every local code change. This scenario works for both console applications and websites.

The instructions assume that you are in the root of the repository.

```console
docker run --rm -it -v c:\git\dotnet-docker\samples\dotnetapp:c:\app\ -w \app\dotnetapp microsoft/dotnet-framework:4.7.2-sdk dotnet watch run
```

## Test your application in a container while you develop

You can retest your application in a container with every local code change. This works for both console applications and websites.

The instructions assume that you are in the root of the repository.

```console
docker run --rm -it -v c:\git\dotnet-docker\samples\dotnetapp:c:\app\ -w \app\tests microsoft/dotnet-framework:4.7.2-sdk dotnet watch test
```

The commands above log test results to the console. You can additionally log results as a TRX file by appending `--logger:trx` to the previous test commands, specifically `dotnet watch test --logger:trx`. TRX logging is also demonstrated in [Running .NET Framework Unit Tests with Docker](dotnet-docker-unit-testing.md).

## Build source with the .NET Framework SDK, using `docker run`

You can build your application with the .NET Framework SDK Docker image. Built assets will be in the `out` directory on your local disk.

The instructions assume that you are in the root of the repository.

```console
docker run --rm -v c:\git\dotnet-docker\samples\dotnetapp:c:\app -w c:\app\dotnetapp microsoft/dotnet-framework:4.7.2-sdk dotnet publish -c release -o out
```

## More Samples

* [.NET Core Docker Samples](../README.md)
* [.NET Framework Docker Samples](https://github.com/microsoft/dotnet-framework-docker-samples/)

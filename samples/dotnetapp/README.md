# .NET Framework Docker Sample

This [sample](Dockerfile) demonstrates how to use .NET Framework and Docker together.

The sample builds the application in a container based on the larger [.NET Framework SDK Docker image](https://hub.docker.com/_/microsoft-dotnet-framework-sdk/).

This sample requires the [Docker client](https://store.docker.com/editions/community/docker-ce-desktop-windows).

## Try a pre-built .NET Framework Docker Image

You can quickly run a container with a pre-built [.NET Framework Docker image](https://hub.docker.com/_/microsoft-dotnet-framework-samples/), based on the [.NET Framework console sample](dotnetapp/README.md).

Type the following [Docker](https://www.docker.com/products/docker) command:

```console
docker run --rm mcr.microsoft.com/dotnet/framework/samples
```

## Getting the sample

The easiest way to get the sample is by cloning the samples repository with [git](https://git-scm.com/downloads), using the following instructions.

```console
git clone https://github.com/microsoft/dotnet-framework-docker/
```

You can also [download the repository as a zip](https://github.com/microsoft/dotnet-framework-docker/archive/main.zip).

## Build and run the sample with Docker

You can build and run the sample in Docker using the following commands. The instructions assume that you are in the root of the repository.

```console
cd samples
cd dotnetapp
docker build --pull -t dotnetapp .
docker run --rm dotnetapp
```

## Build and run the sample locally with the .NET SDK

You can build this [.NET Framework 4.8](https://www.microsoft.com/net/download/dotnet-framework-runtime/net48) application locally with the [.NET SDK](https://dotnet.microsoft.com/download) using the following instructions. The instructions assume that you are in the root of the repository.

You must have the [.NET Framework 4.8 targeting pack](http://go.microsoft.com/fwlink/?LinkId=2085167) installed. It is easiest to install with [Visual Studio](https://visualstudio.microsoft.com/vs/) and the the Visual Studio Installer.

```console
cd samples
cd dotnetapp
dotnet run
```

You can produce an application that is ready to deploy to production using the following command:

```console
dotnet publish -c Release -o out
```

You can run the published application using the following command.

```console
out\dotnetapp.exe
```

Note: The `-c Rrelease` argument builds the application in release mode (the default is debug mode). See the [dotnet publish reference](https://docs.microsoft.com/dotnet/core/tools/dotnet-publish) for more information on commandline parameters.

## Build and run the sample locally with MSBuild

You can build this [.NET Framework 4.8](https://www.microsoft.com/net/download/dotnet-framework-runtime/net48) application locally with MSBuild using the following instructions. The instructions assume that you are in the root of the repository and using the `Developer Command Prompt for VS 2019`.

You must have the [.NET Framework 4.8 targeting pack](https://go.microsoft.com/fwlink/?LinkId=2085167) installed. It is easiest to install with [Visual Studio](https://visualstudio.microsoft.com/vs/) and the the Visual Studio Installer.

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
* [.NET Docker Samples](https://github.com/dotnet/dotnet-docker/blob/main/samples/README.md)

Docs and More Information:

* [.NET Docs](https://docs.microsoft.com/dotnet/)
* [ASP.NET Docs](https://docs.microsoft.com/aspnet/)
* [dotnet/core](https://github.com/dotnet/core) for starting with .NET on GitHub.
* [dotnet/announcements](https://github.com/dotnet/announcements/issues) for .NET announcements.

## Related Docker Hub Repos

.NET Framework:

* [dotnet/framework/sdk](https://hub.docker.com/_/microsoft-dotnet-framework-sdk/): .NET Framework SDK
* [dotnet/framework/aspnet](https://hub.docker.com/_/microsoft-dotnet-framework-aspnet/): ASP.NET Web Forms and MVC
* [dotnet/framework/runtime](https://hub.docker.com/_/microsoft-dotnet-framework-runtime/): .NET Framework Runtime
* [dotnet/framework/wcf](https://hub.docker.com/_/microsoft-dotnet-framework-wcf/): Windows Communication Foundation (WCF)
* [dotnet/framework/samples](https://hub.docker.com/_/microsoft-dotnet-framework-samples/): .NET Framework, ASP.NET and WCF Samples

.NET:

* [dotnet](https://hub.docker.com/_/microsoft-dotnet/): .NET
* [dotnet/samples](https://hub.docker.com/_/microsoft-dotnet-samples/): .NET Samples
* [dotnet-nightly](https://hub.docker.com/_/microsoft-dotnet-nightly/): .NET (Preview)

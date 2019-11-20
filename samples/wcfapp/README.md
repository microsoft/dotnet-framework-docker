# WCF Docker Sample
This sample demonstrates how to dockerize WCF services, either IIS-hosted or self-hosted. A simple "hello world" service contract is used in all samples for both HTTP and NET.TCP transport bindings. The sample can also be used without Docker.

The sample builds the application in a container based on the larger [.NET Framework SDK Docker image](https://hub.docker.com/_/microsoft-dotnet-framework-sdk/). It builds the application and then copies the final build result into a Docker image based on the smaller [WCF Runtime Docker image](https://hub.docker.com/_/microsoft-dotnet-framework-wcf/) or [.NET Framework Runtime Docker image](https://hub.docker.com/_/microsoft-dotnet-framework-runtime/). It uses Docker [multi-stage build](https://github.com/dotnet/announcements/issues/18) and [multi-arch tags](https://github.com/dotnet/announcements/issues/14).

This sample requires [Docker 17.06](https://docs.docker.com/release-notes/docker-ce) or later of the [Docker client](https://store.docker.com/editions/community/docker-ce-desktop-windows).

## Try pre-built WCF Docker Images
You can quickly run a container with a pre-built [sample WCF Docker image](https://hub.docker.com/_/microsoft-dotnet-framework-samples/), based on the WCF Docker sample.

Type the following [Docker](https://www.docker.com/products/docker) command to start a WCF service container:
```console
docker run --name wcfservicesample --rm -it mcr.microsoft.com/dotnet/framework/samples:wcfservice
```
Find the IP address of the container instance.
```console
docker inspect --format="{{.NetworkSettings.Networks.nat.IPAddress}}" wcfservicesample
172.26.236.119
```
Type the following Docker command to start a WCF client container, set environment variable HOST to the IP address of the wcfservicesample container:
```console
docker run --name wcfclientsample --rm -it -e HOST=172.26.236.119 mcr.microsoft.com/dotnet/framework/samples:wcfclient
```

## Getting the sample

The easiest way to get the sample is by cloning the samples repository with [git](https://git-scm.com/downloads), using the following instructions.

```console
git clone https://github.com/microsoft/dotnet-framework-docker/
```

You can also [download the repository as a zip](https://github.com/microsoft/dotnet-framework-docker/archive/master.zip).

## Build and run the sample with Docker
WCF service is supported on .NET Framework, which can run in Windows Server Core based containers. For simplicity, we disabled security in these samples.

### Build a Container with IIS-hosted WCF Service
Project `WcfServiceWebApp` is created from 'WCF Service Application' template in Visual Studio. A [Dockerfile](/Dockerfile.web) is added to the project. We use a [WCF image](https://hub.docker.com/_/microsoft-dotnet-framework-wcf/) as the base image, which has both HTTP and NET.TCP protocols enabled in IIS and exposes ports 80 (for HTTP) and 808 (for NET.TCP) for the container. We use the WCF image with tag `4.8` for .NET Framework 4.8 in this example, but you can change it to use other tags (eg. `4.7.2`) for WCF images with different versions of .NET Framework. The complete list of supported WCF tags can be found on [Docker Hub](https://hub.docker.com/_/microsoft-dotnet-framework-wcf/).

Run commands below to build the container image with name `iishostedwcfservice` and start an instance of it named `myservice1`. Docker parameter `-d` will run the container in background (detached mode).
```
cd samples
cd wcfapp
docker build --pull -t iishostedwcfservice -f Dockerfile.web .
docker run -d --rm --name myservice1 iishostedwcfservice
```
Find the IP address of the container instance. This will be used later for a WCF client to connect to the service.
```
docker inspect --format="{{.NetworkSettings.Networks.nat.IPAddress}}" myservice1
172.23.70.146
```
### Build a Container with Self-hosted WCF Service
Project `WcfServiceConsoleApp` is created from Windows Classic Desktop 'Console App' template in Visual Studio. We added a [Dockerfile](/Dockerfile.console) to the project. We use the [.NET Framework runtime image](https://hub.docker.com/_/microsoft-dotnet-framework-runtime/) as the base image and expose ports 80 (for HTTP) and 808 (for NET.TCP) for the container.

Run commands below to build the container image with name `selfhostedwcfservice` and start an instance of it named `myservice2`.
```
docker build --pull -t selfhostedwcfservice -f Dockerfile.console .
docker run -it --rm --name myservice2 selfhostedwcfservice
```
Open another console window to find the IP address of the self-hosted WCF service (alternatively, you can run the self-hosted WCF service container in detached mode by `docker run -d --rm --name myservice2 selfhostedwcfservice`).
```
docker inspect --format="{{.NetworkSettings.Networks.nat.IPAddress}}" myservice2
172.23.69.75
```
### Build a Container to run WCF Client against the Service
Now that we have WCF services running in containers. Let's run the WCF client against them. Run commands below to build the container image with name `wcfclient`. The IP address of the WCF service is passed through the environment variable `host`.
```
docker build --pull -t wcfclient -f Dockerfile.client .
docker run -it --rm -e HOST=172.23.70.146 --name myclient wcfclient
Client OS: Microsoft Windows NT 6.2.9200.0
Service Host: 172.23.70.146
Hello WCF via Http from Container!
Hello WCF via Net.Tcp from Container!
```
The result above is from running the WCF client against the IIS-hosted WCF service. Changing the environment variable `host` to the IP address of the self-hosted WCF service, you will get a similar result.

*To run WCF client on a different machine other than the one that hosts container instance*, we will need to map ports of a container to ports of its host by using Docker parameter `-p` when we start a constainer instance. For example, the command below started an instance of the self-hosted WCF service container named `myservice3` with its ports 80 and 808 mapped to ports 80 and 808 of its host machine respectively.
```
C:\wcfapp\WcfServiceConsoleApp>docker run -d -p 80:80 -p 808:808 --name myservice3 selfhostedwcfservice
```
Then for the WCF client to connect to the service, we need to set the `host` to be the IP address (or DNS name) of the container host machine (instead of the IP address of the container instance). The rest will be the same to start the WCF client.

## Build and run the sample with Docker Compose
[Docker Compose](https://docs.docker.com/compose/overview/) is a tool for defining and running multi-container Docker applications. In this sample, we also added YML files to configure WCF server and client applications. You can directly start and run service and client applications without having to do any work to hookup the client to the WCF service.

Type the following Docker Compose command to start both iis-hosted  WCF service container and client container:
```
docker-compose -f docker-compose-iishosted.yml up
```
Alternatively, if you want to build and run self-hosted WCF service container, run commands below:
```
docker-compose -f docker-compose-selfhosted.yml up
```

## Build the sample locally

You can build this [.NET Framework 4.8](https://www.microsoft.com/net/download/dotnet-framework-runtime/net48) application locally with MSBuild using the following instructions. The instructions assume that you are in the root of the repository and using the `Developer Command Prompt for VS 2019`.

You must have the [.NET Framework 4.8 targeting pack](https://go.microsoft.com/fwlink/?LinkId=2085167) installed. It is easiest to install with [Visual Studio 2019](https://visualstudio.microsoft.com/vs/) with the Visual Studio Installer.

```console
cd samples
cd wcfapp
msbuild wcfapp.sln /p:Configuration=Release
```

Note: The /p:Configuration=Release argument builds the application in release mode (the default is debug mode). See the [MSBuild Command-Line reference](https://msdn.microsoft.com/en-us/library/ms164311.aspx) for more information on commandline parameters.
You can also build, test and debug the application with [Visual Studio 2019](https://visualstudio.microsoft.com/vs/).

## .NET Resources

More Samples

* [.NET Framework Docker Samples](../README.md)
* [.NET Core Docker Samples](https://github.com/dotnet/dotnet-docker/blob/master/samples/README.md)

Docs and More Information:

* [.NET Docs](https://docs.microsoft.com/dotnet/)
* [WCF Docs](https://docs.microsoft.com/dotnet/framework/wcf/)
* [ASP.NET Docs](https://docs.microsoft.com/aspnet/)
* [dotnet/core](https://github.com/dotnet/core) for starting with .NET Core on GitHub.
* [dotnet/announcements](https://github.com/dotnet/announcements/issues) for .NET announcements.

## Related Docker Hub Repos

.NET Framework:

* [dotnet/framework/sdk](https://hub.docker.com/_/microsoft-dotnet-framework-sdk/): .NET Framework SDK
* [dotnet/framework/aspnet](https://hub.docker.com/_/microsoft-dotnet-framework-aspnet/): ASP.NET Web Forms and MVC
* [dotnet/framework/runtime](https://hub.docker.com/_/microsoft-dotnet-framework-runtime/): .NET Framework Runtime
* [dotnet/framework/wcf](https://hub.docker.com/_/microsoft-dotnet-framework-wcf/): Windows Communication Foundation (WCF)
* [dotnet/framework/samples](https://hub.docker.com/_/microsoft-dotnet-framework-samples/): .NET Framework, ASP.NET and WCF Samples

.NET Core:

* [dotnet/core](https://hub.docker.com/_/microsoft-dotnet-core/): .NET Core
* [dotnet/core/samples](https://hub.docker.com/_/microsoft-dotnet-core-samples/): .NET Core Samples
* [dotnet/core-nightly](https://hub.docker.com/_/microsoft-dotnet-core-nightly/): .NET Core (Preview)

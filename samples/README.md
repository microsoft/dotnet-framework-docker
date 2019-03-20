# .NET Framework Docker Samples

The samples show various ways to use .NET Framework and Docker together. You can use the samples as the basis of your own Docker images or just to play.

The samples exercise various levels of functionality. The [.NET Framework Docker sample](dotnetapp/README.md) includes the most functionality, including build, unit testing, and pushing images to a container registry. The [ASP.NET Docker sample](aspnetapp/README.md) includes instructions for testing images with [Azure Container Instances](https://azure.microsoft.com/services/container-instances/). The samples include detailed instructions for use with and without Docker. The [WCF Docker sample](wcfapp/README.md) includes instructions for dockerizing WCF services, either IIS-hosted or self-hosted, and how to run client app against them. 

## Try a pre-built .NET Framework Docker Image

You can quickly run a container with a pre-built [.NET Framework Docker image](https://hub.docker.com/_/microsoft-dotnet-framework-samples/), based on the [.NET Framework console sample](dotnetapp/README.md).

Type the following [Docker](https://www.docker.com/products/docker) command:

```console
docker run --rm mcr.microsoft.com/dotnet/framework/samples:dotnetapp
```

## Try a pre-built ASP.NET Docker Image

You can quickly run a container with a pre-built [sample ASP.NET Docker image](https://hub.docker.com/_/microsoft-dotnet-framework-samples/), based on the [ASP.NET Docker sample].

Type the following [Docker](https://www.docker.com/products/docker) command:

```console
docker run --name aspnet_sample --rm -it -p 8000:80 mcr.microsoft.com/dotnet/framework/samples:aspnetapp
```

After the application starts, navigate to `http://localhost:8000` in your web browser. You need to navigate to the application via IP address instead of `localhost` for earlier Windows versions, which is demonstrated in [View the ASP.NET app in a running container on Windows](https://github.com/microsoft/dotnet-framework-docker/blob/master/samples/aspnetapp/README.md#view-the-aspnet-app-in-a-running-container-on-windows).

## Building .NET Framework Apps with Docker

* [.NET Framework Console Docker Sample](dotnetapp/README.md) - This [sample](dotnetapp/Dockerfile) builds, tests, and runs the sample. It includes and builds multiple projects.
* [ASP.NET Web Forms Docker Sample](aspnetapp/README.md) - This [sample](aspnetapp/Dockerfile) demonstrates using Docker with an ASP.NET Web Forms app.
* [ASP.NET MVC Docker Sample](aspnetmvcapp/README.md) - This [sample](aspnetmvcapp/Dockerfile) demonstrates using Docker with an ASP.NET MVC app.
* [WCF Docker Sample](wcfapp/README.md) - This [sample](wcfapp/) demonstrates using Docker with a WCF app.

## Push Images to a Container Registry

* [Push Docker Images to Azure Container Registry](dotnetapp/push-image-to-acr.md)
* [Push Docker Images to DockerHub](dotnetapp/push-image-to-dockerhub.md)
* [Deploy ASP.NET Applications to Azure Container Instances](aspnetapp/deploy-container-to-aci.md)

## .NET Resources

More Samples

* [.NET Core Docker Samples](https://github.com/dotnet/dotnet-docker/blob/master/samples/README.md)

Docs and More Information:

* [.NET Docs](https://docs.microsoft.com/dotnet/)
* [ASP.NET Docs](https://docs.microsoft.com/aspnet/)
* [WCF Docs](https://docs.microsoft.com/dotnet/framework/wcf/)
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

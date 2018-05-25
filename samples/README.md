# .NET Framework Docker Samples

The samples show various ways to use .NET Framework and Docker together. You can use the samples as the basis of your own Docker images or just to play.

The samples exercise various levels of functionality. The [.NET Framework Docker sample](dotnetapp/README.md) includes the most functionality, including build, unit testing, and pushing images to a container registry. The [ASP.NET Docker sample](aspnetapp/README.md) includes instructions for testing images with [Azure Container Instances](https://azure.microsoft.com/services/container-instances/). The samples include detailed instructions for use with and without Docker. The [WCF Docker sample](wcfapp/README.md) includes instructions for dockerize WCF services, either IIS-hosted or self-hosted, and how to run client app against them. 

## Try a pre-built .NET Framework Docker Image

You can quickly run a container with a pre-built [.NET Framework Docker image](https://hub.docker.com/r/microsoft/dotnet-framework-samples/), based on the [.NET Framework console sample](dotnetapp/README.md).

Type the following [Docker](https://www.docker.com/products/docker) command:

```console
docker run --rm microsoft/dotnet-framework-samples
```

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

## Related Repositories

.NET Core Docker Hub repos:

* [microsoft/aspnetcore](https://hub.docker.com/r/microsoft/aspnetcore/) for ASP.NET Core images.
* [microsoft/dotnet](https://hub.docker.com/r/microsoft/dotnet/) for .NET Core images.
* [microsoft/dotnet-nightly](https://hub.docker.com/r/microsoft/dotnet-nightly/) for .NET Core preview images.
* [microsoft/dotnet-samples](https://hub.docker.com/r/microsoft/dotnet-samples/) for .NET Core sample images.

.NET Framework Docker Hub repos:

* [microsoft/aspnet](https://hub.docker.com/r/microsoft/aspnet/) for ASP.NET Web Forms and MVC images.
* [microsoft/dotnet-framework](https://hub.docker.com/r/microsoft/dotnet-framework/) for .NET Framework images.
* [microsoft/dotnet-framework-samples](https://hub.docker.com/r/microsoft/dotnet-framework-samples/) for .NET Framework and ASP.NET sample images.
* [microsoft/wcf](https://hub.docker.com/r/microsoft/wcf) for WCF images.
# .NET Framework Docker Samples

The samples show various ways to use .NET Framework and Docker together. You can use the samples as the basis of your own Docker images or just to play.

The samples demonstrate basic functionality. The [.NET Docker Samples](https://github.com/dotnet/dotnet-docker/tree/main/samples) exercise more functionality, most of which can be applied to .NET Framework apps.

## Try a pre-built .NET Framework Docker Image

You can quickly run a container with a pre-built [.NET Framework Docker image](../README.samples.md), based on the [.NET Framework console sample](dotnetapp/README.md).

Type the following [Docker](https://www.docker.com/products/docker) command:

```console
docker run --rm mcr.microsoft.com/dotnet/framework/samples
```

## Try a pre-built ASP.NET Docker Image

You can quickly run a container with a pre-built [sample ASP.NET Docker image](../README.samples.md), based on the [ASP.NET Docker sample].

Type the following [Docker](https://www.docker.com/products/docker) command:

```console
docker run --name aspnet_sample --rm -it -p 8000:80 mcr.microsoft.com/dotnet/framework/samples:aspnetapp
```

After the application starts, navigate to `http://localhost:8000` in your web browser. You need to navigate to the application via IP address instead of `localhost` for earlier Windows versions, which is demonstrated in [View the ASP.NET app in a running container on Windows](https://github.com/microsoft/dotnet-framework-docker/blob/main/samples/aspnetapp/README.md#view-the-aspnet-app-in-a-running-container-on-windows).

## Building .NET Framework Apps with Docker

* [.NET Framework Console Docker Sample](dotnetapp/README.md) - This [sample](dotnetapp/Dockerfile) builds, tests, and runs the sample. It includes and builds multiple projects.
* [ASP.NET Web Forms Docker Sample](aspnetapp/README.md) - This [sample](aspnetapp/Dockerfile) demonstrates using Docker with an ASP.NET Web Forms app.
* [ASP.NET MVC Docker Sample](aspnetmvcapp/README.md) - This [sample](aspnetmvcapp/Dockerfile) demonstrates using Docker with an ASP.NET MVC app.
* [WCF Docker Sample](wcfapp/README.md) - This [sample](wcfapp/) demonstrates using Docker with a WCF app.

## Push Images to a Container Registry

* [Push Docker Images to Azure Container Registry](https://github.com/dotnet/dotnet-docker/blob/main/samples/push-image-to-acr.md)
* [Push Docker Images to DockerHub](https://github.com/dotnet/dotnet-docker/blob/main/samples//push-image-to-dockerhub.md)
* [Deploy ASP.NET Applications to Azure Container Instances](https://github.com/dotnet/dotnet-docker/blob/main/samples//deploy-container-to-aci.md)

## .NET Resources

More Samples

* [.NET Docker Samples](https://github.com/dotnet/dotnet-docker/blob/main/samples/README.md)

Docs and More Information:

* [.NET Docs](https://docs.microsoft.com/dotnet/)
* [ASP.NET Docs](https://docs.microsoft.com/aspnet/)
* [WCF Docs](https://docs.microsoft.com/dotnet/framework/wcf/)
* [dotnet/core](https://github.com/dotnet/core) for starting with .NET on GitHub.
* [dotnet/announcements](https://github.com/dotnet/announcements/issues) for .NET announcements.

## Related Docker Hub Repos

.NET Framework:

* [dotnet/framework/sdk](../README.sdk.md): .NET Framework SDK
* [dotnet/framework/aspnet](../README.aspnet.md): ASP.NET Web Forms and MVC
* [dotnet/framework/runtime](../README.runtime.md): .NET Framework Runtime
* [dotnet/framework/wcf](../README.wcf.md): Windows Communication Foundation (WCF)
* [dotnet/framework/samples](../README.samples.md): .NET Framework, ASP.NET and WCF Samples

.NET:

* [dotnet](https://github.com/dotnet/dotnet-docker/blob/main/README.md): .NET
* [dotnet/samples](https://github.com/dotnet/dotnet-docker/blob/main/README.samples.md): .NET Samples

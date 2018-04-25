# Latest Version of Common Tags

The following tags are the latest stable versions of the most commonly used images. The complete set of tags is listed further down.

- [`4.7.1`](https://github.com/Microsoft/dotnet-framework-docker/blob/master/4.7.1-windowsservercore-1709/runtime/Dockerfile)
- [`3.5`](https://github.com/Microsoft/dotnet-framework-docker/blob/master/3.5-windowsservercore-1709/runtime/Dockerfile)

The [.NET Framework Docker samples](https://github.com/Microsoft/dotnet-framework-docker/blob/master/samples/README.md) show various ways to use .NET Framework and Docker together. See [Building Docker Images for .NET Core Applications](https://docs.microsoft.com/dotnet/framework/docker/) to learn more.

Watch [dotnet/announcements](https://github.com/dotnet/announcements/labels/Docker) for Docker-related .NET announcements.

### Container sample: Run a simple application

Type the following command to run a sample console application:

```console
docker run --rm microsoft/dotnet-framework-samples
```

### Container sample: Run a web application

Type the following command to run a sample web application:

```console
docker run -it --rm -p 8000:80 --name aspnet_sample microsoft/dotnet-framework-samples:aspnetapp
```

After the application starts, navigate to `http://localhost:8000` in your web browser. You need to navigate to the application via IP address instead of `localhost` for Windows containers, which is demonstrated in [View the ASP.NET Core app in a running container on Windows](https://github.com/dotnet/dotnet-docker/blob/master/samples/aspnetapp/README.md#view-the-aspnet-core-app-in-a-running-container-on-windows).

## Complete set of Tags

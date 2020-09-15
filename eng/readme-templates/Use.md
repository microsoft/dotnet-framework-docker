## Container sample: Run a simple application

Type the following command to run a sample console application:

```console
docker run --rm mcr.microsoft.com/dotnet/framework/samples:dotnetapp
```

## Container sample: Run a web application

Type the following command to run a sample web application:

```console
docker run -it --rm -p 8000:80 --name aspnet_sample mcr.microsoft.com/dotnet/framework/samples:aspnetapp
```

After the application starts, navigate to `http://localhost:8000` in your web browser. You need to navigate to the application via IP address instead of `localhost` for earlier Windows versions, which is demonstrated in [View the ASP.NET app in a running container on Windows](https://github.com/microsoft/dotnet-framework-docker/blob/master/samples/aspnetapp/README.md#view-the-aspnet-app-in-a-running-container-on-windows).

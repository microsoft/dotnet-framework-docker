# Featured Tags

* `4.8`
  * `docker pull mcr.microsoft.com/dotnet/framework/aspnet:4.8`
* `3.5`
  * `docker pull mcr.microsoft.com/dotnet/framework/aspnet:3.5`

# What is ASP.NET

ASP.NET is a high productivity framework for building Web Applications using Web Forms, MVC, Web API and SignalR.

This repository contains `Dockerfile` definitions for ASP.NET Docker images. 

This image contains:
- Windows Server Core as the base OS
- IIS 10 as Web Server
- .NET Framework (multiple versions available)
- .NET Extensibility for IIS

# How to use these Images

The [.NET Framework Docker samples](https://github.com/microsoft/dotnet-framework-docker/blob/master/samples/README.md) show various ways to use .NET Framework and Docker together.

There is no need to specify an `ENTRYPOINT` in your Dockerfile since the `microsoft/framework/aspnet` base image already includes an entrypoint application that monitors the status of the IIS World Wide Web Publishing Service (W3SVC).

### Verify in the browser

> With the current release, you can't use `http://localhost` to browse your site from the container host. This is because of a known behavior in WinNAT, and will be resolved in future. Until that is addressed, you need to use the IP address of the container.

Once the container starts, you'll need to finds its IP address so that you can connect to your running container from a browser. You use the `docker inspect` command to do that:

`docker inspect -f "{{ .NetworkSettings.Networks.nat.IPAddress }}" my-running-site`

You will see an output similar to this:

```
172.28.103.186
```

You can connect the running container using the IP address and configured port, `http://172.28.103.186` in the example shown.

For a comprehensive tutorial on running an ASP.NET app in a container, check out [the tutorial on the docs site](https://docs.microsoft.com/en-us/dotnet/articles/framework/docker/aspnetmvc).

## Image variants

The `microsoft/aspnet` images come in different flavors, each designed for a specific use case.

# Full Tag Listing

## Windows Server, version 1903 amd64 Tags
Tag | Dockerfile
---------| ---------------
4.8-20190709-windowsservercore-1903, 4.8-windowsservercore-1903, 4.8, latest | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/master/4.8/runtime/windowsservercore-1903/Dockerfile)
3.5-20190709-windowsservercore-1903, 3.5-windowsservercore-1903, 3.5 | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/master/3.5/runtime/windowsservercore-1903/Dockerfile)

## Windows Server 2019 amd64 Tags
Tag | Dockerfile
---------| ---------------
4.8-20190709-windowsservercore-ltsc2019, 4.8-windowsservercore-ltsc2019, 4.8, latest | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/master/4.8/runtime/windowsservercore-ltsc2019/Dockerfile)
4.7.2-20190709-windowsservercore-ltsc2019, 4.7.2-windowsservercore-ltsc2019, 4.7.2 | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/master/4.7.2/runtime/windowsservercore-ltsc2019/Dockerfile)
3.5-20190709-windowsservercore-ltsc2019, 3.5-windowsservercore-ltsc2019, 3.5 | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/master/3.5/runtime/windowsservercore-ltsc2019/Dockerfile)

## Windows Server, version 1803 amd64 Tags
Tag | Dockerfile
---------| ---------------
4.8-20190709-windowsservercore-1803, 4.8-windowsservercore-1803, 4.8, latest | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/master/4.8/runtime/windowsservercore-1803/Dockerfile)
4.7.2-20190709-windowsservercore-1803, 4.7.2-windowsservercore-1803, 4.7.2 | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/master/4.7.2/runtime/windowsservercore-1803/Dockerfile)
3.5-20190709-windowsservercore-1803, 3.5-windowsservercore-1803, 3.5 | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/master/3.5/runtime/windowsservercore-1803/Dockerfile)

## Windows Server 2016 amd64 Tags
Tag | Dockerfile
---------| ---------------
4.8-20190709-windowsservercore-ltsc2016, 4.8-windowsservercore-ltsc2016, 4.8, latest | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/master/4.8/runtime/windowsservercore-ltsc2016/Dockerfile)
4.7.2-20190709-windowsservercore-ltsc2016, 4.7.2-windowsservercore-ltsc2016, 4.7.2 | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/master/4.7.2/runtime/windowsservercore-ltsc2016/Dockerfile)
4.7.1-20190709-windowsservercore-ltsc2016, 4.7.1-windowsservercore-ltsc2016, 4.7.1 | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/master/4.7.1/runtime/windowsservercore-ltsc2016/Dockerfile)
4.7-20190709-windowsservercore-ltsc2016, 4.7-windowsservercore-ltsc2016, 4.7 | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/master/4.7/runtime/windowsservercore-ltsc2016/Dockerfile)
4.6.2-20190709-windowsservercore-ltsc2016, 4.6.2-windowsservercore-ltsc2016, 4.6.2 | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/master/4.6.2/runtime/windowsservercore-ltsc2016/Dockerfile)
3.5-20190709-windowsservercore-ltsc2016, 3.5-windowsservercore-ltsc2016, 3.5 | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/master/3.5/runtime/windowsservercore-ltsc2016/Dockerfile)

You can retrieve a list of all available tags for dotnet/framework/runtime at https://mcr.microsoft.com/v2/dotnet/framework/aspnet/tags/list.


# Related Repos

See the following related repos for other application types:

- [microsoft/dotnet-framework](https://hub.docker.com/r/microsoft/dotnet-framework/) for .NET Framework applications.
- [microsoft/aspnetcore](https://hub.docker.com/r/microsoft/aspnetcore/) for ASP.NET Core applications.
- [microsoft/dotnet](https://hub.docker.com/r/microsoft/dotnet/) for .NET Core images.

# Support

See the [.NET Framework Lifecycle FAQ](https://support.microsoft.com/en-us/help/17455/lifecycle-faq-net-framework)

# Feedback

* [File a .NET Framework Docker issue](https://github.com/microsoft/dotnet-framework-docker/issues)
* [Report a .NET Framework problem](https://developercommunity.visualstudio.com/spaces/61/index.html)
* [Ask on Stack Overflow](https://stackoverflow.com/questions/tagged/.net)
* [Contact Microsoft Support](https://support.microsoft.com/contactus/)

# License

View [license information](https://www.microsoft.com/net/dotnet_library_license.htm) for the software contained in this image. 

Windows Container images use the same license as the [Windows Server Core base image](https://hub.docker.com/_/microsoft-windows-servercore/).


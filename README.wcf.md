# Featured Tags

* `4.8`
  * `docker pull mcr.microsoft.com/dotnet/framework/wcf:4.8`

## What is WCF?
The Windows Communication Foundation (WCF) is  a framework for building service-oriented applications. Using WCF, you can send data as asynchronous messages from one service endpoint to another. A service endpoint can be part of a continuously available service hosted by IIS, or it can be a service hosted in an application.

![WCF Docker Image](https://avatars2.githubusercontent.com/u/6154722?v=3&s=200)

## How to use this image?
### Create a Dockerfile with your WCF service IIS Hosted or selfhosted
```
FROM mcr.microsoft.com/dotnet/framework/wcf

WORKDIR WcfService

RUN powershell -NoProfile -Command \
    Import-module IISAdministration; \
    New-IISSite -Name "WcfService" -PhysicalPath C:\WcfService -BindingInformation "*:83:"

EXPOSE 83

COPY content/ .
```
You can then build and run the Docker image:
```
$ docker build -t wcfserviceimage .
$ docker run -d -p 83:83 --name my-wcfservice wcfserviceimage
```

There is no need to specify an `ENTRYPOINT` in your Dockerfile since an entrypoint application is already specified that monitors the status of the IIS World Wide Web Publishing Service (W3SVC).

### Verify in the browser

For Windows version 1803 or higher, you can connect to the running container using 'http://localhost:83/<wcfservice.svc>` in the example shown.

For Windows versions prior to 1803, you cannot use `http://localhost` to browse your site from the container host. This is because of a known behavior in WinNAT for those versions which requires you to use the IP address of the container.

Once the container starts, you'll need to find its IP address so that you can connect to your running container from a browser. You use the `docker inspect` command to do that:	
 `docker inspect -f "{{ .NetworkSettings.Networks.nat.IPAddress }}" my-wcfservice`	
 You will see an output similar to this:	
 ```	
172.28.103.001	
```	
 You can connect to the running container using the IP address and configured port, `http://172.28.103.001:83/<wcfservice.svc>` in the example shown.

For a comprehensive tutorial on running an WCF service in a container, check out [WCF service samples in container](https://github.com/Microsoft/dotnet-framework-docker/tree/master/samples/wcfapp)

## Image variants

The `mcr.microsoft.com/dotnet/framework/wcf` images come in different flavors, each designed for a specific use case.
# Full Tag Listing

## Windows Server, version 1903 amd64 Tags
Tag | Dockerfile
---------| ---------------
4.8-20190709-windowsservercore-1903, 4.8-windowsservercore-1903, 4.8, latest | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/master/4.8/runtime/windowsservercore-1903/Dockerfile)


## Windows Server 2019 amd64 Tags
Tag | Dockerfile
---------| ---------------
4.8-20190709-windowsservercore-ltsc2019, 4.8-windowsservercore-ltsc2019, 4.8, latest | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/master/4.8/runtime/windowsservercore-ltsc2019/Dockerfile)
4.7.2-20190709-windowsservercore-ltsc2019, 4.7.2-windowsservercore-ltsc2019, 4.7.2 | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/master/4.7.2/runtime/windowsservercore-ltsc2019/Dockerfile)


## Windows Server, version 1803 amd64 Tags
Tag | Dockerfile
---------| ---------------
4.8-20190709-windowsservercore-1803, 4.8-windowsservercore-1803, 4.8, latest | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/master/4.8/runtime/windowsservercore-1803/Dockerfile)
4.7.2-20190709-windowsservercore-1803, 4.7.2-windowsservercore-1803, 4.7.2 | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/master/4.7.2/runtime/windowsservercore-1803/Dockerfile)

## Windows Server 2016 amd64 Tags
Tag | Dockerfile
---------| ---------------
4.8-20190709-windowsservercore-ltsc2016, 4.8-windowsservercore-ltsc2016, 4.8, latest | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/master/4.8/runtime/windowsservercore-ltsc2016/Dockerfile)
4.7.2-20190709-windowsservercore-ltsc2016, 4.7.2-windowsservercore-ltsc2016, 4.7.2 | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/master/4.7.2/runtime/windowsservercore-ltsc2016/Dockerfile)
4.7.1-20190709-windowsservercore-ltsc2016, 4.7.1-windowsservercore-ltsc2016, 4.7.1 | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/master/4.7.1/runtime/windowsservercore-ltsc2016/Dockerfile)
4.7-20190709-windowsservercore-ltsc2016, 4.7-windowsservercore-ltsc2016, 4.7 | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/master/4.7/runtime/windowsservercore-ltsc2016/Dockerfile)
4.6.2-20190709-windowsservercore-ltsc2016, 4.6.2-windowsservercore-ltsc2016, 4.6.2 | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/master/4.6.2/runtime/windowsservercore-ltsc2016/Dockerfile)

You can retrieve a list of all available tags for dotnet/framework/runtime at https://mcr.microsoft.com/v2/dotnet/framework/wcf/tags/list.

# Related Repos

See the following related repos for other application types:

- [microsoft/dotnet-framework](https://hub.docker.com/r/microsoft/dotnet-framework/) for .NET Framework applications.
- [microsoft/aspnetcore](https://hub.docker.com/r/microsoft/aspnet) for ASP.NET images.
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



# WCF Docker Container Samples
These samples demonstrate how to dockerize WCF services, either IIS-hosted or self-hosted. A simple "hello world" service contract is used in all samples for both HTTP and NET.TCP transport bindings.

## Prepare Your Environment
After cloning or downloading this repository, build the solution in the `wcfapp` folder either in Visual Studio or using command line `msbuild wcfapp.sln` in a VS console window. You will need environment and software below installed to complete these samples.

* [Windows 10](https://www.microsoft.com/en-us/windows/get-windows-10) (or [Windows Server 2016](https://www.microsoft.com/en-us/cloud-platform/windows-server)) with **'Containers'** feature enabled.
* [Docker for Windows](https://docs.docker.com/docker-for-windows/) with **'Switch to Windows Containers...'** by right clicking on the Docker whale icon in system tray
* [Visual Studio 2017](https://www.visualstudio.com/vs/)

## Run WCF Service in Container
WCF service is supported on .NET Framework, which can run in Windows Server Core based containers. For simplicity, we disabled security in these samples.

### Build a Container with IIS-hosted WCF Service
Project `WcfServiceWebApp` is created from 'WCF Service Application' template in Visual Studio. A [Dockerfile](WcfServiceWebApp/Dockerfile) is added to the project. We use `microsoft/wcf` image as the base image, which has both HTTP and NET.TCP protocols enabled in IIS and exposes ports 80 (for HTTP) and 808 (for NET.TCP) for the container. We use WCF image with tag `4.6.2` for .NET Framework 4.6.2 in this example, but you can change it to use other tags (eg. `microsoft/wcf:4.7` with tag `4.7`) for WCF images with different versions of .NET Framework. The complete list of [WCF image tags](https://hub.docker.com/r/microsoft/wcf/tags/) can be found from Docker Hub. 

Run commands below to build the container image with name `wcfservice:iis-hosted` and start an instance of it named `myservice1`. Docker parameter `-d` will run the container in background (detached mode).
```
C:\wcfapp\WcfServiceWebApp>docker build -t wcfservice:iis-hosted .
C:\wcfapp\WcfServiceWebApp>docker run -d --name myservice1 wcfservice:iis-hosted
```
Find the IP address of the container instance. This will be used later for a WCF client to connect to the service.
```
C:\wcfapp\WcfServiceWebApp>docker inspect --format="{{.NetworkSettings.Networks.nat.IPAddress}}" myservice1
172.23.70.146
```
### Build a Container with Self-hosted WCF Service
Project `WcfServiceConsoleApp` is created from Windows Classic Desktop 'Console App' template in Visual Studio. We added a [Dockerfile](WcfServiceConsoleApp/Dockerfile) to the project. We use `microsoft/dotnet-framework` image as the base image and expose ports 80 (for HTTP) and 808 (for NET.TCP) for the container.

Make sure you build project `WcfServiceConsoleApp` first. Then run commands below to build the container image with name `wcfservice:self-hosted` and start an instance of it named `myservice2`.
```
C:\wcfapp\WcfServiceConsoleApp>docker build -t wcfservice:self-hosted .
C:\wcfapp\WcfServiceConsoleApp>docker run -it --rm --name myservice2 wcfservice:self-hosted
The service is ready at http://localhost/Service1.svc
The service is ready at net.tcp://localhost/Service1.svc
The service is running...
```
Open another console window to find the IP address of the self-hosted WCF service (alternatively, you can run the self-hosted WCF service container in detached mode by `docker run -d --name myservice2 wcfservice:self-hosted`).
```
C:\wcfapp\WcfClientNetCore>docker inspect --format="{{.NetworkSettings.Networks.nat.IPAddress}}" myservice2
172.23.69.75
```
### Run WCF Client against the Service in Container
Now that we have WCF services running in containers. Let's run the WCF client against the them. The IP address of the WCF service is passed through the environment variable `host`. *In this example, the WCF client runs on the same machine that hosts the container instance.* 
```
C:\wcfapp\WcfClient>set host=172.23.70.146
C:\wcfapp\WcfClient>bin\Debug\WcfClient.exe
Client OS: Microsoft Windows NT 6.2.9200.0
Service Host: 172.23.70.146
Hello WCF via Http from Container!
Hello WCF via Net.Tcp from Container!
```
The result above is from running the WCF client against the IIS-hosted WCF service. Changing the environment variable `host` to the IP address of the self-hosted WCF service, you will get a similar result.

*To run WCF client on a different machine other than the one that hosts container instance*, we will need to map ports of a container to ports of its host by using Docker parameter `-p` when we start a constainer instance. For example, the command below started an instance of the self-hosted WCF service container named `myservice3` with its ports 80 and 808 mapped to ports 80 and 808 of its host machine respectively.
```
C:\wcfapp\WcfServiceConsoleApp>docker run -d -p 80:80 -p 808:808 --name myservice3 wcfservice:self-hosted
```
Then for the WCF client to connect to the service, we need to set the `host` to be the IP address (or DNS name) of the container host machine (instead of the IP address of the container instance). The rest will be the same to start the WCF client.

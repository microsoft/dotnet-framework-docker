# Featured Tags

* `4.8`
  * `docker pull mcr.microsoft.com/dotnet/framework/sdk:4.8`
* `3.5`
  * `docker pull mcr.microsoft.com/dotnet/framework/sdk:3.5`

# About This Image

This image contains the .NET Framework SDK which is comprised of the following parts:

1. .NET Framework Runtime
1. Visual Studio Build Tools
1. Visual Studio Test Agent
1. NuGet CLI
1. .NET Framework Targeting Packs
1. ASP.NET Web Targets

Use this image for your development process (developing, building and testing applications).

Watch [dotnet/announcements](https://github.com/dotnet/announcements/labels/Docker) for Docker-related .NET announcements.

# How to Use the Image

The [.NET Framework Docker samples](https://github.com/microsoft/dotnet-framework-docker/blob/main/samples/README.md) show various ways to use .NET Framework and Docker together.

## Building .NET Framework Apps with Docker

* [.NET Framework Console Docker Sample](dotnetapp/README.md) - This [sample](dotnetapp/Dockerfile) builds, tests, and runs the sample. It includes and builds multiple projects.
* [ASP.NET Web Forms Docker Sample](aspnetapp/README.md) - This [sample](aspnetapp/Dockerfile) demonstrates using Docker with an ASP.NET Web Forms app.
* [ASP.NET MVC Docker Sample](aspnetmvcapp/README.md) - This [sample](aspnetmvcapp/Dockerfile) demonstrates using Docker with an ASP.NET MVC app.
* [WCF Docker Sample](wcfapp/README.md) - This [sample](wcfapp/) demonstrates using Docker with a WCF app.

# Related Repos

.NET Framework:

* [dotnet/framework/aspnet](https://hub.docker.com/_/microsoft-dotnet-framework-aspnet/): ASP.NET Web Forms and MVC
* [dotnet/framework/runtime](https://hub.docker.com/_/microsoft-dotnet-framework-runtime/): .NET Framework Runtime
* [dotnet/framework/wcf](https://hub.docker.com/_/microsoft-dotnet-framework-wcf/): Windows Communication Foundation (WCF)
* [dotnet/framework/samples](https://hub.docker.com/_/microsoft-dotnet-framework-samples/): .NET Framework, ASP.NET and WCF Samples

.NET:

* [dotnet](https://hub.docker.com/_/microsoft-dotnet/): .NET
* [dotnet-nightly](https://hub.docker.com/_/microsoft-dotnet-nightly/): .NET (Preview)
* [dotnet/samples](https://hub.docker.com/_/microsoft-dotnet-samples/): .NET Samples

# Full Tag Listing

## Windows Server Core 2022 amd64 Tags
Tag | Dockerfile
---------| ---------------
4.8-20220412-windowsservercore-ltsc2022, 4.8-windowsservercore-ltsc2022, 4.8, latest | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/main/src/sdk/4.8/windowsservercore-ltsc2022/Dockerfile)
3.5-20220412-windowsservercore-ltsc2022, 3.5-windowsservercore-ltsc2022, 3.5 | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/main/src/sdk/3.5/windowsservercore-ltsc2022/Dockerfile)

## Windows Server Core, version 20H2 amd64 Tags
Tag | Dockerfile
---------| ---------------
4.8-20220412-windowsservercore-20H2, 4.8-windowsservercore-20H2, 4.8, latest | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/main/src/sdk/4.8/windowsservercore-20H2/Dockerfile)
3.5-20220412-windowsservercore-20H2, 3.5-windowsservercore-20H2, 3.5 | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/main/src/sdk/3.5/windowsservercore-20H2/Dockerfile)

## Windows Server Core 2019 amd64 Tags
Tag | Dockerfile
---------| ---------------
4.8-20220412-windowsservercore-ltsc2019, 4.8-windowsservercore-ltsc2019, 4.8, latest | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/main/src/sdk/4.8/windowsservercore-ltsc2019/Dockerfile)
3.5-20220412-windowsservercore-ltsc2019, 3.5-windowsservercore-ltsc2019, 3.5 | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/main/src/sdk/3.5/windowsservercore-ltsc2019/Dockerfile)

## Windows Server Core 2016 amd64 Tags
Tag | Dockerfile
---------| ---------------
4.8-20220412-windowsservercore-ltsc2016, 4.8-windowsservercore-ltsc2016, 4.8, latest | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/main/src/sdk/4.8/windowsservercore-ltsc2016/Dockerfile)
3.5-20220412-windowsservercore-ltsc2016, 3.5-windowsservercore-ltsc2016, 3.5 | [Dockerfile](https://github.com/microsoft/dotnet-framework-docker/blob/main/src/sdk/3.5/windowsservercore-ltsc2016/Dockerfile)

You can retrieve a list of all available tags for dotnet/framework/sdk at https://mcr.microsoft.com/v2/dotnet/framework/sdk/tags/list.
<!--End of generated tags-->

*Tags not listed in the table above should be considered unsupported.*

# Version Compatibility

Version Tag | OS Version | Supported .NET Versions
-- | -- | --
4.8 | windowsservercore-20H2, windowsservercore-ltsc2019, windowsservercore-ltsc2016 | 4.8*
4.7.2 | windowsservercore-ltsc2019, windowsservercore-ltsc2016 | 4.7.2
4.7.1 | windowsservercore-ltsc2016 | 4.7.1
4.7 | windowsservercore-ltsc2016 | 4.7
4.6.2 | windowsservercore-ltsc2016 | 4.6.2
3.5 | windowsservercore-20H2 | 4.8, 3.5, 3.0, 2.5
3.5 | windowsservercore-ltsc2019 | 4.7.2, 3.5, 3.0, 2.5
3.5 | windowsservercore-ltsc2016 | 4.6.2, 3.5, 3.0, 2.5

\* The 4.8 SDK is also capable of building 4.8, 4.7.2, 4.7.1, 4.7, and 4.6.2 projects.

# Support

See the [.NET Framework Lifecycle FAQ](https://support.microsoft.com/help/17455/lifecycle-faq-net-framework)

See detailed support information for .NET Frameworks containers on [tags](https://github.com/microsoft/dotnet-framework-docker/blob/main/documentation/supported-tags.md).

# Image Update Policy

* We update the supported .NET Framework images within 12 hours of any updates to their base images (e.g. windows/servercore:20H2, windows/servercore:ltsc2019, etc.).
* We publish .NET Framework images as part of releasing new versions of .NET Framework including major/minor and servicing.

# Feedback

* [File an issue](https://github.com/microsoft/dotnet-framework-docker/issues/new/choose)
* [Contact Microsoft Support](https://support.microsoft.com/contactus/)

# License

* [Microsoft Container Images Legal Notice](https://aka.ms/mcr/osslegalnotice): applies to all [.NET Framework container images](https://hub.docker.com/_/microsoft-dotnet-framework/)
* [Microsoft Software Supplemental License for Windows Container Base Image](https://hub.docker.com/_/microsoft-windows-servercore/): applies to all [.NET Framework container images](https://hub.docker.com/_/microsoft-dotnet-framework/)
* [Visual Studio Tools License](https://visualstudio.microsoft.com/license-terms/mlt031519/): applies to all [.NET Framework SDK container images](https://hub.docker.com/_/microsoft-dotnet-framework-sdk/)

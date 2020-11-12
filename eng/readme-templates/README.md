{{if IS_PRODUCT_FAMILY
:# Featured Repos

* [dotnet/framework/sdk](https://hub.docker.com/_/microsoft-dotnet-framework-sdk/): .NET Framework SDK
* [dotnet/framework/aspnet](https://hub.docker.com/_/microsoft-dotnet-framework-aspnet/): ASP.NET Web Forms and MVC
* [dotnet/framework/runtime](https://hub.docker.com/_/microsoft-dotnet-framework-runtime/): .NET Framework Runtime
* [dotnet/framework/wcf](https://hub.docker.com/_/microsoft-dotnet-framework-wcf/): Windows Communication Foundation (WCF)
* [dotnet/framework/samples](https://hub.docker.com/_/microsoft-dotnet-framework-samples/): .NET Framework, ASP.NET and WCF Samples
^else:# Featured Tags

{{if SHORT_REPO = "samples"
:* `dotnetapp`
  * `docker pull mcr.microsoft.com/dotnet/framework/samples:dotnetapp`
* `aspnetapp`
  * `docker pull mcr.microsoft.com/dotnet/framework/samples:aspnetapp`
* `wcfservice`
  * `docker pull mcr.microsoft.com/dotnet/framework/samples:wcfservice`
* `wcfclient`
  * `docker pull mcr.microsoft.com/dotnet/framework/samples:wcfclient`
^else
:* `4.8`
  * `docker pull mcr.microsoft.com/dotnet/framework/{{SHORT_REPO}}:4.8`
{{if SHORT_REPO != "wcf"
:* `3.5`
  * `docker pull mcr.microsoft.com/dotnet/framework/{{SHORT_REPO}}:3.5`
}}}}}}
# About {{if IS_PRODUCT_FAMILY:.NET Framework^else:This Image}}

{{InsertTemplate(join(filter(["About", SHORT_REPO, "md"], len), "."))}}
Watch [dotnet/announcements](https://github.com/dotnet/announcements/labels/Docker) for Docker-related .NET announcements.

# How to Use the Image{{if IS_PRODUCT_FAMILY:s}}

The [.NET Framework Docker samples](https://github.com/microsoft/dotnet-framework-docker/blob/master/samples/README.md) show various ways to use .NET Framework and Docker together.

{{InsertTemplate(join(filter(["Use", SHORT_REPO, "md"], len), "."))}}
# Related Repos
{{if !IS_PRODUCT_FAMILY:
.NET Framework:

{{if SHORT_REPO != "sdk"
    :* [dotnet/framework/sdk](https://hub.docker.com/_/microsoft-dotnet-framework-sdk/): .NET Framework SDK
}}{{if SHORT_REPO != "aspnet"
    :* [dotnet/framework/aspnet](https://hub.docker.com/_/microsoft-dotnet-framework-aspnet/): ASP.NET Web Forms and MVC
}}{{if SHORT_REPO != "runtime"
    :* [dotnet/framework/runtime](https://hub.docker.com/_/microsoft-dotnet-framework-runtime/): .NET Framework Runtime
}}{{if SHORT_REPO != "wcf"
    :* [dotnet/framework/wcf](https://hub.docker.com/_/microsoft-dotnet-framework-wcf/): Windows Communication Foundation (WCF)
}}{{if SHORT_REPO != "samples"
    :* [dotnet/framework/samples](https://hub.docker.com/_/microsoft-dotnet-framework-samples/): .NET Framework, ASP.NET and WCF Samples
}}
.NET Core:
}}
* [dotnet/core](https://hub.docker.com/_/microsoft-dotnet-core/): .NET Core
* [dotnet/core-nightly](https://hub.docker.com/_/microsoft-dotnet-core-nightly/): .NET Core (Preview)
* [dotnet/core/samples](https://hub.docker.com/_/microsoft-dotnet-core-samples/): .NET Core Samples

{{if !IS_PRODUCT_FAMILY:# Full Tag Listing

{{if SHORT_REPO != "samples":# Version Compatibility

Version Tag | OS Version | Supported .NET Versions
- | - | -
`4.8` | _all supported_ | 4.8{{if SHORT_REPO = "sdk":*}}
`4.7.2` | _all supported_ | 4.7.2
`4.7.1` | _all supported_ | 4.7.1
`4.7` | _all supported_ | 4.7
`4.6.2` | _all supported_ | 4.6.2{{if SHORT_REPO != "wcf":
`3.5` | `windowsservercore-2009` | 4.8, 3.5, 3.0, 2.5
`3.5` | `windowsservercore-2004` | 4.8, 3.5, 3.0, 2.5
`3.5` | `windowsservercore-1909` | 4.8, 3.5, 3.0, 2.5
`3.5` | `windowsservercore-1903` | 4.8, 3.5, 3.0, 2.5
`3.5` | `windowsservercore-ltsc2019` | 4.7.2, 3.5, 3.0, 2.5
`3.5` | `windowsservercore-ltsc2016` | 4.6.2, 3.5, 3.0, 2.5}}{{if SHORT_REPO = "sdk":

\* The 4.8 SDK is also capable of building 4.8, 4.7.2, 4.7.1, 4.7, and 4.6.2 projects}}

}}}}# Support

See the [.NET Framework Lifecycle FAQ](https://support.microsoft.com/help/17455/lifecycle-faq-net-framework)

# Image Update Policy

* We update the supported .NET Framework images within 12 hours of any updates to their base images (e.g. windows/servercore:1909, windows/servercore:ltsc2019, etc.).
* We publish .NET Framework images as part of releasing new versions of .NET Framework including major/minor and servicing.

# Feedback

{{if SHORT_REPO = "aspnet"
:* [File an ASP.NET Docker issue](https://github.com/microsoft/dotnet-framework-docker/issues)
* [Report an ASP.NET problem](https://developercommunity.visualstudio.com/spaces/61/index.html)
^elif SHORT_REPO = "wcf"
:* [File a WCF Docker issue](https://github.com/microsoft/dotnet-framework-docker/issues)
* [Report a WCF problem](https://developercommunity.visualstudio.com/spaces/61/index.html)
^else
:* [File a .NET Framework Docker issue](https://github.com/microsoft/dotnet-framework-docker/issues)
* [Report a .NET Framework problem](https://developercommunity.visualstudio.com/spaces/61/index.html)
}}* [File a Visual Studio Docker Tools issue](https://github.com/microsoft/dockertools/issues)
* [File a Microsoft Container Registry (MCR) issue](https://github.com/microsoft/containerregistry/issues)
* [Ask on Stack Overflow](https://stackoverflow.com/questions/tagged/.net)
* [Contact Microsoft Support](https://support.microsoft.com/contactus/)

# License

* Legal Notice: [Container License Information](https://aka.ms/mcr/osslegalnotice)

The .NET Framework images use the same license as the [Windows Server Core base image](https://hub.docker.com/_/microsoft-windows-servercore/).

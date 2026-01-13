## About

This image contains the .NET Framework SDK which is comprised of the following parts:

1. .NET Framework Runtime
1. Visual Studio Build Tools
1. NuGet CLI
1. .NET Framework Targeting Packs
1. ASP.NET Web Targets

Use this image for your development process (developing, building and testing applications).

Watch [discussions](https://github.com/microsoft/dotnet-framework-docker/discussions/categories/announcements) for Docker-related .NET announcements.

## Featured tags

* `4.8.1`
  * `docker pull mcr.microsoft.com/dotnet/framework/sdk:4.8.1`
* `4.8`
  * `docker pull mcr.microsoft.com/dotnet/framework/sdk:4.8`

## Related repositories

.NET Framework:

* [dotnet/framework](https://mcr.microsoft.com/catalog?search=dotnet/framework): .NET Framework
* [dotnet/framework/aspnet](https://mcr.microsoft.com/product/dotnet/framework/aspnet/about): ASP.NET Web Forms and MVC
* [dotnet/framework/runtime](https://mcr.microsoft.com/product/dotnet/framework/runtime/about): .NET Framework Runtime
* [dotnet/framework/wcf](https://mcr.microsoft.com/product/dotnet/framework/wcf/about): Windows Communication Foundation (WCF)
* [dotnet/framework/samples](https://mcr.microsoft.com/product/dotnet/framework/samples/about): .NET Framework, ASP.NET and WCF Samples

.NET:

* [dotnet](https://mcr.microsoft.com/catalog?search=dotnet): .NET
* [dotnet/samples](https://mcr.microsoft.com/product/dotnet/samples/about): .NET Samples

## Usage

The [.NET Framework Docker samples](https://github.com/microsoft/dotnet-framework-docker/blob/main/samples/README.md) show various ways to use .NET Framework and Docker together.

### Building .NET Framework Apps with Docker

* [.NET Framework Console Docker Sample](https://github.com/microsoft/dotnet-framework-docker/tree/main/samples/dotnetapp/README.md) - This [sample](https://github.com/microsoft/dotnet-framework-docker/tree/main/samples/dotnetapp/Dockerfile) builds, tests, and runs the sample. It includes and builds multiple projects.
* [ASP.NET Web Forms Docker Sample](https://github.com/microsoft/dotnet-framework-docker/tree/main/samples/aspnetapp/README.md) - This [sample](https://github.com/microsoft/dotnet-framework-docker/tree/main/samples/aspnetapp/Dockerfile) demonstrates using Docker with an ASP.NET Web Forms app.
* [ASP.NET MVC Docker Sample](https://github.com/microsoft/dotnet-framework-docker/tree/main/samples/aspnetmvcapp/README.md) - This [sample](https://github.com/microsoft/dotnet-framework-docker/tree/main/samples/aspnetmvcapp/Dockerfile) demonstrates using Docker with an ASP.NET MVC app.
* [WCF Docker Sample](https://github.com/microsoft/dotnet-framework-docker/tree/main/samples/wcfapp/README.md) - This [sample](https://github.com/microsoft/dotnet-framework-docker/tree/main/samples/wcfapp/) demonstrates using Docker with a WCF app.

### Version Compatibility

If you created your app using an earlier version of .NET Framework, you can generally upgrade it to .NET Framework 4.8+ easily.
Additionally, .NET Framework 4.8 and 4.8.1 can run apps that were built targeting any version of .NET Framework 4.

* [.NET Framework migration guide](https://learn.microsoft.com/en-us/dotnet/framework/migration-guide/)
* [Application compatibility in .NET Framework](https://learn.microsoft.com/dotnet/framework/migration-guide/application-compatibility).
* [Version compatibility in .NET Framework](https://learn.microsoft.com/dotnet/framework/migration-guide/version-compatibility)

## Support

### Lifecycle

* [.NET Framework Lifecycle FAQ](https://support.microsoft.com/help/17455/lifecycle-faq-net-framework)
* [Supported Tags Policy](https://github.com/microsoft/dotnet-framework-docker/blob/main/documentation/supported-tags.md)

### Image Update Policy

* We update the supported .NET Framework images within 12 hours of any updates to their base images (e.g. windows/servercore:ltsc2019, windows/servercore:ltsc2022, etc.).
* We publish .NET Framework images as part of releasing new versions of .NET Framework including major/minor and servicing.

### Feedback

* [File an issue](https://github.com/microsoft/dotnet-framework-docker/issues/new/choose)
* [Contact Microsoft Support](https://support.microsoft.com/contactus/)

## License

* [Microsoft Container Images Legal Notice](https://aka.ms/mcr/osslegalnotice): applies to all [.NET Framework container images](https://mcr.microsoft.com/catalog?search=dotnet/framework)
* [Windows Base Image License](https://learn.microsoft.com/virtualization/windowscontainers/images-eula): applies to all [.NET Framework container images](https://mcr.microsoft.com/catalog?search=dotnet/framework)
* [Visual Studio Tools License](https://visualstudio.microsoft.com/license-terms/mlt031519/): applies to all [.NET Framework SDK container images](https://mcr.microsoft.com/product/dotnet/framework/sdk/about)

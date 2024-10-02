{{ARGS["top-header"]}} Support

{{ARGS["top-header"]}}# Lifecycle

* [.NET Framework Lifecycle FAQ](https://support.microsoft.com/help/17455/lifecycle-faq-net-framework)
* [Supported Tags Policy](https://github.com/microsoft/dotnet-framework-docker/blob/main/documentation/supported-tags.md)

{{ARGS["top-header"]}}# Image Update Policy

* We update the supported .NET Framework images within 12 hours of any updates to their base images (e.g. windows/servercore:ltsc2019, windows/servercore:ltsc2022, etc.).
* We publish .NET Framework images as part of releasing new versions of .NET Framework including major/minor and servicing.

{{ARGS["top-header"]}}# Feedback

* [File an issue](https://github.com/microsoft/dotnet-framework-docker/issues/new/choose)
* [Contact Microsoft Support](https://support.microsoft.com/contactus/)

{{ARGS["top-header"]}} License

* [Microsoft Container Images Legal Notice](https://aka.ms/mcr/osslegalnotice): applies to all [.NET Framework container images]({{InsertTemplate("Url.md", [ "readme-host": ARGS["readme-host"], "repo": "dotnet/framework", "is-product-family": "true" ])}})
* [Windows base image license](https://learn.microsoft.com/en-us/virtualization/windowscontainers/images-eula): applies to all [.NET Framework container images]({{InsertTemplate("Url.md", [ "readme-host": ARGS["readme-host"], "repo": "dotnet/framework", "is-product-family": "true" ])}})
* [Visual Studio Tools License](https://visualstudio.microsoft.com/license-terms/mlt031519/): applies to all [.NET Framework SDK container images]({{InsertTemplate("Url.md", [ "readme-host": ARGS["readme-host"], "repo": "dotnet/framework/sdk" ])}})

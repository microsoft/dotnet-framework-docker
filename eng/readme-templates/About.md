{{

    _ ARGS:
      top-header: The string to use as the top-level header.
      readme-host: Moniker of the site that will host the readmes

}}{{ARGS["top-header"]}} About

{{if ARGS["readme-host"] = "github":> [!CAUTION]
>^else:> **NOTICE**:}} .NET Framework 3.5 images for Windows Server 2025 will no longer be published after May 13th.
> Guidance for installing .NET Framework 3.5 in your own container images can be found [here](https://github.com/microsoft/dotnet-framework-docker/blob/main/documentation/install-netfx3.md#windows-server-core-2022-and-later).
> See the [announcement](https://github.com/dotnet/announcements/issues/349) for more details.

{{InsertTemplate(join(filter(["About", when(IS_PRODUCT_FAMILY, "product-family", SHORT_REPO), "md"], len), "."))}}

Watch [discussions](https://github.com/microsoft/dotnet-framework-docker/discussions/categories/announcements) for Docker-related .NET announcements.

{{
    _ ARGS:
      top-header: The string to use as the top-level header.
}}{{ARGS["top-header"]}} About

{{InsertTemplate(join(filter(["About", when(IS_PRODUCT_FAMILY, "product-family", SHORT_REPO), "md"], len), "."))}}

Watch [discussions](https://github.com/microsoft/dotnet-framework-docker/discussions/categories/announcements) for Docker-related .NET announcements.

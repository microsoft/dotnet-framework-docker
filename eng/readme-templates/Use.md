{{
    _ ARGS:
      top-header: The string to use as the top-level header.
      readme-host: Moniker of the site that will host the readme
}}{{ARGS["top-header"]}} Usage

The [.NET Framework Docker samples](https://github.com/microsoft/dotnet-framework-docker/blob/main/samples/README.md) show various ways to use .NET Framework and Docker together.

{{InsertTemplate(join(filter(["Use", when(IS_PRODUCT_FAMILY, "product-family", SHORT_REPO), "md"], len), "."),
    [ "top-header": ARGS["top-header"], "readme-host": ARGS["readme-host"] ])}}{{
if !IS_PRODUCT_FAMILY && SHORT_REPO != "samples":

{{ARGS["top-header"]}}# Version Compatibility

If you created your app using an earlier version of .NET Framework, you can generally upgrade it to .NET Framework 4.8+ easily.
Additionally, .NET Framework 4.8 and 4.8.1 can run apps that were built targeting any version of .NET Framework 4.

- [.NET Framework migration guide](https://learn.microsoft.com/en-us/dotnet/framework/migration-guide/)
- [Application compatibility in .NET Framework](https://learn.microsoft.com/dotnet/framework/migration-guide/application-compatibility).
- [Version compatibility in .NET Framework](https://learn.microsoft.com/dotnet/framework/migration-guide/version-compatibility)}}

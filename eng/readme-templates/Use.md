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

Version Tag | OS Version | Supported .NET Versions
-- | -- | --
4.8.1 | windowsservercore-ltsc2022 | 4.8.1{{if SHORT_REPO = "sdk":*}}
4.8 | windowsservercore-ltsc2022, windowsservercore-ltsc2019, windowsservercore-ltsc2016 | 4.8{{if SHORT_REPO = "sdk":*}}
4.7.2 | windowsservercore-ltsc2019, windowsservercore-ltsc2016 | 4.7.2
4.7.1 | windowsservercore-ltsc2016 | 4.7.1
4.7 | windowsservercore-ltsc2016 | 4.7
4.6.2 | windowsservercore-ltsc2016 | 4.6.2{{if SHORT_REPO != "wcf":
3.5 | windowsservercore-ltsc2022 | 4.7.2, 3.5, 3.0, 2.5
3.5 | windowsservercore-ltsc2019 | 4.7.2, 3.5, 3.0, 2.5
3.5 | windowsservercore-ltsc2016 | 4.6.2, 3.5, 3.0, 2.5}}{{if SHORT_REPO = "sdk":

\* The 4.8 and 4.8.1 SDKs are also capable of building 4.8.1, 4.8, 4.7.2, 4.7.1, 4.7, and 4.6.2 projects.}}}}

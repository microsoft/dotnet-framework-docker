{{
    _ ARGS:
      top-header: The string to use as the top-level header.
      readme-host: Moniker of the site that will host the readme
}}{{ARGS["top-header"]}} Related Repos
{{if !IS_PRODUCT_FAMILY:
.NET Framework:

{{if SHORT_REPO != "sdk"
    :* [dotnet/framework/sdk]({{InsertTemplate("Url.md", [ "readme-host": ARGS["readme-host"], "repo": "dotnet/framework/sdk" ])}}): .NET Framework SDK
}}{{if SHORT_REPO != "aspnet"
    :* [dotnet/framework/aspnet]({{InsertTemplate("Url.md", [ "readme-host": ARGS["readme-host"], "repo": "dotnet/framework/aspnet" ])}}): ASP.NET Web Forms and MVC
}}{{if SHORT_REPO != "runtime"
    :* [dotnet/framework/runtime]({{InsertTemplate("Url.md", [ "readme-host": ARGS["readme-host"], "repo": "dotnet/framework/runtime" ])}}): .NET Framework Runtime
}}{{if SHORT_REPO != "wcf"
    :* [dotnet/framework/wcf]({{InsertTemplate("Url.md", [ "readme-host": ARGS["readme-host"], "repo": "dotnet/framework/wcf" ])}}): Windows Communication Foundation (WCF)
}}{{if SHORT_REPO != "samples"
    :* [dotnet/framework/samples]({{InsertTemplate("Url.md", [ "readme-host": ARGS["readme-host"], "repo": "dotnet/framework/samples" ])}}): .NET Framework, ASP.NET and WCF Samples
}}
.NET:
}}
* [dotnet]({{InsertTemplate("Url.md", [ "readme-host": ARGS["readme-host"], "repo": "dotnet", "is-product-family": "true" ])}}): .NET
* [dotnet-nightly]({{InsertTemplate("Url.md", [ "readme-host": ARGS["readme-host"], "repo": "dotnet/nightly", "is-product-family": "true" ])}}): .NET (Preview)
* [dotnet/samples]({{InsertTemplate("Url.md", [ "readme-host": ARGS["readme-host"], "repo": "dotnet/samples" ])}}): .NET Samples

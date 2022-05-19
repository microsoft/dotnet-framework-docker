{{
  set headerArgs to [ "top-header": "#" ]
}}{{if !IS_PRODUCT_FAMILY:{{InsertTemplate("FeaturedTags.md", headerArgs)}}
^else:# Featured Repos

* [dotnet/framework/sdk](https://hub.docker.com/_/microsoft-dotnet-framework-sdk/): .NET Framework SDK
* [dotnet/framework/aspnet](https://hub.docker.com/_/microsoft-dotnet-framework-aspnet/): ASP.NET Web Forms and MVC
* [dotnet/framework/runtime](https://hub.docker.com/_/microsoft-dotnet-framework-runtime/): .NET Framework Runtime
* [dotnet/framework/wcf](https://hub.docker.com/_/microsoft-dotnet-framework-wcf/): Windows Communication Foundation (WCF)
* [dotnet/framework/samples](https://hub.docker.com/_/microsoft-dotnet-framework-samples/): .NET Framework, ASP.NET and WCF Samples
}}
{{InsertTemplate("About.md", headerArgs)}}

{{InsertTemplate("Use.md", headerArgs)}}

{{InsertTemplate("RelatedRepos.md", headerArgs)}}
{{if !IS_PRODUCT_FAMILY:
# Full Tag Listing
<!--End of generated tags-->
*Tags not listed in the table above are not supported. See the [Supported Tags Policy](https://github.com/microsoft/dotnet-framework-docker/blob/main/documentation/supported-tags.md).*
}}
{{InsertTemplate("Support.md", headerArgs)}}

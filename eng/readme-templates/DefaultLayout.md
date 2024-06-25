{{
  set commonArgs to [
    "top-header": "#",
    "readme-host": ARGS["readme-host"]
  ]
}}{{if !IS_PRODUCT_FAMILY:{{InsertTemplate("FeaturedTags.md", commonArgs)}}
^else:# Featured Repos

* [dotnet/framework/sdk]({{InsertTemplate("Url.md", [ "readme-host": "github", "repo": "dotnet/framework/sdk" ])}}): .NET Framework SDK
* [dotnet/framework/aspnet]({{InsertTemplate("Url.md", [ "readme-host": "github", "repo": "dotnet/framework/aspnet" ])}}): ASP.NET Web Forms and MVC
* [dotnet/framework/runtime]({{InsertTemplate("Url.md", [ "readme-host": "github", "repo": "dotnet/framework/runtime" ])}}): .NET Framework Runtime
* [dotnet/framework/wcf]({{InsertTemplate("Url.md", [ "readme-host": "github", "repo": "dotnet/framework/wcf" ])}}): Windows Communication Foundation (WCF)
* [dotnet/framework/samples]({{InsertTemplate("Url.md", [ "readme-host": "github", "repo": "dotnet/framework/samples" ])}}): .NET Framework, ASP.NET and WCF Samples
}}
{{InsertTemplate("About.md", commonArgs)}}

{{InsertTemplate("Use.md", commonArgs)}}

{{InsertTemplate("RelatedRepos.md", commonArgs)}}
{{if !IS_PRODUCT_FAMILY:
# Full Tag Listing
{{if ARGS["readme-host"] = "github":<!--End of generated tags-->
*Tags not listed in the table above are not supported. See the [Supported Tags Policy](https://github.com/microsoft/dotnet-framework-docker/blob/main/documentation/supported-tags.md).*
^elif ARGS["readme-host"] = "dockerhub":
View the current tags at the [Microsoft Artifact Registry portal](https://mcr.microsoft.com/product/{{REPO}}/tags) or on [GitHub](https://github.com/microsoft/dotnet-framework-docker/blob/main/README.{{SHORT_REPO}}.md#full-tag-listing).
}}}}
{{InsertTemplate("Support.md", commonArgs)}}

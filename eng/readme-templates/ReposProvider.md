{{
    _ Wrapper template for providing the list of repos to other templates.

    _ ARGS:
      top-header: The string to use as the top-level header.
      readme-host: Moniker of the site that will host the readme
      template: Template to pass the repo lists to ^

    set productRepos to [
        ["dotnet/framework/sdk", ".NET Framework SDK"],
        ["dotnet/framework/aspnet", "ASP.NET Web Forms and MVC"],
        ["dotnet/framework/runtime", ".NET Framework Runtime"],
        ["dotnet/framework/wcf", "Windows Communication Foundation (WCF)"]
    ] ^
    set productFamilyRepos to [
        ["dotnet/framework", ".NET Framework", 1]
    ] ^
    set samplesRepos to [
        ["dotnet/framework/samples", ".NET Framework, ASP.NET and WCF Samples"]
    ] ^
    set dotnetRepos to [
        ["dotnet", ".NET", 1],
        ["dotnet/samples", ".NET Samples"]
    ]

}}{{InsertTemplate(ARGS["template"], [
    "top-header": ARGS["top-header"],
    "readme-host": ARGS["readme-host"],
    "product-repos": productRepos,
    "product-family-repos": productFamilyRepos,
    "samples-repos": samplesRepos,
    "dotnet-repos": dotnetRepos
])}}
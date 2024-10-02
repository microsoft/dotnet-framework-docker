{{
    _ Generates a URL formatted for the site that will host the readme
    ARGS:
      readme-host: Moniker of the site that will host the readme
      repo: Repo path of the URL to be generated
      is-product-family: Indicates whether the URL refers to a product family page ^

    set isProductFamily to ARGS["is-product-family"] ^
    set readmeHost to ARGS["readme-host"] ^
    set repo to ARGS["repo"] ^
    set repoParts to split(repo, "/") ^
    set isFrameworkRepo to match(repoParts[1], "framework")

}}{{
when(readmeHost = "mar",
    when(isProductFamily,
        cat("https://mcr.microsoft.com/catalog?search=", repo),
        cat("https://mcr.microsoft.com/product/", repo, "/about")),
    when(readmeHost = "github",
        cat("https://github.com/",
            when(isFrameworkRepo, "microsoft", "dotnet"),
            "/dotnet",
            when(isFrameworkRepo, "-framework", ""),
            "-docker/blob/main/README.",
            when(isProductFamily, "", cat(join(slice(repoParts, when(isFrameworkRepo, 2, 1)), "-"), ".")),
            "md"),
        when(readmeHost = "dockerhub",
            cat("https://hub.docker.com/r/microsoft/", join(repoParts, "-"), "/"),
            cat("UNKNOWN HOST: ", readmeHost))))}}

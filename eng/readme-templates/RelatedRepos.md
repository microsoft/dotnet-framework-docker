{{
    _ ARGS:
      top-header: The string to use as the top-level header.
      readme-host: Moniker of the site that will host the readmes
      product-repos: List of .NET product repos
      product-family-repos: List of .NET product family repos
      samples-repos: List of .NET samples repos
      dotnet-repos: List of .NET Framework repos ^

    set repos to ARGS["product-repos"] ^
    set productFamilyRepos to ARGS["product-family-repos"] ^
    set samplesRepos to ARGS["samples-repos"] ^
    set dotnetRepos to ARGS["dotnet-repos"] ^

    _ Common functions to help with repo rendering ^

    set isCurrentRepo(repo) to:{{
        set repoNameParts to split(repo[0], "/") ^
        set shortRepo to repoNameParts[len(repoNameParts) - 1] ^
        return shortRepo = SHORT_REPO
    }} ^

    set isNotCurrentRepo(repo) to:{{
        return not(isCurrentRepo(repo))
    }} ^

    _ Create final set of repos to display ^

    set currentRepo to cat(filter(repos, isCurrentRepo)) ^

    set repos to cat(productFamilyRepos, repos, samplesRepos) ^

    _ Exclude this repo from its own readme ^
    set repos to filter(repos, isNotCurrentRepo)

}}{{ARGS["top-header"]}} Related repositories
{{if !IS_PRODUCT_FAMILY:
.NET Framework:
{{InsertTemplate("RepoList.md", [ "readme-host": ARGS["readme-host"], "repos": repos ])}}

.NET:
}}{{InsertTemplate("RepoList.md", [ "readme-host": ARGS["readme-host"], "repos": dotnetRepos ])}}
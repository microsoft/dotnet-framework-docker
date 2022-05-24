{{
    _ ARGS:
      top-header: The string to use as the top-level header.
      readme-host: Moniker of the site that will host the readme
}}{{ARGS["top-header"]}}# Container sample: Run a simple application

You can quickly run a container with a pre-built [.NET Framework Docker image]({{InsertTemplate("Url.md", [ "readme-host": ARGS["readme-host"], "repo": "dotnet/framework/samples" ])}}), based on the [.NET Framework console sample](https://github.com/microsoft/dotnet-framework-docker/blob/main/samples/dotnetapp/README.md).

Type the following command to run a sample console application:

```console
docker run --rm mcr.microsoft.com/dotnet/framework/samples:dotnetapp
```

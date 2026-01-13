{{
    _ ARGS:
      top-header: The string to use as the top-level header.
}}{{ARGS["top-header"]}} Featured tags

{{if SHORT_REPO = "samples"
:* `dotnetapp`
  * `docker pull mcr.microsoft.com/dotnet/framework/samples:dotnetapp`
* `aspnetapp`
  * `docker pull mcr.microsoft.com/dotnet/framework/samples:aspnetapp`
* `wcfservice`
  * `docker pull mcr.microsoft.com/dotnet/framework/samples:wcfservice`
* `wcfclient`
  * `docker pull mcr.microsoft.com/dotnet/framework/samples:wcfclient`^else
:* `4.8.1`
  * `docker pull mcr.microsoft.com/dotnet/framework/{{SHORT_REPO}}:4.8.1`
* `4.8`
  * `docker pull mcr.microsoft.com/dotnet/framework/{{SHORT_REPO}}:4.8`}}

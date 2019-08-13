FROM mcr.microsoft.com/dotnet/framework/sdk:4.8 AS build
WORKDIR /app
COPY WcfClient/. .
RUN msbuild /p:Configuration=Release

FROM mcr.microsoft.com/dotnet/framework/runtime:4.8 AS runtime
WORKDIR /app
COPY --from=build /app/bin/Release .
ENTRYPOINT WcfClient.exe

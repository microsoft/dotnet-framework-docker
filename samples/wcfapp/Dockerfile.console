FROM mcr.microsoft.com/dotnet/framework/sdk:4.8 AS build
WORKDIR /app
COPY WcfServiceConsoleApp/. .
RUN msbuild /p:Configuration=Release

FROM mcr.microsoft.com/dotnet/framework/runtime:4.8 AS runtime
EXPOSE 80
EXPOSE 808
WORKDIR /app
COPY --from=build /app/bin/Release .
ENTRYPOINT WcfServiceConsoleApp.exe

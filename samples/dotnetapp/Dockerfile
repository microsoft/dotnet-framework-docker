FROM mcr.microsoft.com/dotnet/framework/sdk:4.8.1 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY NuGet.config *.csproj ./
RUN dotnet restore

# copy and build everything else
COPY . .
WORKDIR /app
RUN dotnet publish -c Release -o out --no-restore


FROM mcr.microsoft.com/dotnet/framework/runtime:4.8.1 AS runtime
WORKDIR /app
COPY --from=build /app/out ./
ENTRYPOINT ["dotnetapp.exe"]

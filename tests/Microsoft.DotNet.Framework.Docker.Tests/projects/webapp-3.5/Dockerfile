ARG BASE_BUILD_IMAGE

FROM $BASE_BUILD_IMAGE

WORKDIR /app
COPY . ./

RUN msbuild SimpleWebApplication.csproj /verbosity:quiet /p:Configuration=Release

ENTRYPOINT dir ./bin/SimpleWebApplication.dll

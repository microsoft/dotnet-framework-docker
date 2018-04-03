ARG BASE_BUILD_IMAGE

FROM $BASE_BUILD_IMAGE

WORKDIR /app
COPY . ./

RUN msbuild SimpleWebApplication.csproj /verbosity:quiet /p:Configuration=Release

RUN dir ./bin/SimpleWebApplication.dll

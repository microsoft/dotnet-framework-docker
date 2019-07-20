ARG BASE_BUILD_IMAGE
ARG BASE_RUNTIME_IMAGE

FROM $BASE_BUILD_IMAGE AS build
WORKDIR /app
COPY . .
RUN msbuild /p:Configuration=Release

FROM $BASE_RUNTIME_IMAGE AS runtime
COPY ./EntryPoint.ps1 .

WORKDIR /inetpub/wwwroot
COPY --from=build /app/ .

ENTRYPOINT ["powershell", "/EntryPoint.ps1"]

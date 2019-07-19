ARG BASE_BUILD_IMAGE
ARG BASE_RUNTIME_IMAGE

FROM $BASE_BUILD_IMAGE AS build
WORKDIR /app
COPY . .
RUN msbuild /p:Configuration=Release

FROM $BASE_RUNTIME_IMAGE AS runtime
WORKDIR /inetpub/wwroot
COPY --from=build /app/ .
COPY ./EntryPoint.ps1 .

RUN dir /inetpub/wwroot

ENTRYPOINT ["powershell", "c:\\inetpub\\wwwroot\\EntryPoint.ps1"]

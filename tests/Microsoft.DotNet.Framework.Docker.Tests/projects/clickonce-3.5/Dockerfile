ARG BASE_BUILD_IMAGE

FROM $BASE_BUILD_IMAGE as builder

ARG PUBLISH_DIR

WORKDIR /app
COPY . ./
RUN msbuild.exe clickonce-3.5.csproj /t:Publish /p:Configuration=Release /p:PublishDir="${Env:PUBLISH_DIR}"

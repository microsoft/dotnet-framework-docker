ARG BASE_BUILD_IMAGE

FROM $BASE_BUILD_IMAGE as builder

ARG PUBLISH_DIR

WORKDIR /app
COPY . ./
RUN msbuild.exe clickonce-4.8.csproj /t:Publish /p:Configuration=Release /p:PublishDir="$PUBLISH_DIR"

ARG BASE_BUILD_IMAGE
ARG BASE_RUNTIME_IMAGE


FROM $BASE_BUILD_IMAGE as builder
WORKDIR /app
COPY . ./
RUN ["msbuild.exe", "dotnetapp-3.5.csproj", "/p:Configuration=Release"]


FROM $BASE_RUNTIME_IMAGE
WORKDIR /app
COPY --from=builder /app/bin/Release .

ENTRYPOINT ["dotnetapp-3.5.exe"]

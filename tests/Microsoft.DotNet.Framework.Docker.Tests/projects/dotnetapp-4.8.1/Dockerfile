ARG BASE_BUILD_IMAGE
ARG BASE_RUNTIME_IMAGE


FROM $BASE_BUILD_IMAGE as builder
WORKDIR /app
COPY . ./
RUN nuget restore dotnetapp-4.8.1.sln
RUN dotnet build -c Release


FROM $BASE_RUNTIME_IMAGE
WORKDIR /app
COPY --from=builder /app/bin/Release/net481 .

ENTRYPOINT ["dotnetapp-4.8.1.exe"]

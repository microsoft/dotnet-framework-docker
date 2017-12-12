ARG BASE_BUILD_IMAGE
ARG BASE_RUNTIME_IMAGE


FROM $BASE_BUILD_IMAGE as builder
WORKDIR /app
COPY . ./
RUN dotnet restore
RUN dotnet build -c Release


FROM $BASE_RUNTIME_IMAGE
WORKDIR /app
COPY --from=builder /app/bin/Release/net462 .

ENTRYPOINT ["dotnetapp-4.6.2.exe"]

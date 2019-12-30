ARG BASE_BUILD_IMAGE
ARG BASE_RUNTIME_IMAGE


FROM $BASE_BUILD_IMAGE as builder

RUN Write-Host "Verified PowerShell is active"

WORKDIR /app
COPY . ./
RUN dotnet restore
RUN dotnet build -c Release


FROM $BASE_RUNTIME_IMAGE
WORKDIR /app
COPY --from=builder /app/bin/Release/net48 .

ENTRYPOINT ["dotnetapp-4.8.exe"]

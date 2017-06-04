FROM microsoft/windowsservercore

SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

# Install .NET Core
ENV DOTNET_DOWNLOAD_URL https://go.microsoft.com/fwlink/?linkid=851040

RUN Invoke-WebRequest $Env:DOTNET_DOWNLOAD_URL -OutFile dotnetfx47.exe; \
    ./dotnetfx47 /q /norestart; \
    wait-process dotnetfx47; \
    Remove-Item -Force dotnetfx47.exe

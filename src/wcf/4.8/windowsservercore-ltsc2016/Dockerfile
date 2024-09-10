# escape=`

ARG REPO=mcr.microsoft.com/dotnet/framework/aspnet
FROM $REPO:4.8-20240910-windowsservercore-ltsc2016

# Install Windows components required for WCF service hosted on IIS
RUN Add-WindowsFeature NET-WCF-TCP-Activation45; `
    Add-WindowsFeature NET-WCF-HTTP-Activation45; `
    Add-WindowsFeature Web-WebSockets

# Enable net.tcp protocol for default web site on IIS
RUN &$Env:windir\system32\inetsrv\appcmd set app 'Default Web Site/' /enabledProtocols:"http,net.tcp"
EXPOSE 808

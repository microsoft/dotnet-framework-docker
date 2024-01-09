# escape=`

ARG REPO=mcr.microsoft.com/dotnet/framework/aspnet
FROM $REPO:4.8.1-20240109-windowsservercore-ltsc2022

# Install Windows components required for WCF service hosted on IIS
RUN dism /Online /Quiet /Enable-Feature /All /FeatureName:WCF-HTTP-Activation45 /FeatureName:WCF-TCP-Activation45 /FeatureName:IIS-WebSockets

# Enable net.tcp protocol for default web site on IIS
RUN %windir%\system32\inetsrv\appcmd set app "Default Web Site/" /enabledProtocols:"http,net.tcp"
EXPOSE 808

# escape=`

ARG REPO=mcr.microsoft.com/dotnet/framework/aspnet
FROM $REPO:{{PRODUCT_VERSION}}-{{VARIABLES[cat(PRODUCT_VERSION, "-", OS_VERSION_NUMBER, "-Aspnet-DateStamp")]}}-{{OS_VERSION}}

# Install Windows components required for WCF service hosted on IIS
{{if OS_VERSION_NUMBER = "ltsc2016" || OS_VERSION_NUMBER = "ltsc2019"
:RUN Add-WindowsFeature NET-WCF-TCP-Activation45; `
    Add-WindowsFeature NET-WCF-HTTP-Activation45; `
    Add-WindowsFeature Web-WebSockets
^else
:RUN dism /Online /Quiet /Enable-Feature /All /FeatureName:WCF-HTTP-Activation45 /FeatureName:WCF-TCP-Activation45 /FeatureName:IIS-WebSockets
}}
# Enable net.tcp protocol for default web site on IIS
{{if OS_VERSION_NUMBER = "ltsc2016" || OS_VERSION_NUMBER = "ltsc2019"
:RUN &$Env:windir\system32\inetsrv\appcmd set app 'Default Web Site/' /enabledProtocols:"http,net.tcp"
^else
:RUN %windir%\system32\inetsrv\appcmd set app "Default Web Site/" /enabledProtocols:"http,net.tcp"
}}EXPOSE 808

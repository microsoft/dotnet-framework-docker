FROM microsoft/windowsservercore

ADD source /build
RUN DISM /Online /Add-Package /PackagePath:C:\build\microsoft-windows-netfx3-ondemand-package.cab & del C:\build\microsoft-windows-netfx3-ondemand-package.cab

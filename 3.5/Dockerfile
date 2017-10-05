FROM microsoft/windowsservercore
ADD install /build
RUN DISM /Online /Add-Package /PackagePath:C:\build\microsoft-windows-netfx3-ondemand-package.cab & \
    DISM /Online /Add-Package /PackagePath:C:\build\patch\Windows10.0-KB3213986-x64.cab  & \
    rd /s /q C:\build
RUN set COMPLUS_NGenProtectedProcess_FeatureEnabled=0 & \
    \Windows\Microsoft.NET\Framework64\v4.0.30319\ngen install System.Web & \
    \Windows\Microsoft.NET\Framework64\v4.0.30319\ngen update & \
    \Windows\Microsoft.NET\Framework\v4.0.30319\ngen update & \
    \Windows\Microsoft.NET\Framework64\v2.0.50727\ngen install System.Web & \
    \Windows\Microsoft.NET\Framework64\v2.0.50727\ngen update & \
    \Windows\Microsoft.NET\Framework\v2.0.50727\ngen update

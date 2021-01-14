# escape=`

FROM mcr.microsoft.com/windows/servercore:{{OS_VERSION_NUMBER}}-amd64

ENV {{
if OS_VERSION_NUMBER != "ltsc2019"
:`
    # Enable detection of running in a container
    DOTNET_RUNNING_IN_CONTAINER=true `
    }}COMPLUS_NGenProtectedProcess_FeatureEnabled=0

RUN `
{{if PRODUCT_VERSION = "3.5"
:    # Install .NET Fx 3.5
    curl -fSLo microsoft-windows-netfx3.zip https://dotnetbinaries.blob.core.windows.net/dockerassets/microsoft-windows-netfx3-{{OS_VERSION_NUMBER}}.zip `
    && tar -zxf microsoft-windows-netfx3.zip `
    && del /F /Q microsoft-windows-netfx3.zip `
    && DISM /Online /Quiet /Add-Package /PackagePath:.\microsoft-windows-netfx3-ondemand-package~31bf3856ad364e35~amd64~~.cab `
    && del microsoft-windows-netfx3-ondemand-package~31bf3856ad364e35~amd64~~.cab `
    && powershell Remove-Item -Force -Recurse ${Env:TEMP}\* `
    `
^elif OS_VERSION_NUMBER = "ltsc2019" && PRODUCT_VERSION = "4.8"
:    # Install .NET Fx 4.8
    curl -fSLo dotnet-framework-installer.exe https://download.visualstudio.microsoft.com/download/pr/7afca223-55d2-470a-8edc-6a1739ae3252/abd170b4b0ec15ad0222a809b761a036/ndp48-x86-x64-allos-enu.exe `
    && .\dotnet-framework-installer.exe /q `
    && del .\dotnet-framework-installer.exe `
    && powershell Remove-Item -Force -Recurse ${Env:TEMP}\* `
    `
}}{{if VARIABLES[cat("kb|", OS_VERSION_NUMBER)] = undefined)
:    # Apply latest patch
    {{if PRODUCT_VERSION = "3.5" || (OS_VERSION_NUMBER = "ltsc2019" && PRODUCT_VERSION = "4.8"):&& }}curl -fSLo patch.msu {{if OS_VERSION_NUMBER = "ltsc2019" && PRODUCT_VERSION != "4.8":{{VARIABLES["lcu|ltsc2019|3.5-4.7.2"]}}^else:{{VARIABLES[cat("lcu|", OS_VERSION_NUMBER)]}}}} `
    && mkdir patch `
    && expand patch.msu patch -F:* `
    && del /F /Q patch.msu `
    && DISM /Online /Quiet /Add-Package /PackagePath:C:\patch\windows10.0-{{if OS_VERSION_NUMBER = "ltsc2019" && PRODUCT_VERSION != "4.8":{{VARIABLES["kb|ltsc2019|3.5-4.7.2"]}}^else:{{VARIABLES[cat("kb|", OS_VERSION_NUMBER)]}}}}-x64{{if OS_VERSION_NUMBER != "ltsc2019" || PRODUCT_VERSION = "4.8":-ndp48}}.cab `
    && rmdir /S /Q patch `
    `
}}{{if OS_VERSION_NUMBER = "ltsc2019" && PRODUCT_VERSION = "3.5"
:    # ngen .NET Fx
    && \Windows\Microsoft.NET\Framework64\v2.0.50727\ngen uninstall "Microsoft.Tpm.Commands, Version=10.0.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=amd64" `
    && \Windows\Microsoft.NET\Framework64\v2.0.50727\ngen update `
    && \Windows\Microsoft.NET\Framework\v2.0.50727\ngen update^
else
:{{  if OS_VERSION_NUMBER = "ltsc2019" || OS_VERSION_NUMBER = "1909"
:    # ngen .NET Fx
    && \Windows\Microsoft.NET\Framework64\v4.0.30319\ngen uninstall "Microsoft.Tpm.Commands, Version=10.0.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=amd64" `
^    else
:    # Ngen top of assembly graph to optimize a set of frequently used assemblies
    && \Windows\Microsoft.NET\Framework64\v4.0.30319\ngen install "Microsoft.PowerShell.Utility.Activities, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" `
    # To optimize 32-bit assemblies, uncomment the next line
    # && \Windows\Microsoft.NET\Framework\v4.0.30319\ngen install "Microsoft.PowerShell.Utility.Activities, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" `
}}    && \Windows\Microsoft.NET\Framework64\v4.0.30319\ngen update `
    && \Windows\Microsoft.NET\Framework\v4.0.30319\ngen update}}

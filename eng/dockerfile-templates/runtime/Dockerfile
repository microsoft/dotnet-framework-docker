{{
    set isServer2025 to OS_VERSION_NUMBER = "ltsc2025" ^
    set isServer2022 to OS_VERSION_NUMBER = "ltsc2022" ^
    set isServer2019 to OS_VERSION_NUMBER = "ltsc2019" ^
    set isServer2016 to OS_VERSION_NUMBER = "ltsc2016" ^
    set isNet35 to PRODUCT_VERSION = "3.5" ^
    set isNet462 to PRODUCT_VERSION = "4.6.2" ^
    set isNet472 to PRODUCT_VERSION = "4.7.2" ^
    set isNet48 to PRODUCT_VERSION = "4.8" ^
    set isNet481 to PRODUCT_VERSION = "4.8.1" ^

    set kbExists to VARIABLES[cat("kb|", OS_VERSION_NUMBER, "|", PRODUCT_VERSION)] != void ^
    set is48SecurityRelease to (VARIABLES[cat("4.8-is-security-release|", OS_VERSION_NUMBER)] = "true") ^

    set apply35Patch to kbExists
        && isNet35
        && !isServer2019
        && !isServer2025 ^

    set applyPatch to kbExists
        && !(isServer2019 && isNet472)
        && !(
                (isServer2019 && isNet472) ||
                (
                    (
                        (isNet48 && !isServer2019) ||
                        (isNet35 && !isServer2019)
                    ) &&
                    is48SecurityRelease
                )
            ) ^

    set installNet35UsingWU to isServer2025 ^

    set winver to when(
        isServer2016 || isServer2019 || isServer2022,
        "windows10.0",
        "windows11.0")

}}# escape=`

FROM mcr.microsoft.com/windows/servercore:{{OS_VERSION_NUMBER}}-amd64

ENV {{
if OS_VERSION_NUMBER != "ltsc2019" || PRODUCT_VERSION = "4.8" || PRODUCT_VERSION = "4.8.1"
:`
    # Enable detection of running in a container
    }}{{ if OS_VERSION_NUMBER != "ltsc2019"
:DOTNET_RUNNING_IN_CONTAINER=true `
    }}{{ if PRODUCT_VERSION = "4.8" || PRODUCT_VERSION = "4.8.1":COMPLUS_RUNNING_IN_CONTAINER=1 `
    }}COMPLUS_NGenProtectedProcess_FeatureEnabled=0

RUN `
{{if isNet35 && installNet35UsingWU
:    # Install .NET Fx 3.5
    dism /Online /Add-Capability /CapabilityName:NetFx3 `
    `
^elif PRODUCT_VERSION = "3.5"
:    # Install .NET Fx 3.5
    curl -fSLo microsoft-windows-netfx3.zip https://dotnetbinaries.blob.core.windows.net/dockerassets/microsoft-windows-netfx3-{{OS_VERSION_NUMBER}}.zip `
    && tar -zxf microsoft-windows-netfx3.zip `
    && del /F /Q microsoft-windows-netfx3.zip `
    && dism /Online /Quiet /Add-Package /PackagePath:.\microsoft-windows-netfx3-ondemand-package~31bf3856ad364e35~amd64~~.cab `
    && del microsoft-windows-netfx3-ondemand-package~31bf3856ad364e35~amd64~~.cab `
    && powershell Remove-Item -Force -Recurse ${Env:TEMP}\* `
    `
^elif (OS_VERSION_NUMBER = "ltsc2019" && PRODUCT_VERSION = "4.8") || (!isServer2025 && PRODUCT_VERSION = "4.8.1")
:    # Install .NET Fx {{PRODUCT_VERSION}}
    curl -fSLo dotnet-framework-installer.exe {{VARIABLES[cat(PRODUCT_VERSION, "|url")]}} `
    && .\dotnet-framework-installer.exe /q `
    && del .\dotnet-framework-installer.exe `
    && powershell Remove-Item -Force -Recurse ${Env:TEMP}\* `
    `
}}{{if apply35Patch
:    # Apply latest 3.5 patch
    && curl -fSLo patch.msu {{VARIABLES[cat("lcu|", OS_VERSION_NUMBER, "|", PRODUCT_VERSION)]}} `
    && mkdir patch `
    && expand patch.msu patch -F:* `
    && del /F /Q patch.msu `
    && dism /Online /Quiet /Add-Package /PackagePath:C:\patch\{{winver}}-{{VARIABLES[cat("kb|", OS_VERSION_NUMBER, "|", PRODUCT_VERSION)]}}-x64{{if OS_VERSION_NUMBER = "ltsc2022":-ndp48}}.cab `
    && rmdir /S /Q patch `
    `
}}{{if applyPatch
:    # Apply latest patch
    {{if PRODUCT_VERSION = "3.5" || (OS_VERSION_NUMBER = "ltsc2019" && PRODUCT_VERSION = "4.8") || (OS_VERSION_NUMBER = "ltsc2022" && PRODUCT_VERSION = "4.8.1"):&& }}curl -fSLo patch.msu {{VARIABLES[cat("lcu|", OS_VERSION_NUMBER, "|", when(PRODUCT_VERSION = "3.5", "default", PRODUCT_VERSION))]}} `
    && mkdir patch `
    && expand patch.msu patch -F:* `
    && del /F /Q patch.msu `
    && dism /Online /Quiet /Add-Package /PackagePath:C:\patch\{{winver}}-{{VARIABLES[cat("kb|", OS_VERSION_NUMBER, "|", when(PRODUCT_VERSION = "3.5", "default", PRODUCT_VERSION))]}}-x64{{if OS_VERSION_NUMBER != "ltsc2019" || PRODUCT_VERSION = "4.8":-ndp48}}{{if isServer2025 || isNet481:1}}.cab `
    && rmdir /S /Q patch `
    `
}}{{if OS_VERSION_NUMBER = "ltsc2019" && PRODUCT_VERSION = "3.5"
:    # ngen .NET Fx
    && %windir%\Microsoft.NET\Framework64\v2.0.50727\ngen uninstall "Microsoft.Tpm.Commands, Version=10.0.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=amd64" `
    && %windir%\Microsoft.NET\Framework64\v2.0.50727\ngen update `
    && %windir%\Microsoft.NET\Framework\v2.0.50727\ngen update^
else
:{{  if OS_VERSION_NUMBER = "ltsc2019"
:    # ngen .NET Fx
    {{if PRODUCT_VERSION != "4.7.2":&& }}%windir%\Microsoft.NET\Framework64\v4.0.30319\ngen uninstall "Microsoft.Tpm.Commands, Version=10.0.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=amd64" `^    else
:    # Ngen top of assembly graph to optimize a set of frequently used assemblies
    {{if isNet35 || ((isNet48 && !is48SecurityRelease) || isNet481):&& }}%windir%\Microsoft.NET\Framework64\v4.0.30319\ngen install "Microsoft.PowerShell.Utility.Activities, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" `
    # To optimize 32-bit assemblies, uncomment the next line
    # && %windir%\Microsoft.NET\Framework\v4.0.30319\ngen install "Microsoft.PowerShell.Utility.Activities, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" `}}
    && %windir%\Microsoft.NET\Framework64\v4.0.30319\ngen update `
    && %windir%\Microsoft.NET\Framework\v4.0.30319\ngen update}}

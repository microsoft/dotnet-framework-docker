# How to install .NET Framework 3.5 on Windows Server Core container images

## Windows Server Core 2022 and later

Windows Server Core 2022 and later container images can install [.NET Framework 3.5](https://learn.microsoft.com/en-us/windows-hardware/manufacture/desktop/features-on-demand-non-language-fod?view=windows-11#-net-framework) as a [Feature on Demand](https://learn.microsoft.com/en-us/windows-hardware/manufacture/desktop/features-on-demand-v2--capabilities?view=windows-11).

```Dockerfile
# escape=`
FROM mcr.microsoft.com/windows/servercore:ltsc2022-amd64

RUN dism /Online /Add-Capability /CapabilityName:NetFx3
```

## Windows Server Core 2019 and earlier

Windows Server Core 2019 and earlier can use Windows Update to install .NET Framework 3.5. See [Deploy .NET Framework 3.5 by using Deployment Image Servicing and Management (DISM)](https://learn.microsoft.com/windows-hardware/manufacture/desktop/deploy-net-framework-35-by-using-deployment-image-servicing-and-management--dism) for more info.

```Dockerfile
# escape=`
FROM mcr.microsoft.com/windows/servercore:ltsc2019-amd64

RUN `
    # Enable Windows Update service
    sc config wuauserv start= auto `
    # Install .NET Framework 3.5
    && dism /Online /Enable-Feature /FeatureName:NetFx3 /All `
    # Disable Windows Update service
    && sc config wuauserv start= disabled
```

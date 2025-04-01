# How to install .NET Framework 3.5 on Windows Server Core container images

## Windows Server Core 2022 and later

Windows Server Core 2022 and later container images can install [.NET Framework 3.5](https://learn.microsoft.com/windows-hardware/manufacture/desktop/features-on-demand-non-language-fod?view=windows-11#-net-framework) as a [Feature on Demand](https://learn.microsoft.com/windows-hardware/manufacture/desktop/features-on-demand-v2--capabilities?view=windows-11).

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

## Important note about security updates

.NET Framework 3.5 security updates are included in the base Windows Server Core container image. To ensure you have the latest security updates for .NET 3.5, make sure that you re-build your image whenever there is a new version of the Windows Server Core or .NET Framework base image. Information about the latest Windows Server updates can be found here:

- [Windows Server 2025 Update History](https://support.microsoft.com/topic/windows-server-2025-update-history-10f58da7-e57b-4a9d-9c16-9f1dcd72d7d7)
- [Windows Server 2022 Update History](https://support.microsoft.com/topic/windows-server-2022-update-history-e1caa597-00c5-4ab9-9f3e-8212fe80b2ee)
- [Windows Server 2019 Update History](https://support.microsoft.com/topic/windows-10-and-windows-server-2019-update-history-725fc2e1-4443-6831-a5ca-51ff5cbcb059)
- [Windows Server 2016 Update History](https://support.microsoft.com/topic/windows-10-and-windows-server-2016-update-history-4acfbc84-a290-1b54-536a-1c0430e9f3fd)

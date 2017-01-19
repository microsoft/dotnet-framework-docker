FROM microsoft/windowsservercore

ADD install /build
RUN DISM /Online /Add-Package /PackagePath:C:\build\microsoft-windows-netfx3-ondemand-package.cab & \
	DISM /Online /Add-Package /PackagePath:C:\build\patch\Windows10.0-KB3213986-x64.cab  & \
	rd /s /q C:\build

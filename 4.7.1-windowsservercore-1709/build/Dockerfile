# escape=`

FROM microsoft/dotnet-framework:4.7.1-windowsservercore-1709

SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

# Install Git
RUN Invoke-WebRequest -UseBasicParsing https://github.com/git-for-windows/git/releases/download/v2.14.3.windows.1/MinGit-2.14.3-64-bit.zip -outfile git.zip; `
    Expand-Archive git.zip -DestinationPath $Env:ProgramFiles\git; `
    Remove-Item -Force git.zip; `
    setx /M PATH $($Env:PATH + ';' + $Env:ProgramFiles + '\git')

# Install NuGet CLI
RUN New-Item -Type Directory $Env:ProgramFiles\NuGet; `
    Invoke-WebRequest -UseBasicParsing https://dist.nuget.org/win-x86-commandline/v4.3.0/nuget.exe -OutFile $Env:ProgramFiles\NuGet\nuget.exe; `
    setx /M PATH $($Env:PATH + ';' + $Env:ProgramFiles + '\NuGet')

# Install Targeting Pack
RUN Invoke-WebRequest https://download.microsoft.com/download/9/0/1/901B684B-659E-4CBD-BEC8-B3F06967C2E7/NDP471-DevPack-ENU.exe -OutFile NDP462-DevPack-KB3151934-ENU.exe; `
    Start-Process NDP462-DevPack-KB3151934-ENU.exe -ArgumentList '/install', '/quiet' -NoNewWindow -Wait; `
    Remove-Item -Force NDP462-DevPack-KB3151934-ENU.exe

# Install VS Build Tools
RUN Invoke-WebRequest https://download.visualstudio.microsoft.com/download/pr/100196686/e64d79b40219aea618ce2fe10ebd5f0d/vs_BuildTools.exe -OutFile vs_BuildTools.exe; `
    Start-Process vs_BuildTools.exe -ArgumentList '--add', 'Microsoft.VisualStudio.Workload.MSBuildTools', '--passive' , '--norestart' -NoNewWindow -Wait; `
    Remove-Item -Force vs_buildtools.exe; `
    setx /M PATH $($Env:PATH + ';' + ${Env:ProgramFiles(x86)} +'\Microsoft Visual Studio\2017\BuildTools\MSBuild\15.0\Bin')

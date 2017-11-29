FROM microsoft/dotnet-framework:4.7.1-windowsservercore-1709

SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

# Install NuGet CLI
RUN New-Item -Type Directory $Env:ProgramFiles\NuGet; \
    Invoke-WebRequest -UseBasicParsing https://dist.nuget.org/win-x86-commandline/v4.4.1/nuget.exe -OutFile $Env:ProgramFiles\NuGet\nuget.exe

# Install VS Test Agent
RUN Invoke-WebRequest -UseBasicParsing https://download.visualstudio.microsoft.com/download/pr/100232757/8a386d27295953ee79281fd1f1832e2d/vs_TestAgent.exe -OutFile vs_TestAgent.exe; \
    Start-Process vs_TestAgent.exe -ArgumentList '--quiet', '--norestart', '--nocache' -NoNewWindow -Wait; \
    Remove-Item -Force vs_TestAgent.exe; \
# Install VS Build Tools
    Invoke-WebRequest -UseBasicParsing https://download.visualstudio.microsoft.com/download/pr/100196686/e64d79b40219aea618ce2fe10ebd5f0d/vs_BuildTools.exe -OutFile vs_BuildTools.exe; \
    Start-Process vs_BuildTools.exe -ArgumentList '--add', 'Microsoft.VisualStudio.Workload.MSBuildTools', '--quiet', '--norestart', '--nocache' -NoNewWindow -Wait; \
    Remove-Item -Force vs_buildtools.exe; \
    Remove-Item -Force -Recurse \"${Env:ProgramFiles(x86)}/Microsoft Visual Studio/Installer\"; \
    Remove-Item -Force -Recurse ${Env:TEMP}/*

# Set PATH in one layer to keep image size down.
RUN setx /M PATH $(${Env:PATH} \
    + \";${Env:ProgramFiles}/NuGet\" \
    + \";${Env:ProgramFiles(x86)}/Microsoft Visual Studio/2017/TestAgent/Common7/IDE/CommonExtensions/Microsoft/TestWindow\" \
    + \";${Env:ProgramFiles(x86)}/Microsoft Visual Studio/2017/BuildTools/MSBuild/15.0/Bin\")

# Install Targeting Packs
RUN @('4.0', '4.5.2', '4.6.2', '4.7.1') \
    | %{ \
        Invoke-WebRequest -UseBasicParsing https://dotnetbinaries.blob.core.windows.net/referenceassemblies/v${_}.zip -OutFile referenceassemblies.zip; \
        Expand-Archive referenceassemblies.zip -DestinationPath \"${Env:ProgramFiles(x86)}/Reference Assemblies/Microsoft/Framework/.NETFramework\"; \
        Remove-Item -Force referenceassemblies.zip; \
    }

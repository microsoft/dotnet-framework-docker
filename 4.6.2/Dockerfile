FROM microsoft/windowsservercore

RUN set COMPLUS_NGenProtectedProcess_FeatureEnabled=0 & \
    \Windows\Microsoft.NET\Framework64\v4.0.30319\ngen install System.Web & \
    \Windows\Microsoft.NET\Framework64\v4.0.30319\ngen update & \
    \Windows\Microsoft.NET\Framework\v4.0.30319\ngen update

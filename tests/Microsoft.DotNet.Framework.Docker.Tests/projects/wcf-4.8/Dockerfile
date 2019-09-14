ARG BASE_WCF_IMAGE

FROM $BASE_WCF_IMAGE AS runtime

RUN Add-WindowsFeature Web-AppInit -IncludeAllSubFeature

WORKDIR /inetpub/wwwroot
COPY web.config .
COPY Service1.svc .

WORKDIR /inetpub/wwwroot/App_Code
COPY IService1.cs .
COPY Service1.svc.cs .



ARG BASE_ASPNET_IMAGE

FROM $BASE_ASPNET_IMAGE

RUN Add-WindowsFeature Web-AppInit -IncludeAllSubFeature

WORKDIR  "c:\inetpub\wwwroot"
COPY ./web.config .
COPY ./hello-world.aspx .



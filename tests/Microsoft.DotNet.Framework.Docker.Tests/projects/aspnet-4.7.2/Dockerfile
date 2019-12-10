ARG BASE_ASPNET_IMAGE

FROM $BASE_ASPNET_IMAGE

RUN C:\Windows\Microsoft.NET\Framework64\v4.0.30319\ngen.exe display

WORKDIR  "c:\inetpub\wwwroot"
COPY ./web.config .
COPY ./hello-world.aspx .

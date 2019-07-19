ARG BASE_RUNTIME_IMAGE

FROM $BASE_RUNTIME_IMAGE

COPY ./entrypoint.ps1 .

WORKDIR  "c:\inetpub\wwwroot"
COPY ./web.config .
COPY ./hello-world.aspx .

ENTRYPOINT ["powershell", "/Entrypoint.ps1"]

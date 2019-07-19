ARG BASE_RUNTIME_IMAGE

COPY ./entrypoint.bat .

WORKDIR  "c:\inetpub\wwwroot"
COPY ./web.config .
COPY ./hello-world.aspx .

ENTRYPOINT ["powershell", "/Entrypoint.ps1"]

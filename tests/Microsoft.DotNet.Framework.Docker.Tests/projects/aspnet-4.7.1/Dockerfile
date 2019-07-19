ARG IMAGE
FROM ${IMAGE}

ARG VER

WORKDIR  "c:\inetpub\wwwroot"

COPY ./$VER/web.config .
COPY ./entrypoint.bat .
COPY ./hello-world.aspx .

ENTRYPOINT ["powershell", "c:/inetpub/wwwroot/Entrypoint.ps1"]

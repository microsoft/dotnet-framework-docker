ARG BASE_RUNTIME_IMAGE

FROM $BASE_RUNTIME_IMAGE AS runtime
COPY ./EntryPoint.ps1 .

WORKDIR /inetpub/wwwroot
COPY web.config .
COPY Service1.svc .

WORKDIR /inetpub/wwwroot/App_Code
COPY IService1.cs .
COPY Service1.svc.cs .

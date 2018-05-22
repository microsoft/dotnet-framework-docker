ARG BASE_BUILD_IMAGE

FROM $BASE_BUILD_IMAGE

WORKDIR /app
COPY . ./

RUN nuget restore SimpleWebApplication.csproj -SolutionDirectory -NoCache

RUN msbuild SimpleWebApplication.csproj /p:Configuration=Release
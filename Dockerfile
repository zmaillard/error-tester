FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build-env
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0.2-alpine3.10
EXPOSE 80/tcp
WORKDIR /app
COPY --from=build-env /app/out .
COPY --from=build-env /app/newrelic.config .

#Configure NewRelic Agent
RUN apk add curl
ARG NewRelicVersion=8.21.34.0
ARG NewRelicHome=/usr/local/newrelic-netcore20-agent
ENV CORECLR_ENABLE_PROFILING=1 \
  CORECLR_PROFILER={36032161-FFC0-4B61-B559-F6C5D41BAE5A} \
  CORECLR_NEWRELIC_HOME=/usr/local/newrelic-netcore20-agent \
  CORECLR_PROFILER_PATH=/usr/local/newrelic-netcore20-agent/libNewRelicProfiler.so
ARG NewRelicFile=newrelic-netcore20-agent_${NewRelicVersion}_amd64.tar.gz
ARG NewRelicUrl=https://download.newrelic.com/dot_net_agent/previous_releases/$NewRelicVersion/$NewRelicFile
RUN mkdir "${NewRelicHome}" && cd /usr/local && curl -sS "${NewRelicUrl}" | gzip -dc | tar xf -

RUN cp -f /app/newrelic.config "${NewRelicHome}/newrelic.config"

ENTRYPOINT ["dotnet","/app/error-tester.dll"]

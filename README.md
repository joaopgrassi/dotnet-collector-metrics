# dotnet-collector-metrics

A simple console app demonstrating how to export metrics via a OpenTelemetry collector 
using the [Dynatrace exporter](https://github.com/open-telemetry/opentelemetry-collector-contrib/tree/main/exporter/dynatraceexporter).

## Requirements

- .NET SDK 5.x
- Docker/Compose

## Running 

Make sure to edit the `otel-collector-config.yaml` by adding your Dynatrace endpoint and api token. For more information see the export [README](https://github.com/open-telemetry/opentelemetry-collector-contrib/tree/main/exporter/dynatraceexporter).

After modifying the config file, from the root folder:

- Start the Collector container `docker-compose up`
- Start the console app: `dotnet run --project .\src\App.csproj`

The console app generates metrics and waits in intervals for the 
export to take place.

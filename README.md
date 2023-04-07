# Fictional Winner!
A Project dedicated to exploring the usage of mass transit between multiple applications.

## Goals
+ Create a simple Order Management System with its own database
+ Create a simple Shipment Management System with its own database
+ Create a simple Invoice Management System with its own database
+ When an order is Submitted a shipment should be created.
+ When a Shipment is completed an invoice should be created.
+ When an Invoice is paid/completed it should complete the order.


## Key Packages
+ Mass Transit - https://masstransit.io/
+ Swagger - https://docs.microsoft.com/en-us/aspnet/core/tutorials/web-api-help-pages-using-swagger?view=aspnetcore-2.2
+ Serilog - https://github.com/serilog/serilog/wiki
+ Entity Framework Core - https://learn.microsoft.com/en-us/ef/core/
+ AutoMapper - https://automapper.org/


## Required Docker Containers

### MSSQL 2022 Container

```
docker run -it `
    -e "ACCEPT_EULA=Y" `
    -e "SA_PASSWORD=A&VeryComplex123Password" `
    -p 1433:1433 `
    --name sql-server-2022 `
    mcr.microsoft.com/mssql/server:2022-latest
```

### RabbitMq Container - web app is located at http://localhost:15672/

```
  docker run -p 15672:15672 -p 5672:5672 masstransit/rabbitmq
```

### Jaeger Container - web app is located at http://localhost:16686

```
docker run -d --name jaeger `
  -e COLLECTOR_ZIPKIN_HOST_PORT=:9411 `
  -e COLLECTOR_OTLP_ENABLED=true `
  -p 6831:6831/udp `
  -p 6832:6832/udp `
  -p 5778:5778 `
  -p 16686:16686 `
  -p 4317:4317 `
  -p 4318:4318 `
  -p 14250:14250 `
  -p 14268:14268 `
  -p 14269:14269 `
  -p 9411:9411 `
  jaegertracing/all-in-one:1.43
```
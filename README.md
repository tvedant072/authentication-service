# Authentication Service

Authentication microservice built with ASP.NET Core Web API, SQL Server, Entity Framework Core, and Swagger.

## Local Development

```powershell
dotnet restore
dotnet build
dotnet run --project src\AuthenticationService.Api\AuthenticationService.Api.csproj --launch-profile http
```

Swagger UI will be available at:

```text
http://localhost:5052/swagger
```

The health check endpoint will be available at:

```text
http://localhost:5052/health
```

## Architecture Direction

The service will follow a layered flow:

```text
Controller -> Service -> Repository -> Entity Framework Core -> SQL Server
```

Controllers will handle HTTP concerns, services will hold business rules, repositories will isolate persistence, and Entity Framework Core will manage database access and migrations.

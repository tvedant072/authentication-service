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

## Pull Request Checks

Pull requests targeting `main` must pass the following checks before they are
merged:

- **CI** restores and audits NuGet dependencies, verifies formatting, builds
  the solution in Release mode, and runs its tests.
- **CodeQL** analyzes the C# source for security vulnerabilities and coding
  errors.
- **Dependency Review** rejects newly introduced dependencies with known
  vulnerabilities of moderate severity or higher.
- **Gitleaks** detects credentials, tokens, and other secrets committed to the
  repository.

Use the .NET SDK selected by [`global.json`](global.json). From the repository
root, run the locally reproducible checks in the same order as CI:

```powershell
dotnet restore AuthenticationService.sln -p:NuGetAudit=true -p:NuGetAuditMode=all -warnaserror
dotnet format AuthenticationService.sln --verify-no-changes --no-restore
dotnet build AuthenticationService.sln --configuration Release --no-restore --warnaserror
dotnet test AuthenticationService.sln --configuration Release --no-build --no-restore
gitleaks detect --source . --redact
```

[Install Gitleaks](https://github.com/gitleaks/gitleaks#installing) before
running the final command. CodeQL and Dependency Review depend on GitHub's
code-scanning and dependency-graph services, so the standard local .NET
toolchain cannot reproduce those two checks exactly.

Test projects must use a `*.Tests` naming convention and be added to
`AuthenticationService.sln`; otherwise the solution-level test command will
not discover them.

## Architecture Direction

The service will follow a layered flow:

```text
Controller -> Service -> Repository -> Entity Framework Core -> SQL Server
```

Controllers will handle HTTP concerns, services will hold business rules, repositories will isolate persistence, and Entity Framework Core will manage database access and migrations.


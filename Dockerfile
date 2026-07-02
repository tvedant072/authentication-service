# ─── Build Stage ─────────────────────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy solution-level files first so restore is cached as a separate layer.
# Subsequent source changes won't re-run dotnet restore unless .csproj changes.
COPY global.json .
COPY AuthenticationService.sln .
COPY src/AuthenticationService.Api/AuthenticationService.Api.csproj src/AuthenticationService.Api/

RUN dotnet restore AuthenticationService.sln

# Copy remaining source and publish in Release mode
COPY src/ src/
RUN dotnet publish src/AuthenticationService.Api/AuthenticationService.Api.csproj \
    -c Release \
    -o /app/publish \
    --no-restore

# ─── Watch Stage  ───────────────────────────────
# dotnet watch detects file changes and hot-reloads without rebuilding the image.
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS watch
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENV DOTNET_USE_POLLING_FILE_WATCHER=true
ENTRYPOINT ["dotnet", "watch", "run", "--project", "AuthenticationService.Api.csproj", "--non-interactive", "--no-launch-profile"]

# ─── Runtime Stage ───────────────────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# ASP.NET Core 8+ defaults to port 8080 inside containers
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "AuthenticationService.Api.dll"]

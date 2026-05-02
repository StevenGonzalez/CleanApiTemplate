# Clean API Template (.NET 8 Minimal API)

[![CI](https://github.com/StevenGonzalez/dotnet-clean-api-template/actions/workflows/ci.yml/badge.svg)](https://github.com/StevenGonzalez/dotnet-clean-api-template/actions/workflows/ci.yml)
[![CD](https://github.com/StevenGonzalez/dotnet-clean-api-template/actions/workflows/cd.yml/badge.svg)](https://github.com/StevenGonzalez/dotnet-clean-api-template/actions/workflows/cd.yml)

Production-ready template for .NET services with CQRS, EF Core, JWT auth, structured logging, tracing, Docker, and GitHub Actions.

## What Is Included

- Minimal API endpoints with MediatR-based CQRS handlers.
- Clean architecture boundaries: Api, Application, Domain, Infrastructure.
- PostgreSQL persistence with EF Core and checked-in migrations.
- JWT token endpoint for authenticated write operations.
- Serilog request logging and OpenTelemetry metrics/traces.
- Health endpoints for liveness and readiness.
- Dockerfile, docker-compose stack, and local dev script.
- CI workflow for build/test/container validation.
- CD workflow that publishes container images to GHCR on version tags.

## Project Structure

```text
DotnetCleanApiTemplate/
  Api/
    Auth/
    Endpoints/
    Middleware/
  Application/
    Abstractions/
    Items/
    Services/
  Domain/
    Entities/
    Repositories/
  Infrastructure/
    Persistence/
      Migrations/
    Repositories/
  Program.cs
  appsettings.json
  appsettings.Development.json
```

## Quick Start

### Option A: One command stack

```bash
./dev up
```

For Windows PowerShell:

```powershell
./dev.ps1 up
```

This starts:
- API on http://localhost:8080
- PostgreSQL on localhost:5432

### Option B: Run locally without containers

1. Start PostgreSQL.
2. Update connection string in appsettings.Development.json.
3. Run:

```bash
dotnet run --project ./DotnetCleanApiTemplate/DotnetCleanApiTemplate.csproj
```

## Endpoints

- GET /health
- GET /health/ready
- POST /auth/token
- GET /items
- GET /items/{id}
- POST /items (JWT required)

## Authentication Flow

Default demo credentials:
- username: demo
- password: demo123

Get token:

```bash
curl -X POST http://localhost:8080/auth/token \
  -H "Content-Type: application/json" \
  -d '{"username":"demo","password":"demo123"}'
```

Create item:

```bash
curl -X POST http://localhost:8080/items \
  -H "Authorization: Bearer <access_token>" \
  -H "Content-Type: application/json" \
  -d '{"name":"Release checklist"}'
```

## Migrations

Initial migration is included under Infrastructure/Persistence/Migrations.

Create a new migration:

```bash
dotnet ef migrations add <MigrationName> \
  --project ./DotnetCleanApiTemplate/DotnetCleanApiTemplate.csproj \
  --startup-project ./DotnetCleanApiTemplate/DotnetCleanApiTemplate.csproj \
  --output-dir Infrastructure/Persistence/Migrations
```

Apply migrations:

```bash
./dev migrate
```

## Observability

- Serilog writes structured logs to console.
- OpenTelemetry captures ASP.NET Core and runtime telemetry.
- Telemetry is exported to console by default for a zero-setup developer experience.

## CI/CD

### CI

Workflow: .github/workflows/ci.yml

Runs on push and pull request to main:
- dotnet restore
- dotnet build (Release)
- dotnet test (Release)
- docker build validation

### CD

Workflow: .github/workflows/cd.yml

Runs on tag pushes matching v* and publishes image:
- ghcr.io/<owner>/<repo>/api

## Local Dev Commands

```bash
./dev up       # Start stack
./dev logs     # Tail API logs
./dev test     # Run tests
./dev migrate  # Apply EF migrations
./dev down     # Stop stack
```
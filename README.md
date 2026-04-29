# Clean API Template (.NET 8 Minimal API)

[![CI](https://github.com/your-org/clean-api-template/actions/workflows/ci.yml/badge.svg)](https://github.com/your-org/clean-api-template/actions/workflows/ci.yml)

Minimal API starter focused on clarity, maintainability, and practical defaults.

## Why This Template Exists

Most templates miss the balance between speed and maintainability.
This one keeps useful boundaries without heavy boilerplate.

## Philosophy

- Clarity over cleverness.
- Maintainability over premature architecture.
- No overengineering.
- AI-friendly code shape: explicit names and small files.
- Keep business logic out of HTTP endpoints.

## Runtime and Support

- Current target: .NET 8 (LTS).
- Future direction: straightforward migration path to .NET 10.

## Project Structure

```text
CleanApiTemplate/
  Api/
    Endpoints/
      ItemEndpoints.cs
    Middleware/
      ErrorHandlingMiddleware.cs
  Application/
    Abstractions/
      IItemService.cs
    Services/
      ItemService.cs
    DependencyInjection.cs
  Domain/
    Entities/
      Item.cs
    Repositories/
      IItemRepository.cs
  Infrastructure/
    Repositories/
      InMemoryItemRepository.cs
    DependencyInjection.cs
  Program.cs
  appsettings.json
  appsettings.Development.json
```

## Layer Responsibilities

- Api: HTTP routing, middleware, and response shaping.
- Application: use-case orchestration and coordination.
- Domain: core types and contracts with no infrastructure dependencies.
- Infrastructure: technical implementations (in-memory, database, queues, external services).

## End-to-End Example Flow

1. API endpoint receives request in ItemEndpoints.
2. Endpoint calls IItemService in Application.
3. ItemService depends on IItemRepository from Domain.
4. InMemoryItemRepository in Infrastructure fulfills that contract.
5. Result is returned to the client as JSON.

This demonstrates the intended dependency direction:

Api -> Application -> Domain <- Infrastructure

## Included Endpoints

- GET /health
- GET /items
- GET /items/{id}

## Logging and Error Handling

- Structured JSON logging to console.
- Global error middleware with consistent JSON error shape.
- Trace id included in error payload for quick log correlation.
- Exception detail shown in development only.

## Optional Swagger Support

Swagger support is included as commented examples and disabled by default.
This keeps the template minimal and future-aware while Microsoft evolves built-in OpenAPI support for minimal APIs.

How to enable it:

1. Add the package: Swashbuckle.AspNetCore.
2. Uncomment extension methods in Api/SwaggerExtensions.cs.
3. Uncomment AddOptionalSwagger in Program.cs.
4. Uncomment UseOptionalSwagger in Program.cs inside development-only startup.

## Running the Project

```bash
dotnet run --project .\CleanApiTemplate\CleanApiTemplate.csproj
```

Then open:

- http://localhost:5000/health
- http://localhost:5000/items

## Continuous Integration

This template includes a minimal GitHub Actions pipeline in .github/workflows/ci.yml.
It runs on pushes to main and pull requests targeting main.
The pipeline restores dependencies, builds in Release mode, and runs tests in Release mode.

Update the badge URL at the top of this README to match your repository owner and name.

## How to Extend This Template

Recommended order:

1. Add a domain entity and repository contract.
2. Add an application service for the use case.
3. Add an infrastructure implementation for the contract.
4. Add API endpoints that use the application service.

Examples:

- Replace in-memory repository with EF Core and keep IItemRepository unchanged.
- Add validation at the API boundary and keep business rules in Application/Domain.
- Add another feature by repeating the same entity -> service -> repository -> endpoint pattern.

## What This Template Is Not

- Not a full enterprise platform.
- Not tied to a specific database or cloud.
- Not preloaded with every cross-cutting concern.

Start here, then add only what your service actually needs.
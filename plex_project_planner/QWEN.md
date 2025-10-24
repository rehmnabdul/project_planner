# ClickUp Clone Application Development Guidelines

Auto-generated from all feature plans. Last updated: 2025-10-25

## Active Technologies

- ASP.NET 9.0 (Core)
- .NET Aspire for cloud-ready application composition and observability
- Blazor for UI components
- C# language features (latest version)
- Entity Framework Core for data access
- Microsoft SQL Server for data persistence
- Domain-Driven Design architectural patterns

## Project Structure

```text
src/
├── Core/
│   ├── Entities/
│   ├── ValueObjects/
│   ├── DomainServices/
│   ├── Interfaces/
│   └── Exceptions/
├── Application/
│   ├── Commands/
│   ├── Queries/
│   ├── DTOs/
│   ├── Mappings/
│   └── Interfaces/
├── Infrastructure/
│   ├── Repositories/
│   ├── Persistence/
│   ├── Services/
│   └── Configuration/
└── Web/
    ├── Controllers/
    ├── Pages/
    ├── Components/
    ├── wwwroot/
    ├── Services/
    └── wwwroot/css/

tests/
├── Unit/
├── Integration/
└── E2E/

apphost/
├── Program.cs
└── [AppHost].AppHost.csproj

specs/001-clickup-clone-app/
├── spec.md
├── plan.md
├── research.md
├── data-model.md
├── quickstart.md
├── contracts/
│   └── openapi.yaml
└── checklists/
    └── requirements.md
```

## Commands

- `dotnet run` - Run the application
- `dotnet test` - Run all tests
- `dotnet build` - Build the application
- `dotnet publish` - Publish the application
- `dotnet watch run` - Run with file watching
- `dotnet ef migrations add [Name]` - Add a new database migration
- `dotnet ef database update` - Apply pending database migrations

## Code Style

- Follow .NET coding conventions (https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)
- Use static analysis tools (Roslyn analyzers, StyleCop)
- XML documentation for all public APIs
- Follow SOLID principles and maintain clean, testable code
- Use async/await for I/O operations

## Recent Changes

- Feature 001-clickup-clone-app: Implementation of ClickUp clone with project management, task tracking, collaboration features, and dashboard capabilities using DDD, .NET Aspire, and Blazor

<!-- MANUAL ADDITIONS START -->
<!-- MANUAL ADDITIONS END -->
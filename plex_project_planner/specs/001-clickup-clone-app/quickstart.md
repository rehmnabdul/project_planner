# Quickstart Guide: ClickUp Clone Application

## Prerequisites

- .NET 9.0 SDK
- Microsoft SQL Server (or SQL Server Express)
- Visual Studio 2022 or Visual Studio Code with C# extensions
- Node.js (for any additional build tools, if needed)

## Initial Setup

1. Clone the repository:
   ```bash
   git clone [repository-url]
   cd [repository-name]
   ```

2. Set up the database:
   - Ensure SQL Server is running
   - Update connection string in `appsettings.json` if needed
   - Run migrations to create database schema:
     ```bash
     dotnet ef database update --project src/Infrastructure --startup-project src/Web
     ```

3. Install dependencies:
   ```bash
   dotnet restore
   ```

4. Build the solution:
   ```bash
   dotnet build
   ```

## Running the Application

### Using .NET Aspire (Recommended)

1. Navigate to the app host directory:
   ```bash
   cd apphost
   ```

2. Run with Aspire:
   ```bash
   dotnet run
   ```

### Using Standard .NET Run

1. Navigate to the Web project:
   ```bash
   cd src/Web
   ```

2. Run the application:
   ```bash
   dotnet run
   ```

The application will be available at `https://localhost:5001` or `http://localhost:5000`.

## Project Structure Overview

```
src/
├── Core/                 # Domain entities, value objects, interfaces
│   ├── Entities/         # Domain entities (User, Project, Task, etc.)
│   ├── ValueObjects/     # Value objects for domain concepts
│   ├── DomainServices/   # Domain-specific services
│   └── Interfaces/       # Domain interfaces
├── Application/          # Application layer logic
│   ├── Commands/         # CQRS commands
│   ├── Queries/          # CQRS queries
│   ├── DTOs/             # Data transfer objects
│   └── Interfaces/       # Application interfaces
├── Infrastructure/       # Infrastructure concerns
│   ├── Repositories/     # Repository implementations
│   ├── Persistence/      # Database context and configurations
│   └── Services/         # Infrastructure services
└── Web/                  # Presentation layer
    ├── Controllers/      # API controllers
    ├── Pages/            # Blazor pages
    ├── Components/       # Blazor components
    ├── Services/         # Client-side services
    └── wwwroot/          # Static assets
```

## Key Technologies Used

- **Architecture**: Domain-Driven Design (DDD) with Clean Architecture
- **Backend**: ASP.NET Core Web API with .NET 9.0
- **Frontend**: Blazor Server and/or Blazor WebAssembly
- **Data Access**: Entity Framework Core with SQL Server
- **Deployment**: .NET Aspire for cloud-ready application composition
- **Testing**: xUnit for unit/integration tests
- **Code Quality**: Roslyn analyzers and StyleCop for static analysis

## Development Workflow

1. Create a new feature branch: `git checkout -b feature/your-feature-name`
2. Follow TDD practices - write tests first
3. Ensure code follows .NET coding conventions
4. Maintain 85%+ test coverage (95%+ for critical paths)
5. Submit a pull request when ready for review

## Testing

1. Run unit tests:
   ```bash
   dotnet test tests/Unit
   ```

2. Run all tests:
   ```bash
   dotnet test
   ```

3. Check test coverage (if configured):
   ```bash
   dotnet test --collect:"XPlat Code Coverage"
   ```

## Common Tasks

### Adding a New Entity

1. Create the entity in `src/Core/Entities/`
2. Create required ValueObjects if needed
3. Add to the appropriate DbContext
4. Create Repository interface in Core, implementation in Infrastructure
5. Create API endpoints in Web layer
6. Create Blazor components as needed

### Running Migrations

1. Add a new migration:
   ```bash
   dotnet ef migrations add "MigrationName" --project src/Infrastructure --startup-project src/Web
   ```

2. Update database:
   ```bash
   dotnet ef database update --project src/Infrastructure --startup-project src/Web
   ```

### Environment Configuration

Configuration files can be found in each project:
- `appsettings.json` - Base configuration
- `appsettings.Development.json` - Development overrides
- `appsettings.Production.json` - Production overrides

## Architecture Notes

This application follows Domain-Driven Design principles:
- Domain logic is isolated in the Core layer
- Infrastructure concerns are in the Infrastructure layer
- Application use cases are in the Application layer
- Presentation is handled in the Web layer
- The architecture supports both server-side Blazor and WebAssembly patterns
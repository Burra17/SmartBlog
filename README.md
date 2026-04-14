# SmartBlog API

A RESTful blog API built with ASP.NET Core following Clean Architecture principles. The project implements the CQRS pattern with MediatR, request validation, soft-delete with EF Core global query filters, and a logging pipeline behavior.

## Tech Stack

- **ASP.NET Core** — Web API framework
- **Entity Framework Core** — ORM with SQL Server
- **MediatR** — CQRS and pipeline behaviors
- **FluentValidation** — Request validation
- **AutoMapper** — Entity-to-DTO mapping
- **OpenAI** — AI-powered blog post summaries
- **xUnit** — Unit testing

## Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (10.0+)
- SQL Server (local or remote)

### Clone

```bash
git clone https://github.com/Burra17/SmartBlog.git
cd SmartBlog
```

### Configure

Update the connection string in `src/SmartBlog.Api/appsettings.json` to point to your SQL Server instance.

### Run

```bash
dotnet ef database update --project src/SmartBlog.Infrastructure --startup-project src/SmartBlog.Api
dotnet run --project src/SmartBlog.Api
```

## Project Structure

```
src/
├── SmartBlog.Domain/            # Entities and domain logic
│   └── Entities/
│       └── BlogPost.cs
│
├── SmartBlog.Application/       # Use cases, DTOs, interfaces
│   ├── Behaviors/
│   │   ├── LoggingPipelineBehavior.cs
│   │   └── ValidationBehavior.cs
│   ├── DTOs/
│   ├── Features/
│   │   ├── Commands/
│   │   │   ├── CreateBlogPost/
│   │   │   └── DeleteBlogPost/
│   │   └── Queries/
│   │       └── GetAllBlogPosts/
│   ├── Interfaces/
│   └── Mappings/
│
├── SmartBlog.Infrastructure/    # EF Core, repositories, external services
│   ├── Persistence/
│   │   └── AppDbContext.cs
│   ├── Repositories/
│   └── Services/
│
└── SmartBlog.Api/               # Controllers, middleware, DI setup
    ├── Controllers/
    └── Middleware/

tests/
└── SmartBlog.Application.Tests/ # Unit tests
```

## Architecture

The project follows **Clean Architecture** with four layers:

- **Domain** — Core entities with no external dependencies
- **Application** — Business logic, CQRS handlers, validation, and interfaces
- **Infrastructure** — Data access, EF Core configuration, and external service implementations
- **Api** — HTTP layer, controllers, and middleware

### Key Features

- **CQRS** — Commands (create, delete) and queries (get all) are separated via MediatR
- **Pipeline Behaviors** — Logging and validation run automatically on every request through the MediatR pipeline
- **Soft Delete** — `DeleteBlogPostCommand` sets `IsDeleted = true` instead of removing the record. A global EF Core query filter ensures deleted posts are excluded from all queries
- **Error Handling** — Centralized `ErrorHandlingMiddleware` catches exceptions and returns consistent error responses

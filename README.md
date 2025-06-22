# AutoTallerManager

A RESTful API for automotive workshop management built with ASP.NET Core following Clean Architecture principles.

## Project Structure

The solution follows a hexagonal (ports and adapters) architecture with the following projects:

### AutoTallerManager.Domain

Core business logic and domain entities:
- `Entities`: Domain models
- `Enums`: Enumeration types
- `Exceptions`: Custom domain exceptions
- `Interfaces`: Core business interfaces

### AutoTallerManager.Application

Application business logic:
- `Common`: Shared components and behaviors
- `DTOs`: Data transfer objects
- `Features`: CQRS handlers (commands/queries)
- `Interfaces`: Application service interfaces
- `Mappings`: AutoMapper profiles
- `Validators`: FluentValidation rules

### AutoTallerManager.Infrastructure

External concerns and implementations:
- `Persistence`: EF Core DbContext and configurations
- `Repositories`: Data access implementations
- `Services`: External service implementations

### AutoTallerManager.Api

API endpoints and configuration:
- `Controllers`: API endpoints
- `Extensions`: Service collection extensions
- `Filters`: Action filters
- `Middleware`: Custom middleware components

## Features

- **Customer and Vehicle Management**
  - CRUD operations
  - One-to-many relationship between customers and vehicles

- **Service Orders**
  - Create and manage service orders
  - Assign mechanics and parts
  - Track order status
  - Generate invoices

- **Inventory Management**
  - Track auto parts and stock levels
  - Automatic stock validation
  - Unique part codes

- **Billing**
  - Calculate service totals
  - Link invoices to service orders

- **Security**
  - JWT authentication
  - Role-based authorization
  - Admin, Mechanic, and Receptionist roles

- **Auditing**
  - Track all important CRUD operations
  - User action logging

## Technical Stack

- **.NET 8.0**
- **ASP.NET Core Web API**
- **Entity Framework Core** with MySQL
- **AutoMapper** for object mapping
- **MediatR** for CQRS
- **FluentValidation** for input validation
- **Serilog** for structured logging
- **Swagger/OpenAPI** for API documentation

## Getting Started

1. Ensure you have the following installed:
   - .NET 8.0 SDK
   - MySQL Server

2. Clone the repository

3. Update the connection string in `appsettings.json`

4. Run Entity Framework migrations:
   ```bash
   dotnet ef database update
   ```

5. Run the application:
   ```bash
   dotnet run
   ```

6. Access Swagger documentation at `https://localhost:5001/swagger`

## Authentication

The API uses JWT Bearer authentication. To access protected endpoints:

1. Register a new user or use seeded admin credentials
2. Obtain a JWT token from the `/api/auth/login` endpoint
3. Include the token in the Authorization header: `Bearer {token}` 
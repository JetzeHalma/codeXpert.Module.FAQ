# codeXpert FAQ Module

This project is a FAQ module for the Oqtane platform. It provides a flexible and auditable FAQ entity, supporting integration with Oqtane and Entity Framework Core migrations.

## Features

- Blazor project targeting .NET 9 and C# 13
- FAQ entity with fields: `FAQId`, `ModuleId`, `Question`, `Answer`, `Order`
- Auditable columns: `CreatedBy`, `CreatedOn`, `ModifiedBy`, `ModifiedOn`
- Entity migrations using Oqtane and EF Core
- Modular design for easy integration

## Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- Visual Studio 2022 or later
- Oqtane framework dependencies

### Installation

1. **Clone the repository**  
2. **Restore dependencies** 
Open the solution in Visual Studio 2022 and restore NuGet packages.
3. **Build and run**  
Build the solution and run the project.

### Usage

- The FAQ module can be added to your Oqtane site.
- Use the provided repository and service classes to manage FAQ entries.
- Migrations are handled via the `FAQEntityBuilder` and related migration classes.

## Project Structure

- `Server/Repository/FAQRepository.cs` - Data access for FAQ entities
- `Shared/Models/FAQ.cs` - Shared FAQ model
- `Server/Repository/FAQContext.cs` - EF Core DbContext for FAQ
- `Server/Migrations/EntityBuilders/FAQEntityBuilder.cs` - Migration builder for FAQ table
- `Client/Services/FAQService.cs` - Client-side service for FAQ operations

## Contributing

Contributions are welcome! Please submit issues and pull requests via GitHub.

## License

This project is licensed under the MIT License.
# Budget Tracker .NET

A personal finance tracking Web API built with ASP.NET Core and Entity Framework Core.  
This project is designed for self-study and aims to simulate a real-world backend system to manage income, expenses, categories, and summaries.

## Features (planned)
- Record income and expenses
- Categorize transactions
- Filter by date, type, and category
- View monthly and category-based summaries
- Optional: authentication and per-user data
- Optional: export data to CSV or PDF
- Optional: frontend using Blazor, MAUI, or another framework

## Tech Stack
- ASP.NET Core 9
- Entity Framework Core
- SQLite / SQL Server LocalDB
- Swagger for API testing
- (Optional) JWT authentication

## Database Management

To manage database migrations and updates, use the following commands:

- Add a migration:
  ```sh
  dotnet ef migrations add InitialCreate --project BudgetTracker.Infrastructure --startup-project BudgetTracker.API
  ```
- Update the database:
  ```sh
  dotnet ef database update --project BudgetTracker.Infrastructure --startup-project BudgetTracker.API
  ```
- Drop the database:
  ```sh
  dotnet ef database drop --project BudgetTracker.Infrastructure --startup-project BudgetTracker.API
  ```
- Undo the last migration:
  ```sh
  dotnet ef migrations remove --project BudgetTracker.Infrastructure --startup-project BudgetTracker.API
  ```

## MAUI App Development Setup

If you want to build and run the MAUI app (cross-platform UI), you must install the required .NET workloads and platform SDKs.

**On your own computer with admin rights, run:**
```sh
dotnet workload install maui maui-android maui-ios maui-maccatalyst
```

- These workloads enable cross-platform development for Android, iOS, MacCatalyst, and Windows.
- If you see missing references in the MAUI project, it likely means the workloads or platform SDKs are not installed.
- On Windows, Android builds require the Android SDK, which is installed by the above command.
- On Mac, iOS/MacCatalyst builds require Xcode.
- If you do not have permission to install these at work, you can install them at home or on a machine where you have admin rights.

See the official docs for more:
- [Install .NET MAUI workloads](https://learn.microsoft.com/dotnet/maui/get-started/installation)

## Status
🚧 Project is in the early development phase (planning & setup)

## Author
Rick van Nieuwland

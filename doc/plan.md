# Budget Tracker Web API with .NET 8

## Notes
- User is a .NET developer with 1 year of experience and a Unity background
- This project is meant for self-study while waiting for a client assignment
- Goal: Learn practical .NET backend development using real-world scenarios
- Stack: ASP.NET Core Web API, EF Core, SQLite or SQL Server LocalDB
- Focus is on clean API design, database interaction, filtering, and summaries
- Optional: add authentication and a frontend (Blazor, MAUI, or React) later

## Task List
- [x] Create ASP.NET Core Web API project
- [x] Install and configure EF Core with SQLite or SQL Server LocalDB
- [x] Create `Transaction` model with properties:
    - Id, Title, Amount, Date, Type (Income/Expense), CategoryId
- [x] Create `Category` model with properties:
    - Id, Name
- [x] Configure `ApplicationDbContext` and register DbSets
- [x] Create initial migration and apply it to the database
- [x] Add CRUD endpoints for transactions
- [ ] Optional: Add CRUD endpoints for categories
- [x] Implement basic validation in models and DTOs
- [x] Add filtering endpoint for transactions (by date range, type(s), category(s))
- [x] Add filtering endpoint for recurring transactions (by date range, type(s), category(s))
- [ ] Add monthly summary endpoint (total income/expenses per month)
- [ ] Add category summary endpoint (total spent per category)
- [ ] Add Swagger documentation for all endpoints
- [ ] Organize code with DTOs and separate service layer (optional)
- [ ] Optional: Add JWT-based user authentication
- [ ] Optional: Add `User` model and link transactions to users
- [ ] Optional: Restrict data access by logged-in user
- [ ] Optional: Export transactions to CSV or PDF
- [ ] Optional: Add recurring transactions support
- [ ] Optional: Add budget limits per category
- [ ] Optional: Build frontend using Blazor Server, MAUI, or another UI framework

## Progress
- Models for Transaction and Category implemented (basic structure).
- SqlLiteContext set up for EF Core and SQLite.
- Ready for CRUD and filtering features.
- Improvements needed:
    - Consider using an enum for Transaction.Type instead of string.

## Next Steps
- Fix Category model access modifiers.
- Add user support if authentication/per-user data is required.
- Implement CRUD endpoints for Transaction.
- (Optional, for later) Implement CRUD endpoints for Category.
- Apply EF Core migrations and test database.
- Continue with filtering, summaries, and optional features.

### New: CRUD Logic Layer for Transaction
- Create folders in `.Logic`:
    - `Services/Interfaces`
    - `Services/Implementations`
    - `Dtos`
    - `Results`
- Define `ITransactionService` interface in `Services/Interfaces`.
- Implement `TransactionService` in `Services/Implementations`.
- Create DTOs for Transaction (e.g., `CreateTransactionDto`, `UpdateTransactionDto`) in `Dtos`.
- Create result classes for Transaction (e.g., `TransactionResult`) in `Results`.
- Ensure all CRUD logic for Transaction is in `.Logic` and uses DTOs for input and result classes for output.

## Current Goal
Start by setting up the project and building the Transaction model.
(Optional, for later) Build the Category model and add Category CRUD endpoints.

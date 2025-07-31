# Budget Tracker Web API with .NET 8

## Notes
- User is a .NET developer with 1 year of experience and a Unity background
- This project is meant for self-study while waiting for a client assignment
- Goal: Learn practical .NET backend development using real-world scenarios
- Stack: ASP.NET Core Web API, EF Core, SQLite or SQL Server LocalDB
- Focus is on clean API design, database interaction, filtering, and summaries
- Optional: add authentication and a frontend (Blazor, MAUI, or React) later

## Task List
- [ ] Create ASP.NET Core Web API project
- [ ] Install and configure EF Core with SQLite or SQL Server LocalDB
- [ ] Create `Transaction` model with properties:
    - Id, Title, Amount, Date, Type (Income/Expense), CategoryId
- [ ] Create `Category` model with properties:
    - Id, Name
- [ ] Configure `ApplicationDbContext` and register DbSets
- [ ] Create initial migration and apply it to the database
- [ ] Add CRUD endpoints for transactions
- [ ] Add CRUD endpoints for categories
- [ ] Implement basic validation in models and DTOs
- [ ] Add filtering endpoint (by date range, type, category)
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
- 🔄 Planning phase — project not yet started
- 🧠 Architecture and features defined
- 🧰 Technology stack chosen: .NET 9, EF Core, SQLite/SQL Server

## Current Goal
Start by setting up the project and building the Transaction & Category models

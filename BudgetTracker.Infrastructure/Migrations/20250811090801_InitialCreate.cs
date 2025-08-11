using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BudgetTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecurringTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Pattern = table.Column<int>(type: "INTEGER", maxLength: 20, nullable: false),
                    Type = table.Column<int>(type: "INTEGER", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecurringTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecurringTransactions_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    RecurringTransactionId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transactions_RecurringTransactions_RecurringTransactionId",
                        column: x => x.RecurringTransactionId,
                        principalTable: "RecurringTransactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Food, drinks, and supermarket purchases", "Groceries" },
                    { 2, "Restaurants, cafes, takeaways, bars", "Dining Out" },
                    { 3, "Electricity, water, gas, internet, phone", "Utilities" },
                    { 4, "Rent, mortgage, property maintenance", "Housing" },
                    { 5, "Public transport, fuel, tolls", "Transport" },
                    { 6, "Car payments, repairs, insurance", "Vehicle" },
                    { 7, "Movies, hobbies, games, streaming services", "Entertainment" },
                    { 8, "Trips, vacations, hotels, flights", "Travel" },
                    { 9, "Medical, dental, pharmacy, health insurance", "Health" },
                    { 10, "Haircuts, beauty, grooming", "Personal Care" },
                    { 11, "Courses, books, learning materials", "Education" },
                    { 12, "Professional, repair, or cleaning services", "Services" },
                    { 13, "Premiums for health, life, property", "Insurance" },
                    { 14, "Crypto, shares, bonds, assets", "Investments" },
                    { 15, "Loan payments and repayments", "Loan" },
                    { 16, "Charity and contributions", "Donations" },
                    { 17, "Income from employment", "Salary" },
                    { 18, "Freelance or business earnings", "Business Income" },
                    { 19, "Received or given gifts", "Gifts" },
                    { 20, "Interest income or loan interest paid", "Interest" },
                    { 21, "Income tax, property tax, other taxes", "Taxes" },
                    { 22, "Transfers to savings or emergency funds", "Savings" },
                    { 23, "Miscellaneous expenses", "Other" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecurringTransactions_CategoryId",
                table: "RecurringTransactions",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CategoryId",
                table: "Transactions",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_RecurringTransactionId",
                table: "Transactions",
                column: "RecurringTransactionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "RecurringTransactions");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}

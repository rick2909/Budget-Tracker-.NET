using BudgetTracker.Logic.Dtos;
using BudgetTracker.Logic.Results;
using BudgetTracker.Infrastructure;
using BudgetTracker.Logic.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BudgetTracker.Logic.Services.Implementations
{
    public class TransactionService(SqliteContext context) : ITransactionService
    {
        private readonly SqliteContext _context = context;

        public async Task<TransactionResult> GetTransactionByIdAsync(int id)
        {
            // TODO: Implement logic
            return null;
        }

        public async Task<IEnumerable<TransactionResult>> GetAllTransactionsAsync()
        {
            // TODO: Implement logic
            return null;
        }

        public async Task<TransactionResult> CreateTransactionAsync(CreateTransactionDto dto)
        {
            // TODO: Implement logic
            return null;
        }

        public async Task<TransactionResult> UpdateTransactionAsync(int id, UpdateTransactionDto dto)
        {
            // TODO: Implement logic
            return null;
        }

        public async Task<bool> DeleteTransactionAsync(int id)
        {
            // TODO: Implement logic
            return false;
        }
    }
}

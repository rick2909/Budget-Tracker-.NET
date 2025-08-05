using BudgetTracker.Logic.Dtos;
using BudgetTracker.Logic.Results;
using BudgetTracker.Infrastructure;
using BudgetTracker.Infrastructure.Models;
using BudgetTracker.Logic.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BudgetTracker.Logic.Services.Implementations
{
    public class TransactionService(SqliteContext context) : ITransactionService
    {
        private readonly SqliteContext _context = context;

        public async Task<TransactionResult> GetTransactionByIdAsync(int id)
        {
            var transaction = await _context.Transactions.FirstOrDefaultAsync(t => t.Id == id);
            
            if (transaction == null)
            {
                return TransactionResult.Fail(404, "Transaction not found");
            }
            
            return TransactionResult.Success(transaction);
        }

        public async Task<IEnumerable<TransactionResult>> GetAllTransactionsAsync()
        {
            var transactions = await _context.Transactions.ToListAsync();
            
            return transactions.Select(t => TransactionResult.Success(t));
        }

        public async Task<TransactionResult> CreateTransactionAsync(CreateTransactionDto dto)
        {
            // convert dto to entity
            var transaction = new Transaction
            {
                Title = dto.Title,
                Amount = dto.Amount,
                Date = dto.Date,
                Type = dto.Type,
                CategoryId = dto.CategoryId
            };
            
            context.Transactions.Add(transaction);
            await context.SaveChangesAsync();
            
            return TransactionResult.Success(transaction);
        }

        public async Task<TransactionResult> UpdateTransactionAsync(int id, UpdateTransactionDto dto)
        {
            var transaction = await _context.Transactions.FirstOrDefaultAsync(t => t.Id == id);
            
            if (transaction == null)
            {
                return TransactionResult.Fail(404, "Transaction not found");
            }
            
            transaction.Title = dto.Title ?? transaction.Title;
            transaction.Amount = dto.Amount ?? transaction.Amount;
            transaction.Date = dto.Date ?? transaction.Date;
            transaction.Type = dto.Type ?? transaction.Type;
            transaction.CategoryId = dto.CategoryId ?? transaction.CategoryId;
            
            await _context.SaveChangesAsync();
            
            return TransactionResult.Success(transaction);
        }

        public async Task<TransactionResult> DeleteTransactionAsync(int id)
        {
            var transaction = await _context.Transactions.FirstOrDefaultAsync(t => t.Id == id);
            
            if (transaction == null)
            {
                return TransactionResult.Fail(404, "Transaction not found");
            }
            
            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
            
            return TransactionResult.Success(null, 202, "Transaction deleted");
        }
        public async Task<IEnumerable<TransactionResult>> FilterTransactionsAsync(TransactionFilterDto filterDto)
        {
            var query = _context.Transactions.AsQueryable();

            if (filterDto.DateFrom.HasValue)
            {
                query = query.Where(t => t.Date >= filterDto.DateFrom.Value);
            }

            if (filterDto.DateTo.HasValue)
            {
                query = query.Where(t => t.Date <= filterDto.DateTo.Value);
            }

            if (filterDto.Types != null && filterDto.Types.Any())
            {
                query = query.Where(t => filterDto.Types.Contains(t.Type));
            }

            if (filterDto.CategoryIds != null && filterDto.CategoryIds.Any())
            {
                query = query.Where(t => filterDto.CategoryIds.Contains<int>(t.CategoryId));
            }

            var transactions = await query.ToListAsync();
            return transactions.Select(t => TransactionResult.Success(t));
        }
    }
}

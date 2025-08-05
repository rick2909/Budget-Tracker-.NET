using BudgetTracker.Infrastructure;
using BudgetTracker.Infrastructure.Models;
using BudgetTracker.Logic.Dtos;
using BudgetTracker.Logic.Results;
using BudgetTracker.Logic.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BudgetTracker.Logic.Services.Implementations;

public class RecurringTransactionService(SqliteContext context) : IRecurringTransactionService
{
    private readonly SqliteContext _context = context;

    public async Task<RecurringTransactionResult> GetRecurringTransactionByIdAsync(int id)
    {
        var recurringTransaction = await _context.RecurringTransactions
            .Include(rt => rt.Category)
            .FirstOrDefaultAsync(rt => rt.Id == id);
        
        if (recurringTransaction == null)
        {
            return RecurringTransactionResult.Fail(404, "Recurring transaction not found");
        }
        return RecurringTransactionResult.Success(recurringTransaction);
    }

    public async Task<IEnumerable<RecurringTransactionResult>> GetAllRecurringTransactionsAsync()
    {
        var recurringTransactions = await _context.RecurringTransactions
            .Include(rt => rt.Category)
            .ToListAsync();
        return recurringTransactions.Select(rt => RecurringTransactionResult.Success(rt));
    }

    public async Task<RecurringTransactionResult> CreateRecurringTransactionAsync(CreateRecurringTransactionDto dto)
    {
        var recurringTransaction = new RecurringTransaction
        {
            Title = dto.Title,
            Amount = dto.Amount,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            Pattern = dto.RecurrencePattern,
            Type = dto.Type,
            CategoryId = dto.CategoryId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        
        _context.RecurringTransactions.Add(recurringTransaction);
        await _context.SaveChangesAsync();
        return RecurringTransactionResult.Success(recurringTransaction);
    }

    public async Task<RecurringTransactionResult> UpdateRecurringTransactionAsync(int id, UpdateRecurringTransactionDto dto)
    {
        var recurringTransaction = await _context.RecurringTransactions.FirstOrDefaultAsync(rt => rt.Id == id);
        if (recurringTransaction == null)
        {
            return RecurringTransactionResult.Fail(404, "Recurring transaction not found");
        }
        recurringTransaction.Title = dto.Title ?? recurringTransaction.Title;
        recurringTransaction.Amount = dto.Amount ?? recurringTransaction.Amount;
        recurringTransaction.Type = dto.Type ?? recurringTransaction.Type;
        recurringTransaction.CategoryId = dto.CategoryId ?? recurringTransaction.CategoryId;
        recurringTransaction.StartDate = dto.StartDate ?? recurringTransaction.StartDate;
        recurringTransaction.EndDate = dto.EndDate ?? recurringTransaction.EndDate;
        recurringTransaction.Pattern = dto.RecurrencePattern ?? recurringTransaction.Pattern;
        recurringTransaction.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return RecurringTransactionResult.Success(recurringTransaction);
    }

    public async Task<RecurringTransactionResult> DeleteRecurringTransactionAsync(int id)
    {
        var recurringTransaction = await _context.RecurringTransactions.FirstOrDefaultAsync(rt => rt.Id == id);
        if (recurringTransaction == null)
        {
            return RecurringTransactionResult.Fail(404, "Recurring transaction not found");
        }
        _context.RecurringTransactions.Remove(recurringTransaction);
        await _context.SaveChangesAsync();
        return RecurringTransactionResult.Success(null, 202, "Recurring transaction deleted");
    }

    public async Task<IEnumerable<RecurringTransactionResult>> FilterRecurringTransactionsAsync(RecurringTransactionFilterDto filterDto)
    {
        var query = _context.RecurringTransactions.AsQueryable();

        if (filterDto.StartDateFrom.HasValue)
        {
            query = query.Where(rt => rt.StartDate >= filterDto.StartDateFrom.Value);
        }

        if (filterDto.StartDateTo.HasValue)
        {
            query = query.Where(rt => rt.StartDate <= filterDto.StartDateTo.Value);
        }

        if (filterDto.Types != null && filterDto.Types.Any())
        {
            query = query.Where(rt => filterDto.Types.Contains(rt.Type));
        }

        if (filterDto.CategoryIds != null && filterDto.CategoryIds.Any())
        {
            query = query.Where(rt => filterDto.CategoryIds.Contains<int>(rt.CategoryId));
        }

        var recurringTransactions = await query.Include(rt => rt.Category).ToListAsync();
        return recurringTransactions.Select(rt => RecurringTransactionResult.Success(rt));
    }
}

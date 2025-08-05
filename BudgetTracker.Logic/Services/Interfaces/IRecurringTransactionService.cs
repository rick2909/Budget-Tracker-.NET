using System.Collections.Generic;
using System.Threading.Tasks;
using BudgetTracker.Logic.Dtos;
using BudgetTracker.Logic.Results;

namespace BudgetTracker.Logic.Services.Interfaces;

public interface IRecurringTransactionService
{
    Task<RecurringTransactionResult> GetRecurringTransactionByIdAsync(int id);
    Task<IEnumerable<RecurringTransactionResult>> GetAllRecurringTransactionsAsync();
    Task<RecurringTransactionResult> CreateRecurringTransactionAsync(CreateRecurringTransactionDto dto);
    Task<RecurringTransactionResult> UpdateRecurringTransactionAsync(int id, UpdateRecurringTransactionDto dto);
    Task<RecurringTransactionResult> DeleteRecurringTransactionAsync(int id);
    Task<IEnumerable<RecurringTransactionResult>> FilterRecurringTransactionsAsync(RecurringTransactionFilterDto filterDto);
}

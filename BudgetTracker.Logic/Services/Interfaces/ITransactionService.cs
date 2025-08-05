using System.Collections.Generic;
using System.Threading.Tasks;
using BudgetTracker.Logic.Dtos;
using BudgetTracker.Logic.Results;

namespace BudgetTracker.Logic.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<TransactionResult> GetTransactionByIdAsync(int id);
        Task<IEnumerable<TransactionResult>> GetAllTransactionsAsync();
        Task<TransactionResult> CreateTransactionAsync(CreateTransactionDto dto);
        Task<TransactionResult> UpdateTransactionAsync(int id, UpdateTransactionDto dto);
        Task<TransactionResult> DeleteTransactionAsync(int id);
        Task<IEnumerable<TransactionResult>> FilterTransactionsAsync(TransactionFilterDto filterDto);
    }
}

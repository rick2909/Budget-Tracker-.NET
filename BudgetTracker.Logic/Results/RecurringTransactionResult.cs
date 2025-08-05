using BudgetTracker.Infrastructure.Models;

namespace BudgetTracker.Logic.Results;

public class RecurringTransactionResult : IResult
{
    public bool IsSuccess { get; private set; }
    public int StatusCode { get; private set; }
    public string Message { get; private set; }
    
    public RecurringTransaction? RecurringTransaction { get; private set; }

    private RecurringTransactionResult(bool isSuccess, int statusCode, string message, RecurringTransaction? recurringTransaction = null)
    {
        IsSuccess = isSuccess;
        StatusCode = statusCode;
        Message = message;
        RecurringTransaction = recurringTransaction;
    }

    public static RecurringTransactionResult Success(RecurringTransaction? recurringTransaction, int statusCode = 200, string message = "Success")
    {
        return new RecurringTransactionResult(true, statusCode, message, recurringTransaction);
    }

    public static RecurringTransactionResult Fail(int statusCode, string message)
    {
        return new RecurringTransactionResult(false, statusCode, message);
    }
}

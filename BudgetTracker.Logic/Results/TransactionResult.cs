using BudgetTracker.Infrastructure.Models;

namespace BudgetTracker.Logic.Results;

public class TransactionResult : IResult
{
    public bool IsSuccess { get; private set; }
    public int StatusCode { get; private set; }
    public string Message { get; private set; }
    
    public Transaction? Transaction { get; private set; }

    private TransactionResult(bool isSuccess, int statusCode, string message, Transaction? transaction = null)
    {
        IsSuccess = isSuccess;
        StatusCode = statusCode;
        Message = message;
        Transaction = transaction;
    }

    public static TransactionResult Success(Transaction? transaction, string message = "Success", int statusCode = 200)
    {
        return new TransactionResult(true, statusCode, message, transaction);
    }

    public static TransactionResult Fail(int statusCode, string message)
    {
        return new TransactionResult(false, statusCode, message);
    }
}
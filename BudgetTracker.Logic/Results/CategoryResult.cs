using BudgetTracker.Infrastructure.Models;

namespace BudgetTracker.Logic.Results;

public class CategoryResult : IResult
{
    public bool IsSuccess { get; }
    public int StatusCode { get; }
    public string Message { get; }
    public Category? Category { get; }

    private CategoryResult(bool isSuccess, int statusCode, string message, Category? category)
    {
        IsSuccess = isSuccess;
        StatusCode = statusCode;
        Message = message;
        Category = category;
    }
    
    public static CategoryResult Success(Category category)
    {
        return new CategoryResult(true, 200, "Category retrieved successfully.", category);
    }
    
    public static CategoryResult Fail(string message = "Category not found.")
    {
        return new CategoryResult(false, 404, message, null);
    }
}
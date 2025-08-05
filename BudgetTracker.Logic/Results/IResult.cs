namespace BudgetTracker.Logic.Results
{
    public interface IResult
    {
        bool IsSuccess { get; }
        int StatusCode { get; }
        string Message { get; }
    }
}

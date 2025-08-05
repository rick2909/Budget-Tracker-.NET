using BudgetTracker.Logic.Results;

namespace BudgetTracker.Logic.Services.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryResult>> GetAllCategoriesAsync();
}
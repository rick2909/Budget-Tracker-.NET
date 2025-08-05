using BudgetTracker.Infrastructure;
using BudgetTracker.Logic.Results;
using BudgetTracker.Logic.Services.Interfaces;

namespace BudgetTracker.Logic.Services.Implementations;

public class CategoryService(SqliteContext context) : ICategoryService
{
    private readonly SqliteContext _context = context;

    public Task<IEnumerable<CategoryResult>> GetAllCategoriesAsync()
    {
        return Task.FromResult(_context.Categories
            .Select(c => CategoryResult.Success(c))
            .AsEnumerable());
    }
}
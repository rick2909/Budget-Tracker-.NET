using BudgetTracker.Infrastructure;
using BudgetTracker.Logic.Results;
using BudgetTracker.Logic.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BudgetTracker.Logic.Services.Implementations;

public class CategoryService(SqliteContext context) : ICategoryService
{
    private readonly SqliteContext _context = context;

    public async Task<IEnumerable<CategoryResult>> GetAllCategoriesAsync()
    {
        var categories = await _context.Categories.ToListAsync();
        return categories.Select(CategoryResult.Success);
    }
}
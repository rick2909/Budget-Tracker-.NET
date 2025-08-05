using BudgetTracker.Logic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BudgetTracker.API.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoryController(ICategoryService categoryService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var results = await categoryService.GetAllCategoriesAsync();
        var categoryResults = results.ToList();
        if (categoryResults.Count == 0)
            return NotFound("No categories found.");
        
        // Check if the results contain any errors
        if (categoryResults.Any(r => !r.IsSuccess))
        {
            var errorMessages = categoryResults.Where(r => !r.IsSuccess).Select(r => r.Message);
            return BadRequest(string.Join(", ", errorMessages));
        }
        
        //convert Result list to a simple list of categories
        var categories = categoryResults.Select(r => r.Category).ToList();
        return Ok(categories);
    }
}
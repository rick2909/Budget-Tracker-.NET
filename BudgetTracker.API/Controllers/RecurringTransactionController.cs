using Microsoft.AspNetCore.Mvc;
using BudgetTracker.Logic.Services.Interfaces;
using BudgetTracker.Logic.Dtos;
using BudgetTracker.Logic.Results;

namespace BudgetTracker.API.Controllers;

[ApiController]
[Route("api/transaction/recurring")]
public class RecurringTransactionController : ControllerBase
{
    private readonly IRecurringTransactionService _recurringTransactionService;

    public RecurringTransactionController(IRecurringTransactionService recurringTransactionService)
    {
        _recurringTransactionService = recurringTransactionService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var results = await _recurringTransactionService.GetAllRecurringTransactionsAsync();
        return Ok(results);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _recurringTransactionService.GetRecurringTransactionByIdAsync(id);
        if (!result.IsSuccess)
            return NotFound(result.Message);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRecurringTransactionDto dto)
    {
        var result = await _recurringTransactionService.CreateRecurringTransactionAsync(dto);
        if (!result.IsSuccess)
            return BadRequest(result.Message);
        return CreatedAtAction(nameof(GetById), new { id = result.RecurringTransaction?.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateRecurringTransactionDto dto)
    {
        var result = await _recurringTransactionService.UpdateRecurringTransactionAsync(id, dto);
        if (!result.IsSuccess)
            return NotFound(result.Message);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _recurringTransactionService.DeleteRecurringTransactionAsync(id);
        if (!result.IsSuccess)
            return NotFound(result.Message);
        return Ok(result);
    }
}

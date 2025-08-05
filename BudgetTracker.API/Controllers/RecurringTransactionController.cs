using Microsoft.AspNetCore.Mvc;
using BudgetTracker.Logic.Services.Interfaces;
using BudgetTracker.Logic.Dtos;
using BudgetTracker.Logic.Results;

namespace BudgetTracker.API.Controllers;

[ApiController]
[Route("api/transaction/recurring")]
public class RecurringTransactionController(IRecurringTransactionService recurringTransactionService) : ControllerBase
{
    [HttpGet("filter")]
    public async Task<IActionResult> Filter([FromQuery] RecurringTransactionFilterDto filterDto)
    {
        var results = await recurringTransactionService.FilterRecurringTransactionsAsync(filterDto);
        return Ok(results);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var results = await recurringTransactionService.GetAllRecurringTransactionsAsync();
        return Ok(results);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await recurringTransactionService.GetRecurringTransactionByIdAsync(id);
        if (!result.IsSuccess)
            return NotFound(result.Message);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRecurringTransactionDto dto)
    {
        var result = await recurringTransactionService.CreateRecurringTransactionAsync(dto);
        if (!result.IsSuccess)
            return BadRequest(result.Message);
        return CreatedAtAction(nameof(GetById), new { id = result.RecurringTransaction?.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateRecurringTransactionDto dto)
    {
        var result = await recurringTransactionService.UpdateRecurringTransactionAsync(id, dto);
        if (!result.IsSuccess)
            return NotFound(result.Message);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await recurringTransactionService.DeleteRecurringTransactionAsync(id);
        if (!result.IsSuccess)
            return NotFound(result.Message);
        return Ok(result);
    }
}

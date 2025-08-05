using Microsoft.AspNetCore.Mvc;
using BudgetTracker.Logic.Services.Interfaces;
using BudgetTracker.Logic.Dtos;
using BudgetTracker.Logic.Results;

namespace BudgetTracker.API.Controllers;

[ApiController]
[Route("api/transaction")]
public class TransactionController(ITransactionService transactionService) : ControllerBase
{
    [HttpGet("filter")]
    public async Task<IActionResult> Filter([FromQuery] TransactionFilterDto filterDto)
    {
        var results = await transactionService.FilterTransactionsAsync(filterDto);
        return Ok(results);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var results = await transactionService.GetAllTransactionsAsync();
        return Ok(results);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await transactionService.GetTransactionByIdAsync(id);
        if (!result.IsSuccess)
            return NotFound(result.Message);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTransactionDto dto)
    {
        var result = await transactionService.CreateTransactionAsync(dto);
        if (!result.IsSuccess)
            return BadRequest(result.Message);
        return CreatedAtAction(nameof(GetById), new { id = result.Transaction?.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateTransactionDto dto)
    {
        var result = await transactionService.UpdateTransactionAsync(id, dto);
        if (!result.IsSuccess)
            return NotFound(result.Message);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await transactionService.DeleteTransactionAsync(id);
        if (!result.IsSuccess)
            return NotFound(result.Message);
        return Ok(result);
    }
}

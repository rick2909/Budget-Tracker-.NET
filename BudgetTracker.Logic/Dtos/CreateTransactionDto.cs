using System.ComponentModel.DataAnnotations;
using BudgetTracker.Infrastructure.Enums;
using BudgetTracker.Infrastructure.Models;
using BudgetTracker.Logic.Attributes;

namespace BudgetTracker.Logic.Dtos;

public class CreateTransactionDto : BaseTransactionDto
{
    [Required, DataType(DataType.Date)]
    public required  DateTime Date { get; set; }
}
using System.ComponentModel.DataAnnotations;
using BudgetTracker.Infrastructure.Enums;

namespace BudgetTracker.Logic.Dtos;

public class CreateRecurringTransactionDto : BaseTransactionDto
{
    [Required, DataType(DataType.Date)]
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    [Required]
    public RecurrencePattern RecurrencePattern { get; set; }
}
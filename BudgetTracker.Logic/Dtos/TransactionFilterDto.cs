using System;
using BudgetTracker.Infrastructure.Enums;

namespace BudgetTracker.Logic.Dtos
{
    public class TransactionFilterDto
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public IEnumerable<TransactionType>? Types { get; set; }
        public IEnumerable<int>? CategoryIds { get; set; }
    }
}

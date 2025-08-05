using System;
using System.Collections.Generic;
using BudgetTracker.Infrastructure.Enums;

namespace BudgetTracker.Logic.Dtos
{
    public class RecurringTransactionFilterDto
    {
        public DateTime? StartDateFrom { get; set; }
        public DateTime? StartDateTo { get; set; }
        public IEnumerable<TransactionType>? Types { get; set; }
        public IEnumerable<int>? CategoryIds { get; set; }
    }
}

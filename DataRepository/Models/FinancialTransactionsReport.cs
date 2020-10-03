using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManagement.DataRepository.Models
{
    public class FinancialTransactionsReport
    {
        public IEnumerable<FinancialTransaction> FinancialTransactions { get; set; }
        public decimal TotalValue { get; set; }
    }
}

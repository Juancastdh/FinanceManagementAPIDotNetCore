using FinanceManagement.BusinessLogic.Managers;
using FinanceManagement.DataRepository;
using FinanceManagement.DataRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinanceManagement.BusinessLogic.Implementations
{
    public class FinancialTransactionsManager : IFinancialTransactionsManager
    {
        private readonly IFinancialTransactionsRepository FinancialTransactionsRepository;

        public FinancialTransactionsManager(IFinancialTransactionsRepository financialTransactionsRepository)
        {
            FinancialTransactionsRepository = financialTransactionsRepository;
        }

        public void AddFinancialTransaction(FinancialTransaction financialTransaction)
        {
            FinancialTransactionsRepository.Add(financialTransaction);
        }

        public FinancialTransaction GetFinancialTransactionById(int id)
        {
            FinancialTransaction financialTransaction =  FinancialTransactionsRepository.GetById(id);
            return financialTransaction;
        }

        public IEnumerable<FinancialTransaction> GetFinancialTransactions()
        {
            IEnumerable<FinancialTransaction> financialTransactions = FinancialTransactionsRepository.Get();
            return financialTransactions;
        }

        public FinancialTransactionsReport GetIncomeFinancialTransactionsReport()
        {
            IEnumerable<FinancialTransaction> incomeFinancialTransactions = GetIncomeFinancialTransactions().OrderBy(financialTransaction => financialTransaction.Date);       

            decimal totalValue = incomeFinancialTransactions.Sum(financialTransaction => financialTransaction.Value);

            FinancialTransactionsReport incomeFinancialTransactionsReport = new FinancialTransactionsReport
            {
                FinancialTransactions = incomeFinancialTransactions,
                TotalValue = totalValue
            };

            return incomeFinancialTransactionsReport;
        }

        public IEnumerable<FinancialTransaction> GetIncomeFinancialTransactions()
        {
            IEnumerable<FinancialTransaction> financialTransactions = FinancialTransactionsRepository.Get();
            IEnumerable<FinancialTransaction> incomeFinancialTransactions = financialTransactions.Where(financialTransaction => financialTransaction.IsExpense == false);

            return incomeFinancialTransactions;
        }

        public void UpdateFinancialTransaction(FinancialTransaction financialTransaction)
        {
            FinancialTransactionsRepository.Update(financialTransaction);
        }

    }
}

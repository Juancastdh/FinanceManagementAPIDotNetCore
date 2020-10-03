using FinanceManagement.BusinessLogic.Managers;
using FinanceManagement.BusinessLogic.Models.Enums;
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
            IEnumerable<FinancialTransaction> incomeFinancialTransactions = GetFinancialTransactionsByType(FinancialTransactionType.Income).OrderBy(financialTransaction => financialTransaction.Date);       

            decimal totalValue = incomeFinancialTransactions.Sum(financialTransaction => financialTransaction.Value);

            FinancialTransactionsReport incomeFinancialTransactionsReport = new FinancialTransactionsReport
            {
                FinancialTransactions = incomeFinancialTransactions,
                TotalValue = totalValue
            };

            return incomeFinancialTransactionsReport;
        }

        public void UpdateFinancialTransaction(FinancialTransaction financialTransaction)
        {
            FinancialTransactionsRepository.Update(financialTransaction);
        }

        public void DeleteFinancialTransactionById(int id)
        {
            FinancialTransaction financialTransactionToBeDeleted = GetFinancialTransactionById(id);
            FinancialTransactionsRepository.Delete(financialTransactionToBeDeleted);
        }

        public IEnumerable<FinancialTransaction> GetFinancialTransactionsByType(FinancialTransactionType financialTransactionType)
        {
            IEnumerable<FinancialTransaction> financialTransactions = FinancialTransactionsRepository.Get();

            if(financialTransactionType == FinancialTransactionType.Expense)
            {
                financialTransactions = financialTransactions.Where(financialTransaction => financialTransaction.IsExpense);
            }
            else
            {
                financialTransactions = financialTransactions.Where(financialTransaction => !financialTransaction.IsExpense);
            }

            return financialTransactions;
        }
    }
}

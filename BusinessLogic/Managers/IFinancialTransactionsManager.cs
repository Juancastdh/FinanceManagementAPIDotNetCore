using FinanceManagement.BusinessLogic.Models.Enums;
using FinanceManagement.DataRepository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManagement.BusinessLogic.Managers
{
    public interface IFinancialTransactionsManager
    {
        void AddFinancialTransaction(FinancialTransaction financialTransaction);
        IEnumerable<FinancialTransaction> GetFinancialTransactions();

        FinancialTransaction GetFinancialTransactionById(int id);       
        FinancialTransactionsReport GetFinancialTransactionsReportByType(FinancialTransactionType financialTransactionType);
        void UpdateFinancialTransaction(FinancialTransaction financialTransaction);
        void DeleteFinancialTransactionById(int id);
        IEnumerable<FinancialTransaction> GetFinancialTransactionsByType(FinancialTransactionType financialTransactionType);

    }
}

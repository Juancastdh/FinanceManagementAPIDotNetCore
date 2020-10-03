﻿using FinanceManagement.DataRepository.Models;
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
        FinancialTransactionsReport GetIncomeFinancialTransactionsReport();
        IEnumerable<FinancialTransaction> GetIncomeFinancialTransactions();
        void UpdateFinancialTransaction(FinancialTransaction financialTransaction);
    }
}

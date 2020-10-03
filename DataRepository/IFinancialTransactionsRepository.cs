using FinanceManagement.DataRepository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManagement.DataRepository
{
    public interface IFinancialTransactionsRepository
    {
        IEnumerable<FinancialTransaction> Get();
        FinancialTransaction GetById(int id);
        void Add(FinancialTransaction financialTransaction);
        void Update(FinancialTransaction financialTransaction);
        void Delete(FinancialTransaction financialTransaction);
        void DeleteAll();
        void AddMany(IEnumerable<FinancialTransaction> financialTransactions);
    }
}

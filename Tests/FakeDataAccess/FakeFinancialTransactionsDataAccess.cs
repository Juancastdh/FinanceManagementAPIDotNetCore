using FinanceManagement.DataRepository;
using FinanceManagement.DataRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinanceManagement.Tests.FakeDataAccess
{
    public class FakeFinancialTransactionsDataAccess: IFinancialTransactionsRepository
    {
        public List<FinancialTransaction> FinancialTransactions;

        public FakeFinancialTransactionsDataAccess()
        {
            FinancialTransactions = new List<FinancialTransaction>();
        }

        public void Add(FinancialTransaction financialTransaction)
        {
            FinancialTransactions.Add(financialTransaction);
        }

        public void AddMany(IEnumerable<FinancialTransaction> financialTransactions)
        {
            FinancialTransactions.AddRange(financialTransactions);
        }

        public void Delete(FinancialTransaction financialTransaction)
        {
            FinancialTransactions.Remove(financialTransaction);
        }

        public void DeleteAll()
        {
            FinancialTransactions.Clear();
        }

        public IEnumerable<FinancialTransaction> Get()
        {
            return FinancialTransactions;
        }

        public FinancialTransaction GetById(int id)
        {
            return FinancialTransactions.SingleOrDefault(financialTransaction => financialTransaction.Id == id);
        }

        public void Update(FinancialTransaction financialTransaction)
        {
            FinancialTransactions[FinancialTransactions.FindIndex(c => c.Id == financialTransaction.Id)] = financialTransaction;
        }
    }
}

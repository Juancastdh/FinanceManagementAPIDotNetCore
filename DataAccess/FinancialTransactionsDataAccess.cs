using FinanceManagement.DataAccess.Context;
using FinanceManagement.DataRepository;
using FinanceManagement.DataRepository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinanceManagement.DataAccess
{
    public class FinancialTransactionsDataAccess: IFinancialTransactionsRepository
    {
        private readonly FinanceManagementContext DatabaseContext;
        public FinancialTransactionsDataAccess(FinanceManagementContext databaseContext)
        {
            DatabaseContext = databaseContext;
        }

        public void Add(FinancialTransaction financialTransaction)
        {
            DatabaseContext.Database.EnsureCreated();

            financialTransaction.CategoryId = financialTransaction.Category.Id;
            financialTransaction.Category = null;

            using (var transaction = DatabaseContext.Database.BeginTransaction())
            {             
                try
                {
                    DatabaseContext.FinancialTransactions.Add(financialTransaction);
                    DatabaseContext.SaveChanges();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void AddMany(IEnumerable<FinancialTransaction> financialTransactions)
        {

            DatabaseContext.Database.EnsureCreated();
            using (var transaction = DatabaseContext.Database.BeginTransaction())
            {
                try
                {
                    DatabaseContext.FinancialTransactions.AddRange(financialTransactions);
                    DatabaseContext.SaveChanges();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void Delete(FinancialTransaction financialTransaction)
        {
            DatabaseContext.Database.EnsureCreated();
            using (var transaction = DatabaseContext.Database.BeginTransaction())
            {
                try
                {
                    DatabaseContext.FinancialTransactions.Remove(financialTransaction);
                    DatabaseContext.SaveChanges();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }

        }

        public void DeleteAll()
        {
            DatabaseContext.Database.EnsureCreated();

            using (var transaction = DatabaseContext.Database.BeginTransaction())
            {
                try
                {
                    DatabaseContext.FinancialTransactions.RemoveRange(DatabaseContext.FinancialTransactions);
                    DatabaseContext.SaveChanges();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public IEnumerable<FinancialTransaction> Get()
        {
            DatabaseContext.Database.EnsureCreated();

            List<FinancialTransaction> financialTransactions = DatabaseContext.FinancialTransactions.Include("Category").ToList();

            return financialTransactions;
        }

        public FinancialTransaction GetById(int id)
        {
            DatabaseContext.Database.EnsureCreated();

            FinancialTransaction financialTransaction = DatabaseContext.FinancialTransactions.Include("Category").SingleOrDefault(p => p.Id == id);

            return financialTransaction;
        }

        public void Update(FinancialTransaction financialTransaction)
        {
            DatabaseContext.Database.EnsureCreated();

            using (var transaction = DatabaseContext.Database.BeginTransaction())
            {
                try
                {
                    FinancialTransaction updatedFinancialTransaction = DatabaseContext.FinancialTransactions.SingleOrDefault(p => p.Id == financialTransaction.Id);
                    if (updatedFinancialTransaction != null)
                    {
                        updatedFinancialTransaction = financialTransaction;
                    }
                    DatabaseContext.SaveChanges();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}

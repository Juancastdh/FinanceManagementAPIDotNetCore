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
        private readonly ICategoriesRepository CategoriesRepository;

        public FinancialTransactionsManager(IFinancialTransactionsRepository financialTransactionsRepository, ICategoriesRepository categoriesRepository)
        {
            FinancialTransactionsRepository = financialTransactionsRepository;
            CategoriesRepository = categoriesRepository;
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

        public FinancialTransactionsReport GetFinancialTransactionsReportByType(FinancialTransactionType financialTransactionType)
        {
            IEnumerable<FinancialTransaction> financialTransactions = GetFinancialTransactionsByType(financialTransactionType).OrderBy(financialTransaction => financialTransaction.Date);       

            decimal totalValue = financialTransactions.Sum(financialTransaction => financialTransaction.Value);

            FinancialTransactionsReport financialTransactionsReport = new FinancialTransactionsReport
            {
                FinancialTransactions = financialTransactions,
                TotalValue = totalValue
            };

            return financialTransactionsReport;
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

        public FinancialTransactionsReport GetTotalFinancialTransactionsReport()
        {
            FinancialTransactionsReport incomeFinancialTransactionsReport = GetFinancialTransactionsReportByType(FinancialTransactionType.Income);
            FinancialTransactionsReport expenseFinancialTransactionsReport = GetFinancialTransactionsReportByType(FinancialTransactionType.Expense);

            IEnumerable<FinancialTransaction> totalFinancialTransactions = incomeFinancialTransactionsReport.FinancialTransactions.Concat(expenseFinancialTransactionsReport.FinancialTransactions);
            totalFinancialTransactions = totalFinancialTransactions.OrderBy(financialTransaction => financialTransaction.Date);

            decimal totalValue = incomeFinancialTransactionsReport.TotalValue - expenseFinancialTransactionsReport.TotalValue;

            FinancialTransactionsReport totalFinancialTransactionsReport = new FinancialTransactionsReport
            {
                FinancialTransactions = totalFinancialTransactions,
                TotalValue = totalValue
            };

            return totalFinancialTransactionsReport;
        }        

        public decimal GetTotalFinancialTransactionsValueByCategoryId(int categoryId)
        {
            decimal totalValue = 0;

            Category category = CategoriesRepository.GetById(categoryId);

            totalValue += GetTotalGeneralTransactionsValueByCategory(category);
            totalValue += GetTotalCategoryTransactionsValueByCategoryId(categoryId);

            return totalValue;
        }

        private decimal GetTotalGeneralTransactionsValueByCategory(Category category)
        {
            IEnumerable<FinancialTransaction> generalTransactions = GetFinancialTransactionsByCategoryId(category.Id);
            decimal totalValue = 0;

            foreach (FinancialTransaction transaction in generalTransactions)
            {
                if (transaction.IsExpense == false)
                {
                    totalValue += transaction.Value * ((decimal)(category.Percentage) / 100);
                }
                else
                {
                    totalValue -= transaction.Value * ((decimal)(category.Percentage) / 100);
                }
            }


            return totalValue;
        }

        private decimal GetTotalCategoryTransactionsValueByCategoryId(int categoryId)
        {
            IEnumerable<FinancialTransaction> categoryTransactions = GetFinancialTransactionsByCategoryId(categoryId);
            decimal totalValue = 0;

            foreach (FinancialTransaction transaction in categoryTransactions)
            {
                if (transaction.IsExpense == false)
                {
                    totalValue += transaction.Value;
                }
                else
                {
                    totalValue -= transaction.Value;
                }

            }

            return totalValue;

        }

        public IEnumerable<FinancialTransaction> GetFinancialTransactionsByCategoryId(int categoryId)
        {
            IEnumerable<FinancialTransaction> financialTransactions = FinancialTransactionsRepository.Get().Where(financialTransaction => financialTransaction.CategoryId == categoryId);
            return financialTransactions;
        }
    }
}

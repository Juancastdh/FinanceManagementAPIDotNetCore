using FinanceManagement.BusinessLogic.Implementations;
using FinanceManagement.BusinessLogic.Models.Enums;
using FinanceManagement.DataRepository.Models;
using FinanceManagement.Tests.FakeDataAccess;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinanceManagement.Tests
{
    public class FinancialTransactionsManagerTests
    {
        private FakeFinancialTransactionsDataAccess FakeFinancialTransactionsDataAccess;
        private FinancialTransactionsManager FinancialTransactionsManager;

        [SetUp]
        public void Setup()
        {
            FakeFinancialTransactionsDataAccess = new FakeFinancialTransactionsDataAccess();
            FinancialTransactionsManager = new FinancialTransactionsManager(FakeFinancialTransactionsDataAccess);
        }

        [Test]
        public void IncomeReportTransactions_AreOrderedByDate()
        {

            FinancialTransaction financialTransaction1 = new FinancialTransaction
            {
                Id = 1,
                IsExpense = false,
                Date = DateTime.Now.AddDays(-5)
            };
            FinancialTransaction financialTransaction2 = new FinancialTransaction
            {
                Id = 2,
                IsExpense = false,
                Date = DateTime.Now.AddMonths(-6)
            };
            FinancialTransaction financialTransaction3 = new FinancialTransaction
            {
                Id = 3,
                IsExpense = false,
                Date = DateTime.Now
            };
            IEnumerable<FinancialTransaction> expectedFinancialTransactions = new List<FinancialTransaction>
            {
                financialTransaction1,
                financialTransaction2,
                financialTransaction3
            };
            expectedFinancialTransactions = expectedFinancialTransactions.OrderBy(financialTransaction => financialTransaction.Date);

            FakeFinancialTransactionsDataAccess.Add(financialTransaction1);
            FakeFinancialTransactionsDataAccess.Add(financialTransaction2);
            FakeFinancialTransactionsDataAccess.Add(financialTransaction3);

            FinancialTransactionsReport obtainedFinancialTransactionsReport = FinancialTransactionsManager.GetIncomeFinancialTransactionsReport();
            IEnumerable<FinancialTransaction> obtainedFinancialTransactions = obtainedFinancialTransactionsReport.FinancialTransactions;

            CollectionAssert.AreEqual(expectedFinancialTransactions, obtainedFinancialTransactions);

        }


        [Test]
        public void IncomeReportTransactions_OnlyContainIncomes()
        {

            FinancialTransaction incomeTransaction1 = new FinancialTransaction
            {
                Id = 1,
                IsExpense = false,
            };
            FinancialTransaction incomeTransaction2 = new FinancialTransaction
            {
                Id = 3,
                IsExpense = false
            };
            List<FinancialTransaction> expectedFinancialTransactions = new List<FinancialTransaction>
            {
                incomeTransaction1,
                incomeTransaction2
            };

            FakeFinancialTransactionsDataAccess.Add(incomeTransaction1);
            FakeFinancialTransactionsDataAccess.Add(incomeTransaction2);
            FakeFinancialTransactionsDataAccess.Add(new FinancialTransaction
            {
                Id = 2,
                IsExpense = true
            });
            FakeFinancialTransactionsDataAccess.Add(new FinancialTransaction
            {
                Id = 4,
                IsExpense = true
            });


            FinancialTransactionsReport obtainedFinancialTransactionsReport = FinancialTransactionsManager.GetIncomeFinancialTransactionsReport();
            IEnumerable<FinancialTransaction> obtainedFinancialTransactions = obtainedFinancialTransactionsReport.FinancialTransactions.ToList();

            CollectionAssert.AreEquivalent(expectedFinancialTransactions, obtainedFinancialTransactions);

        }


        [Test]
        public void IncomeReportTotalValue_IsSumOfIncomeValues()
        {

            decimal expectedValue = 500;

            FakeFinancialTransactionsDataAccess.Add(new FinancialTransaction
            {
                Id = 1,
                Value = 150
            });
            FakeFinancialTransactionsDataAccess.Add(new FinancialTransaction
            {
                Id = 2,
                Value = 100
            });
            FakeFinancialTransactionsDataAccess.Add(new FinancialTransaction
            {
                Id = 3,
                Value = 250
            });

            FinancialTransactionsReport obtainedFinancialTransactionsReport = FinancialTransactionsManager.GetIncomeFinancialTransactionsReport();
            decimal obtainedValue = obtainedFinancialTransactionsReport.TotalValue;

            Assert.AreEqual(expectedValue, obtainedValue);

        }


        [Test]
        [TestCase(FinancialTransactionType.Income, false)]
        [TestCase(FinancialTransactionType.Expense, true)]
        public void GetFinancialTransactions_Returns_FinancialTransactions_Of_Specified_Type_Only(FinancialTransactionType financialTransactionType, bool isFinancialTransactionExpense)
        {

            FinancialTransaction financialTransaction1 = new FinancialTransaction
            {
                Id = 1,
                IsExpense = isFinancialTransactionExpense,
            };
            FinancialTransaction financialTransaction2 = new FinancialTransaction
            {
                Id = 3,
                IsExpense = isFinancialTransactionExpense
            };

            List<FinancialTransaction> expectedFinancialTransactions = new List<FinancialTransaction>
            {
                financialTransaction1,
                financialTransaction2
            };

            FakeFinancialTransactionsDataAccess.Add(financialTransaction1);
            FakeFinancialTransactionsDataAccess.Add(financialTransaction2);
            FakeFinancialTransactionsDataAccess.Add(new FinancialTransaction
            {
                Id = 2,
                IsExpense = !isFinancialTransactionExpense
            });
            FakeFinancialTransactionsDataAccess.Add(new FinancialTransaction
            {
                Id = 4,
                IsExpense = !isFinancialTransactionExpense
            });


            IEnumerable<FinancialTransaction> obtainedFinancialTransactions = FinancialTransactionsManager.GetFinancialTransactionsByType(financialTransactionType);

            CollectionAssert.AreEquivalent(expectedFinancialTransactions, obtainedFinancialTransactions);
        }

      

    }
}

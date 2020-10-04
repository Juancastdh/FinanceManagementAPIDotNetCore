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
        private FakeCategoriesDataAccess FakeCategoriesDataAccess;
        private FinancialTransactionsManager FinancialTransactionsManager;

        [SetUp]
        public void Setup()
        {
            FakeFinancialTransactionsDataAccess = new FakeFinancialTransactionsDataAccess();
            FakeCategoriesDataAccess = new FakeCategoriesDataAccess();
            FinancialTransactionsManager = new FinancialTransactionsManager(FakeFinancialTransactionsDataAccess, FakeCategoriesDataAccess);
        }

        [Test]
        [TestCase(FinancialTransactionType.Income, false)]
        [TestCase(FinancialTransactionType.Expense, true)]
        public void GetFinancialTransactionsReport_Transactions_AreOrderedByDate(FinancialTransactionType financialTransactionType, bool isFinancialTransactionExpense)
        {

            FinancialTransaction financialTransaction1 = new FinancialTransaction
            {
                Id = 1,
                IsExpense = isFinancialTransactionExpense,
                Date = DateTime.Now.AddDays(-5)
            };
            FinancialTransaction financialTransaction2 = new FinancialTransaction
            {
                Id = 2,
                IsExpense = isFinancialTransactionExpense,
                Date = DateTime.Now.AddMonths(-6)
            };
            FinancialTransaction financialTransaction3 = new FinancialTransaction
            {
                Id = 3,
                IsExpense = isFinancialTransactionExpense,
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

            FinancialTransactionsReport obtainedFinancialTransactionsReport = FinancialTransactionsManager.GetFinancialTransactionsReportByType(financialTransactionType);
            IEnumerable<FinancialTransaction> obtainedFinancialTransactions = obtainedFinancialTransactionsReport.FinancialTransactions;

            CollectionAssert.AreEqual(expectedFinancialTransactions, obtainedFinancialTransactions);

        }


        [Test]
        [TestCase(FinancialTransactionType.Income, false)]
        [TestCase(FinancialTransactionType.Expense, true)]
        public void GetFinancialTransactionsReport_Contains_FinancialTransactions_Of_Specified_Type_Only(FinancialTransactionType financialTransactionType, bool isFinancialTransactionExpense)
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


            FinancialTransactionsReport obtainedFinancialTransactionsReport = FinancialTransactionsManager.GetFinancialTransactionsReportByType(financialTransactionType);
            IEnumerable<FinancialTransaction> obtainedFinancialTransactions = obtainedFinancialTransactionsReport.FinancialTransactions.ToList();

            CollectionAssert.AreEquivalent(expectedFinancialTransactions, obtainedFinancialTransactions);

        }


        [Test]
        [TestCase(FinancialTransactionType.Income, false)]
        [TestCase(FinancialTransactionType.Expense, true)]
        public void GetFinancialTransactionsReport_TotalValue_Is_Sum_Of_All_FinancialTransaction_Values(FinancialTransactionType financialTransactionType, bool isFinancialTransactionExpense)
        {

            decimal expectedValue = 500;

            FakeFinancialTransactionsDataAccess.Add(new FinancialTransaction
            {
                Id = 1,
                Value = 150,
                IsExpense = isFinancialTransactionExpense
            });
            FakeFinancialTransactionsDataAccess.Add(new FinancialTransaction
            {
                Id = 2,
                Value = 100,
                IsExpense = isFinancialTransactionExpense
            });
            FakeFinancialTransactionsDataAccess.Add(new FinancialTransaction
            {
                Id = 3,
                Value = 250,
                IsExpense = isFinancialTransactionExpense
            });

            FinancialTransactionsReport obtainedFinancialTransactionsReport = FinancialTransactionsManager.GetFinancialTransactionsReportByType(financialTransactionType);
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

        [Test]
        public void GetTotalFinancialTransactionsReport_Transactions_AreOrderedByDate()
        {
            FinancialTransaction financialTransaction1 = new FinancialTransaction
            {
                Id = 1,
                IsExpense = true,
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
                IsExpense = true,
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

            FinancialTransactionsReport obtainedFinancialTransactionsReport = FinancialTransactionsManager.GetTotalFinancialTransactionsReport();
            IEnumerable<FinancialTransaction> obtainedFinancialTransactions = obtainedFinancialTransactionsReport.FinancialTransactions;

            CollectionAssert.AreEqual(expectedFinancialTransactions, obtainedFinancialTransactions);
        }

        [Test]
        public void GetTotalFinancialTransactionsReport_Contains_Transactions_Of_Both_Types()
        {

            FinancialTransaction financialTransaction1 = new FinancialTransaction
            {
                Id = 1,
                IsExpense = true,
            };
            FinancialTransaction financialTransaction2 = new FinancialTransaction
            {
                Id = 2,
                IsExpense = false
            };
            FinancialTransaction financialTransaction3 = new FinancialTransaction
            {
                Id = 3,
                IsExpense = true
            };
            FinancialTransaction financialTransaction4 = new FinancialTransaction
            {
                Id = 4,
                IsExpense = false
            };

            List<FinancialTransaction> expectedFinancialTransactions = new List<FinancialTransaction>
            {
                financialTransaction1,
                financialTransaction2,
                financialTransaction3,
                financialTransaction4
            };

            FakeFinancialTransactionsDataAccess.Add(financialTransaction1);
            FakeFinancialTransactionsDataAccess.Add(financialTransaction2);
            FakeFinancialTransactionsDataAccess.Add(financialTransaction3);
            FakeFinancialTransactionsDataAccess.Add(financialTransaction4);


            FinancialTransactionsReport obtainedFinancialTransactionsReport = FinancialTransactionsManager.GetTotalFinancialTransactionsReport();
            IEnumerable<FinancialTransaction> obtainedFinancialTransactions = obtainedFinancialTransactionsReport.FinancialTransactions.ToList();

            CollectionAssert.AreEquivalent(expectedFinancialTransactions, obtainedFinancialTransactions);
        }

        [Test]
        public void GetTotalFinancialTransactionsReport_TotalValue_Is_TotalIncomValue_Minus_TotalExpenseValue()
        {
            decimal expectedValue = 1000;
            FakeFinancialTransactionsDataAccess.Add(new FinancialTransaction
            {
                Id = 1,
                Value = 1000,
                IsExpense = false
            });
            FakeFinancialTransactionsDataAccess.Add(new FinancialTransaction
            {
                Id = 2,
                Value = 1500,
                IsExpense = false
            });
            FakeFinancialTransactionsDataAccess.Add(new FinancialTransaction
            {
                Id = 3,
                Value = 500,
                IsExpense = true
            });
            FakeFinancialTransactionsDataAccess.Add(new FinancialTransaction
            {
                Id = 4,
                Value = 1000,
                IsExpense = true
            });

            FinancialTransactionsReport obtainedFinancialTransactionsReport = FinancialTransactionsManager.GetTotalFinancialTransactionsReport();
            decimal obtainedValue = obtainedFinancialTransactionsReport.TotalValue;

            Assert.AreEqual(expectedValue, obtainedValue);

        }

        [Test]
        public void GetFinancialTransactionsByCategoryId_Returns_FinancialTransactions_Of_Specified_Category_Only()
        {
            FinancialTransaction financialTransaction1 = new FinancialTransaction
            {
                Id = 1,
                CategoryId = 1
            };
            FinancialTransaction financialTransaction2 = new FinancialTransaction
            {
                Id = 2,
                CategoryId = 1
            };

            IEnumerable<FinancialTransaction> expectedFinancialTransactions = new List<FinancialTransaction>
            {
                financialTransaction1,
                financialTransaction2
            };

            FakeFinancialTransactionsDataAccess.Add(financialTransaction1);
            FakeFinancialTransactionsDataAccess.Add(financialTransaction2);
            FakeFinancialTransactionsDataAccess.Add(new FinancialTransaction
            {
                Id = 3,
                CategoryId = 2
            });
            FakeFinancialTransactionsDataAccess.Add(new FinancialTransaction
            {
                Id = 4,
                CategoryId = 3
            });

            IEnumerable<FinancialTransaction> obtainedFinancialTransactions = FinancialTransactionsManager.GetFinancialTransactionsByCategoryId(1);

            CollectionAssert.AreEquivalent(expectedFinancialTransactions, obtainedFinancialTransactions);

        }




    }
}

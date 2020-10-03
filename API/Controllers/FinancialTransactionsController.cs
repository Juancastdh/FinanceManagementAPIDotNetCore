using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceManagement.API.Models;
using FinanceManagement.BusinessLogic.Managers;
using FinanceManagement.DataRepository.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManagement.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [ProducesResponseType(typeof(ErrorResponse), 500)]
    public class FinancialTransactionsController : ControllerBase
    {
        private readonly IFinancialTransactionsManager FinancialTransactionsManager;

        public FinancialTransactionsController(IFinancialTransactionsManager financialTransactionsManager)
        {
            FinancialTransactionsManager = financialTransactionsManager;
        }


        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<FinancialTransaction>), 200)]
        public IActionResult GetFinancialTransactions()
        {
            IEnumerable<FinancialTransaction> financialTransactions = FinancialTransactionsManager.GetFinancialTransactions();
            return Ok(financialTransactions);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(FinancialTransaction), 200)]
        public IActionResult GetFinancialTransactionById(int id)
        {
            FinancialTransaction financialTransaction = FinancialTransactionsManager.GetFinancialTransactionById(id);

            return Ok(financialTransaction);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        public IActionResult AddFinancialTransaction([FromBody]FinancialTransaction financialTransaction)
        {
            FinancialTransactionsManager.AddFinancialTransaction(financialTransaction);
            return CreatedAtAction(nameof(GetFinancialTransactionById), new { id = financialTransaction.Id });
        }

        [HttpGet("Incomes")]
        [ProducesResponseType(typeof(IEnumerable<FinancialTransaction>), 200)]
        public IActionResult GetIncomeFinancialTransactions()
        {
            IEnumerable<FinancialTransaction> incomeFinancialTransactions = FinancialTransactionsManager.GetIncomeFinancialTransactions();
            return Ok(incomeFinancialTransactions);
        }

        [HttpGet("Incomes/Report")]
        [ProducesResponseType(typeof(FinancialTransactionsReport), 200)]
        public IActionResult GetIncomeFinancialTransactionsReport()
        {
            FinancialTransactionsReport incomeFinancialTransactionsReport = FinancialTransactionsManager.GetIncomeFinancialTransactionsReport();
            return Ok(incomeFinancialTransactionsReport);
        }

        [HttpPut]
        [ProducesResponseType(200)]
        public IActionResult UpdateFinancialTransaction([FromBody]FinancialTransaction financialTransaction)
        {
            FinancialTransactionsManager.UpdateFinancialTransaction(financialTransaction);
            return Ok();
        }


    }
}

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
    public class PeriodsController : ControllerBase
    {
        private readonly IPeriodsManager PeriodsManager;

        public PeriodsController(IPeriodsManager periodsManager)
        {
            PeriodsManager = periodsManager;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Period>), 200)]
        public IActionResult GetPeriods()
        {
            IEnumerable<Period> periods = PeriodsManager.GetPeriods();

            return Ok(periods);
        }
    }
}

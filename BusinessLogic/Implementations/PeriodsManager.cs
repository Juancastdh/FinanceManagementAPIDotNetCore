using FinanceManagement.BusinessLogic.Managers;
using FinanceManagement.DataRepository;
using FinanceManagement.DataRepository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManagement.BusinessLogic.Implementations
{
    public class PeriodsManager : IPeriodsManager
    {

        private readonly IPeriodsRepository PeriodsRepository;

        public PeriodsManager(IPeriodsRepository periodsRepository)
        {
            PeriodsRepository = periodsRepository;
        }

        public IEnumerable<Period> GetPeriods()
        {
            IEnumerable<Period> periods = PeriodsRepository.Get();

            return periods;
        }
    }
}

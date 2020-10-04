using FinanceManagement.DataRepository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManagement.BusinessLogic.Managers
{
    public interface IPeriodsManager
    {
        IEnumerable<Period> GetPeriods();
    }
}

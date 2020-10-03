using FinanceManagement.DataRepository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManagement.DataRepository
{
    public interface IPeriodsRepository
    {
        IEnumerable<Period> Get();
        void Add(Period period);
        void Update(Period period);
        void Delete(Period period);
        void DeleteAll();
        void AddMany(IEnumerable<Period> periods);
    }
}

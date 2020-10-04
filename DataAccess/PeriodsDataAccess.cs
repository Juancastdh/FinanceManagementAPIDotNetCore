using FinanceManagement.DataAccess.Context;
using FinanceManagement.DataRepository;
using FinanceManagement.DataRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinanceManagement.DataAccess
{
    public class PeriodsDataAccess: IPeriodsRepository
    {

        private readonly FinanceManagementContext DatabaseContext;

        public PeriodsDataAccess(FinanceManagementContext databaseContext)
        {
            DatabaseContext = databaseContext;
        }

        public void Add(Period period)
        {
            DatabaseContext.Database.EnsureCreated();

            using (var transaction = DatabaseContext.Database.BeginTransaction())
            {
                try
                {
                    DatabaseContext.Periods.Add(period);
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

        public void AddMany(IEnumerable<Period> periods)
        {
            DatabaseContext.Database.EnsureCreated();
            using (var transaction = DatabaseContext.Database.BeginTransaction())
            {
                try
                {
                    DatabaseContext.Periods.AddRange(periods);
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

        public void Delete(Period period)
        {
            DatabaseContext.Database.EnsureCreated();
            using (var transaction = DatabaseContext.Database.BeginTransaction())
            {
                try
                {
                    DatabaseContext.Periods.Remove(period);
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
                    DatabaseContext.Periods.RemoveRange(DatabaseContext.Periods);
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

        public IEnumerable<Period> Get()
        {
            DatabaseContext.Database.EnsureCreated();

            List<Period> periods = DatabaseContext.Periods.ToList();

            return periods;
        }

        public void Update(Period period)
        {
            DatabaseContext.Database.EnsureCreated();

            using (var transaction = DatabaseContext.Database.BeginTransaction())
            {
                try
                {
                    Period updatedPeriod = DatabaseContext.Periods.SingleOrDefault(p => p.Id == period.Id);
                    if (updatedPeriod != null)
                    {
                        updatedPeriod = period;
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

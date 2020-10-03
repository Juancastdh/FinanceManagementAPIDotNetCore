using FinanceManagement.DataAccess.Context;
using FinanceManagement.DataRepository;
using FinanceManagement.DataRepository.Models;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinanceManagement.DataAccess
{
    public class CategoriesDataAccess : ICategoriesRepository
    {

        private readonly FinanceManagementContext DatabaseContext;

        public CategoriesDataAccess(FinanceManagementContext databaseContext)
        {
            DatabaseContext = databaseContext;
        }

        public void Add(Category category)
        {

            DatabaseContext.Database.EnsureCreated();

            using(var transaction = DatabaseContext.Database.BeginTransaction())
            {
                try
                {
                    DatabaseContext.Categories.Add(category);
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

        public void AddMany(IEnumerable<Category> categories)
        {
            throw new NotImplementedException();
        }

        public void Delete(Category category)
        {

            DatabaseContext.Database.EnsureCreated();

            using (var transaction = DatabaseContext.Database.BeginTransaction())
            {
                try
                {

                    DatabaseContext.Categories.Remove(category);
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
                    DatabaseContext.Categories.RemoveRange(DatabaseContext.Categories);
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

        public IEnumerable<Category> Get()
        {
            DatabaseContext.Database.EnsureCreated();

            List<Category> categories = DatabaseContext.Categories.ToList();      
           
            return categories;
        }

        public Category GetById(int id)
        {
            Category category;

            DatabaseContext.Database.EnsureCreated();
                    

            category = DatabaseContext.Categories.Find(id);

            return category;
        }

        public void Update(Category category)
        {

            DatabaseContext.Database.EnsureCreated();

            using(var transaction = DatabaseContext.Database.BeginTransaction())
            {
                try
                {
                    Category updatedCategory = DatabaseContext.Categories.SingleOrDefault(p => p.Id == category.Id);
                    if (updatedCategory != null)
                    {
                        updatedCategory = category;
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

using FinanceManagement.DataRepository;
using FinanceManagement.DataRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinanceManagement.Tests.FakeDataAccess
{
    public class FakeCategoriesDataAccess: ICategoriesRepository
    {
        public List<Category> Categories { get; set; }

        public FakeCategoriesDataAccess()
        {
            Categories = new List<Category>();
        }

        public IEnumerable<Category> Get()
        {
            return Categories;
        }

        public Category GetById(int id)
        {
            return Categories.SingleOrDefault(category => category.Id == id);
        }

        public void Add(Category category)
        {
            Categories.Add(category);
        }

        public void Update(Category category)
        {
            Categories[Categories.FindIndex(c => c.Id == category.Id)] = category;
        }

        public void Delete(Category category)
        {
            Categories.Remove(Categories.Find(c => c.Id == category.Id));
        }

        public void DeleteAll()
        {
            Categories.Clear();
        }

        public void AddMany(IEnumerable<Category> categories)
        {
            Categories.AddRange(categories);
        }
    }
}

using FinanceManagement.DataRepository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManagement.DataRepository
{
    public interface ICategoriesRepository
    {
        IEnumerable<Category> Get();
        Category GetById(int id);
        void Add(Category category);
        void Update(Category category);
        void Delete(Category category);
        void DeleteAll();
        void AddMany(IEnumerable<Category> categories);
    }
}

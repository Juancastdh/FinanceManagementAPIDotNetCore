using FinanceManagement.BusinessLogic.Managers;
using FinanceManagement.DataRepository;
using FinanceManagement.DataRepository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManagement.BusinessLogic.Implementations
{
    public class CategoriesManager: ICategoriesManager
    {
        private readonly ICategoriesRepository CategoriesRepository;

        public CategoriesManager(ICategoriesRepository categoriesRepository)
        {
            CategoriesRepository = categoriesRepository;
        }

        public IEnumerable<Category> GetCategories()
        {
            IEnumerable<Category> categories = CategoriesRepository.Get();
            return categories;
        }

        public void AddCategory(Category category)
        {
            CategoriesRepository.Add(category);
        }
    }
}

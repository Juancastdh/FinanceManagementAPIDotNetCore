using FinanceManagement.DataRepository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManagement.BusinessLogic.Managers
{
    public interface ICategoriesManager
    {
        IEnumerable<Category> GetCategories();
        void AddCategory(Category category);
    }
}

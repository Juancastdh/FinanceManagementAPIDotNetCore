using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceManagement.API.Models;
using FinanceManagement.BusinessLogic.Managers;
using FinanceManagement.DataRepository.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FinanceManagementAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ProducesResponseType(typeof(ErrorResponse), 500)]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesManager CategoriesManager;

        public CategoriesController(ICategoriesManager categoriesManager)
        {
            CategoriesManager = categoriesManager;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Category>), 200)]
        public IActionResult GetCategories()
        {
            IEnumerable<Category> categories = CategoriesManager.GetCategories();

            return Ok(categories);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult AddCategory([FromBody]Category category)
        {
            CategoriesManager.AddCategory(category);
            return Ok();
        }
    }
}

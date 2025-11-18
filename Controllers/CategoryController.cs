using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskApi.Dtos;
using TaskApi.Interfaces;

namespace TaskApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {

        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        // fetch all categories 
        [HttpGet("get-user-categories")]

        public async Task<IActionResult> getAllCategories()
        {
            var categories = await _categoryService.GetUserCategories();

            return Ok(categories);
        }
        [HttpGet("{categoryId}/with-tasks")]
        public async Task<IActionResult> getAllUserCategoriesWithTasks(string categoryId)
        {
            try
            {
                var categoryWithTasks = await _categoryService.GetCategoryWithTasks(categoryId);

                if (categoryWithTasks == null)
                {
                    return NotFound();
                }

                return Ok(categoryWithTasks);
            }

            catch (Exception ex)
            {
                return StatusCode(500, "Error retrieving category with tasks ");
            }
        }



    }
}
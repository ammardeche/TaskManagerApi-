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

        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetUserCategories();

            return Ok(categories);
        }
        [HttpGet("{categoryId}/with-tasks")]
        public async Task<IActionResult> GetAllUserCategoriesWithTasks(string categoryId)
        {
            try
            {
                var categoryEntity = await _categoryService.GetCategoryWithTasks(categoryId);

                if (categoryEntity == null)
                {
                    return NotFound();
                }

                var categoryWithTasks = new CategoryWithTasksDto(categoryEntity);

                return Ok(categoryWithTasks);
            }

            catch (Exception ex)
            {
                return StatusCode(500, "Error retrieving category with tasks ");
            }
        }

        [HttpPost("create-category")]

        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto createCategoryDto)
        {
            await _categoryService.CreateCategory(name: createCategoryDto.Name);
            return Ok("The category has been created ");
        }

        [HttpDelete("{categoryId}")]

        public async Task<IActionResult> DeleteCategory(string categoryId)
        {
            await _categoryService.DeleteCategory(categoryId);

            return Ok("the category has been deleted successfully");
        }

    }
}
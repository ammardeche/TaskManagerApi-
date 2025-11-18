using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Identity.Client;
using TaskApi.Data;
using TaskApi.Interfaces;
using TaskApi.Models;

namespace TaskApi.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context, ICurrentUserService currentUserService, ICategoryRepository categoryRepository)
        {
            _context = context;
            _currentUserService = currentUserService;
            _categoryRepository = categoryRepository;
        }

        public async Task<Category> CreateCategory(string name)
        {

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Category Name is required");
            }

            if (name.Length > 20)
            {
                throw new ArgumentException("The name should be contain less than 20 character");
            }

            var user_id = _currentUserService.GetUserId();

            var category = new Category
            {
                Id = Guid.NewGuid().ToString(),
                Name = name,
                UserId = user_id,
                IsDefault = false,
            };

            await _categoryRepository.AddCategory(category);
            await _context.SaveChangesAsync();

            return category;  // here i have a question why we return a category here 

        }

        public async Task DeleteCategory(string categoryId)
        {
            if (string.IsNullOrEmpty(categoryId))
            {
                throw new ArgumentException(" the category id it should be updated ");
            }

            var user_id = _currentUserService.GetUserId();

            // we need to check if the category is exist 

            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);

            if (category == null)
            {
                throw new KeyNotFoundException("this category doesn't exist");
            }

            if (user_id != category.UserId)
            {
                throw new UnauthorizedAccessException("you don't have a permission to access to this category ");
            }

            await _categoryRepository.DeleteCategory(category: category);
            await _context.SaveChangesAsync();
        }

        public async Task<Category> GetCategoryWithTasks(string categoryId)
        {
            // here we need to check if the category id is null or empty 
            if (string.IsNullOrEmpty(categoryId))
                throw new ArgumentException("the category id cannot be null or empty");

            var user_id = _currentUserService.GetUserId();

            var hasAccess = await _categoryRepository.CategoryExists(categoryId, user_id);

            if (!hasAccess)
                throw new UnauthorizedAccessException("you don't have access to this task");

            var category = await _categoryRepository.getAllCategoriesWithTasks(categoryId: categoryId, userId: user_id);

            if (category == null)
            {
                throw new KeyNotFoundException("the category cannot be found");
            }
            return category;
        }

        public async Task<List<Category>> GetUserCategories()
        {
            var user_id = _currentUserService.GetUserId();
            return await _categoryRepository.GetUserCategories(user_id);
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public Task DeleteCategory(string categoryId)
        {
            throw new NotImplementedException();
        }

        public async Task<Category> GetCategoryWithTasks(string categoryId)
        {
            // here we need to check if the task already exist 
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

        public Task<List<Category>> GetUserCategories()
        {
            return null;
        }
    }



}
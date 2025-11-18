using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskApi.Data;
using TaskApi.Models;

namespace TaskApi.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        // add category 
        public async Task<Category?> AddCategory(Category category)
        {
            await _context.Categories.AddAsync(category);
            return category;
        }

        // check if the category already exist 
        public async Task<bool> CategoryExists(string categoryId, string userId) =>
         await _context.Categories.AnyAsync(c => c.Id == categoryId && (c.UserId == null || c.UserId == userId));

        // delete category
        public async Task DeleteCategory(Category category) =>
        _context.Categories.Remove(category);

        public async Task<Category> getAllCategoriesWithTasks(string categoryId, string userId) =>
        await _context.Categories
        .Include(t => t.TaskItems)
        .Where(p => p.Id == categoryId && (p.UserId == userId || p.UserId == null)).FirstOrDefaultAsync();

        public async Task<List<Category>> GetUserCategories(string userId)
        => await _context.Categories
             .Include(t => t.TaskItems)
             .Where(c => c.UserId == null || c.UserId == userId)
            .OrderBy(c => c.UserId) // Defaults first
            .ThenBy(c => c.Name)
            .ToListAsync();
    }
}
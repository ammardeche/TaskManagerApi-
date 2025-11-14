using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskApi.Models;

namespace TaskApi.Interfaces
{
    public interface ICategoryService
    {
        Task<List<Category>> GetUserCategories();
        Task<Category> CreateCategory(string name);
        Task DeleteCategory(string categoryId);
        Task<Category> GetCategoryWithTasks(string categoryId);
    }
}
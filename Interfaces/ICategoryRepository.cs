using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskApi.Models
{
    public interface ICategoryRepository
    {

        Task<List<Category>> GetUserCategories(string userId);
        Task<Category?> AddCategory(Category category);
        Task DeleteCategory(Category category);
        Task<Category> getAllCategoriesWithTasks(string categoryId, string userId);
        Task<bool> CategoryExists(string categoryId, string userId);



    }
}

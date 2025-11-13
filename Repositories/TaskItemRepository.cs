using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskApi.Data;
using TaskApi.Interfaces;
using TaskApi.Models;

namespace TaskApi.Repositories
{
    public class TaskItemRepository : ITaskItemRepository
    {
        private readonly ApplicationDbContext _context;

        public TaskItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // add task
        public async Task AddTask(TaskItem task) => await _context.TaskItems.AddAsync(task);
        // delete task
        public async Task DeleteTask(TaskItem task) => _context.TaskItems.Remove(task);
        // get all tasks
        public async Task<List<TaskItem>> GetAllTasks(string userId) => await _context.TaskItems.Include(c => c.Category).Where(t => t.UserId == userId).ToListAsync();
        // get task by id
        public async Task<TaskItem?> GetTaskById(string taskId)
        {
            return await _context.TaskItems.AsNoTracking()
            .Include(c => c.Category)
            .FirstOrDefaultAsync(i => i.Id == taskId);
        }
        // update task
        public async Task UpdateTask(TaskItem task) => _context.TaskItems.Update(task);
    }
}
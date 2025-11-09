using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task AddTask(TaskItem task)
        {
            await _context.TaskItems.AddAsync(task);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTask(TaskItem task)
        {
            _context.TaskItems.Remove(task);
            await _context.SaveChangesAsync();
        }

        public async Task<List<TaskItem>> GetAllTasks(string userId)
        {
            var tasks = _context.TaskItems.Where(t => t.UserId == userId).ToList();
            return tasks;
        }

        public async Task<TaskItem?> GetTaskById(string taskId)
        {
            var task = await _context.TaskItems.FindAsync(taskId);
            return task;
        }

        public async Task UpdateTask(TaskItem task)
        {
            _context.TaskItems.Update(task);
            await _context.SaveChangesAsync();
        }
    }
}
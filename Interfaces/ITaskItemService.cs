using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskApi.Models;

namespace TaskApi.Interfaces
{
    public interface ITaskItemService
    {
        Task<List<TaskItem>> GetAllTasks();
        Task<TaskItem?> GetTaskById(string taskId);
        Task AddTask(string title, string description, DateTime createdAt, DateTime? dueDate, string categoryId, TaskStatus status);
        Task UpdateTask(string title, string description, DateTime createdAt, DateTime? dueDate, string categoryId, string status);
        Task DeleteTask(string taskId);
    }
}
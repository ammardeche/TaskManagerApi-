using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskApi.Enums;
using TaskApi.Models;

namespace TaskApi.Interfaces
{
    public interface ITaskItemService
    {
        Task<List<TaskItem>> GetAllTasks();
        Task<TaskItem?> GetTaskById(string taskId);
        Task AddTask(string title, string description, DateTime startDate, DateTime? dueDate, string categoryId, TaskItemStatus status);
        Task UpdateTask(string taskId, string title, string description, DateTime startDate, DateTime? dueDate, string categoryId, TaskItemStatus status);
        Task DeleteTask(string taskId);
    }
}
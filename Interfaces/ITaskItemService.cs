using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskApi.Models;

namespace TaskApi.Interfaces
{
    public interface ITaskItemService
    {
        Task<List<TaskItem>> GetAllTasks(string userId);
        Task<TaskItem?> GetTaskById(string taskId, string userId);
        Task AddTask(TaskItem task, string userId);
        Task UpdateTask(TaskItem task, string userId);
        Task DeleteTask(string taskId, string userId);
    }
}
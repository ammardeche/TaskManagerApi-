using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskApi.Models;

namespace TaskApi.Interfaces
{
    public interface ITaskItemRepository
    {
        Task<List<TaskItem>> GetAllTasks(string userId);
        Task<TaskItem?> GetTaskById(string taskId);
        Task AddTask(TaskItem task);
        Task UpdateTask(TaskItem task);
        Task DeleteTask(TaskItem task);

        // endpoint for the filtered tasks
        Task<List<TaskItem>> GetInProgressTasks(string UserId);
        Task<List<TaskItem>> GetToDoTasks(string UserId);
        Task<List<TaskItem>> GetCompletedTasks(string UserId);

    }
}
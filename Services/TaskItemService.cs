using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TaskApi.Data;
using TaskApi.Interfaces;
using TaskApi.Models;

namespace TaskApi.Services
{
    public class TaskItemService : ITaskItemService
    {
        private readonly ITaskItemRepository _taskItemRepository;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly ICurrentUserService _currentUserService;
        public TaskItemService(ICurrentUserService currentUserService, ITaskItemRepository taskItemRepository, ApplicationDbContext context, UserManager<User> userManager)
        {
            _taskItemRepository = taskItemRepository;
            _context = context;
            _userManager = userManager;
            _currentUserService = currentUserService;
        }

        public async Task AddTask(string title, string description, DateTime createdAt, DateTime? dueDate, string categoryId, TaskStatus status)
        {
            var user_id = _currentUserService.GetUserId();


        }

        public async Task DeleteTask(string taskId)
        {
            var user_id = _currentUserService.GetUserId();

            var task = await _taskItemRepository.GetTaskById(taskId);

            if (task == null)
                throw new KeyNotFoundException($"Task with Id {taskId} not found");

            if (task.UserId != user_id)
                throw new UnauthorizedAccessException("cannot delete other users task ");

            await _taskItemRepository.DeleteTask(task);
            await _context.SaveChangesAsync();

        }

        public Task<List<TaskItem>> GetAllTasks()
        {
            var user_Id = _currentUserService.GetUserId();

            return _taskItemRepository.GetAllTasks(user_Id);
        }
        // get task by id 
        public async Task<TaskItem?> GetTaskById(string taskId)
        {
            // get the user id 
            var user_Id = _currentUserService.GetUserId();
            // get the task Item by id 
            var task = await _taskItemRepository.GetTaskById(taskId);

            if (task == null) throw new KeyNotFoundException($"task item with id {taskId} doesn't found");

            if (task.UserId != user_Id)
            {
                throw new UnauthorizedAccessException("Access Dined ");
            }
            return task;

        }

        public async Task UpdateTask(string title, string description, DateTime createdAt, DateTime? dueDate, string categoryId, string status)
        {
            // check if the task item already exist 
            return;
        }
    }
}
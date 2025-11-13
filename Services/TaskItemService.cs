using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TaskApi.Data;
using TaskApi.Enums;
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

        // add task 
        public async Task AddTask(string title, string description, DateTime startDate, DateTime? dueDate, string categoryId, TaskItemStatus status)
        {
            // get user id 
            var user_id = _currentUserService.GetUserId();



            if (startDate >= dueDate)
            {
                throw new ArgumentException("the start date must be before the due date ");
            }
            if (startDate >= DateTime.UtcNow.Date)
            {
                throw new ArgumentException("the start cannot be in the past  ");
            }

            // create new task 
            var task = new TaskItem
            {
                Id = Guid.NewGuid().ToString(),
                Title = title,
                Description = description,
                CategoryId = categoryId,
                CreatedAt = DateTime.UtcNow,
                DueDate = dueDate,
                StartDate = startDate,
                Status = status,
                UserId = user_id
            };

            // send the task to the repository
            await _taskItemRepository.AddTask(task);
            //  save the changes 
            await _context.SaveChangesAsync();
        }
        // delete task
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
        // get all tasks 
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
        // update all tasks 
        public async Task UpdateTask(string taskId, string title, string description, DateTime startDate, DateTime? dueDate, string categoryId, TaskItemStatus status)
        {
            var user_Id = _currentUserService.GetUserId();

            if (string.IsNullOrWhiteSpace(taskId))
            {
                throw new ArgumentException("Task id is required");
            }


            var task = await _taskItemRepository.GetTaskById(taskId);

            if (task == null)
            {
                throw new KeyNotFoundException($"task with id {taskId} doesn't exist");
            }

            if (task.UserId != user_Id)
            {
                throw new UnauthorizedAccessException("you don't have permission to update this task");
            }

            if (task.Status == TaskItemStatus.Completed)
            {
                throw new InvalidOperationException("cannot update a completed task");
            }

            if (dueDate.HasValue && dueDate < DateTime.UtcNow)
                throw new ArgumentException("Due date cannot be in the past");
            if (startDate >= dueDate)
            {
                throw new ArgumentException("the start date must be before the due date ");
            }
            if (startDate >= DateTime.UtcNow.Date)
            {
                throw new ArgumentException("the start cannot be in the past  ");
            }

            task.Title = title;
            task.Description = description;
            task.DueDate = dueDate;
            task.StartDate = startDate;
            task.CategoryId = categoryId;
            task.Status = status;

            await _taskItemRepository.UpdateTask(task);
            await _context.SaveChangesAsync();
        }
    }
}
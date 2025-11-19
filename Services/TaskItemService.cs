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
        private readonly ApplicationDbContext _context;
        private readonly ITaskItemRepository _taskItemRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly ICategoryRepository _categoryRepository;

        public TaskItemService(ICategoryRepository categoryRepository, ICurrentUserService currentUserService, ITaskItemRepository taskItemRepository, ApplicationDbContext context)
        {
            _context = context;
            _taskItemRepository = taskItemRepository;
            _currentUserService = currentUserService;
            _categoryRepository = categoryRepository;
        }

        // add task 
        public async Task AddTask(string title, string description, DateTime startDate, DateTime? dueDate, string categoryId, TaskItemStatus status)
        {
            // get user id 
            var user_id = _currentUserService.GetUserId();

            // call the reusable validation method 
            await ValidateCategoryAccess(categoryId, user_id);

            // check the date 
            if (startDate >= dueDate)
            {
                throw new ArgumentException("the start date must be before the due date ");
            }
            if (startDate >= DateTime.UtcNow.Date)
            {
                throw new ArgumentException("the start cannot be in the past");
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
            // check if the task already exist and the user has a permission to update this task
            if (task == null || task.UserId != user_Id)
            {
                throw new UnauthorizedAccessException("task not found or access denied");
            }

            // call the reusable validation method for categories

            await ValidateCategoryAccess(categoryId, user_Id);

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


        // Reusable validation method for category
        public async Task ValidateCategoryAccess(string categoryId, string userId)
        {
            var categoryExist = await _categoryRepository.CategoryExists(categoryId, userId);

            if (!categoryExist)
            {
                throw new ArgumentException("the category you are looking for doesn't exist or you don't have permission to access to this category");
            }
        }


    }
}
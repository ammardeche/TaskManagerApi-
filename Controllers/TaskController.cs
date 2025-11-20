using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using TaskApi.Dtos;
using TaskApi.Interfaces;
using TaskApi.Models;

namespace TaskApi.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TaskController : ControllerBase
    {
        // inject the task item service 

        private readonly ITaskItemService _taskItemService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IProgressCalculationService _progressCalculationService;


        public TaskController(IProgressCalculationService progressCalculationService, ITaskItemService taskItemService, ICurrentUserService currentUserService)
        {
            _taskItemService = taskItemService;
            _currentUserService = currentUserService;
            _progressCalculationService = progressCalculationService;
        }

        [HttpPost()]
        [AllowAnonymous] // Remove this in production!

        public async Task<IActionResult> CreateTask([FromBody] CreateTaskDto createTaskDto)
        {
            // try to create a task if the task fails we need to catch the error
            try
            {
                await _taskItemService.AddTask(
                     title: createTaskDto.Title,
                    categoryId: createTaskDto.CategoryId,
                     description: createTaskDto.Description,
                     startDate: createTaskDto.StartDate,
                     status: createTaskDto.Status,
                    dueDate: createTaskDto.DueDate
                 );
                return Ok("task created successfully");
            }

            catch (ArgumentException ex)
            {
                return BadRequest(ex);
            }

        }


        [HttpGet()]
        public async Task<IActionResult> GetAllTasks()
        {
            var tasks = await _taskItemService.GetAllTasks();
            var taskDto = tasks.Select(t => new TaskItemDto(t));
            return Ok(taskDto);
        }


        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteTask(string categoryId)
        {

            await _taskItemService.DeleteTask(categoryId);
            return Ok("Task has been removed ");
        }

        [HttpPut("{taskId}")]
        public async Task<IActionResult> UpdateTask([FromRoute] string taskId, [FromBody] UpdateTaskDto updateTaskDto)
        {
            await _taskItemService.UpdateTask(
                taskId: taskId,
                title: updateTaskDto.Title,
                description: updateTaskDto.Description,
                status: updateTaskDto.Status,
                dueDate: updateTaskDto.DueDate,
                startDate: updateTaskDto.StartDate,
                categoryId: updateTaskDto.CategoryId
            );
            return Ok("task has been updated ");
        }

        [HttpGet("{taskId}")]
        public async Task<IActionResult> GetTaskById(string taskId)
        {
            var task = await _taskItemService.GetTaskById(taskId);
            var taskDto = new TaskItemDto(task);
            return Ok(taskDto);
        }

        [HttpGet("statistic")]
        public async Task<IActionResult> GetTasksStats()
        {
            try
            {
                var GetTasks = await _taskItemService.GetAllTasks();

                var AllTasks = GetTasks.ToList();

                var statistics = new GetTodayProgressDto(AllTasks, _progressCalculationService);

                Console.WriteLine($"tasks =======> {AllTasks}");

                return Ok(statistics);
            }

            catch (Exception ex)
            {
                throw new Exception("....");
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskApi.Dtos;
using TaskApi.Interfaces;

namespace TaskApi.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TaskController : ControllerBase
    {
        // inject the task item service 

        private readonly ITaskItemService _taskItemService;
        private readonly ICurrentUserService _currentUserService;


        public TaskController(ITaskItemService taskItemService, ICurrentUserService currentUserService)
        {
            _taskItemService = taskItemService;
            _currentUserService = currentUserService;
        }

        [HttpPost("create-task")]
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


        // [NonAction]
        // // public async Task<IActionResult> GetAllTasks()
        // // {
        // //     var userId =  _currentUserService.GetUserId();
        // //     await _taskItemService.GetAllTasks(userId);
        // // }


        [NonAction]
        public async Task<IActionResult> DeleteTask()
        {
            return null!;
        }

        [NonAction]
        public async Task<IActionResult> UpdateTask()
        {
            return null!;
        }

        [NonAction]
        public async Task<IActionResult> GetTaskById()
        {
            return null!;
        }
    }
}
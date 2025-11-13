using System;
using System.Collections.Generic;
using System.Linq;
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

        public TaskController(ITaskItemService taskItemService)
        {
            _taskItemService = taskItemService;
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


    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskApi.Interfaces;
using TaskApi.Models;

namespace TaskApi.Dtos
{
    public class CategoryWithTasksDto : ICategoryWithTasks
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public List<TaskItemDto> TaskItems { get; set; } = new List<TaskItemDto>();

        public CategoryWithTasksDto(Category category)
        {
            if (category == null) return;

            Id = category.Id;
            Name = category.Name;

            if (category.TaskItems != null)
            {
                TaskItems = category.TaskItems
                    .Select(t => new TaskItemDto(t)) // Each TaskItemDto maps itself
                    .ToList();
            }
        }
    }
}
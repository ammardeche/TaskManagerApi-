using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskApi.Enums;
using TaskApi.Models;

namespace TaskApi.Dtos
{
    public class TaskItemDto
    {
        public string Id { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string? TaskCategory { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime StartDate { get; set; }
        public TaskItemStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public TaskItemDto(TaskItem taskItem)
        {
            if (taskItem == null) return;

            Id = taskItem.Id;
            Title = taskItem.Title;
            Description = taskItem.Description;
            DueDate = taskItem.DueDate;
            TaskCategory = taskItem.Category?.Name;
            StartDate = taskItem.StartDate;
            Status = taskItem.Status;
            CreatedAt = taskItem.CreatedAt;
        }
    }
}
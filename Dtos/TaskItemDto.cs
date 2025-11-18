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
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime StartDate { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public TaskItemDto(TaskItem taskItem)
        {
            if (taskItem == null) return;

            Id = taskItem.Id;
            Title = taskItem.Title;
            Description = taskItem.Description;
            DueDate = taskItem.DueDate;
            StartDate = taskItem.StartDate;
            Status = taskItem.Status.ToString();
            CreatedAt = taskItem.CreatedAt;
        }
    }
}
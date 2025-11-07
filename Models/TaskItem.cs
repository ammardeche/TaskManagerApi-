using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskApi.Models
{
    public class TaskItem
    {
        [Key]

        public string Id { get; set; } = null!;

        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime? DueDate { get; set; }
        public TaskStatus Status { get; set; }
        public string UserId { get; set; } = null!;
        public string CategoryId { get; set; } = null!;
        public User? User { get; set; }
        public Category? Category { get; set; }

    }
}
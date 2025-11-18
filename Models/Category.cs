using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskApi.Models
{
    public class Category
    {
        [Key]
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;

        public bool IsDefault { get; set; } = false;

        public List<TaskItem> TaskItems { get; set; } = new List<TaskItem>();  
        public string? UserId { get; set; } // Nullable for default categories

        public User? User { get; set; }
    }
}
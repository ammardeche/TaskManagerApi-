using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskApi.Models;

namespace TaskApi.Dtos
{
    public class CategoryWithTasksDto
    {
        public string id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public List<TaskItem> Tasks { get; set; } = new();
    }
}
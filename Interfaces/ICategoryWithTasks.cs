using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskApi.Dtos;
using TaskApi.Models;

namespace TaskApi.Interfaces
{
    public interface ICategoryWithTasks
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<TaskItemDto> TaskItems { get; set; }
    }
}
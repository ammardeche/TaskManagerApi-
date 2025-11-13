using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TaskApi.Enums;

namespace TaskApi.Dtos
{
    public class CreateTaskDto
    {

        [Required]
        public string Title { get; set; } = null!;
        [Required]

        public string Description { get; set; } = null!;
        [Required]
        public DateTime StartDate { get; set; }
        [Required]

        public DateTime? DueDate { get; set; }

        public TaskItemStatus Status { get; set; }
        public string CategoryId { get; set; } = null!;

    }
}
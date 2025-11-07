using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TaskApi.Models
{
    public class User : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;

        public List<TaskItem> TaskItems { get; set; } = new List<TaskItem>();
        public List<Category> Categories { get; set; } = new List<Category>();
    }
}
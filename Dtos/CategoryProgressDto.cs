using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskApi.Interfaces;
using TaskApi.Models;

namespace TaskApi.Dtos
{
    public class CategoryProgressDto
    {
        public string CategoryId { get; set; } = null!;
        public string CategoryName { get; set; } = null!;
        public double ProgressPercentage { get; set; }
        public int TotalTasks { get; set; }
        public int CompletedTasks { get; set; }


        public CategoryProgressDto(Category category, IProgressCalculationService progressCalculation)
        {
            CategoryId = category.Id;
            CategoryName = category.Name;
            TotalTasks = progressCalculation.CountTotalTaskInCategory(category);
            CompletedTasks = progressCalculation.CountCompletedTasksInCategory(category);
            ProgressPercentage = progressCalculation.CalculateCategoryProgress(category);
        }
    }
}
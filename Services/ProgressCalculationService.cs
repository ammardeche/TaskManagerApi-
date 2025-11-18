using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using TaskApi.Enums;
using TaskApi.Interfaces;
using TaskApi.Models;

namespace TaskApi.Services
{
    public class ProgressCalculationService : IProgressCalculationService
    {
        // calculation for category
        public double CalculateCategoryProgress(Category category)
        {
            if (category.TaskItems == null || !category.TaskItems.Any())

                return 0;
            // here we need to count the total tasks in the category
            var TotalTasks = category.TaskItems.Count();
            // count the completed tasks in the category
            var completedTasks = category.TaskItems.Count(p => p.Status == TaskItemStatus.Completed);

            return CalculateProgressPercentage(Total: TotalTasks, completed: completedTasks);
        }
        public int CountCompletedTasksInCategory(Category category)
        {
            return category?.TaskItems?.Count(t => t.Status == TaskItemStatus.Completed) ?? 0;
        }
        public int CountTotalTaskInCategory(Category category)
        {
            return category?.TaskItems.Count() ?? 0;
        }


        // today's tasks overall
        public double CountTodayCompletedTasks(List<TaskItem> tasks)
        {
            return tasks.Count(t => IsTaskFromToday(t) && t.Status == TaskItemStatus.Completed);
        }

        public int CountTodayPendingTasks(List<TaskItem> tasks)
        {
            return tasks.Count(t => IsTaskFromToday(t) && t.Status == TaskItemStatus.InProgress);

        }
        public bool IsTaskFromToday(TaskItem task)
        {
            // initialize today variables 
            var today = DateTime.UtcNow.Date;
            // should return the task who match the previous today variable 
            return task.CreatedAt.Date == today;

        }

        // overall progress
        public double CalculateOverallProgress(List<Category> categories)
        {
            if (categories == null || !categories.Any()) return 0;

            var totalTasks = categories.Sum(c => c.TaskItems?.Count() ?? 0);
            if (totalTasks == 0) return 0;

            var completedTasks = categories.Sum(c => c.TaskItems?.Count(t => t.Status == TaskItemStatus.Completed) ?? 0);
            if (completedTasks == 0) return 0;

            return CalculateProgressPercentage(completed: completedTasks, Total: totalTasks);
        }


        // private helper method - reusable progress calculation

        private double CalculateProgressPercentage(int completed, int Total)
        {
            if (Total == 0) return 0;

            return Math.Round((double)completed / Total * 100, 2);
        }
    }
}
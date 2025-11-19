using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskApi.Models;

namespace TaskApi.Interfaces
{
    public interface IProgressCalculationService
    {
        // category level calculation 
        double CalculateCategoryProgress(Category category);
        int CountCompletedTasksInCategory(Category category);
        int CountTotalTaskInCategory(Category category);

        // today's tasks calculation 

        int CountTodayCompletedTasks(List<TaskItem> tasks);
        int CountTodayPendingTasks(List<TaskItem> tasks);
        int CountTodayTotalTasks(List<TaskItem> tasks);
        bool IsTaskFromToday(TaskItem task);

        // Overall progress
        double CalculateOverallProgress(List<Category> categories);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskApi.Interfaces;
using TaskApi.Models;
using TaskApi.Services;

namespace TaskApi.Dtos
{
    public class GetTodayProgressDto
    {
        public string Id { get; set; } = null!;

        public int? CompletedToday { get; set; }
        public int? InProgressToday { get; set; }
        public double CompletionRate { get; set; }
        public double TotalToday { get; set; }
        public GetTodayProgressDto(List<TaskItem> tasks, IProgressCalculationService _progressCalculationService)
        {
            Id = "overall"; // or generate a unique ID
            CompletedToday = _progressCalculationService.CountTodayCompletedTasks(tasks);
            InProgressToday = _progressCalculationService.CountTodayPendingTasks(tasks);
            TotalToday = _progressCalculationService.CountTodayTotalTasks(tasks); // You'll need to add this method

            // Calculate completion rate
            if (TotalToday > 0)
            {
                CompletionRate = Math.Round((double)CompletedToday / TotalToday * 100, 2);
            }
            else
            {
                CompletionRate = 0;
            }
        }


    }
}
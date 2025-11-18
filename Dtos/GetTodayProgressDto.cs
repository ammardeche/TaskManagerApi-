using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskApi.Dtos
{
    public class GetTodayProgressDto
    {
        public string Id { get; set; } = null!;

        public string? CompletedToday { get; set; }
        public string? InProgressToday { get; set; }
        public double CompletionRate { get; set; }
        public double TotalToday { get; set; }

    }
}
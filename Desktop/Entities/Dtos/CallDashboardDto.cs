using System.Collections.Generic;
using Entities.Models;

namespace Entities.Dtos
{
    public class CallDashboardDto
    {
        public List<CallCountDto> DailyCalls { get; set; }

        public List<CallCountDto> WeeklyCalls { get; set; }

        public List<CallCountDto> MonthlyCalls { get; set; }

        public List<CallCountDto> YearlyCalls { get; set; }
    }
}

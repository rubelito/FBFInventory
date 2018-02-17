using System;
using System.Collections.Generic;

namespace FBFInventory.Infrastructure.ReportPoco
{
    public class DailyReportDTO
    {
        public DailyReportDTO(){
            Records = new List<DailyItem>();
        }

        public DateTime Date { get; set; }
        public List<DailyItem> Records { get; set; } 
    }
}

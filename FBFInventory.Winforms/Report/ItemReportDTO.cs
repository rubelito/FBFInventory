using System.Collections.Generic;
using FBFInventory.Domain.Entity;

namespace FBFInventory.Winforms.Report
{
    public class ItemReportDTO
    {
        public ItemReportDTO(){
            ItemHistories = new List<ItemHistory>();
        }

        public string ItemName { get; set; }
        public string MeasuredBy { get; set; }
        public List<ItemHistory> ItemHistories { get; set; } 
    }
}

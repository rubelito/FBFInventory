using System;
using System.Collections.Generic;

namespace FBFInventory.Domain.Entity
{
    public class ReturnedHistory
    {
        public ReturnedHistory(){
            GoodItems = new List<ReturnedItem>();
            ScrapItems = new List<ScrapItem>();
        }

        public long Id { get; set; }
        public DR DR { get; set; }
        public string DRNumber { get; set; }
        public DateTime Date { get; set; }
        public string ProjectEngineer { get; set; }

        public string Note { get; set; }

        public virtual List<ReturnedItem> GoodItems { get; set; }
        public virtual List<ScrapItem> ScrapItems { get; set; } 
 
    }
}
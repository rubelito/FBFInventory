using FBFInventory.Domain.Entity;

namespace FBFInventory.Infrastructure.ReportPoco
{
    public class DailyItem
    {
        public DailyItem(){
            NotesForIn = string.Empty;
            NotesForOut = string.Empty;
        }

        public long Id { get; set; }
        public string ItemName { get; set; }
        public MeasuredBy MeasuredBy { get; set; }

        public double BeginningQty { get; set; }
        public double In { get; set; }
        public double Out { get; set; }       
        public double EndingQty { get; set; }
        public string NotesForIn { get; set; }
        public string NotesForOut { get; set; }

        public bool HasReturnItems { get; set; }
    }
}

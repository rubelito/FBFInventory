using System;
using System.Collections.Generic;
using System.Linq;

namespace FBFInventory.Infrastructure.ReportPoco
{
    public class WeeklyReportDTO
    {
        public WeeklyReportDTO(){
            DaySections = new List<DaySection>();
            Items = new List<ItemRow>();
        }

        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public string DateCoverage{
            get { return From.ToString("MMM dd yyyy") + " - " + To.ToString("MMM dd, yyyy"); }
        }

        public List<DaySection> DaySections { get; set; }
        public List<ItemRow> Items { get; set; }

        public double Total{
            get{
                double total = Items.Sum(i => i.Total);
                return total;
            }
        }
    }

    public class DaySection
    {
        public int DateId { get; set; }
        public DateTime Day { get; set; }
        public int ColumnIndex { get; set; }

        public string WeekDay{
            get { return Day.DayOfWeek.ToString(); }
        }
    }

    public class ItemRow
    {
        public ItemRow(){
            DailyOuts = new List<DailyOut>();
        }

        public long Id { get; set; }
        public string ItemName { get; set; }

        public List<DailyOut> DailyOuts { get; set; }

        public double Total{
            get{
                double total = DailyOuts.Sum(d => d.OutQty);
                return total;
            }
        }
    }

    public class DailyOut
    {
        public DaySection DaySection { get; set; }
        public double OutQty { get; set; }
    }
}
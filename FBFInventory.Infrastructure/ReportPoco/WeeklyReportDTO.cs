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

        public double InTotal{
            get{
                double total = Items.Sum(i => i.InTotal);
                return total;
            }
        }

        public double OutTotal{
            get{
                double total = Items.Sum(i => i.OutTotal);
                return total;
            }
        }

        public WeeklySupplierDTO SupplierReport { get; set; }
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
            DailyInOuts = new List<DailyInOut>();
        }

        public long Id { get; set; }
        public string ItemName { get; set; }

        public List<DailyInOut> DailyInOuts { get; set; }

        public double InTotal{
            get{
                double total = DailyInOuts.Sum(d => d.InQty);
                return total;
            }
        }

        public double OutTotal{
            get{
                double total = DailyInOuts.Sum(d => d.OutQty);
                return total;
            }
        }
    }

    public class DailyInOut
    {
        public DaySection DaySection { get; set; }
        public double InQty { get; set; }
        public double OutQty { get; set; }
    }
}
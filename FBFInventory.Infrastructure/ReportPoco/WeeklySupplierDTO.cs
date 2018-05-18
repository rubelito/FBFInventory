using System.Collections.Generic;
using System.Linq;
using FBFInventory.Domain.Entity;

namespace FBFInventory.Infrastructure.ReportPoco
{
    public class WeeklySupplierDTO
    {
        public WeeklySupplierDTO()
        {
            SupplierSections = new List<SupplierSection>();
            TotalForEachItems = new List<ItemSection>();
        }

        public List<SupplierSection> SupplierSections { get; set; }

        public List<ItemSection> TotalForEachItems { get; set; }

        public double GrandTotal{
            get { return TotalForEachItems.Sum(i => i.InQty); }
        }
    }

    public class SupplierSection
    {
        public SupplierSection(){
            Items = new List<ItemSection>();
        }

        public string SupplierName { get; set; }
        public List<ItemSection> Items { get; set; }

        public double Total{
            get { return Items.Sum(i => i.InQty); }
        }
    }

    public class ItemSection
    {
        public string ItemName { get; set; }
        public double InQty { get; set; }
        public MeasuredBy MeasuredBy { get; set; }
    }
}

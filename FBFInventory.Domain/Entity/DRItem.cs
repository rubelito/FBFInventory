
using System;

namespace FBFInventory.Domain.Entity
{
    //This class is similar to PurchaseOrderItem
    public class DRItem
    {
        public DRItem(){
            DateAdded = DateTime.Now;
        }

        public long Id { get; set; }
        public Item Item { get; set; }
        public double Qty { get; set; }
        public DateTime DateAdded { get; set; }

        public DR DR { get; set; }
    }
}

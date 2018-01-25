using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FBFInventory.Domain.Entity
{
    public class Item
    {
        public Item()
        {
            Histories = new List<ItemHistory>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public double Cost { get; set; }

        public double Quantity { get; set; }
        public double Meters { get; set; }
        public double Feet { get; set; }

        public double CurrentQty { get; set; }

        public double Threshold { get; set; }

        public bool IsNearOutOfStock{
            get { return CurrentQty < Threshold; }
        }

        [Required]
        public MeasuredBy MeasuredBy { get; set; }

        [Required]
        public virtual Category Category { get; set; }
        
        [Required]
        public virtual Supplier Supplier { get; set; }

        public bool IsPhaseOut { get; set; }

        public virtual List<ItemHistory> Histories { get; set; }

        public double GetAppropriateQuantity{
            get{
                double qty = 0;

                if (MeasuredBy == MeasuredBy.Quantity)
                    qty = Quantity;
                else if (MeasuredBy == MeasuredBy.Meters)
                    qty = Meters;
                else if (MeasuredBy == MeasuredBy.Feet)
                    qty = Feet;

                return qty;
            }
        }
    }
}
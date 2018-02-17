using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FBFInventory.Domain.Entity
{
    public class ItemHistory
    {
        public int Id { get; set; }
        [ForeignKey("Item")]
        public long Item_Id { get; set; }
        public Item Item { get; set; }

        public double BeginningQuantity { get; set; }
        public double BeginningMeters { get; set; }
        public double BeginningFeet { get; set; }

        public double Quantity { get; set; }
        public double Meters { get; set; }
        public double Feet { get; set; }

        public double EndingQuantity { get; set; }
        public double EndingMeters { get; set; }
        public double EndingFeet { get; set; }

        public DateTime DateAdded { get; set; }
        public bool IsMistaken { get; set; }

        public string Note { get; set; }

        public DR DR { get; set; }
      
        public MeasuredBy MeasuredBy { get; set; }

        public InOrOut InOrOut { get; set; }

        public ReceiptType Type { get; set; }

        public double AppopriateBeginningQty{
            get{
                double qty = 0;
                if (MeasuredBy == MeasuredBy.Quantity)
                    qty = BeginningQuantity;
                if (MeasuredBy == MeasuredBy.Meters)
                    qty = BeginningMeters;
                if (MeasuredBy == MeasuredBy.Feet)
                    qty = BeginningFeet;

                return qty;
            }
        }

        public double AppopriateQty{
            get{
                double qty = 0;
                if (MeasuredBy == MeasuredBy.Quantity)
                    qty = Quantity;
                if (MeasuredBy == MeasuredBy.Meters)
                    qty = Meters;
                if (MeasuredBy == MeasuredBy.Feet)
                    qty = Feet;

                return qty;
            }
        }

        public double AppopriateEndingQty{
            get{
                double qty = 0;
                if (MeasuredBy == MeasuredBy.Quantity)
                    qty = EndingQuantity;
                if (MeasuredBy == MeasuredBy.Meters)
                    qty = EndingMeters;
                if (MeasuredBy == MeasuredBy.Feet)
                    qty = EndingFeet;

                return qty;
            }
        }
    }
}

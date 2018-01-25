using System;
using System.ComponentModel.DataAnnotations;

namespace FBFInventory.Domain.Entity
{
    public class ItemHistory
    {
        public int Id { get; set; }
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

        public string Note { get; set; }

        public DR DR { get; set; }
      
        public MeasuredBy MeasuredBy { get; set; }

        public InOrOut InOrOut { get; set; }

        public ReceiptType Type { get; set; }
    }
}

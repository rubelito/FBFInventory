using System;
using System.Collections.Generic;

namespace FBFInventory.Domain.Entity
{
    public class DR
    {
        public DR()
        {
            Items = new List<DRItem>();
        }

        public int Id { get; set; }

        public string SDRNumber { get; set; }
        public string DRNumber { get; set; }
        public List<DRItem> Items { get; set; } 

        public virtual Supplier Supplier { get; set; }
        public virtual Customer Customer { get; set; }

        public string Project { get; set; }
        public string DeliveryAddress { get; set; }

        public ReceiptType Type { get; set; }
        public DateTime Date { get; set; }
       
        public string Note { get; set; }
    }
}

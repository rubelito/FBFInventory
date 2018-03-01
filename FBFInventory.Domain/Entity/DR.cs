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

        public virtual string RecipientToDisplay{
            get{
                string recipient = string.Empty;
                if (Type == ReceiptType.SDR)
                    recipient = Supplier.Name;
                if (Type == ReceiptType.DR)
                    recipient = Customer.Name;

                return recipient;
            }
        }

        public string Project { get; set; }
        public string DeliveryAddress { get; set; }

        public string DeliveredBy { get; set; }
        public string VehiclePlateNumber { get; set; }

        public ReceiptType Type { get; set; }
        public DateTime Date { get; set; }
       
        public string Note { get; set; }

        public string CreatedBy { get; set; }

        public virtual ReturnedHistory ReturnedHistory { get; set; }
        public bool HasReturnedHistory { get { return ReturnedHistory != null; } }

        public string DRNumberToDisplay{
            get{
                string dr = string.Empty;

                if (Type == ReceiptType.SDR)
                    dr = SDRNumber;
                if (Type == ReceiptType.DR)
                    dr = DRNumber;

                return dr;
            }
        }
    }
}

using FBFInventory.Domain.Entity;

namespace FBFInventory.Winforms.Helper
{
    public class DRParam
    {
        public ReceiptType ReceiptType { get; set; }
        public Operation Operation { get; set; }
        public SupplierOrCustomer SC { get; set; }

        public DR SelectedDR { get; set; }
    }
}

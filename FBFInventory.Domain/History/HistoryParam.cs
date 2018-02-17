using FBFInventory.Domain.Entity;

namespace FBFInventory.Domain.History
{
    public class HistoryParam
    {
        public double OldQty { get; set; }
        public double InOutQty { get; set; }
        public double NewQty { get; set; }

        public Item ItemToMonitor { get; set; }
        public ReceiptType Type { get; set; }
        public string Note { get; set; }
        public DR DR { get; set; }
        public InOrOut InOrOut { get; set; }
        public bool IsMistaken { get; set; }
    }
}

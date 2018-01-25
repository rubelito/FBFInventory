using FBFInventory.Domain.Entity;

namespace FBFInventory.Domain.History
{
    public class InOutParam
    {
        public Item Item { get; set; }

        public InOrOut InOrOut { get; set; }
        public double Qty { get; set; }
        public string Note { get; set; }
    }
}

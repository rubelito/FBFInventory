using FBFInventory.Domain.Entity;

namespace FBFInventory.Domain.History
{
    public class InOutDRParam
    {
        public DRItem DRItem { get; set; }

        public string Note { get; set; }
        public InOrOut InOrOut { get; set; }
    }
}

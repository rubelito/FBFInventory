using FBFInventory.Domain.Entity;

namespace FBFInventory.Domain.History
{
    public class ReturnInOutParam
    {
        public ReturnedItem ReturnedItem { get; set; }
        public InOrOut InOrOut { get; set; }
        public string Note { get; set; }

        public long DrId { get; set; }
        public long ItemId { get; set; }
        public string Name { get; set; }
    }
}

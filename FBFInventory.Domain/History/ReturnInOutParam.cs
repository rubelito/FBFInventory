using FBFInventory.Domain.Entity;

namespace FBFInventory.Domain.History
{
    public class ReturnInOutParam
    {
        public ReturnedItem ReturnedItem { get; set; }
        public InOrOut InOrOut { get; set; }
        public string Note { get; set; }
    }
}

using FBFInventory.Domain.Entity;

namespace FBFInventory.Infrastructure.Dto
{
    public class ScrapInOutParam
    {
        public ScrapItem Scrap { get; set; }
        public InOrOut InOrOut { get; set; }
    }
}

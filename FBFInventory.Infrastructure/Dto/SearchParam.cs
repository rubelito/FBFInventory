
using FBFInventory.Domain.Entity;

namespace FBFInventory.Infrastructure.Dto
{
    public class SearchParam
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }

        public OrdeBy OrderBy { get; set; }
        public ReceiptType ReceiptType { get; set; }


        public string DRNumber { get; set; }
        public long ItemId { get; set; }
    }
}

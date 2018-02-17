using System;
using FBFInventory.Domain.Entity;

namespace FBFInventory.Infrastructure.Dto
{
    public class SearchParam
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }

        public OrdeBy OrderBy { get; set; }
        public ReceiptType ReceiptType { get; set; }

        public bool ShouldFilterByInOrOut { get; set; }
        public InOrOut InOrOut { get; set; }

        public string DRNumber { get; set; }
        public long ItemId { get; set; }
        public bool SearchWithDate { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}

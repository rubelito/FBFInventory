using System.Collections.Generic;
using FBFInventory.Domain.Entity;

namespace FBFInventory.Infrastructure.Dto
{
    public class ItemSearchResult
    {
        public int TotalItems { get; set; }
        public int PageCount { get; set; }
        public List<Item> Results { get; set; }
    }
}
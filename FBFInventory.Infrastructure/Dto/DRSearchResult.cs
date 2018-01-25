using System.Collections.Generic;
using FBFInventory.Domain.Entity;

namespace FBFInventory.Infrastructure.Dto
{
    public class DRSearchResult
    {
        public int TotalItems { get; set; }
        public int PageCount { get; set; }
        public List<DR> Results { get; set; }  
    }
}

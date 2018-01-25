using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FBFInventory.Domain.Entity;

namespace FBFInventory.Infrastructure.Dto
{
    public class HistorySearchResult
    {
        public int TotalItems { get; set; }
        public int PageCount { get; set; }
        public List<ItemHistory> Results { get; set; }
    }
}

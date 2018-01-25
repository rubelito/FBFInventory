using System.Collections.Generic;
using System.ComponentModel;

namespace FBFInventory.Domain.Entity
{
    public class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public virtual List<Item> Items { get; set; } 
    }
}

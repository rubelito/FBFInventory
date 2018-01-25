using System;
using System.Collections.Generic;

namespace FBFInventory.Domain.Entity
{
    public class Category
    {
        public Category(){
            Items = new List<Item>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Item> Items { get; set; }
    }
}

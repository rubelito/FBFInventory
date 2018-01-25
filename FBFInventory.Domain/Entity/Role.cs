using System;
using System.Collections.Generic;

namespace FBFInventory.Domain.Entity
{
    public class Role
    {
        public int Id { get; set; }
        public int Name { get; set; }


        public virtual List<User> Users { get; set; }
    }
}

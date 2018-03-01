﻿using System.Collections.Generic;

namespace FBFInventory.Domain.Entity
{
    public class Role
    {
        public Role(){
            Users = new List<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<User> Users { get; set; }
    }
}

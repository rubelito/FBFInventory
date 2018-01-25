using System;
using System.Collections.Generic;

namespace FBFInventory.Domain.Entity
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        public virtual List<Role> Roles { get; set; }
    }
}

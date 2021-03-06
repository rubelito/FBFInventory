﻿using System;

namespace FBFInventory.Domain.Entity
{
    public class ReturnedItem
    {
        public ReturnedItem(){
            DateAdded = DateTime.Now;
        }

        public long Id { get; set; }
        public Item Item { get; set; }
        public double Qty { get; set; }
        public DateTime DateAdded { get; set; }

        public ReturnedHistory ReturnedHistory { get; set; }
    }
}

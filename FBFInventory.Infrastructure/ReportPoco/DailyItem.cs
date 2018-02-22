using System;
using System.Collections.Generic;
using FBFInventory.Domain.Entity;

namespace FBFInventory.Infrastructure.ReportPoco
{
    public class DailyItem
    {
        public DailyItem(){
            NotesForIn = new List<string>();
            NotesForOut = new List<string>();
        }

        public long Id { get; set; }
        public string ItemName { get; set; }
        public MeasuredBy MeasuredBy { get; set; }

        public double BeginningQty { get; set; }
        public double In { get; set; }
        public double Out { get; set; }       
        public double EndingQty { get; set; }

        public List<string> NotesForIn { get; set; }
        public List<string> NotesForOut { get; set; }

        public string CommentsForIn{
            get{
                string Notes = string.Empty;
                foreach (var note in NotesForIn){
                    Notes = Notes + note + Environment.NewLine;
                }
                return Notes;
            }
        }

        public string CommentsForOut{
            get{
                string Notes = string.Empty;
                foreach (var note in NotesForOut)
                {
                    Notes = Notes + note + Environment.NewLine;
                }
                return Notes;
            }
        }

        public bool HasReturnItems { get; set; }
    }
}

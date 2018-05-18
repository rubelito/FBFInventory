using System;
using FBFInventory.Domain.Entity;

namespace FBFInventory.Domain.History
{
    public static class InOutHelper
    {
        public static ItemHistory MakeHistory(HistoryParam param){
            ItemHistory h = new ItemHistory();
            h.Item = param.ItemToMonitor;
            h.MeasuredBy = h.Item.MeasuredBy;

            if (h.MeasuredBy == MeasuredBy.pcs){
                h.BeginningQuantity = param.OldQty;
                h.Quantity = param.InOutQty;
                h.EndingQuantity = param.NewQty;
            }
            else if (h.MeasuredBy == MeasuredBy.Meters){
                h.BeginningMeters = param.OldQty;
                h.Meters = param.InOutQty;
                h.EndingMeters = param.NewQty;
            }
            else if (h.MeasuredBy == MeasuredBy.Feet){
                h.BeginningFeet = param.OldQty;
                h.Feet = param.InOutQty;
                h.EndingFeet = param.NewQty;
            }

            h.DateAdded = DateTime.Now;
            //h.DateAdded = new DateTime(2018, 2, 9, 1,1,1);
            h.InOrOut = param.InOrOut;
            h.Type = param.Type;
            h.Note = param.Note;
            h.DR = param.DR;
            h.IsMistaken = param.IsMistaken;
            h.CreatedBy = param.Name;
            
            return h;
        }

        public static void AddToAppopriateMeasurement(Item item, double qty){
            if (item.MeasuredBy == MeasuredBy.pcs){
                item.Quantity = item.Quantity + qty;
            }
            else if (item.MeasuredBy == MeasuredBy.Meters)
            {
                item.Meters = item.Meters + qty;
            }
            else if (item.MeasuredBy == MeasuredBy.Feet)
            {
                item.Feet = item.Feet + qty;
            }
        }

        public static void SubtractToAppopriateMeasurement(Item item, double qty)
        {
            if (item.MeasuredBy == MeasuredBy.pcs)
            {
                item.Quantity = item.Quantity - qty;
            }
            else if (item.MeasuredBy == MeasuredBy.Meters)
            {
                item.Meters = item.Meters - qty;
            }
            else if (item.MeasuredBy == MeasuredBy.Feet)
            {
                item.Feet = item.Feet - qty;
            }
        }
    }
}

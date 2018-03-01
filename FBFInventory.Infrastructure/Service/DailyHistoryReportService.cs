using System;
using System.Collections.Generic;
using System.Linq;
using FBFInventory.Domain.Entity;
using FBFInventory.Infrastructure.Dto;
using FBFInventory.Infrastructure.ReportPoco;

namespace FBFInventory.Infrastructure.Service
{
    public class DailyHistoryReportService
    {
        private readonly ItemService _itemService;
        private readonly HistoryService _historyService;

        private DateTime _date;
        private List<ItemHistory> _histories;
        private List<DailyItem> _perDays;

        private List<ItemHistory> _previousHistories;
        private List<ItemHistory> _nextHistories;

        public DailyHistoryReportService(ItemService itemService, HistoryService historyService){
            _itemService = itemService;
            _historyService = historyService;
            _perDays = new List<DailyItem>();
        }

        public DailyReportDTO GetDailyReport(DateTime date){
            _date = date;
            DailyReportDTO dR = new DailyReportDTO();
            dR.Date = date;

            SearchParam p = new SearchParam();
            p.SearchWithDate = true;
            p.From = date;
            p.To = date.AddDays(1);

            List<Item> items = _itemService.AllActiveItems();
            _histories = _historyService.SearchHistories(p);
            _previousHistories = _historyService.GetItemsHistoryFromOtherPreviousDays(_date);
            _nextHistories = _historyService.GetItemsHistoryAfterThisDate(_date);

            foreach (var i in items){
                DailyItem d = CreateRow(i);
                _perDays.Add(d);
            }

            dR.Records = _perDays;

            return dR;
        }

        private DailyItem CreateRow(Item i){
            DailyItem d = new DailyItem();
            d.Id = i.Id;
            d.ItemName = i.Name;
            d.BeginningQty = GetBeginningQty(i);
            GetInQtyAndSetNotes(i, d);
            GetOutQtyAndSetNotes(i, d);
            d.EndingQty = GetEndingQty(i);

            return d;
        }

        private double GetBeginningQty(Item item){
            double qty = 0;
            ItemHistory h = _histories.Where(d => d.Item_Id == item.Id)
                .OrderBy(d => d.DateAdded)
                .FirstOrDefault();

            if (h != null)
                qty = h.AppopriateBeginningQty;
            else{
                qty = TryGettingQtyFromOtherPastHistories(item.Id);
            }

            return qty;
        }

        private double TryGettingQtyFromOtherPastHistories(long itemId){
            double qty = 0;
            ItemHistory h;
            h = _previousHistories.FirstOrDefault(e => e.Item_Id == itemId);
            if (h != null){
                qty = h.AppopriateEndingQty;
            }
            return qty;
        }

        private void GetInQtyAndSetNotes(Item item, DailyItem dI){
            double qty = 0;
            List<ItemHistory> hs = _histories
                .Where(d => d.InOrOut == InOrOut.In
                            && d.Item_Id == item.Id)
                .ToList();

            foreach (var h in hs){
                qty = qty + h.AppopriateQty;

                if (string.IsNullOrEmpty(h.Note)){
                    if (h.DR != null){
                        dI.NotesForIn.Add(h.DR.Note);
                    }
                }
                else{
                    dI.NotesForIn.Add(h.Note + ". By :" + h.CreatedBy);
                }
            }

            dI.HasReturnItems = hs.Any(h => h.IsMistaken);

            dI.In = qty;
        }

        private void GetOutQtyAndSetNotes(Item item, DailyItem dI){
            double qty = 0;
            List<ItemHistory> hs = _histories
                .Where(d => d.InOrOut == InOrOut.Out
                            && d.Item_Id == item.Id)
                .ToList();

            foreach (var h in hs){
                qty = qty + h.AppopriateQty;
                if (string.IsNullOrEmpty(h.Note)){
                    if (h.DR != null){
                        dI.NotesForOut.Add(h.DR.Note);
                    }
                }
                else{
                    dI.NotesForOut.Add(h.Note + ". By :" + h.CreatedBy);
                }
            }

            dI.Out = qty;
        }

        private double GetEndingQty(Item item){
            double qty = 0;
            ItemHistory h = _histories.Where(d => d.Item_Id == item.Id)
                .OrderByDescending(d => d.DateAdded)
                .FirstOrDefault();

            if (h != null)
                qty = h.AppopriateEndingQty;
            else{
                qty = TryGettingQtyFromLatestDays(item);
            }

            return qty;
        }

        private double TryGettingQtyFromLatestDays(Item item){
            double qty = 0;
            ItemHistory h;
            h = _nextHistories.FirstOrDefault(e => e.Item_Id == item.Id);

            if (h != null){
                qty = h.AppopriateBeginningQty;
            }
            else{
                qty = item.GetAppropriateQuantity;
            }

            return qty;
        }
    }
}

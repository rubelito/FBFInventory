using System;
using System.Collections.Generic;
using System.Linq;
using FBFInventory.Domain.Entity;
using FBFInventory.Infrastructure.Dto;
using FBFInventory.Infrastructure.ReportPoco;

namespace FBFInventory.Infrastructure.Service
{
    public class WeeklyHistoryReportService
    {
        private readonly ItemService _itemService;
        private readonly HistoryService _historyService;

        private DateTime _from;
        private DateTime _to;

        private List<DaySection> _range; 
        private List<TemporaryHistoryStorage> _tempHistories;
        private List<Item> _items;
        private List<ItemRow> _itemRows; 

        public WeeklyHistoryReportService(ItemService itemService, HistoryService historyService){
            _itemService = itemService;
            _historyService = historyService;
            _range = new List<DaySection>();
            _tempHistories = new List<TemporaryHistoryStorage>();
            _items = new List<Item>();
            _itemRows = new List<ItemRow>();
        }

        public WeeklyReportDTO GetWeeklyReport(DateTime from, DateTime to){
            _from = from;
            _to = to;

            GenerateCoverangeRange();
            RetrieveItemHistoriesForEachCoverage();
            ComputeOutQuantityForEachItems();

            WeeklyReportDTO dto = new WeeklyReportDTO();
            dto.From = from;
            dto.To = to;
            dto.DaySections = _range;
            dto.Items = _itemRows;

            return dto;
        }

        private void ComputeOutQuantityForEachItems(){
            _items = _itemService.AllActiveItems();

            foreach (var item in _items){
                ItemRow row = new ItemRow();
                row.Id = item.Id;
                row.ItemName = item.Name;

                foreach (var daySection in _range){
                    DailyInOut dInOut = ComputeDailyInOut(daySection, item.Id);
                    row.DailyInOuts.Add(dInOut);
                }

                _itemRows.Add(row);
            }
        }

        private DailyInOut ComputeDailyInOut(DaySection section, long itemId){
            DailyInOut dInOut = new DailyInOut();
            TemporaryHistoryStorage temp =
                _tempHistories.FirstOrDefault(t => t.DaySection.DateId == section.DateId);

            List<ItemHistory> historiesOfItem = temp.Histories.Where(h => h.Item_Id == itemId).ToList();

            List<ItemHistory> inHistories = historiesOfItem.Where(h => h.InOrOut == InOrOut.In).ToList();
            double inQty = 0;
            foreach (var h in inHistories){
                inQty = inQty + h.AppopriateQty;
            }

            List<ItemHistory> outHistories = historiesOfItem.Where(h => h.InOrOut == InOrOut.Out).ToList();
            double outQty = 0;
            foreach (var h in outHistories){
                outQty = outQty + h.AppopriateQty;
            }

            dInOut.DaySection = section;
            dInOut.InQty = inQty;
            dInOut.OutQty = outQty;

            return dInOut;
        }

        private void GenerateCoverangeRange(){
            _range = MakeDaySections();
        }

        private void RetrieveItemHistoriesForEachCoverage(){           
            foreach (var dSection in _range){
                var p = MakeSearchParam(dSection.Day);

                TemporaryHistoryStorage temp = new TemporaryHistoryStorage();
                temp.DaySection = dSection;
                temp.Histories = _historyService.SearchHistories(p);

                _tempHistories.Add(temp);
            }
        }

        private SearchParam MakeSearchParam(DateTime day){
            SearchParam p = new SearchParam();
            p.SearchWithDate = true;
            p.From = day;
            p.To = day.AddDays(1);
            p.ShouldFilterByInOrOut = false;

            return p;
        }

        private List<DaySection> MakeDaySections(){
            List<DaySection> sections = new List<DaySection>();
            List<DateTime> daysInWeek = GenerateTheDaysWithinThisRange();
            int sectionId = 1;

            foreach (var day in daysInWeek){
                DaySection d = new DaySection();
                d.DateId = sectionId;
                d.Day = day;

                sections.Add(d);
                sectionId++;
            }

            return sections;
        }

        private List<DateTime> GenerateTheDaysWithinThisRange()
        {
            double numberOfDaysInWeek = (_to - _from).TotalDays;
            List<DateTime> daysInWeek = new List<DateTime>();
            DateTime currentInterValDay = _from;

            daysInWeek.Add(currentInterValDay);
            for (int i = 0; i < numberOfDaysInWeek; i++)
            {
                currentInterValDay = currentInterValDay.AddDays(1);
                daysInWeek.Add(new DateTime(currentInterValDay.Year, currentInterValDay.Month, currentInterValDay.Day));
            }

            return daysInWeek;
        }
    }

    public class TemporaryHistoryStorage
    {
        public TemporaryHistoryStorage(){
            Histories = new List<ItemHistory>();
        }

        public DaySection DaySection { get; set; }
        public List<ItemHistory> Histories { get; set; }
    }
}

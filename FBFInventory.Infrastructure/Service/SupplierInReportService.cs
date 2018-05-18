using System;
using System.Collections.Generic;
using System.Linq;
using FBFInventory.Domain.Entity;
using FBFInventory.Infrastructure.ReportPoco;

namespace FBFInventory.Infrastructure.Service
{
    public class SupplierInReportService
    {
        private readonly DRService _drService;

        private List<Supplier> _suppliers;
        private List<DRItem> _drItems;
        private WeeklySupplierDTO _weeklySupplierDto;

        public SupplierInReportService(DRService drService){
            _drService = drService;
        }

        public WeeklySupplierDTO blah(DateTime from, DateTime to){
            _weeklySupplierDto = new WeeklySupplierDTO();

            _drItems = _drService.GetItemsByRange(from, to);
            List<DR> tempDrs = GetDistinctDRs();
            _suppliers = GetDistinctSuppliers(tempDrs);

            MakeSupplierSections();
            MakeTotalSection();

            return _weeklySupplierDto;
        }

        private void MakeTotalSection(){
            List<ItemSection> allItems = new List<ItemSection>();

            foreach (var s in _weeklySupplierDto.SupplierSections){
                allItems.AddRange(s.Items);
            }

            List<ItemSection> listOfItemNames = GetDistinctItems(allItems);

            foreach (var item in listOfItemNames){
                ItemSection totalForEarchItem = new ItemSection();
                totalForEarchItem.ItemName = item.ItemName;
                totalForEarchItem.InQty = allItems.Where(i => i.ItemName == item.ItemName).Sum(i => i.InQty);
                
                _weeklySupplierDto.TotalForEachItems.Add(totalForEarchItem);
            }
        }

        private List<ItemSection> GetDistinctItems(List<ItemSection> allItems){
            return allItems.GroupBy(i => i.ItemName)
                .Select(g => g.First())
                .ToList();
        }

        private List<DR> GetDistinctDRs(){
            List<DR> tempDrs = _drItems.Select(i => i.DR).ToList();

            return tempDrs
                .GroupBy(p => p.Id)
                .Select(g => g.First())
                .ToList();
        }

        private List<Supplier> GetDistinctSuppliers(List<DR> tempDrs){
            var tempSupplier = tempDrs.Select(t => t.Supplier).ToList();

            return  tempSupplier
                .GroupBy(p => p.Id)
                .Select(g => g.First())
                .ToList();
        }

        private void MakeSupplierSections(){
            foreach (var s in _suppliers){
                SupplierSection sSection = new SupplierSection();
                sSection.SupplierName = s.Name;

                List<Item> itemsInSupplier = GetDistinctItemsInSupplier(s.Id);

                foreach (var iInSupplier in itemsInSupplier){
                    ItemSection iSection = new ItemSection();

                    iSection.ItemName = iInSupplier.Name;
                    iSection.InQty =
                        _drItems.Where(d => d.DR.Supplier.Id == s.Id && d.Item.Id == iInSupplier.Id).Sum(d => d.Qty);
                    iSection.MeasuredBy = iInSupplier.MeasuredBy;

                    sSection.Items.Add(iSection);
                }

                _weeklySupplierDto.SupplierSections.Add(sSection);
            }
        }

        private List<Item> GetDistinctItemsInSupplier(int supplierId){
            return _drItems.Where(d => d.DR.Supplier.Id == supplierId).Select(d => d.Item).ToList()
                .GroupBy(i => i.Id)
                .Select(g => g.First())
                .ToList();
        }
    }
}

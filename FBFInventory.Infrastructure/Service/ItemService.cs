using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using FBFInventory.Domain.Entity;
using FBFInventory.Infrastructure.Dto;
using FBFInventory.Infrastructure.EntityFramework;
using log4net;

namespace FBFInventory.Infrastructure.Service
{
    public class ItemService
    {
        private readonly FBFDBContext _context;
        private static ILog Log = LogManager.GetLogger(typeof (ItemService));

        public ItemService(FBFDBContext context){
            _context = context;
        }

        public void Edit(Item iToEdit){
            Item oldItem = _context.Items.FirstOrDefault(i => i.Id == iToEdit.Id);
            oldItem.Name = iToEdit.Name;
            oldItem.MeasuredBy = iToEdit.MeasuredBy;
            oldItem.Quantity = iToEdit.Quantity;
            oldItem.Meters = iToEdit.Meters;
            oldItem.Feet = iToEdit.Feet;
            oldItem.Cost = iToEdit.Cost;
            oldItem.Category = iToEdit.Category;
            oldItem.Supplier = iToEdit.Supplier;
            oldItem.CurrentQty = iToEdit.GetAppropriateQuantity;

            SaveChanges("Edit");
        }

        public List<Item> AllActiveItems(){
            return _context.Items.AsNoTracking().Where(i => !i.IsPhaseOut).ToList();
        }

        public Item Find(long Id){
            return _context.Items.Include("Category")
                .Include("Supplier")
                .FirstOrDefault(i => i.Id == Id);
        }

        public List<Item> GetItemsByDR(long drId){
            List<Item> items = _context.DRItems
                .Where(d => d.DR.Id == drId).Select(d => d.Item).ToList();

            return items;
        }

        public List<DRItem> GetDRItemsByDR(long drId){
            List<DRItem> items = _context.DRItems.Include("Item")
                .Where(d => d.DR.Id == drId).ToList();

            return items;
        }

        public ItemSearchResult SearchItems(ItemSearchParam param){
            IQueryable<Item> query;

            if (param.ShouldIncludeSupplierAndCustomer){
                query = _context.Items
                    .Include("Supplier")
                    .Include("Category");
            }
            else{
                query = _context.Items;
            }

            ItemSearchResult r = new ItemSearchResult();

            query = ApplyConditions(param, query);

            r.TotalItems = query.Count();
            r.PageCount = (int)Math.Ceiling((double)r.TotalItems / param.PageSize);

            int skipRows = param.CurrentPage*param.PageSize;

            query = ApplyOrderBy(param, query);

            query = query.Skip(skipRows)
                    .Take(param.PageSize);

            r.Results = query.ToList();
            return r;
        }

        private static IQueryable<Item> ApplyOrderBy(ItemSearchParam param, IQueryable<Item> query){
            if (param.OrberyBy == OrdeBy.Ascending){
                query = query.OrderBy(d => d.Id);
            }
            else if (param.OrberyBy == OrdeBy.Descending){
                query = query.OrderByDescending(d => d.Id);
            }
            return query;
        }

        private static IQueryable<Item> ApplyConditions(ItemSearchParam param, IQueryable<Item> query){
            if (param.ShouldFilterByStatus){
                if (param.ActiveOnly){
                    query = query.Where(i => !i.IsPhaseOut);
                }
                else{
                    query = query.Where(i => i.IsPhaseOut);
                }
            }

            if (param.Criteria == ItemSearchCriteria.Near_Out_Of_Stock){
                query = query.Where(i => i.CurrentQty < i.Threshold);
            }
            if (!string.IsNullOrWhiteSpace(param.Name))
                query = query.Where(i => i.Name.ToLower().Contains(param.Name.ToLower()));          

            return query;
        }

        private void SaveChanges(string method){
            try{
                _context.SaveChanges();
            }
            catch (DbEntityValidationException ex){
                LogEntityValidatinError(method, ex);
                throw;
            }
        }

        private void LogEntityValidatinError(string Operation, DbEntityValidationException ex){
            Log.Error(Operation + " : ", ex);
        }
    }
}
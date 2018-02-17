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
    public class HistoryService
    {
        private readonly FBFDBContext _context;
        private static ILog Log = LogManager.GetLogger(typeof (HistoryService));

        public HistoryService(FBFDBContext context){
            _context = context;
        }

        public void Add(ItemHistory iToAdd){
            _context.ItemHistories.Add(iToAdd);
            SaveChanges("Add");
        }

        public HistorySearchResult SearchHistoriesWithPaging(SearchParam param){
            var query = BuildQuery(param);

            HistorySearchResult r = new HistorySearchResult();

            r.TotalItems = query.Count();
            r.PageCount = (int) Math.Ceiling((double) r.TotalItems/param.PageSize);

            int skipRows = param.CurrentPage*param.PageSize;

            query = ApplyOrderBy(param, query);

            query = query.Skip(skipRows)
                .Take(param.PageSize);

            r.Results = query.ToList();
            return r;
        }

        public List<ItemHistory> SearchHistories(SearchParam param){
            var query = BuildQuery(param);
            query = ApplyOrderBy(param, query);

            return query.ToList();
        }

        public List<ItemHistory> GetItemsHistoryFromOtherPreviousDays(DateTime beforeThisDate){
            return _context.ItemHistories
                .Where(h => h.DateAdded < beforeThisDate)
                .GroupBy(h => h.Item_Id)
                .Select(h => h.OrderByDescending(h1 => h1.DateAdded).FirstOrDefault())
                .ToList();
        }

        public List<ItemHistory> GetItemsHistoryAfterThisDate(DateTime afterThisDate){
            return _context.ItemHistories
                .Where(h => h.DateAdded > afterThisDate)
                .GroupBy(h => h.Item_Id)
                .Select(h => h.OrderBy(h1 => h1.DateAdded).FirstOrDefault())
                .ToList();
        }

        private IQueryable<ItemHistory> BuildQuery(SearchParam param){
            IQueryable<ItemHistory> query = _context.ItemHistories
                .Include("Item")
                .Include("DR");
            query = ApplyCondition(param, query);
            return query;
        }

        private static IQueryable<ItemHistory> ApplyCondition(SearchParam param, IQueryable<ItemHistory> query){
            if (param.ItemId != 0){
                query = query.Where(h => h.Item != null
                                         && h.Item.Id == param.ItemId);
            }

            if (param.ShouldFilterByInOrOut)
                query = query.Where(h => h.InOrOut == param.InOrOut);

            if (param.SearchWithDate){
                query = query.Where(h =>
                    h.DateAdded > param.From.Date &&
                    h.DateAdded < param.To.Date);
            }

            return query;
        }

        private static IQueryable<ItemHistory> ApplyOrderBy(SearchParam param, IQueryable<ItemHistory> query){
            if (param.OrderBy == OrdeBy.Ascending){
                query = query.OrderBy(d => d.Id);
            }
            else if (param.OrderBy == OrdeBy.Descending){
                query = query.OrderByDescending(d => d.Id);
            }
            return query;
        }

        public void DeleteHistoryByDRAndItem(long drId, long itemId){
            ItemHistory hToDelete = _context.ItemHistories.FirstOrDefault(i => i.DR.Id == drId && i.Item_Id == itemId);
            if (hToDelete != null){
                _context.ItemHistories.Remove(hToDelete);
                SaveChanges("DeleteHistoryByDRAndItem");
            }
        }

        public void DeleteReturnedHistoryByDRAndItem(long drId, long itemId)
        {
            ItemHistory hToDelete = _context.ItemHistories
                .FirstOrDefault(i => 
                    i.DR.Id == drId 
                    && i.Item_Id == itemId
                    && i.IsMistaken);

            if (hToDelete != null)
            {
                _context.ItemHistories.Remove(hToDelete);
                SaveChanges("DeleteReturnedHistoryByDRAndItem");
            }
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

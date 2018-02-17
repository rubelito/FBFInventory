using System;
using System.Data.Entity.Validation;
using System.Linq;
using FBFInventory.Domain.Entity;
using FBFInventory.Infrastructure.Dto;
using FBFInventory.Infrastructure.EntityFramework;
using log4net;

namespace FBFInventory.Infrastructure.Service
{
    public class ReturnedHistoryService
    {
        private readonly FBFDBContext _context;

        private static ILog Log = LogManager.GetLogger(typeof (DRService));

        public ReturnedHistoryService(FBFDBContext context){
            _context = context;
        }

        private ReturnedHistory _newlyCreatedHistory;

        public void Add(ReturnedHistory hToAdd){
            _context.ReturnedHistories.Add(hToAdd);
            SaveChanges("Add");
            _newlyCreatedHistory = hToAdd;
        }

        public ReturnedHistory NewlyCreatedHistory{
            get { return _newlyCreatedHistory; }
        }

        public void Edit(ReturnedHistory hToEdit){
            ReturnedHistory oldHistory = _context.ReturnedHistories.FirstOrDefault(h => h.Id == hToEdit.Id);

            oldHistory.ProjectEngineer = hToEdit.ProjectEngineer;
            oldHistory.Date = hToEdit.Date;
            oldHistory.Note = hToEdit.Note;
            SaveChanges("Edit");
        }

        public void AddScrap(long historyId, ScrapItem item){
            ReturnedHistory parentHistory = _context.ReturnedHistories.FirstOrDefault(h => h.Id == historyId);

            parentHistory.ScrapItems.Add(item);
            SaveChanges("AddScrap");
        }

        public void AddReturnedGoodItem(long historyId, ReturnedItem item){
            ReturnedHistory parentHistory = _context.ReturnedHistories.FirstOrDefault(h => h.Id == historyId);

            parentHistory.GoodItems.Add(item);
            SaveChanges("AddReturnedItem");
        }

        public void DeleteGoodItem(long itemId){
            ReturnedItem itemToRemove = _context.GoodItems.FirstOrDefault(i => i.Id == itemId);

            _context.GoodItems.Remove(itemToRemove);
            SaveChanges("DeleteGoodItems");
        }

        public void DeleteScrapItem(long itemId){
            ScrapItem itemToRemove = _context.ScrapItems.FirstOrDefault(i => i.Id == itemId);

            _context.ScrapItems.Remove(itemToRemove);
            SaveChanges("DeleteScrapItem");
        }

        public ReturnedHistory GetHistory(long Id){
            return _context.ReturnedHistories
                .Include("DR")
                .Include("GoodItems")
                .Include("ScrapItems")
                .FirstOrDefault(h => h.Id == Id);
        }

        public ReturnedHistory GetHistoryByDR(long drId){
            return _context.ReturnedHistories.FirstOrDefault(h => h.DR.Id == drId);
        }

        public ReturnItemResult SearchReturnedHistories(SearchParam param){
            IQueryable<ReturnedHistory> query = _context.ReturnedHistories
                .Include("DR");

            ReturnItemResult r = new ReturnItemResult();

            query = ApplyCondition(param, query);

            r.TotalItems = query.Count();
            r.PageCount = (int) Math.Ceiling((double) r.TotalItems/param.PageSize);

            int skipRows = param.CurrentPage*param.PageSize;

            query = ApplyOrderBy(param, query);

            query = query.Skip(skipRows)
                .Take(param.PageSize);

            r.Results = query.ToList();
            return r;
        }

        private static IQueryable<ReturnedHistory> ApplyCondition(SearchParam param, IQueryable<ReturnedHistory> query){
            if (!string.IsNullOrWhiteSpace(param.DRNumber)){
                query = query.Where(h => h.DRNumber.ToLower().Contains(param.DRNumber.ToLower()));
            }
            return query;
        }

        private static IQueryable<ReturnedHistory> ApplyOrderBy(SearchParam param, IQueryable<ReturnedHistory> query){
            if (param.OrderBy == OrdeBy.Ascending){
                query = query.OrderBy(h => h.Id);
            }
            else if (param.OrderBy == OrdeBy.Descending){
                query = query.OrderByDescending(h => h.Id);
            }
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

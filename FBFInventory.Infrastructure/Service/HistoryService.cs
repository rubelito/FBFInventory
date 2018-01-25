using System;
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

        public HistorySearchResult SearchHistories(SearchParam param){
            IQueryable<ItemHistory> query = _context.ItemHistories
                .Include("Item")
                .Include("DR");

            HistorySearchResult r = new HistorySearchResult();

            query = ApplyCondition(param, query);
            
            r.TotalItems = query.Count();
            r.PageCount = (int)Math.Ceiling((double)r.TotalItems / param.PageSize);

            int skipRows = param.CurrentPage*param.PageSize;

            query = ApplyOrderBy(param, query);

            query = query.Skip(skipRows)
                .Take(param.PageSize);
            
            r.Results = query.ToList();
            return r;
        }

        private static IQueryable<ItemHistory> ApplyCondition(SearchParam param, IQueryable<ItemHistory> query){
            if (param.ItemId != 0){
                query = query.Where(h => h.Item != null 
                    && h.Item.Id == param.ItemId);
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

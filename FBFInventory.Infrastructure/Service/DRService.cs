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
    public class DRService
    {
        private readonly FBFDBContext _context;
        private static ILog Log = LogManager.GetLogger(typeof (DRService));

        public string ErrorMessage = string.Empty;
        public bool HasError;

        public DRService(FBFDBContext context){
            _context = context;
        }

        private DR _newlyCreatedDr;

        public void Add(DR dToAdd){
            if (IsDrNumberExist(dToAdd)){
                HasError = true;
                ErrorMessage = "(S)DR number already exist!";
                return;
            }
            else{
                HasError = false;
            }
            
            _context.DRs.Add(dToAdd);
            SaveChanges("Add");
            _newlyCreatedDr = dToAdd;
        }

        private bool IsDrNumberExist(DR dr){
            bool isExist = false;

            if (dr.Type == ReceiptType.SDR)
                isExist = _context.DRs.Any(d => d.SDRNumber.ToLower() == dr.SDRNumber.ToLower());
            else if (dr.Type == ReceiptType.DR)
                isExist = _context.DRs.Any(d => d.DRNumber.ToLower() == dr.DRNumber.ToLower());

            return isExist;
        }

        public void Edit(DR dToEdit){
            DR oldDr = _context.DRs.FirstOrDefault(d => d.Id == dToEdit.Id);

            if (oldDr.Type == ReceiptType.SDR){
                oldDr.SDRNumber = dToEdit.SDRNumber;
                oldDr.Supplier = dToEdit.Supplier;
            }
            else if (oldDr.Type == ReceiptType.DR){
                oldDr.DRNumber = dToEdit.DRNumber;
                oldDr.Customer = dToEdit.Customer;
                oldDr.Project = dToEdit.Project;
                oldDr.DeliveryAddress = dToEdit.DeliveryAddress;
                oldDr.DeliveredBy = dToEdit.DeliveredBy;
                oldDr.VehiclePlateNumber = dToEdit.VehiclePlateNumber;
            }
            oldDr.Note = dToEdit.Note;
            oldDr.Date = dToEdit.Date;           

            SaveChanges("Edit");
        }

        public void AddToDR(long drId, DRItem iToAdd){
            DR parentDR = _context.DRs.FirstOrDefault(d => d.Id == drId);

            iToAdd.DateAdded = DateTime.Now;
            parentDR.Items.Add(iToAdd);
            SaveChanges("AddToDR");
        }

        public void DeleteDRItem(long drItemId){
            DRItem itemToRemove = _context.DRItems.FirstOrDefault(d => d.Id == drItemId);

            _context.DRItems.Remove(itemToRemove);
            SaveChanges("DeleteDRItem");
        }

        public DR GetDRById(long id){
            return _context.DRs.FirstOrDefault(d => d.Id == id);
        }

        public DR GetDRWithItems(long id){
            return _context.DRs.Include("Items").FirstOrDefault(d => d.Id == id);
        }

        public List<DRItem> GetItemsWithinDR(long id){
            return _context.DRItems.Include("Item").Where(d => d.DR.Id == id).ToList();
        }

        public DR NewlyCreatedDR{
            get { return _newlyCreatedDr; }
        }

        public DRSearchResult DRSearch(SearchParam param){
            IQueryable<DR> query = _context.DRs.Include("ReturnedHistory").AsNoTracking();

            DRSearchResult r = new DRSearchResult();

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

        private static IQueryable<DR> ApplyOrderBy(SearchParam param, IQueryable<DR> query){
            if (param.OrderBy == OrdeBy.Ascending){
                query = query.OrderBy(d => d.Id);
            }
            else if (param.OrderBy == OrdeBy.Descending){
                query = query.OrderByDescending(d => d.Id);
            }
            return query;
        }

        private static IQueryable<DR> ApplyCondition(SearchParam param, IQueryable<DR> query){
            if (!string.IsNullOrWhiteSpace(param.DRNumber)){
                query = query.Where(i => i.SDRNumber.ToLower().Contains(param.DRNumber.ToLower())
                                         || i.DRNumber.ToLower().Contains(param.DRNumber.ToLower()));
            }

            if (param.ReceiptType == ReceiptType.DR){
                query = query.Where(d => d.Type == ReceiptType.DR);
            }
            else if (param.ReceiptType == ReceiptType.SDR){
                query = query.Where(d => d.Type == ReceiptType.SDR);
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

        public List<DR> SearchDr(string drNumber){
            return _context.DRs
                .Where(d => d.DRNumber.ToLower().Contains(drNumber.ToLower()))
                .ToList();
        }
    }
}

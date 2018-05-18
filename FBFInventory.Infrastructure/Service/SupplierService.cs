using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using FBFInventory.Domain.Entity;
using FBFInventory.Infrastructure.EntityFramework;
using log4net;

namespace FBFInventory.Infrastructure.Service
{
    public class SupplierService
    {
        private readonly FBFDBContext _context;
        private static ILog Log = LogManager.GetLogger(typeof (SupplierService));

        public SupplierService(FBFDBContext context){
            _context = context;
        }

        public List<Supplier> GetAllSuppliers(){
            return _context.Suppliers.ToList();
        }

        public void Add(Supplier sToAdd){
            _context.Suppliers.Add(sToAdd);
            SaveChanges("Add");
        }

        public void Edit(Supplier sToEdit){
            Supplier oldSupplier = _context.Suppliers.FirstOrDefault(s => s.Id == sToEdit.Id);
            oldSupplier.Name = sToEdit.Name;
            oldSupplier.Address = sToEdit.Address;
            SaveChanges("Edit");
        }

        public List<Supplier> SearchSupplier(string name){
            return _context.Suppliers.Where(s => s.Name.ToLower().Contains(name.ToLower())).ToList();
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
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using FBFInventory.Domain.Entity;
using FBFInventory.Infrastructure.EntityFramework;
using log4net;

namespace FBFInventory.Infrastructure.Service
{
    public class CustomerService
    {
        private readonly FBFDBContext _context;
        private static ILog Log = LogManager.GetLogger(typeof (CustomerService));

        public CustomerService(FBFDBContext context){
            _context = context;
        }

        public List<Customer> GetAllCustomers(){
            return _context.Customers.ToList();
        }

        public void Add(Customer cToAdd){
            _context.Customers.Add(cToAdd);
            SaveChanges("Add");
        }

        public void Edit(Customer cToEdit){
            Customer oldCustomer = _context.Customers.FirstOrDefault(c => c.Id == cToEdit.Id);
            oldCustomer.Name = cToEdit.Name;
            SaveChanges("Edit");
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

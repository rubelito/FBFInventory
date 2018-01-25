using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using FBFInventory.Domain.Entity;
using FBFInventory.Infrastructure.EntityFramework;
using log4net;

namespace FBFInventory.Infrastructure.Service
{
    public class CategoryService
    {
        private readonly FBFDBContext _context;
        private static ILog Log = LogManager.GetLogger(typeof (CategoryService));

        public CategoryService(FBFDBContext context){
            _context = context;
        }

        public void Add(Category cToAdd){
            _context.Categories.Add(cToAdd);
            SaveChanges("Add");
        }

        public void Edit(Category cToEdit){
            Category oldCategory = _context.Categories.FirstOrDefault(c => c.Id == cToEdit.Id);
            oldCategory.Name = cToEdit.Name;
            SaveChanges("Edit");
        }

        public List<Category> GetAllCategories(){
            return _context.Categories.ToList();
        }

        public void AddNewItem(Item iToAdd, int categoryId){
            Category category = _context.Categories.FirstOrDefault(c => c.Id == categoryId);
            iToAdd.CurrentQty = iToAdd.GetAppropriateQuantity;
            category.Items.Add(iToAdd);
            SaveChanges("AddNewItem");
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
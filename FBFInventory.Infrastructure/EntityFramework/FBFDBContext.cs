using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using FBFInventory.Domain.Entity;
using FBFInventory.Infrastructure.Service;

namespace FBFInventory.Infrastructure.EntityFramework
{
    public class FBFDBContext : DbContext
    {
        public FBFDBContext(IDatabaseType databaseType) :
            base(databaseType.Connectionstring(), true){
        }
        
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<DR> DRs { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<DRItem> DRItems { get; set; }
        public DbSet<ReturnedHistory> ReturnedHistories { get; set; }
        public DbSet<ReturnedItem> GoodItems { get; set; }
        public DbSet<ScrapItem> ScrapItems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DbSet<ItemHistory> ItemHistories { get; set; }

        public override int SaveChanges(){
            try{
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex){
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DR>()
                .HasOptional(e => e.ReturnedHistory)
                .WithRequired(a => a.DR);
        }
    }
}
using FBFInventory.Domain.Entity;
using FBFInventory.Infrastructure.Service;

namespace FBFInventory.Winforms.Helper
{
    public class DialogParam
    {
        public SupplierService SupplierService { get; set; }
        public CustomerService CustomerService { get; set; }

        public Operation Mode { get; set; }
        public SupplierOrCustomer SC { get; set; }

        public Supplier Supplier { get; set; }
        public Customer Customer { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using FBFInventory.Domain.Entity;
using FBFInventory.Infrastructure.EntityFramework;
using FBFInventory.Infrastructure.Service;
using FBFInventory.Winforms.Helper;
using log4net;
using log4net.Config;

[assembly: XmlConfigurator]

namespace FBFInventory.Winforms
{
    static class Program
    {
        private static ILog Log = LogManager.GetLogger(typeof(Program));
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
           Log.Info("Program Started - " + DateTime.Now);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            IDatabaseType type = new EfSqlServer("SQLServerDb");
            FBFDBContext context = new FBFDBContext(type);
            SupplierService supplierService = new SupplierService(context);
            CustomerService customerService = new CustomerService(context);
            CategoryService categoryService = new CategoryService(context);
            ItemService itemService = new ItemService(context);
            HistoryService historyService = new HistoryService(context);
            DRService drService = new DRService(context);
            ReturnedHistoryService returnedHistoryService = new ReturnedHistoryService(context);
            InOutService inOutService = new InOutService(itemService, 
                historyService, drService, returnedHistoryService);

            ReceiptType receiptType = ReceiptType.SDR;
            Operation op = Operation.Add;
            SupplierOrCustomer sc = SupplierOrCustomer.Supplier;

            //Application.Run(new SupplierCustomerMaintenaceForm(supplierService, customerService, op, sc));
            //Application.Run(new CategoryMaintenanceForm(categoryService));

            //List<Item> items = itemService.AllActiveItems();
            //Application.Run(new ItemBrowserForm(items));

            DRParam p = new DRParam();

            p.Operation = op;
            p.ReceiptType = receiptType;
            p.SC = sc;
           // p.SelectedDR = context.DRs.FirstOrDefault(d => d.Id == 7);

            //Application.Run(new InWithDRForm(itemService, supplierService, customerService, 
              //  drService, inOutService, p));

           // Application.Run(new NewItemForm(supplierService, categoryService, itemService));         
           // Application.Run(new InOutWithOutDRForm(itemService, inOutService, 
             //   historyService, InOrOut.In));

            try{
                Application.Run(new MainForm());

                //DRBrowserForm f = new DRBrowserForm(drService);
                //f.ShowDialog();

                //ReturnedHistory h = returnedHistoryService.GetHistory(1);

                //ReturnedItemForm f = new ReturnedItemForm(h,
                  //  itemService, returnedHistoryService, inOutService, drService);
                //f.ShowDialog();
            }
            catch (Exception ex){
                Log.Error(ex);
                throw;
            }
            Log.Info("Program Ended - " + DateTime.Now);
        }
    }
}

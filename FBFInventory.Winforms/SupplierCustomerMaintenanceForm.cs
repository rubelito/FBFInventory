using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using FBFInventory.Domain.Entity;
using FBFInventory.Infrastructure.Service;
using FBFInventory.Winforms.Helper;

namespace FBFInventory.Winforms
{
    public partial class SupplierCustomerMaintenaceForm : Form
    {
        private readonly SupplierService _supplierService;
        private readonly CustomerService _customerService;
        private readonly Operation _mode;
        private readonly SupplierOrCustomer _sc;
        private List<Supplier> _suppliers;
        private List<Customer> _customers;
        private Supplier _selectedSupplier;
        private Customer _selectedCustomer;

        private bool _isClosed = true;

        private string _entityText;

        public SupplierCustomerMaintenaceForm(SupplierService supplierService,
            CustomerService customerService, Operation mode, SupplierOrCustomer sc){
            _supplierService = supplierService;
            _customerService = customerService;
            _mode = mode;
            _sc = sc;
            LoadSupplierOrCustomer();
            InitializeComponent();
        }

        private void LoadSupplierOrCustomer(){
            if (_sc == SupplierOrCustomer.Supplier){
                _suppliers = _supplierService.GetAllSuppliers();
                _entityText = "Supplier";
                this.Text = "Supplier Maintenance";
                
            }
            else if (_sc == SupplierOrCustomer.Customer){
                _customers = _customerService.GetAllCustomers();
                _entityText = "Customer";
                this.Text = "Customer Maintenance";
            }
        }

        private void SupplierMaintenaceForm_Load(object sender, EventArgs e){
            HideControlsBasedOnUsage();
            Search();
            listView1.Columns[1].Text = _entityText;
        }

        private void txtFilter_TextChanged(object sender, EventArgs e){
            Search();
        }

        private void HideControlsBasedOnUsage(){
            if (_mode == Operation.Search){
                cmdEdit.Visible = false;
                cmdAdd.Text = "Select";
            }
        }

        private void Search(){
            if (_sc == SupplierOrCustomer.Supplier)
                SearchSupplier();
            else
                SearchCustomer();
        }

        private void SearchSupplier(){
            List<Supplier> filteredSuppliers = new List<Supplier>();

            if (string.IsNullOrWhiteSpace(txtFilter.Text)){
                filteredSuppliers = _suppliers;
            }
            else{
                filteredSuppliers = _suppliers
                    .Where(s => s.Name.ToLower().Contains(txtFilter.Text.ToLower())).ToList();
            }

            FillListViewWithSuppiers(filteredSuppliers);
        }

        private void SearchCustomer(){
            List<Customer> filteredCustomer = new List<Customer>();

            if (string.IsNullOrWhiteSpace(txtFilter.Text)){
                filteredCustomer = _customers;
            }
            else{
                filteredCustomer = _customers
                    .Where(c => c.Name.ToLower().Contains(txtFilter.Text.ToLower())).ToList();
            }

            FillListViewWithCustomers(filteredCustomer);
        }

        private void FillListViewWithSuppiers(List<Supplier> suppliers){
            ClearListView();
            foreach (var s in suppliers){
                string[] arr = new string[3];
                arr[0] = Convert.ToString(s.Id);
                arr[1] = Convert.ToString(s.Name);
                arr[2] = Convert.ToString(s.Address);
                ListViewItem i = new ListViewItem(arr);

                listView1.Items.Add(i);
            }
        }

        private void FillListViewWithCustomers(List<Customer> customers){
            ClearListView();
            foreach (var c in customers){
                string[] arr = new string[3];
                arr[0] = Convert.ToString(c.Id);
                arr[1] = Convert.ToString(c.Name);
                arr[2] = Convert.ToString(c.Address);
                ListViewItem i = new ListViewItem(arr);

                listView1.Items.Add(i);
            }
        }

        private void cmdAdd_Click(object sender, EventArgs e){
            if (_mode == Operation.Search){
                if (listView1.SelectedItems.Count == 0){
                    MessageBox.Show("Please select " + _entityText + " from the list",
                        "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else{
                    _isClosed = false;
                    this.Close();
                }
            }
            else{
                DialogParam param = MakeParameter(Operation.Add);
                SupplierOrCustomerDialog sDialog = new SupplierOrCustomerDialog(param);
                sDialog.ShowDialog();

                LoadSupplierOrCustomer();
                Search();
            }
        }

        private void cmdEdit_Click(object sender, EventArgs e){
            if (listView1.SelectedItems.Count == 0){
                MessageBox.Show("Please select " + _entityText + " from the list",
                    "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DialogParam param = MakeParameter(Operation.Edit);
            SupplierOrCustomerDialog sDialog = new SupplierOrCustomerDialog(param);
            sDialog.ShowDialog();
            Search();
        }

        private DialogParam MakeParameter(Operation operation){
            DialogParam p = new DialogParam();
            p.SupplierService = _supplierService;
            p.CustomerService = _customerService;

            p.SC = _sc;
            p.Mode = operation;

            p.Supplier = _selectedSupplier;
            p.Customer = _selectedCustomer;

            return p;
        }

        private void ClearListView(){
            listView1.Items.Clear();
            listView1.Refresh();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e){
            if (listView1.SelectedItems.Count > 0){
                int Id = Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text);

                if (_sc == SupplierOrCustomer.Supplier)
                    _selectedSupplier = _suppliers.FirstOrDefault(s => s.Id == Id);
                else if (_sc == SupplierOrCustomer.Customer)
                    _selectedCustomer = _customers.FirstOrDefault(c => c.Id == Id);
            }
        }

        private void cmdClose_Click(object sender, EventArgs e){
            _isClosed = true;
            Close();
        }

        public Supplier SelectedSupplier{
            get { return _selectedSupplier; }
        }

        public Customer SelectedCustomer{
            get { return _selectedCustomer; }
        }

        public bool HasSelectedSupplier{
            get { return !_isClosed; }
        }
    }
}

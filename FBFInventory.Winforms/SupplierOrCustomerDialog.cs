using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FBFInventory.Domain.Entity;
using FBFInventory.Infrastructure.Service;
using FBFInventory.Winforms.Helper;

namespace FBFInventory.Winforms
{
    public partial class SupplierOrCustomerDialog : Form
    {
        private readonly DialogParam _param;

        public SupplierOrCustomerDialog(DialogParam param){
            _param = param;
            InitializeComponent();
        }

        private void SupplierDialog_Load(object sender, EventArgs e){
            SetValueToControl();
        }

        private void SetValueToControl(){
            if (_param.SC == SupplierOrCustomer.Supplier){
                lblSupplierOrCustomer.Text = "Supplier :";
                if (_param.Mode == Operation.Add){
                    Text = "Add Supplier";
                }
                else{
                    Text = "Edit Supplier";
                    txtSupplierOrCustomer.Text = _param.Supplier.Name;
                    txtAddress.Text = _param.Supplier.Address;
                }
            }
            else if (_param.SC == SupplierOrCustomer.Customer){
                lblSupplierOrCustomer.Text = "Customer :";
                if (_param.Mode == Operation.Add){
                    Text = "Add Customer";
                }
                else{
                    Text = "Edit Customer";
                    txtSupplierOrCustomer.Text = _param.Customer.Name;
                    txtAddress.Text = _param.Customer.Address;
                }
            }
        }

        private void cmdSave_Click(object sender, EventArgs e){
            
            if (_param.SC == SupplierOrCustomer.Supplier)
                SaveSupplier();
            else if (_param.SC == SupplierOrCustomer.Customer){
                SaveCustomer();
            }

            this.Close();
        }

        private void SaveSupplier(){
            if (_param.Mode == Operation.Add){
                Supplier newSupplier = new Supplier();
                newSupplier.Name = txtSupplierOrCustomer.Text;
                newSupplier.Address = txtAddress.Text;

                _param.SupplierService.Add(newSupplier);
                MessageBox.Show("Record Added!");
            }
            else if (_param.Mode == Operation.Edit){
                Supplier sToEdit = new Supplier();
                sToEdit.Id = _param.Supplier.Id;
                sToEdit.Name = txtSupplierOrCustomer.Text;
                sToEdit.Address = txtAddress.Text;

                _param.SupplierService.Edit(sToEdit);
                MessageBox.Show("Record Edited!");
            }
        }

        private void SaveCustomer(){
            if (_param.Mode == Operation.Add)
            {
                Customer newCustomer = new Customer();
                newCustomer.Name = txtSupplierOrCustomer.Text;
                newCustomer.Address = txtAddress.Text;

                _param.CustomerService.Add(newCustomer);
                MessageBox.Show("Record Added!");
            }
            else if (_param.Mode == Operation.Edit)
            {
                Customer cToEdit = new Customer();
                cToEdit.Id = _param.Customer.Id;
                cToEdit.Name = txtSupplierOrCustomer.Text;
                cToEdit.Address = txtAddress.Text;

                _param.CustomerService.Edit(cToEdit);
                MessageBox.Show("Record Edited!");
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e){
            this.Close();
        }
    }
}

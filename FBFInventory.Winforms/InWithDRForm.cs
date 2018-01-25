using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using FBFInventory.Domain.Entity;
using FBFInventory.Domain.History;
using FBFInventory.Infrastructure.Service;
using FBFInventory.Winforms.Helper;

namespace FBFInventory.Winforms
{
    public partial class InWithDRForm : Form
    {
        private readonly ItemService _itemService;
        private readonly SupplierService _supplierService;
        private readonly CustomerService _customerService;
        private readonly DRService _drService;
        private readonly InOutService _inOutService;
        private readonly DRParam _param;
        private List<Item> _items;
        private List<DRItem> _drItems; 

        private Item _selectedItemToAdd;
        private DRItem _selectedItemInListView;
        private Supplier _selectedSupplier;
        private Customer _selectedCustomer;
        private string _drText;

        private DR _currentDr;

        public InWithDRForm(ItemService itemService, SupplierService supplierService,
            CustomerService customerService, DRService drService, InOutService inOutService,
            DRParam param){
            _itemService = itemService;
            _supplierService = supplierService;
            _customerService = customerService;
            _drService = drService;
            _inOutService = inOutService;
            _param = param;

            _items = _itemService.AllActiveItems();
            _drItems = new List<DRItem>();

            InitializeComponent();
        }

        private void InWithDR_Load(object sender, EventArgs e){
            ShowSaveAndCancelForEdit(false);
            SetTitle();

            if (_param.Operation == Operation.Add){
                EnableDisableDrControls(true);
                EnableDisableItemControls(false);
                cmdCreate.Text = "Create";
            }
            else if (_param.Operation == Operation.Edit){
                LoadDRToControls();
                LoadItemsToGridView();
                cmdCreate.Text = "Edit";
                EnableDisableDrControls(false);
            }
        }

        private void ShowSaveAndCancelForEdit(bool shouldShow){
            cmdSaveEdit.Visible = shouldShow;
            cmdCancel.Visible = shouldShow;
            cmdCreate.Visible = !shouldShow;
        }

        private void LoadDRToControls(){
            _param.SelectedDR = _drService.GetDRById(_param.SelectedDR.Id);

            if (_param.ReceiptType == ReceiptType.SDR){
                lblSupplier.Text = _param.SelectedDR.Supplier.Name;
                txtSDR.Text = _param.SelectedDR.SDRNumber;
                _selectedSupplier = _param.SelectedDR.Supplier;
            }
            else if (_param.ReceiptType == ReceiptType.DR){
                lblSupplier.Text = _param.SelectedDR.Customer.Name;
                txtSDR.Text = _param.SelectedDR.DRNumber;
                _selectedCustomer = _param.SelectedDR.Customer;
                txtProject.Text = _param.SelectedDR.Project;
                txtAddress.Text = _param.SelectedDR.DeliveryAddress;
            }

            dateTimePicker1.Value = _param.SelectedDR.Date;
            txtNote.Text = _param.SelectedDR.Note;
            _currentDr = _param.SelectedDR;
        }

        private void LoadItemsToGridView(){
            _drItems = _itemService.GetDRItemsByDR(_currentDr.Id);

            foreach (var i in _drItems){
                AddToListView(i);
            }
        }

        private void SetTitle(){
            if (_param.ReceiptType == ReceiptType.SDR){
                this.Text = "In (SDR)";
                grdSupplierCustomer.Text = "Supplier DR";
                _drText = "SDR";
                lblSupplierLabel.Text = "Supplier :";
                lblSupplier.Text = "Click here to provide supplier....";
                cmdInOut.Text = "In";
                listView1.Columns[3].Text = "In";
            }
            else if (_param.ReceiptType == ReceiptType.DR){
                this.Text = "Out (DR)";
                grdSupplierCustomer.Text = "Customer DR";
                _drText = "DR";
                lblSupplierLabel.Text = "Customer :";
                lblSupplier.Text = "Click here to provide customer....";
                cmdInOut.Text = "Out";
                listView1.Columns[3].Text = "Out";
            }
        }

        private void lblSupplier_Click(object sender, EventArgs e){
            SupplierCustomerMaintenaceForm sDialog = new SupplierCustomerMaintenaceForm(_supplierService,
                _customerService, Operation.Search, _param.SC);
            sDialog.ShowDialog();

            if (sDialog.HasSelectedSupplier){
                if (_param.ReceiptType == ReceiptType.SDR){
                    _selectedSupplier = sDialog.SelectedSupplier;
                    if (_selectedSupplier != null)
                        lblSupplier.Text = _selectedSupplier.Name;
                }
                else if (_param.ReceiptType == ReceiptType.DR){
                    _selectedCustomer = sDialog.SelectedCustomer;
                    if (_selectedCustomer != null)
                        lblSupplier.Text = _selectedCustomer.Name;
                }
            }
        }

        private void lblItem_Click(object sender, EventArgs e){
            ItemBrowserForm f = new ItemBrowserForm(_items);
            f.ShouldShowQuantityColumn = true;
            f.ShowDialog();

            if (!f.IsClosed){
                _selectedItemToAdd = f.SelectedItem;
                SetValueToControls();
            }
        }

        private void AddToListView(DRItem drItem){
            string[] arr = new string[4];
            arr[0] = Convert.ToString(drItem.Item.Id);
            arr[1] = Convert.ToString(drItem.Item.Name);
            arr[2] = Convert.ToString(drItem.Item.MeasuredBy);
            arr[3] = Convert.ToString(drItem.Qty);

            _selectedItemToAdd = null;

            ListViewItem lit = new ListViewItem(arr);
            listView1.Items.Add(lit);
        }

        private void SetDefaultForUI(){
            lblItem.Text = "Click here to search item...";
            lblCurrentQty.Text = "0";
            txtQty.Text = "0";
        }

        private bool IsItemAlreadyIncluded(){
            bool isIncluded = false;

            foreach (ListViewItem i in listView1.Items){
                long itemId = Convert.ToInt32(i.SubItems[0].Text);
                if (itemId == _selectedItemToAdd.Id){
                    isIncluded = true;
                    break;
                }
            }

            return isIncluded;
        }

        private void SetValueToControls(){
            lblItem.Text = _selectedItemToAdd.Name;
            lblCurrentQty.Text = Convert.ToString(_selectedItemToAdd.GetAppropriateQuantity);

            if (_selectedItemToAdd.MeasuredBy == MeasuredBy.Quantity){
                lblMeasuredBy.Text = "Qty.";
                lblMeasuredBy1.Text = "Qty.";
            }
            else if (_selectedItemToAdd.MeasuredBy == MeasuredBy.Meters){
                lblMeasuredBy.Text = "m.";
                lblMeasuredBy1.Text = "m.";
            }
            else if (_selectedItemToAdd.MeasuredBy == MeasuredBy.Feet){
                lblMeasuredBy.Text = "f.";
                lblMeasuredBy1.Text = "f.";
            }
        }

        private void cmdRemove_Click(object sender, EventArgs e){
            if (listView1.SelectedIndices.Count > 0){
                RemoveItemDialog d = new RemoveItemDialog();
                d.ShowDialog();
                if (d.IsOk){
                    int selectedIndex = listView1.SelectedIndices[0];
                    RemoveItemFromDBandMakeHistory(d.Comment);
                    listView1.Items.RemoveAt(selectedIndex);
                }
            }
            else{
                MessageBox.Show("Please select Item from the list!",
                    "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void RemoveItemFromDBandMakeHistory(string comment){
            InOutDRParam p = new InOutDRParam();
            p.DRItem = GetSelectedItemInListView();
            p.Note = "Item is removed from " + _drText + " : " + comment;
            p.InOrOut = DetermineIfInOrOut(true);

            _inOutService.RemoveFromDR(p);
            _drItems.Remove(p.DRItem);
        }

        private DRItem GetSelectedItemInListView(){
            long itemId = Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text);
            DRItem item = _drItems.FirstOrDefault(d => d.Item.Id == itemId);
            return item;
        }

        private void cmdCreate_Click(object sender, EventArgs e){
            if (_param.Operation == Operation.Add)
                CreateDR();
            else if (_param.Operation == Operation.Edit){
                EnableDisableDrControls(true);
                ShowSaveAndCancelForEdit(true);
            }
        }

        private void CreateDR(){
            if (_param.ReceiptType == ReceiptType.SDR){
                if (_selectedSupplier == null){
                    MessageBox.Show("Please provide supplier",
                        "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            else if (_param.ReceiptType == ReceiptType.DR){
                if (_selectedCustomer == null){
                    MessageBox.Show("Please provide customer", 
                        "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }

            if (string.IsNullOrWhiteSpace(txtSDR.Text)){
                MessageBox.Show("Please provide " + _drText + " no.",
                    "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DR dr = new DR();
            if (_param.ReceiptType == ReceiptType.SDR){
                dr.Supplier = _selectedSupplier;
                dr.SDRNumber = txtSDR.Text; 
            }
            else if (_param.ReceiptType == ReceiptType.DR){
                dr.Customer = _selectedCustomer;
                dr.DRNumber = txtSDR.Text;
                dr.Project = txtProject.Text;
                dr.DeliveryAddress = txtAddress.Text;
            }

            dr.Type = _param.ReceiptType;
            dr.Date = dateTimePicker1.Value;
            dr.Note = txtNote.Text;

            _drService.Add(dr);
            _currentDr = _drService.NewlyCreatedDR;

            EnableDisableDrControls(false);
            cmdCreate.Enabled = false;
            EnableDisableItemControls(true);
        }

        private void EnableDisableDrControls(bool enabled){
            grdSupplierCustomer.Enabled = enabled;

            if (_param.ReceiptType == ReceiptType.SDR){
                txtProject.Enabled = !enabled;
                txtAddress.Enabled = !enabled;
            }
            else if (_param.ReceiptType == ReceiptType.DR){
                txtProject.Enabled = enabled;
                txtAddress.Enabled = enabled;
            }
        }

        private void EnableDisableItemControls(bool enabled){
            grdItems.Enabled = enabled;
        }

        private void cmdClose_Click(object sender, EventArgs e){
            this.Close();
        }

        private void cmdSaveEdit_Click(object sender, EventArgs e){
            UpdateDR();
            MessageBox.Show(_drText + " Edited!");
            ShowSaveAndCancelForEdit(false);
            EnableDisableDrControls(false);
        }

        private void UpdateDR(){
            if (_param.ReceiptType == ReceiptType.SDR){
                _param.SelectedDR.SDRNumber = txtSDR.Text;
                _param.SelectedDR.Supplier = _selectedSupplier;               
            }
            else if (_param.ReceiptType == ReceiptType.DR){
                _param.SelectedDR.DRNumber = txtSDR.Text;
                _param.SelectedDR.Customer = _selectedCustomer;
                _param.SelectedDR.Project = txtProject.Text;
                _param.SelectedDR.DeliveryAddress = txtAddress.Text;
            }

            _param.SelectedDR.Date = dateTimePicker1.Value;
            _param.SelectedDR.Note = txtNote.Text;
            _drService.Edit(_param.SelectedDR);
        }

        private void cmdCancel_Click(object sender, EventArgs e){
            ShowSaveAndCancelForEdit(false);
            EnableDisableDrControls(false);
        }

        private void cmdInOut_Click(object sender, EventArgs e){
            if (_selectedItemToAdd != null){
                if (IsItemAlreadyIncluded()){
                    MessageBox.Show("Item already in the list!",
                        "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if (txtQty.Text == "0" || string.IsNullOrWhiteSpace(txtQty.Text)){
                    MessageBox.Show("Please provide Quantity!",
                        "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else{
                    DRItem item = MakeDRItem();
                    AddOrSubtractToStockAndMakeHistory(item);
                    AddToListView(item);
                    _drItems.Add(item);
                    SetDefaultForUI();
                }
            }
            else{
                MessageBox.Show("Please select item!",
                    "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void AddOrSubtractToStockAndMakeHistory(DRItem item){
            InOutDRParam p = new InOutDRParam();
            p.DRItem = item;
            p.InOrOut = DetermineIfInOrOut(false);

            _inOutService.InOutWithDR(p);
        }

        private InOrOut DetermineIfInOrOut(bool isRemovedItem){
            InOrOut inOrOut = InOrOut.In;

            if (_param.ReceiptType == ReceiptType.SDR){
                if (isRemovedItem)
                    inOrOut = InOrOut.Out;
                else
                    inOrOut = InOrOut.In; 
            }
            else if (_param.ReceiptType == ReceiptType.DR){
                if (isRemovedItem)
                    inOrOut = InOrOut.In;
                else
                    inOrOut = InOrOut.Out;
            }

            return inOrOut;
        }

        private DRItem MakeDRItem(){
            DRItem item = new DRItem();
            item.DR = _currentDr;
            item.Item = _selectedItemToAdd;
            item.Qty = Convert.ToDouble(txtQty.Text);

            return item;
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e){
            AllowNumberOnly(sender, e);
        }

        private static void AllowNumberOnly(object sender, KeyPressEventArgs e){
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.')){
                e.Handled = true;
            }
            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1)){
                e.Handled = true;
            }
        }
    }
}

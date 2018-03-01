using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using FBFInventory.Domain.Entity;
using FBFInventory.Infrastructure.Dto;
using FBFInventory.Infrastructure.Service;
using FBFInventory.Winforms.Helper;

namespace FBFInventory.Winforms
{
    public partial class NewItemForm : Form
    {
        private readonly SupplierService _supplierService;
        private readonly CategoryService _categoryService;
        private readonly ItemService _itemService;
        private List<Item> _items;
        private Item _selectedItem;

        private List<Category> _categories; 
        private Supplier _selectedSupplier;
        private MeasuredBy _measuredBy;
        private int _selectedSupplierId = 0;
        private int _selectedCategoryId = 0;

        private Operation _currentOperation;

        private string _search;
        //This variable is for paging
        private int _pageSize = 20;
        private int _currentPage = 1;
        private int _pageCount = 1;


        public NewItemForm(SupplierService supplierService, CategoryService categoryService,
            ItemService itemService){
            _supplierService = supplierService;
            _categoryService = categoryService;
            _itemService = itemService;
            InitializeComponent();
        }

        private void NewItemForm_Load(object sender, EventArgs e){
            LoadCategories();
            LoadMeasuredBy();
            SearchItem();
            OnNormalMode();
        }

        private void LoadCategories(){
            _categories = _categoryService.GetAllCategories();
            cboCategory.DataSource = _categories;
            cboCategory.ValueMember = "Id";
            cboCategory.DisplayMember = "Name";

            cboCategory.SelectedIndex = 0;
        }

        private void LoadMeasuredBy(){
            cboMeasuredBy.DataSource = Enum.GetValues(typeof (MeasuredBy));
            cboMeasuredBy.SelectedIndex = 0;
        }

        private void lblSupplier_Click(object sender, EventArgs e){
            SupplierCustomerMaintenaceForm sDialog =
                new SupplierCustomerMaintenaceForm(_supplierService, null, Operation.Search, SupplierOrCustomer.Supplier);
            sDialog.ShowDialog();
            _selectedSupplier = sDialog.SelectedSupplier;

            if (sDialog.HasSelectedSupplier){
                if (_selectedSupplier != null)
                    lblSupplier.Text = _selectedSupplier.Name;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e){
            if (cboCategory.SelectedValue is Int32){
                _selectedCategoryId = Convert.ToInt32(cboCategory.SelectedValue);
            }
        }

        private void cboMeasuredBy_SelectedIndexChanged(object sender, EventArgs e){
            MeasuredBy measuredBy;
            Enum.TryParse<MeasuredBy>(cboMeasuredBy.SelectedValue.ToString(), out measuredBy);

            _measuredBy = measuredBy;
            SetThresholdLabelValue();
        }

        private void SetThresholdLabelValue(){
            if (_measuredBy == MeasuredBy.Quantity){
                lblMeasuredBy.Text = "Qty";
            }
            else if (_measuredBy == MeasuredBy.Meters){
                lblMeasuredBy.Text = "m";
            }
            else if (_measuredBy == MeasuredBy.Feet){
                lblMeasuredBy.Text = "f";
            }
        }

        private void SearchItem(){
            ItemSearchParam p = new ItemSearchParam();
            p.Name = _search;
            p.CurrentPage = _currentPage - 1;
            p.PageSize = _pageSize;          
            p.ShouldFilterByStatus = false;
            p.ShouldIncludeSupplierAndCustomer = true;

            ItemSearchResult result = _itemService.SearchItemsWithPaging(p);

            _items = result.Results;
            PopulateListview(result.Results);
            _pageCount = result.PageCount;
            lblNavigation.Text = (_currentPage) + " / " + (_pageCount);
        }

        private void PopulateListview(List<Item> items){
            listView1.Items.Clear();
            foreach (var i in items){
                string[] arr = new string[4];
                arr[0] = Convert.ToString(i.Id);
                arr[1] = Convert.ToString(i.Name);
                arr[2] = Convert.ToString(i.MeasuredBy);

                if (i.MeasuredBy == MeasuredBy.Quantity)
                    arr[3] = Convert.ToString(i.Quantity);
                else if (i.MeasuredBy == MeasuredBy.Meters)
                    arr[3] = Convert.ToString(i.Meters);
                else if (i.MeasuredBy == MeasuredBy.Feet)
                    arr[3] = Convert.ToString(i.Feet);

                ListViewItem lit = new ListViewItem(arr);

                listView1.Items.Add(lit);
            }
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            _search = txtSearch.Text;
            _pageSize = 5;
            _currentPage = 1;
            SearchItem();
        }

        private void cmdFirst_Click(object sender, EventArgs e){
            _currentPage = 1;
            SearchItem();
        }

        private void cmdPrevious_Click(object sender, EventArgs e){
            if (_currentPage > 1){
                _currentPage--;
                SearchItem();
            }  
        }

        private void cmdNext_Click(object sender, EventArgs e){
            if (_currentPage < _pageCount){
                _currentPage++;
                SearchItem();
            }
        }

        private void cmdLast_Click(object sender, EventArgs e){
            _currentPage = _pageCount == 0 ? 1 : _pageCount;
            SearchItem();
        }

        private void cmdAdd_Click(object sender, EventArgs e){
            _currentOperation = Operation.Add;
            OnAddMode();
        }

        private void cmdEdit_Click(object sender, EventArgs e){
            if (_selectedItem == null){
                MessageBox.Show("Please select Item from the list!",
                    "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            _currentOperation = Operation.Edit;
            OnEditMode();
        }

        private void cmdSave_Click(object sender, EventArgs e){
            if (_selectedSupplier == null){
                MessageBox.Show("Please provide supplier!",
                    "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (_currentOperation == Operation.Add){
                Item item = new Item();
                item.Name = txtItemDesc.Text;
                item.Supplier = _selectedSupplier;
                item.MeasuredBy = _measuredBy;

                item.Threshold = SetDefaultIfNull(txtThreshold.Text);
                item.Cost = SetDefaultIfNull(txtCost.Text);

                _categoryService.AddNewItem(item, _selectedCategoryId);
                MessageBox.Show("Item Added!");
                OnNormalMode();
            }
            else if (_currentOperation == Operation.Edit){
                _selectedItem.Name = txtItemDesc.Text;
                _selectedItem.Category = _categories.FirstOrDefault(c => c.Id == _selectedCategoryId);
                _selectedItem.Supplier = _selectedSupplier;
                _selectedItem.MeasuredBy = _measuredBy;
                _selectedItem.Threshold = SetDefaultIfNull(txtThreshold.Text);
                _selectedItem.Cost = SetDefaultIfNull(txtCost.Text);
                _selectedItem.IsPhaseOut = chkPhaseOut.Checked;

                _itemService.Edit(_selectedItem);

                MessageBox.Show("Item Edited!");
                OnNormalMode();
            }

            SearchItem();
            ClearFields();
        }

        private double SetDefaultIfNull(string value){
            if (string.IsNullOrWhiteSpace(value))
                return 0;

            return Convert.ToDouble(value);
        }

        private void cmdCancel_Click(object sender, EventArgs e){
            OnNormalMode();
            SearchItem();
            ClearFields();
        }

        private void OnEditMode(){
            EnableControl();
            listView1.Enabled = false;
            SearchGroupBox.Enabled = false;

            cmdAdd.Enabled = false;
            cmdAdd.Visible = false;

            cmdEdit.Enabled = false;
            cmdEdit.Visible = false;

            cmdSave.Enabled = true;
            cmdSave.Visible = true;

            cmdCancel.Enabled = true;
            cmdCancel.Visible = true;
        }

        private void OnAddMode(){
            OnEditMode();
            ClearFields();
        }

        private void OnNormalMode(){
            _currentOperation = Operation.Nothing;
            DisableControl();

            cmdSave.Enabled = false;
            cmdSave.Visible = false;

            cmdCancel.Enabled = false;
            cmdCancel.Visible = false;

            cmdAdd.Enabled = true;
            cmdAdd.Visible = true;

            cmdEdit.Enabled = true;
            cmdEdit.Visible = true;

            SearchGroupBox.Enabled = true;
        }

        private void DisableControl(){
            AddEditControls.Enabled = false;
            listView1.Enabled = true;
        }

        private void EnableControl(){
            AddEditControls.Enabled = true;
            listView1.Enabled = false;
        }

        private void ClearFields(){
            txtItemDesc.Text = string.Empty;
            txtThreshold.Text = string.Empty;
            txtCost.Text = string.Empty;
            _measuredBy = MeasuredBy.Quantity;
            cboMeasuredBy.SelectedIndex = 0;
            cboCategory.SelectedIndex = 0;
            _selectedSupplier = null;
            lblSupplier.Text = "Click here to provide supplier....";
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetSelectedItem();
            BindSelectedItemToControls();
        }

        private void SetSelectedItem(){
            if (listView1.SelectedItems.Count > 0)
            {
                int id = Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text);
                _selectedItem = _items.FirstOrDefault(i => i.Id == id);
            }
        }

        private void BindSelectedItemToControls(){
            _selectedSupplier = _selectedItem.Supplier;
            lblSupplier.Text = _selectedSupplier.Name;

            txtItemDesc.Text = _selectedItem.Name;
            SetSelectionOfCategoryCombobox();
            txtThreshold.Text = Convert.ToString(_selectedItem.Threshold);
            txtCost.Text = Convert.ToString(_selectedItem.Cost);
            chkPhaseOut.Checked = _selectedItem.IsPhaseOut;
            cboMeasuredBy.SelectedItem = _selectedItem.MeasuredBy;
        }

        private void SetSelectionOfCategoryCombobox(){
            cboCategory.SelectedItem = _categories.FirstOrDefault(c => c.Id == _selectedItem.Category.Id);
        }
    }
}

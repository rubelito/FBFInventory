using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using FBFInventory.Domain.Entity;
using FBFInventory.Domain.History;
using FBFInventory.Infrastructure.Dto;
using FBFInventory.Infrastructure.Service;
using FBFInventory.Winforms.Helper;

namespace FBFInventory.Winforms
{
    public partial class ReturnedItemForm : Form
    {
        private readonly ReturnedHistoryService _returnedHistoryService;
        private readonly InOutService _inOutService;
        private readonly DRService _drService;

        private bool _isEdit;
        private DR _dr;
        private ReturnedHistory _history;

        private List<DRItem> _drItems;
        private List<Item> _items;

        private Item _selectedItemForReturn;
        private Item _selectedItemForScrap;

        private List<ReturnedItem> _goodItems;
        private List<ScrapItem> _scrapItems;

        private Operation _op;

        public ReturnedItemForm(ReturnedHistory history, ReturnedHistoryService returnedHistoryService,
            InOutService inOutService, DRService drService){
            _history = history;
            _returnedHistoryService = returnedHistoryService;
            _inOutService = inOutService;
            _drService = drService;
            InitializeComponent();
            _goodItems = new List<ReturnedItem>();
            _scrapItems = new List<ScrapItem>();
        }

        private void ReturnedItemForm_Load(object sender, EventArgs e){
            DetermineIfEdit();
        }

        private void DetermineIfEdit(){
            ShowSaveAndCancelForEdit(false);
            if (_history != null){
                _op = Operation.Edit;
                LoadHistoryToControls();
                cmdCreate.Text = "Edit";
                EnableDisableDrControls(false);
                lblDr.Enabled = false;
                _dr = _history.DR;
                GetItemsWithinDr();
                LoadGoodAndScrapItemsToListView();
            }
            else{
                _op = Operation.Add;
                cmdCreate.Text = "Create";
                EnableDisableItemControls(false);
            }
        }

        private void GetItemsWithinDr(){
            _drItems = _drService.GetItemsWithinDR(_dr.Id);
            _items = _drItems.Select(d => d.Item).ToList();
        }

        private void LoadGoodAndScrapItemsToListView(){
            _goodItems = _history.GoodItems;
            _scrapItems = _history.ScrapItems;

            foreach (var good in _goodItems){
                AddToListView(false, good, null);
            }

            foreach (var scrap in _scrapItems){
                AddToListView(true, null, scrap);
            }
        }

        private void LoadHistoryToControls(){
            lblDr.Text = _history.DR.DRNumber;
            txtProjectEngineer.Text = _history.ProjectEngineer;
            lblProject.Text = _history.DR.Project;
            lblAddress.Text = _history.DR.DeliveryAddress;
            dateTimePicker1.Value = _history.Date;
            txtNote.Text = _history.Note;
        }

        private void cmdCreate_Click(object sender, EventArgs e){
            if (_op == Operation.Add){
                CreateReturnHistory();
            }
            else if (_op == Operation.Edit){
                EnableDisableDrControls(true);
                ShowSaveAndCancelForEdit(true);
            }
        }

        private void CreateReturnHistory(){
            if (_dr == null){
                MessageBox.Show("Please provide DR",
                    "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtProjectEngineer.Text)){
                MessageBox.Show("Please provide Project Engr.",
                    "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            ReturnedHistory h = new ReturnedHistory();
            h.DR = _dr;
            h.DRNumber = _dr.DRNumber;
            h.ProjectEngineer = txtProjectEngineer.Text;
            h.Date = dateTimePicker1.Value;
            h.Note = txtNote.Text;

            _returnedHistoryService.Add(h);
            _history = _returnedHistoryService.NewlyCreatedHistory;
            LoadGoodAndScrapItemsToListView();

            EnableDisableDrControls(false);
            cmdCreate.Enabled = false;
            EnableDisableItemControls(true);

            MessageBox.Show("Record Created!");
            _dr = _history.DR;
            GetItemsWithinDr();
        }

        private void cmdSaveEdit_Click(object sender, EventArgs e){
            UpdateReturnedHistory();
            ShowSaveAndCancelForEdit(false);
            EnableDisableDrControls(false);
        }

        private void UpdateReturnedHistory(){
            _history.ProjectEngineer = txtProjectEngineer.Text;
            _history.Date = dateTimePicker1.Value;
            _history.Note = txtNote.Text;

            _returnedHistoryService.Edit(_history);
            MessageBox.Show("Record Edited!");
        }

        private void cmdCancel_Click(object sender, EventArgs e){
            ShowSaveAndCancelForEdit(false);
            EnableDisableDrControls(false);
        }

        private void ShowSaveAndCancelForEdit(bool shouldShow){
            cmdSaveEdit.Visible = shouldShow;
            cmdCancel.Visible = shouldShow;
            cmdCreate.Visible = !shouldShow;
        }

        private void EnableDisableDrControls(bool enabled){
            grdControl.Enabled = enabled;
        }

        private void EnableDisableItemControls(bool enabled){
            grdItem.Enabled = enabled;
        }

        private void lblDr_Click(object sender, EventArgs e){
            DRBrowserForm f = new DRBrowserForm(_drService);
            f.ShowDialog();

            if (!f.IsClosed){
                _dr = f.SelectedDR;
                if (IsDRHasExistingReturnedHistory()){
                    MessageBox.Show("DR " + _dr.DRNumber +
                                    " already have existing Returned History. Please use existing"
                        , "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else{
                    LoadDrInfoToControls();
                    EnableDisableDrControls(true);
                    ShowSaveAndCancelForEdit(false);
                }
            }
        }

        private bool IsDRHasExistingReturnedHistory(){
            ReturnedHistory h = _returnedHistoryService.GetHistoryByDR(_dr.Id);
            return h != null;
        }

        private void LoadDrInfoToControls(){
            lblDr.Text = _dr.DRNumber;
            lblProject.Text = _dr.Project;
            lblAddress.Text = _dr.DeliveryAddress;
        }

        private void lblReturnItem_Click(object sender, EventArgs e){
            ItemBrowserForm f = new ItemBrowserForm(_items);
            f.ShouldShowQuantityColumn = false;
            f.ShowDialog();

            if (!f.IsClosed){
                _selectedItemForReturn = f.SelectedItem;
                SetReturnValueToControls();
            }
        }

        private void lblScrapItem_Click(object sender, EventArgs e){
            ItemBrowserForm f = new ItemBrowserForm(_items);
            f.ShouldShowQuantityColumn = false;
            f.ShowDialog();

            if (!f.IsClosed){
                _selectedItemForScrap = f.SelectedItem;
                SetScrapValueToControls();
            }
        }

        private void SetReturnValueToControls(){
            lblReturnItem.Text = _selectedItemForReturn.Name;
            DRItem item = _drItems.FirstOrDefault(d => d.Item.Id == _selectedItemForReturn.Id);
            lblGoodQty.Text = Convert.ToString(item.Qty);

            if (_selectedItemForReturn.MeasuredBy == MeasuredBy.Quantity){
                lblGoodMeasuredBy.Text = "Qty.";
                lblReturnMeasuredBy1.Text = "Qty.";
            }
            else if (_selectedItemForReturn.MeasuredBy == MeasuredBy.Meters){
                lblGoodMeasuredBy.Text = "m.";
                lblReturnMeasuredBy1.Text = "m.";
            }
            else if (_selectedItemForReturn.MeasuredBy == MeasuredBy.Feet){
                lblGoodMeasuredBy.Text = "f.";
                lblReturnMeasuredBy1.Text = "f.";
            }
        }

        private void SetScrapValueToControls(){
            lblScrapItem.Text = _selectedItemForScrap.Name;
            DRItem item = _drItems.FirstOrDefault(d => d.Item.Id == _selectedItemForScrap.Id);
            lblScrapQty.Text = Convert.ToString(item.Qty);

            if (_selectedItemForScrap.MeasuredBy == MeasuredBy.Quantity){
                lblScrapMeasuredBy.Text = "Qty.";
                lblScrapMeasuredBy1.Text = "Qty.";
            }
            else if (_selectedItemForScrap.MeasuredBy == MeasuredBy.Meters){
                lblScrapMeasuredBy.Text = "m.";
                lblScrapMeasuredBy1.Text = "m.";
            }
            else if (_selectedItemForScrap.MeasuredBy == MeasuredBy.Feet){
                lblScrapMeasuredBy.Text = "f.";
                lblScrapMeasuredBy1.Text = "f.";
            }
        }

        private void SetDefaultUIForReturn(){
            lblReturnItem.Text = "Click here to search item...";
            lblGoodQty.Text = "0";
            txtGoodQty.Text = "0";
        }

        private void SetDefaultUIForScrap(){
            lblScrapItem.Text = "Click here to search item...";
            lblScrapQty.Text = "0";
            txtScrapQty.Text = "0";
        }

        private void cmdReturn_Click(object sender, EventArgs e){
            ReturnGoodItem();
        }

        private void ReturnGoodItem(){
            if (_selectedItemForReturn != null)
            {
                if (IsItemAlreadyIncluded(goodListView, _selectedItemForReturn.Id))
                {
                    MessageBox.Show("Item already in the good list!",
                        "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if (txtGoodQty.Text == "0" || string.IsNullOrWhiteSpace(txtGoodQty.Text))
                {
                    MessageBox.Show("Please provide Quantity!",
                        "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if (IsInputIsGreaterThanCurrentQty(txtGoodQty, lblGoodQty))
                {
                    MessageBox.Show("In should not be greated than DR stocks!",
                        "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    ReturnedItem item = MakeGoodItem();
                    AddToStocksAndMakeHistory(item);
                    AddToListView(false, item, null);
                    SetDefaultUIForReturn();
                    _selectedItemForReturn = null;
                }
            }
            else
            {
                MessageBox.Show("Please select item!",
                    "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private bool IsInputIsGreaterThanCurrentQty(TextBox inTextBox, Label qtyLabel){
            return Convert.ToDouble(inTextBox.Text) > Convert.ToDouble(qtyLabel.Text);
        }

        private void AddToStocksAndMakeHistory(ReturnedItem itemTobeReturned){
            ReturnedItem rI = new ReturnedItem();
            rI.Item = itemTobeReturned.Item;
            rI.ReturnedHistory = _history;
            rI.Qty = Convert.ToDouble(itemTobeReturned.Qty);
            rI.DateAdded = DateTime.Now;

            ReturnInOutParam p = new ReturnInOutParam();
            p.ReturnedItem = rI;
            p.InOrOut = InOrOut.In;
            p.Note = rI.Qty + " Item(s) Returned To Stocks. Please refer to Return History. DR : " +
                     _dr.DRNumberToDisplay;
            p.DrId = _dr.Id;
            p.ItemId = rI.Item.Id;

            _inOutService.InOutGoodItems(p);
        }

        private void cmdScrap_Click(object sender, EventArgs e){
            ScrapItem();
        }

        private void ScrapItem(){
            if (_selectedItemForScrap != null){
                if (IsItemAlreadyIncluded(scrapListView, _selectedItemForScrap.Id)){
                    MessageBox.Show("Item already in the scrap list!",
                        "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if (txtScrapQty.Text == "0" || string.IsNullOrWhiteSpace(txtScrapQty.Text)){
                    MessageBox.Show("Please provide Quantity!",
                        "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if (IsInputIsGreaterThanCurrentQty(txtScrapQty, lblScrapQty)){
                    MessageBox.Show("In should not be greated than DR stocks!",
                        "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else{
                    ScrapItem item = MakeScrapItem();
                    AddToScrapAndMakeHistory(item);
                    AddToListView(true, null, item);
                    SetDefaultUIForScrap();
                    _selectedItemForScrap = null;
                }
            }
            else{
                MessageBox.Show("Please select item!",
                    "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void AddToScrapAndMakeHistory(ScrapItem scrap){
            ScrapItem sI = new ScrapItem();
            sI.Item = scrap.Item;
            sI.ReturnedHistory = _history;
            sI.Qty = Convert.ToDouble(scrap.Qty);
            sI.DateAdded = DateTime.Now;

            ScrapInOutParam p = new ScrapInOutParam();
            p.Scrap = sI;
            p.InOrOut = InOrOut.In;

            _inOutService.InOutScrapItems(p);
        }

        private bool IsItemAlreadyIncluded(ListView listView, long Id){
            bool isIncluded = false;

            foreach (ListViewItem i in listView.Items){
                long itemId = Convert.ToInt32(i.SubItems[0].Text);
                if (itemId == Id){
                    isIncluded = true;
                    break;
                }
            }

            return isIncluded;
        }

        private ReturnedItem MakeGoodItem(){
            ReturnedItem i = new ReturnedItem();
            i.Item = _selectedItemForReturn;
            i.Qty = Convert.ToDouble(txtGoodQty.Text);

            return i;
        }

        private ScrapItem MakeScrapItem(){
            ScrapItem i = new ScrapItem();
            i.Item = _selectedItemForScrap;
            i.Qty = Convert.ToDouble(txtScrapQty.Text);

            return i;
        }

        private void AddToListView(bool isScrap, ReturnedItem goodItem, ScrapItem scrapItem){
            string[] arr = new string[3];

            if (isScrap){
                arr[0] = Convert.ToString(scrapItem.Item.Id);
                arr[1] = scrapItem.Item.Name;
                arr[2] = Convert.ToString(scrapItem.Qty);
                ListViewItem lit = new ListViewItem(arr);
                scrapListView.Items.Add(lit);
            }
            else{
                arr[0] = Convert.ToString(goodItem.Item.Id);
                arr[1] = goodItem.Item.Name;
                arr[2] = Convert.ToString(goodItem.Qty);
                ListViewItem lit = new ListViewItem(arr);
                goodListView.Items.Add(lit);
            }
        }

        private void cmdGoodRemove_Click(object sender, EventArgs e){
            if (goodListView.SelectedIndices.Count > 0){
                DialogResult r = MessageBox.Show("Are you sure you want to remove?",
                    "Remove",
                    MessageBoxButtons.YesNo);
                if (r == DialogResult.Yes){
                    int selectedIndex = goodListView.SelectedIndices[0];
                    RemoveGoodItemFromDBandMakeHistory();
                    goodListView.Items.RemoveAt(selectedIndex);
                }
            }
            else{
                MessageBox.Show("Please select Item from the good list!",
                    "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void RemoveGoodItemFromDBandMakeHistory(){
            ReturnInOutParam p = new ReturnInOutParam();
            p.ReturnedItem = GetGoodItemInListView();
            p.Note = p.ReturnedItem.Qty + " Item(s) is removed from Return History";
            p.InOrOut = InOrOut.Out;
            p.DrId = _dr.Id;
            p.ItemId = p.ReturnedItem.Item.Id;

            _inOutService.InOutGoodItems(p);
        }

        private ReturnedItem GetGoodItemInListView(){
            long itemId = Convert.ToInt32(goodListView.SelectedItems[0].SubItems[0].Text);
            ReturnedItem item = _goodItems.FirstOrDefault(d => d.Item.Id == itemId);
            return item;
        }

        private void cmdScrapRemove_Click(object sender, EventArgs e){
            if (scrapListView.SelectedIndices.Count > 0){
                DialogResult r = MessageBox.Show("Are you sure you want to remove?",
                    "Remove",
                    MessageBoxButtons.YesNo);
                if (r == DialogResult.Yes){
                    int selectedIndex = scrapListView.SelectedIndices[0];
                    RemoveScrapItemFromDBandMakeHistory();
                    scrapListView.Items.RemoveAt(selectedIndex);
                }
            }
            else{
                MessageBox.Show("Please select Item from the scrap list!",
                    "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void RemoveScrapItemFromDBandMakeHistory(){
            ScrapInOutParam p = new ScrapInOutParam();
            p.Scrap = GetScrapItemInListView();
            p.InOrOut = InOrOut.Out;

            _inOutService.InOutScrapItems(p);
        }

        private ScrapItem GetScrapItemInListView(){
            long itemId = Convert.ToInt32(scrapListView.SelectedItems[0].SubItems[0].Text);
            ScrapItem item = _scrapItems.FirstOrDefault(d => d.Item.Id == itemId);
            return item;
        }

        private void cmdClose_Click(object sender, EventArgs e){
            this.Close();
        }

        private void txtGoodQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            AllowNumberOnly(sender, e);
        }

        private void txtScrapQty_KeyPress(object sender, KeyPressEventArgs e){
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

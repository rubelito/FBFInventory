using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FBFInventory.Domain.Entity;
using FBFInventory.Infrastructure.Dto;
using FBFInventory.Infrastructure.EntityFramework;
using FBFInventory.Infrastructure.Service;
using FBFInventory.Winforms.Helper;
using FBFInventory.Winforms.Report;

namespace FBFInventory.Winforms
{
    public partial class MainForm : Form
    {

        private FBFDBContext _context;

        private int _pageSize = 19;

        private int _drCurrentPage = 1;
        private int _drPageCount = 1;

        private int _returnCurrentPage = 1;
        private int _returnPageCount = 1;

        private int _itemCurrentPage = 1;
        private int _itemPageCount = 1;

        private List<DR> _Drs;
        private List<Item> _items;
        private List<ReturnedHistory> _returnedHistories;

        private string _navigationTextForDr;
        private string _navigationTextForItem;
        private string _navigationTextForReturnHistories;

        private bool _isLoggedIn = false;
        private string _name;
        private string _role;

        public bool HasError { get; set; }
        public Exception Exception { get; set; }

        private string _databaseIPAddress;

        public MainForm(){
            InitializeComponent();

            //IDatabaseType type = new EfSqlServer("SQLServerDb");
            //_context = new FBFDBContext(type);           
        }

        private void MainForm_Load(object sender, EventArgs e){
            tabControl.Enabled = false;
            try{
                TryLogUser();
                if (_isLoggedIn)
                {
                    PopulateComboBox();
                    InitializeDbContext(_databaseIPAddress);
                    SearchDR();
                }
                else
                {
                    Close();
                }
            }
            catch (Exception ex){
                HasError = true;
                this.Exception = ex;
                this.Close();
            }
        }

        private void TryLogUser(){
            Login loginForm = new Login(_context);

            loginForm.ShowDialog();
            if (loginForm.IsLoggedIn){
                _name = loginForm.UserName;
                _role = loginForm.Role;
                _isLoggedIn = true;

                tabControl.Enabled = true;
                _databaseIPAddress = loginForm.IpAddress;

                if (_role == "Administrator")
                    EnablePrivilageForAdmin();
                else if (_role == "Member")
                    EnablePrivilageForMember();
                else if (_role == "Spectator")
                    EnablePrivilageForSpectator();
            }
        }

        private void EnablePrivilageForAdmin(){
            //All this can do by Admin;
        }

        private void EnablePrivilageForMember(){
            cmdUser.Enabled = false;
        }

        private void EnablePrivilageForSpectator(){
            cmdNewSDR.Enabled = false;
            cmdNewDR.Enabled = false;
            cmdEditDR.Enabled = false;
            cmdCreateReturn.Enabled = false;
            cmdEditReturn.Enabled = false;
            cmdIn.Enabled = false;
            cmdOut.Enabled = false;
            cmdShowSupplier.Enabled = false;
            cmdShowCustomer.Enabled = false;
            cmdShowCategory.Enabled = false;
            cmdItem.Enabled = false;
            cmdUser.Enabled = false;
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e){
            SetNavigationText();
        }

        private void PopulateComboBox(){
            cboCategory.DataSource = Enum.GetValues(typeof (ReceiptType));
            cboCategory.SelectedIndex = 0;

            cboItemCriteria.DataSource = Enum.GetValues(typeof (ItemSearchCriteria));
            cboItemCriteria.SelectedIndex = 0;
        }

        private void InitializeDbContext(string ipAddress){
            IDatabaseType type = new EfSqlServer("SQLServerDb", ipAddress);
            _context = new FBFDBContext(type);
        }

        private void SearchDR(){
            DRService service = new DRService(_context);

            SearchParam p = new SearchParam();
            p.CurrentPage = _drCurrentPage - 1;
            p.PageSize = _pageSize;

            p.ReceiptType = (ReceiptType) cboCategory.SelectedItem;
            p.DRNumber = txtDrNumber.Text;
            p.OrderBy = rbDRAscending.Checked ? OrdeBy.Ascending : OrdeBy.Descending;

            DRSearchResult r = service.DRSearch(p);
            _Drs = r.Results;
            LoadDRToListView();
            _drPageCount = r.PageCount;

            SetNavigationText();
        }

        private void LoadDRToListView(){
            listViewDr.Items.Clear();
            foreach (var i in _Drs){
                string[] arr = new string[7];
                arr[0] = Convert.ToString(i.Id);
                if (i.Type == ReceiptType.SDR){
                    arr[1] = Convert.ToString(i.SDRNumber);
                    arr[2] = Convert.ToString(i.Supplier.Name);
                }
                else if (i.Type == ReceiptType.DR){
                    arr[1] = Convert.ToString(i.DRNumber);
                    arr[2] = Convert.ToString(i.Customer.Name);
                }
                arr[3] = Convert.ToString(Convert.ToString(i.Type));
                arr[4] = i.Date.ToString();
                if (i.HasReturnedHistory)
                    arr[5] = "Yes";

                arr[6] = i.CreatedBy;

                ListViewItem lit = new ListViewItem(arr);
                listViewDr.Items.Add(lit);
            }
        }

        private void cmdDrSearch_Click(object sender, EventArgs e){
            _drCurrentPage = 1;
            SearchDR();
        }

        private void cmdSearchReturnHistories_Click(object sender, EventArgs e){
            _returnCurrentPage = 1;
            SearchReturnedHistory();
        }

        private void SearchReturnedHistory(){
            ReturnedHistoryService service = new ReturnedHistoryService(_context);

            SearchParam p = new SearchParam();
            p.CurrentPage = _returnCurrentPage - 1;
            p.PageSize = _pageSize;

            p.DRNumber = txtReturnedDr.Text;
            p.OrderBy = rbReturnedAscending.Checked ? OrdeBy.Ascending : OrdeBy.Descending;

            ReturnItemResult r = service.SearchReturnedHistories(p);
            _returnedHistories = r.Results;
            LoadToReturnHistoryListView();
            _returnPageCount = r.PageCount;

            SetNavigationText();
        }

        private void LoadToReturnHistoryListView(){
            listViewReturn.Items.Clear();
            foreach (var i in _returnedHistories){
                string[] arr = new string[7];
                arr[0] = Convert.ToString(i.Id);
                arr[1] = i.DR.DRNumber;
                arr[2] = i.DR.Customer.Name;
                arr[3] = i.DR.Project;
                arr[4] = i.ProjectEngineer;
                arr[5] = i.Date.ToString();
                arr[6] = i.CreatedBy;

                ListViewItem lit = new ListViewItem(arr);
                listViewReturn.Items.Add(lit);
            }
        }

        private void cmdFirst_Click(object sender, EventArgs e){
            if (tabControl.SelectedIndex == 0)
                FirstForDR();
            if (tabControl.SelectedIndex == 1)
                FirstForReturnedItems();
            if (tabControl.SelectedIndex == 2)
                FirstForItem();
        }

        private void FirstForDR(){
            _drCurrentPage = 1;
            SearchDR();
        }

        private void FirstForReturnedItems(){
            _returnCurrentPage = 1;
            SearchReturnedHistory();
        }

        private void FirstForItem(){
            _itemCurrentPage = 1;
            SearchItem();
        }

        private void cmdPrevious_Click(object sender, EventArgs e){
            if (tabControl.SelectedIndex == 0)
                PreviousForDR();
            if (tabControl.SelectedIndex == 1)
                PreviousForReturnedItem();
            if (tabControl.SelectedIndex == 2)
                PreviousForItem();
        }

        private void PreviousForDR(){
            if (_drCurrentPage > 1){
                _drCurrentPage--;
                SearchDR();
            }
        }

        private void PreviousForReturnedItem(){
            if (_returnCurrentPage > 1){
                _returnCurrentPage--;
                SearchReturnedHistory();
            }
        }

        private void PreviousForItem(){
            if (_itemCurrentPage > 1){
                _itemCurrentPage--;
                SearchItem();
            }
        }

        private void cmdNext_Click(object sender, EventArgs e){
            if (tabControl.SelectedIndex == 0)
                NextForDR();
            if (tabControl.SelectedIndex == 1)
                NextForReturnedItems();
            if (tabControl.SelectedIndex == 2)
                NextForItem();
        }

        private void NextForDR(){
            if (_drCurrentPage < _drPageCount){
                _drCurrentPage++;
                SearchDR();
            }
        }

        private void NextForReturnedItems(){
            if (_returnCurrentPage < _returnPageCount){
                _returnCurrentPage++;
                SearchReturnedHistory();
            }
        }

        private void NextForItem(){
            if (_itemCurrentPage < _itemPageCount){
                _itemCurrentPage++;
                SearchItem();
            }
        }

        private void cmdLast_Click(object sender, EventArgs e){
            if (tabControl.SelectedIndex == 0)
                LastForDR();
            if (tabControl.SelectedIndex == 1)
                LastForReturnItem();
            if (tabControl.SelectedIndex == 2)
                LastForItem();
        }

        private void LastForDR(){
            _drCurrentPage = _drPageCount == 0 ? 1 : _drPageCount;
            SearchDR();
        }

        private void LastForReturnItem(){
            _returnCurrentPage = _returnPageCount == 0 ? 1 : _returnPageCount;
            SearchReturnedHistory();
        }

        private void LastForItem(){
            _itemCurrentPage = _itemPageCount == 0 ? 1 : _itemPageCount;
            SearchItem();
        }

        private void cmdNewSDR_Click(object sender, EventArgs e){
            loadSDRForm(ReceiptType.SDR, Operation.Add, SupplierOrCustomer.Supplier, null);
        }

        private void cmdNewDR_Click(object sender, EventArgs e){
            loadSDRForm(ReceiptType.DR, Operation.Add, SupplierOrCustomer.Customer, null);
        }

        private void cmdEditDR_Click(object sender, EventArgs e){
            if (listViewDr.SelectedItems.Count == 0){
                MessageBox.Show("Please select (S)DR from the list", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            long drId = Convert.ToInt64(listViewDr.SelectedItems[0].SubItems[0].Text);

            DR selectedDr = _Drs.FirstOrDefault(d => d.Id == drId);
            if (selectedDr.Type == ReceiptType.SDR){
                loadSDRForm(ReceiptType.SDR, Operation.Edit,
                    SupplierOrCustomer.Supplier, selectedDr);
            }
            else if (selectedDr.Type == ReceiptType.DR){
                loadSDRForm(ReceiptType.DR, Operation.Edit,
                    SupplierOrCustomer.Customer, selectedDr);
            }
        }

        private void loadSDRForm(ReceiptType type, Operation operation,
            SupplierOrCustomer sc, DR selectedSDR){
            ItemService itemService = new ItemService(_context);
            HistoryService historyService = new HistoryService(_context);
            SupplierService supplierService = new SupplierService(_context);
            CustomerService customerService = new CustomerService(_context);
            DRService drService = new DRService(_context);
            ReturnedHistoryService returnedHistoryService = new ReturnedHistoryService(_context);
            InOutService inOutService = new InOutService(itemService, historyService,
                drService, returnedHistoryService);

            DRParam p = new DRParam();
            p.ReceiptType = type;
            p.Operation = operation;
            p.SC = sc;
            p.SelectedDR = selectedSDR;

            InWithDRForm f = new InWithDRForm(itemService, supplierService, customerService,
                drService, inOutService, p, _name);

            f.ShowDialog();
        }

        private void cmdShowSupplier_Click(object sender, EventArgs e){
            LoadMaintenance(SupplierOrCustomer.Supplier);
        }

        private void cmdShowCustomer_Click(object sender, EventArgs e){
            LoadMaintenance(SupplierOrCustomer.Customer);
        }

        private void LoadMaintenance(SupplierOrCustomer supplierOrCustomer){
            SupplierService supplierService = new SupplierService(_context);
            CustomerService customerService = new CustomerService(_context);

            var f = new SupplierCustomerMaintenaceForm(supplierService, customerService,
                Operation.Add, supplierOrCustomer);

            f.ShowDialog();
        }

        private void cmdShowCategory_Click(object sender, EventArgs e){
            CategoryService service = new CategoryService(_context);
            CategoryMaintenanceForm f = new CategoryMaintenanceForm(service);

            f.ShowDialog();
        }

        private void cmdItemSearch_Click(object sender, EventArgs e){
            _itemCurrentPage = 1;
            SearchItem();
        }

        private void SearchItem(){
            ItemService service = new ItemService(_context);

            ItemSearchParam p = new ItemSearchParam();
            p.CurrentPage = _itemCurrentPage - 1;
            p.PageSize = _pageSize;
            p.ShouldIncludeSupplierAndCustomer = false;
            p.Criteria = (ItemSearchCriteria) cboItemCriteria.SelectedItem;

            p.ShouldFilterByStatus = !rbAll.Checked;
            p.ActiveOnly = rbActive.Checked;
            p.OrberyBy = rbItemDescending.Checked ? OrdeBy.Descending : OrdeBy.Ascending;

            p.Name = txtItemSearch.Text;
            ItemSearchResult r = service.SearchItemsWithPaging(p);
            _items = r.Results;
            LoadItemtoListView();
            _itemPageCount = r.PageCount;

            SetNavigationText();
        }

        private void LoadItemtoListView(){
            listViewItem.Items.Clear();
            foreach (var i in _items){
                string[] arr = new string[5];
                arr[0] = Convert.ToString(i.Id);
                arr[1] = Convert.ToString(i.Name);
                arr[2] = Convert.ToString(i.MeasuredBy);
                arr[3] = Convert.ToString(i.GetAppropriateQuantity);
                arr[4] = Convert.ToString(i.Threshold);

                ListViewItem lit = new ListViewItem(arr);

                if (i.IsNearOutOfStock)
                    lit.BackColor = Color.LightPink;

                listViewItem.Items.Add(lit);
            }
        }

        private void cmdIn_Click(object sender, EventArgs e){
            ShowInOut(InOrOut.In);
        }

        private void cmdOut_Click(object sender, EventArgs e){
            ShowInOut(InOrOut.Out);
        }

        private void ShowInOut(InOrOut inOrOut){
            if (listViewItem.SelectedItems.Count >= 1){
                ItemService itemService = new ItemService(_context);
                DRService drService = new DRService(_context);
                HistoryService historyService = new HistoryService(_context);
                ReturnedHistoryService returnedHistoryService = new ReturnedHistoryService(_context);

                InOutService inOutService = new InOutService(itemService, historyService
                    , drService, returnedHistoryService);

                long itemId = Convert.ToInt64(listViewItem.SelectedItems[0].SubItems[0].Text);
                Item selectedItem = _items.FirstOrDefault(i => i.Id == itemId);

                InOutWithOutDRForm f = new InOutWithOutDRForm(itemService, inOutService,
                    inOrOut, selectedItem, _name);

                f.ShowDialog();
                if (f.HaveChangedQuantity)
                    SearchItem();
            }
            else{
                MessageBox.Show("Please select Item from the list", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void cmdItem_Click(object sender, EventArgs e){
            SupplierService supplierService = new SupplierService(_context);
            CategoryService categoryService = new CategoryService(_context);
            ItemService itemService = new ItemService(_context);

            NewItemForm f = new NewItemForm(supplierService, categoryService, itemService);
            f.ShowDialog();
        }

        private void cmdViewHistory_Click(object sender, EventArgs e){
            if (listViewItem.SelectedItems.Count > 0){
                long itemId = Convert.ToInt64(listViewItem.SelectedItems[0].SubItems[0].Text);
                Item selectedItem = _items.FirstOrDefault(i => i.Id == itemId);

                HistoryService historyService = new HistoryService(_context);

                HistoryViewForm f = new HistoryViewForm(historyService, selectedItem);
                f.ShowDialog();
            }
            else{
                MessageBox.Show("Please select item from list!", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void SetNavigationText(){
            _navigationTextForDr = (_drCurrentPage) + " / " + (_drPageCount);
            _navigationTextForReturnHistories = (_returnCurrentPage) + " / " + (_returnPageCount);
            _navigationTextForItem = (_itemCurrentPage) + " / " + (_itemPageCount);

            if (tabControl.SelectedIndex == 0)
                lblNavigation.Text = _navigationTextForDr;
            if (tabControl.SelectedIndex == 1)
                lblNavigation.Text = _navigationTextForReturnHistories;
            if (tabControl.SelectedIndex == 2)
                lblNavigation.Text = _navigationTextForItem;
        }

        private void cmdEditReturn_Click(object sender, EventArgs e){
            if (listViewReturn.SelectedItems.Count == 0){
                MessageBox.Show("Please select History from the list", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            long returnId = Convert.ToInt64(listViewReturn.SelectedItems[0].SubItems[0].Text);
            ItemService itemService = new ItemService(_context);
            HistoryService historyService = new HistoryService(_context);
            ReturnedHistoryService returnedHistoryService = new ReturnedHistoryService(_context);
            DRService drService = new DRService(_context);

            InOutService inOutService = new InOutService(itemService, historyService,
                drService, returnedHistoryService);

            ReturnedHistory h = returnedHistoryService.GetHistory(returnId);
            ReturnedItemForm f = new ReturnedItemForm(h, returnedHistoryService,
                inOutService, drService, _name);
            f.ShowDialog();
        }

        private void cmdCreateReturn_Click(object sender, EventArgs e){
            ItemService itemService = new ItemService(_context);
            HistoryService historyService = new HistoryService(_context);
            ReturnedHistoryService returnedHistoryService = new ReturnedHistoryService(_context);
            DRService drService = new DRService(_context);

            InOutService inOutService = new InOutService(itemService, historyService,
                drService, returnedHistoryService);

            ReturnedItemForm f = new ReturnedItemForm(null, returnedHistoryService
                , inOutService, drService, _name);
            f.ShowDialog();
        }

        private void cmdPrintForDaily_Click(object sender, EventArgs e){
            string fileName = "Daily - " + dtpDaily.Value.ToString("MMM dd, yyyy");
            var dialogResult = OpenSaveDialog(fileName);

            if (dialogResult.Result == DialogResult.OK){
                try{
                    GenerateDailyReport(dialogResult.FileName);
                    MessageBox.Show("Report Generated!");
                }
                catch (Exception ex){
                    MessageBox.Show(ex.Message, "Error Exporting Report");
                }
            }
        }

        private void GenerateDailyReport(string fileName){
            ItemService itemService = new ItemService(_context);
            HistoryService historyService = new HistoryService(_context);

            DailyHistoryReportService s = new DailyHistoryReportService(itemService, historyService);
            DateTime now = dtpDaily.Value.Date;
            var record = s.GetDailyReport(now);

            DailyReporter reporter =
                new DailyReporter(record, fileName);

            reporter.Export();
        }

        private void cmdPrintForWeekly_Click(object sender, EventArgs e){
            string fileName = "Weekly " + dtpWeeklyFrom.Value.ToString("MMM dd, yyyy")
                              + " - " + dtpWeeklyTo.Value.ToString("MMM dd, yyyy");

            var dialogResult = OpenSaveDialog(fileName);

            if (dialogResult.Result == DialogResult.OK){
                try{
                    GenerateWeeklyReport(dialogResult.FileName);
                    MessageBox.Show("Report Generated!");
                }
                catch (Exception ex){
                    MessageBox.Show(ex.Message, "Error Exporting Report");
                }
            }
        }

        private void GenerateWeeklyReport(string fileName){
            ItemService itemService = new ItemService(_context);
            HistoryService historyService = new HistoryService(_context);

            WeeklyHistoryReportService s = new WeeklyHistoryReportService(itemService, historyService);
            DateTime from = dtpWeeklyFrom.Value.Date;
            DateTime to = dtpWeeklyTo.Value.Date;
            var record = s.GetWeeklyReport(from, to);

            WeeklyReporter reporter =
                new WeeklyReporter(record, fileName);

            reporter.Export();
        }

        private void cmdPrintItem_Click(object sender, EventArgs e){
            string fileName = "Items - " + DateTime.Now.ToString("MMM dd, yyyy");

            var dialogResult = OpenSaveDialog(fileName);
            if (dialogResult.Result == DialogResult.OK){
                try{
                    GenerateItemsReport(dialogResult.FileName);
                    MessageBox.Show("Report Generated!");
                }
                catch (Exception ex){
                    MessageBox.Show(ex.Message, "Error Exporting Report");
                }
            }
        }

        private void GenerateItemsReport(string fileName){
            ItemService service = new ItemService(_context);

            ItemSearchParam p = new ItemSearchParam();
            p.ShouldIncludeSupplierAndCustomer = false;
            p.Criteria = (ItemSearchCriteria) cboItemCriteria.SelectedItem;

            p.ShouldFilterByStatus = !rbAll.Checked;
            p.ActiveOnly = rbActive.Checked;
            p.OrberyBy = rbItemDescending.Checked ? OrdeBy.Descending : OrdeBy.Ascending;

            p.Name = txtItemSearch.Text;
            var items = service.SearchItems(p);

            ItemsReporter reporter = new ItemsReporter(items, fileName);
            reporter.Export();
        }

        private SaveDialogResult OpenSaveDialog(string fileName){
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel Documents (*.xlsx)|*.xlsx";
            saveFileDialog.FileName = fileName;

            SaveDialogResult r = new SaveDialogResult();

            r.Result = saveFileDialog.ShowDialog();
            r.FileName = saveFileDialog.FileName;

            return r;
        }

        private void cmdChangePassword_Click(object sender, EventArgs e){
            UserService userService = new UserService(_context);
            ChangePasswordForm form = new ChangePasswordForm(userService, _name);
            form.ShowDialog();

            if (form.HasChangePassword)
            {
                this.Close();
            }
        }

        private void cmdUser_Click(object sender, EventArgs e)
        {
            UserService userService = new UserService(_context);
            UserListForm form = new UserListForm(userService, _name);

            form.ShowDialog();
        }
    }
}

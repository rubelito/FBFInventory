using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using FBFInventory.Domain.Entity;
using FBFInventory.Infrastructure.Dto;
using FBFInventory.Infrastructure.Service;
using FBFInventory.Winforms.Report;

namespace FBFInventory.Winforms
{
    public partial class HistoryViewForm : Form
    {
        private readonly HistoryService _historyService;
        private readonly Item _item;

        private List<ItemHistory> _histories;

        private int _pageSize = 20;
        private int _currentPage = 1;
        private int _pageCount = 1;

        private bool _searchWithDate;

        public HistoryViewForm(HistoryService historyService, Item item){
            _historyService = historyService;
            _item = item;
            InitializeComponent();
        }

        private void HistoryView_Load(object sender, EventArgs e){
            ChangeColumnTextForAppopriateMeasurement();
            _searchWithDate = false;
            SearchHistory();
            lblItemDesc.Text = _item.Name;
            lblMeasuredBy.Text = _item.MeasuredBy.ToString();
        }

        private void ChangeColumnTextForAppopriateMeasurement(){
            ColumnHeader beginningColumn = listView1.Columns[4];
            ColumnHeader qtyColumn = listView1.Columns[5];
            ColumnHeader endingColumn = listView1.Columns[6];

            if (_item.MeasuredBy == MeasuredBy.Quantity){
                beginningColumn.Text = "Beginning Qty";
                qtyColumn.Text = "Qty";
                endingColumn.Text = "Ending Qty";
            }
            else if (_item.MeasuredBy == MeasuredBy.Meters){
                beginningColumn.Text = "Beginning Meters";
                qtyColumn.Text = "Meters";
                endingColumn.Text = "Ending Meters";
            }
            else if (_item.MeasuredBy == MeasuredBy.Feet){
                beginningColumn.Text = "Beginning Feet";
                qtyColumn.Text = "Feet";
                endingColumn.Text = "Ending Feet";
            }
        }

        private void SearchHistory(){
            SearchParam p = MakeSearchParameter();

            HistorySearchResult r = _historyService.SearchHistoriesWithPaging(p);
            _histories = r.Results;
            LoadHistoryToListView();
            _pageCount = r.PageCount;

            SetNavigationText();
        }

        private SearchParam MakeSearchParameter(){
            SearchParam p = new SearchParam();
            p.CurrentPage = _currentPage - 1;
            p.PageSize = _pageSize;
            p.ItemId = _item.Id;
            p.SearchWithDate = _searchWithDate;
            p.From = dateTimePicker1.Value;
            p.To = dateTimePicker1.Value.AddDays(1);

            p.OrderBy = OrdeBy.Descending;
            return p;
        }

        private void LoadHistoryToListView(){
            listView1.Items.Clear();
            foreach (var h in _histories){
                string[] arr = new string[10];
                arr[0] = Convert.ToString(h.Id);
                arr[1] = Convert.ToString(h.Type);
                arr[2] = Convert.ToString(h.InOrOut);

                if (h.DR != null){
                    if (h.DR.Type == ReceiptType.SDR){
                        arr[3] = h.DR.SDRNumber;
                    }
                    else if (h.DR.Type == ReceiptType.DR){
                        arr[3] = h.DR.DRNumber;
                    }
                }

                arr[4] = Convert.ToString(h.AppopriateBeginningQty);
                arr[5] = Convert.ToString(h.AppopriateQty);
                arr[6] = Convert.ToString(h.AppopriateEndingQty);
                arr[7] = h.DateAdded.ToString();       

                if (string.IsNullOrEmpty(h.Note)){
                    if (h.DR != null)
                        arr[8] = h.DR.Note;
                }
                else{
                    arr[8] = h.Note;
                }

                arr[9] = h.CreatedBy;

                ListViewItem lit = new ListViewItem(arr);

                if (h.InOrOut == InOrOut.In)
                    lit.BackColor = Color.PaleGreen;
                else if (h.InOrOut == InOrOut.Out)
                    lit.BackColor = Color.LightCoral;

                if (h.IsMistaken)
                    lit.BackColor = Color.Orange;

                listView1.Items.Add(lit);
            }
        }

        private void cmdFirst_Click(object sender, EventArgs e){
            _currentPage = 1;
            SearchHistory();
        }

        private void cmdPrevious_Click(object sender, EventArgs e){
            if (_currentPage > 1){
                _currentPage--;
                SearchHistory();
            }
        }

        private void cmdNext_Click(object sender, EventArgs e){
            if (_currentPage < _pageCount){
                _currentPage++;
                SearchHistory();
            }
        }

        private void cmdLast_Click(object sender, EventArgs e){
            _currentPage = _pageCount == 0 ? 1 : _pageCount;
            SearchHistory();
        }

        private void SetNavigationText(){
            lblNavigation.Text = (_currentPage) + " / " + (_pageCount);
        }

        private void cmdClose_Click(object sender, EventArgs e){
            this.Close();
        }

        private void cmdDrSearch_Click(object sender, EventArgs e){
            _searchWithDate = true;
            SearchHistory();
        }

        private void cmdPrint_Click(object sender, EventArgs e){
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel Documents (*.xlsx)|*.xlsx";
            saveFileDialog.FileName = _item.Name + " Histories";
            if (saveFileDialog.ShowDialog() == DialogResult.OK){
                try{
                    GenerateExcelReport(saveFileDialog.FileName);
                    MessageBox.Show("Report Generated!");
                }
                catch (Exception ex){
                    MessageBox.Show(ex.Message, "Error Exporting Report");
                }
            }
        }

        private void GenerateExcelReport(string fileName){
            ItemReportDTO dto = new ItemReportDTO();
            dto.ItemName = _item.Name;
            dto.MeasuredBy = _item.MeasuredBy.ToString();

            SearchParam p = MakeSearchParameter();
            dto.ItemHistories = _historyService.SearchHistories(p);

            ItemHistoryReporter reporter = new ItemHistoryReporter(dto, fileName);
            reporter.Export();
        }
    }
}

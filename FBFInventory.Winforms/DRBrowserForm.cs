using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using FBFInventory.Domain.Entity;
using FBFInventory.Infrastructure.Service;

namespace FBFInventory.Winforms
{
    public partial class DRBrowserForm : Form
    {
        private readonly DRService _drService;
        private List<DR> _drs;
        private DR _selectedDr;
        private bool _isClosed = true;

        public DRBrowserForm(DRService drService){
            _drService = drService;
            InitializeComponent();
            _drs = new List<DR>();
        }

        private void DRBrowserForm_Load(object sender, EventArgs e){

        }

        private void cmdDrSearch_Click(object sender, EventArgs e){
            _drs = _drService.SearchDr(txtDr.Text);
            PopulateListview();
        }

        private void PopulateListview(){
            listView1.Items.Clear();
            foreach (var i in _drs){
                string[] arr = new string[3];
                arr[0] = Convert.ToString(i.Id);
                arr[1] = Convert.ToString(i.DRNumber);
                arr[2] = Convert.ToString(i.Date);

                ListViewItem lit = new ListViewItem(arr);

                listView1.Items.Add(lit);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e){
            if (listView1.SelectedItems.Count > 0){
                int drId = Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text);
                _selectedDr = _drs.FirstOrDefault(s => s.Id == drId);
            }
        }

        private void cmdSelect_Click(object sender, EventArgs e){
            if (listView1.SelectedItems.Count == 0){
                MessageBox.Show("Please select DR from the list",
                    "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else{
                _isClosed = false;
                this.Close();
            }
        }

        private void cmdClose_Click(object sender, EventArgs e){
            _isClosed = true;
            this.Close();
        }

        public DR SelectedDR{
            get { return _selectedDr; }
        }

        public bool IsClosed{
            get { return _isClosed; }
        }
    }
}

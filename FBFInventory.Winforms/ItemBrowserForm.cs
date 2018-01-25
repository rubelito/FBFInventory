using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using FBFInventory.Domain.Entity;

namespace FBFInventory.Winforms
{
    public partial class ItemBrowserForm : Form
    {
        private readonly List<Item> _items;
        private Item _selectedItem;

        private bool _isClosed = true;

        public bool ShouldShowQuantityColumn { get; set; }

        public ItemBrowserForm(List<Item> items){
            _items = items;
            InitializeComponent();
        }

        private void ItemBrowserForm_Load(object sender, EventArgs e){
            Search();
        }

        private void txtFilter_TextChanged(object sender, EventArgs e){
            Search();
        }

        private void Search(){
            List<Item> filteredItems = new List<Item>();

            if (string.IsNullOrWhiteSpace(txtFilter.Text)){
                filteredItems = _items;
            }
            else{
                filteredItems = _items.Where(i => i.Name.ToLower()
                    .Contains(txtFilter.Text.ToLower())).ToList();
            }

            PopulateListview(filteredItems);
        }

        private void PopulateListview(List<Item> items){
            listView1.Items.Clear();
            foreach (var i in items){
                string[] arr = new string[4];
                arr[0] = Convert.ToString(i.Id);
                arr[1] = Convert.ToString(i.Name);
                arr[2] = Convert.ToString(i.MeasuredBy);

                if (ShouldShowQuantityColumn)
                    arr[3] = Convert.ToString(i.GetAppropriateQuantity);

                ListViewItem lit = new ListViewItem(arr);

                listView1.Items.Add(lit);
            }
        }

        private void cmdSelect_Click(object sender, EventArgs e){
            if (listView1.SelectedItems.Count == 0){
                MessageBox.Show("Please select supplier from the list",
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

        public Item SelectedItem{
            get { return _selectedItem; }
        }

        public bool IsClosed{
            get { return _isClosed; }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e){
            if (listView1.SelectedItems.Count > 0){
                int itemId = Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text);
                _selectedItem = _items.FirstOrDefault(s => s.Id == itemId);
            }
        }
    }
}

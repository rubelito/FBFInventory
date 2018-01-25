using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FBFInventory.Domain.Entity;
using FBFInventory.Infrastructure.Service;
using FBFInventory.Winforms.Helper;

namespace FBFInventory.Winforms
{
    public partial class CategoryMaintenanceForm : Form
    {
        private readonly CategoryService _categoryService;

        public CategoryMaintenanceForm(CategoryService categoryService){
            _categoryService = categoryService;
            InitializeComponent();
        }

        private void CategoryMaintenanceForm_Load(object sender, EventArgs e)
        {
            LoadCategories();
        }

        private void LoadCategories(){
            List<Category> categories = _categoryService.GetAllCategories();

            listBox1.DataSource = categories;
            listBox1.ValueMember = "Id";
            listBox1.DisplayMember = "Name";
        }

        private void cmdAdd_Click(object sender, EventArgs e){
            CategoryDialog cDialog = new CategoryDialog(_categoryService, Operation.Add, null);
            cDialog.ShowDialog();

            LoadCategories();
        }

        private void cmdEdit_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems.Count == 0){
                MessageBox.Show("Please select category from the list!",
                    "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            Category selectedCategory = listBox1.SelectedItem as Category;

            CategoryDialog cDialog = new CategoryDialog(_categoryService, Operation.Edit, selectedCategory);
            cDialog.ShowDialog();

            LoadCategories();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

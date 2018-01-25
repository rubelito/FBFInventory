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
    public partial class CategoryDialog : Form
    {
        private readonly CategoryService _categoryService;
        private readonly Operation _mode;
        private readonly Category _cToEdit;

        public CategoryDialog(CategoryService categoryService, Operation mode, Category cToEdit){
            _categoryService = categoryService;
            _mode = mode;
            _cToEdit = cToEdit;
            InitializeComponent();
        }

        private void CategoryDialog_Load(object sender, EventArgs e)
        {
            if (_mode == Operation.Add){
                this.Text = "Add Category";
            }
            else if (_mode == Operation.Edit){
                this.Text = "Edit Category";
                txtCategory.Text = _cToEdit.Name;
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (_mode == Operation.Add){
                Category newCategory = new Category();
                newCategory.Name = txtCategory.Text;

                _categoryService.Add(newCategory);
                MessageBox.Show("Record Added!");
            }
            else if (_mode == Operation.Edit){
                Category cToEdit = new Category();
                cToEdit.Id = _cToEdit.Id;
                cToEdit.Name = txtCategory.Text;

                _categoryService.Edit(cToEdit);
                MessageBox.Show("Record Edited!");
            }

            this.Close();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }   
    }
}

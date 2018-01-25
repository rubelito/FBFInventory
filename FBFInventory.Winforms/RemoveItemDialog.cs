using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FBFInventory.Winforms
{
    public partial class RemoveItemDialog : Form
    {
        private bool _isOk;

        public RemoveItemDialog()
        {
            InitializeComponent();
        }

        public bool IsOk {get { return _isOk; }}
        public string Comment {get { return txtComment.Text; }}

        private void cmdOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtComment.Text)){
                MessageBox.Show("Please provide comments!", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            _isOk = true;
            this.Close();
        }

        private void cmdCancel_Click(object sender, EventArgs e){
            _isOk = false;
            this.Close();
        }
    }
}

using System;
using System.Windows.Forms;
using FBFInventory.Domain.Entity;

namespace FBFInventory.Winforms
{
    public partial class RemoveItemDialog : Form
    {
        private readonly ReceiptType _type;
        private bool _isOk;

        public RemoveItemDialog(ReceiptType type){
            _type = type;
            InitializeComponent();
        }

        public bool IsOk {get { return _isOk; }}

        private void cmdOk_Click(object sender, EventArgs e)
        {
            _isOk = true;
            this.Close();
        }

        private void cmdCancel_Click(object sender, EventArgs e){
            _isOk = false;
            this.Close();
        }

        private void RemoveItemDialog_Load(object sender, EventArgs e){
            string message = string.Empty;

            if (_type == ReceiptType.SDR)
                message = "Selected Items will be subtracted to stocks";
            else if (_type == ReceiptType.DR)
                message = "Selected Items will return to stocks";

            lblMessage.Text = message;
        }
    }
}

using System;
using System.Windows.Forms;
using FBFInventory.Domain.Entity;
using FBFInventory.Domain.History;
using FBFInventory.Infrastructure.Service;

namespace FBFInventory.Winforms
{
    public partial class InOutWithOutDRForm : Form
    {
        private readonly ItemService _itemService;
        private readonly InOutService _inOutService;
        private readonly InOrOut _inOrOut;

        private bool _haveChangedQuantity;

        private Item _item;

        public InOutWithOutDRForm(ItemService itemService, InOutService inOutService,
            InOrOut inOrOut, Item selectedItem){
            _itemService = itemService;
            _inOutService = inOutService;
            _inOrOut = inOrOut;
            _item = selectedItem;
            InitializeComponent();
        }

        private void InOutWithOutDRForm_Load(object sender, EventArgs e){
            SetTitle();
            LoadItemToControls();
        }

        private void LoadItemToControls(){
            _item = _itemService.Find(_item.Id);
            SetValueToControls();
        }

        private void SetTitle(){
            if (_inOrOut == InOrOut.In){
                this.Text = "In";
                cmdInOut.Text = "In";
            }
            else if (_inOrOut == InOrOut.Out){
                this.Text = "Out";
                cmdInOut.Text = "Out";
            }
        }

        private void SetValueToControls(){
            lblItem.Text = _item.Name;

            if (_item.MeasuredBy == MeasuredBy.Quantity){
                lblCurrentQty.Text = Convert.ToString(_item.Quantity);
                lblMeasuredBy.Text = "Qty.";
                lblMeasuredBy1.Text = "Qty.";
            }
            else if (_item.MeasuredBy == MeasuredBy.Meters){
                lblCurrentQty.Text = Convert.ToString(_item.Meters);
                lblMeasuredBy.Text = "m.";
                lblMeasuredBy1.Text = "m.";
            }
            else if (_item.MeasuredBy == MeasuredBy.Feet){
                lblCurrentQty.Text = Convert.ToString(_item.Feet);
                lblMeasuredBy.Text = "f.";
                lblMeasuredBy1.Text = "f.";
            }
        }

        private void cmdInOut_Click(object sender, EventArgs e){
            if (_item == null){
                MessageBox.Show("Please select Item!",
                    "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (txtQty.Text == "0" || string.IsNullOrWhiteSpace(txtQty.Text)){
                MessageBox.Show("Please provide Quantity!",
                    "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtNote.Text)){
                MessageBox.Show("Please provide Notes!",
                    "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }         

            InOutParam p = new InOutParam();
            p.Item = _item;
            p.Note = txtNote.Text;
            p.Qty = Convert.ToDouble(txtQty.Text);

            if (_inOrOut == InOrOut.In){
                p.InOrOut = InOrOut.In;
                _inOutService.In(p);
                MessageBox.Show(p.Qty + " stock(s) added to inventory of " + p.Item.Name);
            }
            else if (_inOrOut == InOrOut.Out){
                p.InOrOut = InOrOut.Out;
                _inOutService.Out(p);
                MessageBox.Show(p.Qty + " stock(s) subtracted to inventory of " + p.Item.Name);
            }

            _haveChangedQuantity = true;
            this.Close();
        }

        private void SetDefaultForUI(){
            lblItem.Text = "Click here to search item...";
            lblCurrentQty.Text = "0";
            txtQty.Text = "0";
            txtNote.Text = string.Empty;
        }

        private void cmdClose_Click(object sender, EventArgs e){
            this.Close();
        }

        public bool HaveChangedQuantity { get { return _haveChangedQuantity; } }
    }
}

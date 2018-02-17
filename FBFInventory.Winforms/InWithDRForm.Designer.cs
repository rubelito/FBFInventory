namespace FBFInventory.Winforms
{
    partial class InWithDRForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grdSupplierCustomer = new System.Windows.Forms.GroupBox();
            this.txtVehicleNumber = new System.Windows.Forms.TextBox();
            this.txtDeliveredBy = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.txtProject = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblSupplier = new System.Windows.Forms.Label();
            this.lblSupplierLabel = new System.Windows.Forms.Label();
            this.txtSDR = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdCreate = new System.Windows.Forms.Button();
            this.grdItems = new System.Windows.Forms.GroupBox();
            this.cmdRemove = new System.Windows.Forms.Button();
            this.cmdInOut = new System.Windows.Forms.Button();
            this.lblMeasuredBy1 = new System.Windows.Forms.Label();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.lblInOut = new System.Windows.Forms.Label();
            this.lblMeasuredBy = new System.Windows.Forms.Label();
            this.lblCurrentQty = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblItem = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmdClose = new System.Windows.Forms.Button();
            this.cmdSaveEdit = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdPrint = new System.Windows.Forms.Button();
            this.grdSupplierCustomer.SuspendLayout();
            this.grdItems.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdSupplierCustomer
            // 
            this.grdSupplierCustomer.Controls.Add(this.txtVehicleNumber);
            this.grdSupplierCustomer.Controls.Add(this.txtDeliveredBy);
            this.grdSupplierCustomer.Controls.Add(this.label11);
            this.grdSupplierCustomer.Controls.Add(this.label10);
            this.grdSupplierCustomer.Controls.Add(this.txtAddress);
            this.grdSupplierCustomer.Controls.Add(this.txtProject);
            this.grdSupplierCustomer.Controls.Add(this.label9);
            this.grdSupplierCustomer.Controls.Add(this.label8);
            this.grdSupplierCustomer.Controls.Add(this.label7);
            this.grdSupplierCustomer.Controls.Add(this.dateTimePicker1);
            this.grdSupplierCustomer.Controls.Add(this.txtNote);
            this.grdSupplierCustomer.Controls.Add(this.label2);
            this.grdSupplierCustomer.Controls.Add(this.lblSupplier);
            this.grdSupplierCustomer.Controls.Add(this.lblSupplierLabel);
            this.grdSupplierCustomer.Controls.Add(this.txtSDR);
            this.grdSupplierCustomer.Controls.Add(this.label1);
            this.grdSupplierCustomer.Location = new System.Drawing.Point(12, 12);
            this.grdSupplierCustomer.Name = "grdSupplierCustomer";
            this.grdSupplierCustomer.Size = new System.Drawing.Size(512, 173);
            this.grdSupplierCustomer.TabIndex = 0;
            this.grdSupplierCustomer.TabStop = false;
            this.grdSupplierCustomer.Text = "Supplier DR";
            // 
            // txtVehicleNumber
            // 
            this.txtVehicleNumber.Location = new System.Drawing.Point(374, 137);
            this.txtVehicleNumber.Name = "txtVehicleNumber";
            this.txtVehicleNumber.Size = new System.Drawing.Size(132, 20);
            this.txtVehicleNumber.TabIndex = 6;
            // 
            // txtDeliveredBy
            // 
            this.txtDeliveredBy.Location = new System.Drawing.Point(374, 104);
            this.txtDeliveredBy.Name = "txtDeliveredBy";
            this.txtDeliveredBy.Size = new System.Drawing.Size(132, 20);
            this.txtDeliveredBy.TabIndex = 5;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(279, 143);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(88, 13);
            this.label11.TabIndex = 18;
            this.label11.Text = "Vehicle Number :";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(294, 109);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(73, 13);
            this.label10.TabIndex = 17;
            this.label10.Text = "Delivered By :";
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(72, 106);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(195, 20);
            this.txtAddress.TabIndex = 3;
            // 
            // txtProject
            // 
            this.txtProject.Location = new System.Drawing.Point(72, 80);
            this.txtProject.Name = "txtProject";
            this.txtProject.Size = new System.Drawing.Size(155, 20);
            this.txtProject.TabIndex = 2;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 111);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(51, 13);
            this.label9.TabIndex = 14;
            this.label9.Text = "Address :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(20, 84);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(46, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "Project :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(30, 143);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(36, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Date :";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(72, 137);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 4;
            // 
            // txtNote
            // 
            this.txtNote.Location = new System.Drawing.Point(273, 37);
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(233, 63);
            this.txtNote.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(270, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Note :";
            // 
            // lblSupplier
            // 
            this.lblSupplier.AutoSize = true;
            this.lblSupplier.Location = new System.Drawing.Point(72, 28);
            this.lblSupplier.Name = "lblSupplier";
            this.lblSupplier.Size = new System.Drawing.Size(155, 13);
            this.lblSupplier.TabIndex = 8;
            this.lblSupplier.Text = "Click here to provide supplier....";
            this.lblSupplier.Click += new System.EventHandler(this.lblSupplier_Click);
            // 
            // lblSupplierLabel
            // 
            this.lblSupplierLabel.AutoSize = true;
            this.lblSupplierLabel.Location = new System.Drawing.Point(15, 28);
            this.lblSupplierLabel.Name = "lblSupplierLabel";
            this.lblSupplierLabel.Size = new System.Drawing.Size(51, 13);
            this.lblSupplierLabel.TabIndex = 2;
            this.lblSupplierLabel.Text = "Supplier :";
            // 
            // txtSDR
            // 
            this.txtSDR.Location = new System.Drawing.Point(72, 53);
            this.txtSDR.Name = "txtSDR";
            this.txtSDR.Size = new System.Drawing.Size(155, 20);
            this.txtSDR.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "New DR :";
            // 
            // cmdCreate
            // 
            this.cmdCreate.Location = new System.Drawing.Point(95, 191);
            this.cmdCreate.Name = "cmdCreate";
            this.cmdCreate.Size = new System.Drawing.Size(75, 23);
            this.cmdCreate.TabIndex = 4;
            this.cmdCreate.Text = "Create";
            this.cmdCreate.UseVisualStyleBackColor = true;
            this.cmdCreate.Click += new System.EventHandler(this.cmdCreate_Click);
            // 
            // grdItems
            // 
            this.grdItems.Controls.Add(this.cmdRemove);
            this.grdItems.Controls.Add(this.cmdInOut);
            this.grdItems.Controls.Add(this.lblMeasuredBy1);
            this.grdItems.Controls.Add(this.txtQty);
            this.grdItems.Controls.Add(this.lblInOut);
            this.grdItems.Controls.Add(this.lblMeasuredBy);
            this.grdItems.Controls.Add(this.lblCurrentQty);
            this.grdItems.Controls.Add(this.label5);
            this.grdItems.Controls.Add(this.label4);
            this.grdItems.Controls.Add(this.lblItem);
            this.grdItems.Controls.Add(this.label3);
            this.grdItems.Controls.Add(this.listView1);
            this.grdItems.Location = new System.Drawing.Point(24, 220);
            this.grdItems.Name = "grdItems";
            this.grdItems.Size = new System.Drawing.Size(444, 338);
            this.grdItems.TabIndex = 1;
            this.grdItems.TabStop = false;
            this.grdItems.Text = "Items";
            // 
            // cmdRemove
            // 
            this.cmdRemove.Location = new System.Drawing.Point(371, 248);
            this.cmdRemove.Name = "cmdRemove";
            this.cmdRemove.Size = new System.Drawing.Size(55, 19);
            this.cmdRemove.TabIndex = 11;
            this.cmdRemove.Text = "Remove";
            this.cmdRemove.UseVisualStyleBackColor = true;
            this.cmdRemove.Click += new System.EventHandler(this.cmdRemove_Click);
            // 
            // cmdInOut
            // 
            this.cmdInOut.Location = new System.Drawing.Point(196, 310);
            this.cmdInOut.Name = "cmdInOut";
            this.cmdInOut.Size = new System.Drawing.Size(75, 23);
            this.cmdInOut.TabIndex = 10;
            this.cmdInOut.Text = "In";
            this.cmdInOut.UseVisualStyleBackColor = true;
            this.cmdInOut.Click += new System.EventHandler(this.cmdInOut_Click);
            // 
            // lblMeasuredBy1
            // 
            this.lblMeasuredBy1.AutoSize = true;
            this.lblMeasuredBy1.Location = new System.Drawing.Point(120, 315);
            this.lblMeasuredBy1.Name = "lblMeasuredBy1";
            this.lblMeasuredBy1.Size = new System.Drawing.Size(26, 13);
            this.lblMeasuredBy1.TabIndex = 9;
            this.lblMeasuredBy1.Text = "Qty.";
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(61, 313);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(52, 20);
            this.txtQty.TabIndex = 8;
            this.txtQty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQty_KeyPress);
            // 
            // lblInOut
            // 
            this.lblInOut.AutoSize = true;
            this.lblInOut.Location = new System.Drawing.Point(23, 316);
            this.lblInOut.Name = "lblInOut";
            this.lblInOut.Size = new System.Drawing.Size(32, 13);
            this.lblInOut.TabIndex = 7;
            this.lblInOut.Text = "Add :";
            // 
            // lblMeasuredBy
            // 
            this.lblMeasuredBy.AutoSize = true;
            this.lblMeasuredBy.Location = new System.Drawing.Point(103, 292);
            this.lblMeasuredBy.Name = "lblMeasuredBy";
            this.lblMeasuredBy.Size = new System.Drawing.Size(26, 13);
            this.lblMeasuredBy.TabIndex = 6;
            this.lblMeasuredBy.Text = "Qty.";
            // 
            // lblCurrentQty
            // 
            this.lblCurrentQty.AutoSize = true;
            this.lblCurrentQty.Location = new System.Drawing.Point(62, 292);
            this.lblCurrentQty.Name = "lblCurrentQty";
            this.lblCurrentQty.Size = new System.Drawing.Size(13, 13);
            this.lblCurrentQty.TabIndex = 5;
            this.lblCurrentQty.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 291);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Stocks :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 292);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 13);
            this.label4.TabIndex = 3;
            // 
            // lblItem
            // 
            this.lblItem.AutoSize = true;
            this.lblItem.Location = new System.Drawing.Point(61, 266);
            this.lblItem.Name = "lblItem";
            this.lblItem.Size = new System.Drawing.Size(133, 13);
            this.lblItem.TabIndex = 2;
            this.lblItem.Text = "Click here to search Item...";
            this.lblItem.Click += new System.EventHandler(this.lblItem_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 266);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Item :";
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(6, 19);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(420, 223);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Id";
            this.columnHeader1.Width = 39;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Item Desc";
            this.columnHeader2.Width = 239;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Measured By";
            this.columnHeader3.Width = 76;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "";
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(381, 564);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(75, 23);
            this.cmdClose.TabIndex = 3;
            this.cmdClose.Text = "Close";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdSaveEdit
            // 
            this.cmdSaveEdit.Location = new System.Drawing.Point(95, 191);
            this.cmdSaveEdit.Name = "cmdSaveEdit";
            this.cmdSaveEdit.Size = new System.Drawing.Size(75, 23);
            this.cmdSaveEdit.TabIndex = 9;
            this.cmdSaveEdit.Text = "Save";
            this.cmdSaveEdit.UseVisualStyleBackColor = true;
            this.cmdSaveEdit.Click += new System.EventHandler(this.cmdSaveEdit_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Location = new System.Drawing.Point(185, 191);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 10;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdPrint
            // 
            this.cmdPrint.Location = new System.Drawing.Point(393, 191);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Size = new System.Drawing.Size(75, 23);
            this.cmdPrint.TabIndex = 11;
            this.cmdPrint.Text = "Print";
            this.cmdPrint.UseVisualStyleBackColor = true;
            this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click);
            // 
            // InWithDRForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(541, 595);
            this.Controls.Add(this.cmdPrint);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdSaveEdit);
            this.Controls.Add(this.cmdCreate);
            this.Controls.Add(this.grdItems);
            this.Controls.Add(this.grdSupplierCustomer);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InWithDRForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "In (SDR)";
            this.Load += new System.EventHandler(this.InWithDR_Load);
            this.grdSupplierCustomer.ResumeLayout(false);
            this.grdSupplierCustomer.PerformLayout();
            this.grdItems.ResumeLayout(false);
            this.grdItems.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grdSupplierCustomer;
        private System.Windows.Forms.Button cmdCreate;
        private System.Windows.Forms.Label lblSupplierLabel;
        private System.Windows.Forms.TextBox txtSDR;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grdItems;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblItem;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button cmdRemove;
        private System.Windows.Forms.Button cmdInOut;
        private System.Windows.Forms.Label lblMeasuredBy1;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.Label lblInOut;
        private System.Windows.Forms.Label lblMeasuredBy;
        private System.Windows.Forms.Label lblCurrentQty;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Label lblSupplier;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdSaveEdit;
        private System.Windows.Forms.TextBox txtNote;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.TextBox txtProject;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtVehicleNumber;
        private System.Windows.Forms.TextBox txtDeliveredBy;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button cmdPrint;
    }
}
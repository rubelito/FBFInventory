namespace FBFInventory.Winforms
{
    partial class NewItemForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewItemForm));
            this.label1 = new System.Windows.Forms.Label();
            this.txtItemDesc = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmdAdd = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cboCategory = new System.Windows.Forms.ComboBox();
            this.lblSupplier = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cboMeasuredBy = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtThreshold = new System.Windows.Forms.TextBox();
            this.lblMeasuredBy = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCost = new System.Windows.Forms.TextBox();
            this.cmdEdit = new System.Windows.Forms.Button();
            this.AddEditControls = new System.Windows.Forms.GroupBox();
            this.chkPhaseOut = new System.Windows.Forms.CheckBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmdFirst = new System.Windows.Forms.Button();
            this.cmdPrevious = new System.Windows.Forms.Button();
            this.lblNavigation = new System.Windows.Forms.Label();
            this.cmdLast = new System.Windows.Forms.Button();
            this.cmdNext = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.cmdSearch = new System.Windows.Forms.Button();
            this.SearchGroupBox = new System.Windows.Forms.GroupBox();
            this.cmdSave = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.AddEditControls.SuspendLayout();
            this.SearchGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Item Desc:";
            // 
            // txtItemDesc
            // 
            this.txtItemDesc.Location = new System.Drawing.Point(98, 97);
            this.txtItemDesc.Name = "txtItemDesc";
            this.txtItemDesc.Size = new System.Drawing.Size(182, 20);
            this.txtItemDesc.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(42, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Suppier :";
            // 
            // cmdAdd
            // 
            this.cmdAdd.Location = new System.Drawing.Point(279, 177);
            this.cmdAdd.Name = "cmdAdd";
            this.cmdAdd.Size = new System.Drawing.Size(75, 23);
            this.cmdAdd.TabIndex = 4;
            this.cmdAdd.Text = "Add";
            this.cmdAdd.UseVisualStyleBackColor = true;
            this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Category :";
            // 
            // cboCategory
            // 
            this.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCategory.FormattingEnabled = true;
            this.cboCategory.Location = new System.Drawing.Point(98, 64);
            this.cboCategory.Name = "cboCategory";
            this.cboCategory.Size = new System.Drawing.Size(121, 21);
            this.cboCategory.TabIndex = 6;
            this.cboCategory.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // lblSupplier
            // 
            this.lblSupplier.AutoSize = true;
            this.lblSupplier.Location = new System.Drawing.Point(98, 25);
            this.lblSupplier.Name = "lblSupplier";
            this.lblSupplier.Size = new System.Drawing.Size(155, 13);
            this.lblSupplier.TabIndex = 7;
            this.lblSupplier.Text = "Click here to provide supplier....";
            this.lblSupplier.Click += new System.EventHandler(this.lblSupplier_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 134);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Measured By:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(330, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 13);
            this.label5.TabIndex = 9;
            // 
            // cboMeasuredBy
            // 
            this.cboMeasuredBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMeasuredBy.FormattingEnabled = true;
            this.cboMeasuredBy.Location = new System.Drawing.Point(98, 126);
            this.cboMeasuredBy.Name = "cboMeasuredBy";
            this.cboMeasuredBy.Size = new System.Drawing.Size(121, 21);
            this.cboMeasuredBy.TabIndex = 10;
            this.cboMeasuredBy.SelectedIndexChanged += new System.EventHandler(this.cboMeasuredBy_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(294, 67);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Threshold :";
            // 
            // txtThreshold
            // 
            this.txtThreshold.Location = new System.Drawing.Point(360, 64);
            this.txtThreshold.Name = "txtThreshold";
            this.txtThreshold.Size = new System.Drawing.Size(63, 20);
            this.txtThreshold.TabIndex = 12;
            // 
            // lblMeasuredBy
            // 
            this.lblMeasuredBy.AutoSize = true;
            this.lblMeasuredBy.Location = new System.Drawing.Point(429, 67);
            this.lblMeasuredBy.Name = "lblMeasuredBy";
            this.lblMeasuredBy.Size = new System.Drawing.Size(0, 13);
            this.lblMeasuredBy.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(320, 100);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Cost :";
            // 
            // txtCost
            // 
            this.txtCost.Location = new System.Drawing.Point(360, 97);
            this.txtCost.Name = "txtCost";
            this.txtCost.Size = new System.Drawing.Size(63, 20);
            this.txtCost.TabIndex = 15;
            // 
            // cmdEdit
            // 
            this.cmdEdit.Location = new System.Drawing.Point(360, 177);
            this.cmdEdit.Name = "cmdEdit";
            this.cmdEdit.Size = new System.Drawing.Size(75, 23);
            this.cmdEdit.TabIndex = 16;
            this.cmdEdit.Text = "Edit";
            this.cmdEdit.UseVisualStyleBackColor = true;
            this.cmdEdit.Click += new System.EventHandler(this.cmdEdit_Click);
            // 
            // AddEditControls
            // 
            this.AddEditControls.Controls.Add(this.chkPhaseOut);
            this.AddEditControls.Controls.Add(this.txtItemDesc);
            this.AddEditControls.Controls.Add(this.label1);
            this.AddEditControls.Controls.Add(this.txtCost);
            this.AddEditControls.Controls.Add(this.label2);
            this.AddEditControls.Controls.Add(this.label7);
            this.AddEditControls.Controls.Add(this.lblMeasuredBy);
            this.AddEditControls.Controls.Add(this.label3);
            this.AddEditControls.Controls.Add(this.txtThreshold);
            this.AddEditControls.Controls.Add(this.cboCategory);
            this.AddEditControls.Controls.Add(this.label6);
            this.AddEditControls.Controls.Add(this.lblSupplier);
            this.AddEditControls.Controls.Add(this.cboMeasuredBy);
            this.AddEditControls.Controls.Add(this.label4);
            this.AddEditControls.Controls.Add(this.label5);
            this.AddEditControls.Location = new System.Drawing.Point(12, 12);
            this.AddEditControls.Name = "AddEditControls";
            this.AddEditControls.Size = new System.Drawing.Size(464, 159);
            this.AddEditControls.TabIndex = 17;
            this.AddEditControls.TabStop = false;
            // 
            // chkPhaseOut
            // 
            this.chkPhaseOut.AutoSize = true;
            this.chkPhaseOut.Location = new System.Drawing.Point(333, 133);
            this.chkPhaseOut.Name = "chkPhaseOut";
            this.chkPhaseOut.Size = new System.Drawing.Size(74, 17);
            this.chkPhaseOut.TabIndex = 17;
            this.chkPhaseOut.Text = "Phase out";
            this.chkPhaseOut.UseVisualStyleBackColor = true;
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
            this.listView1.Location = new System.Drawing.Point(12, 206);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(463, 298);
            this.listView1.TabIndex = 18;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Id";
            this.columnHeader1.Width = 30;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Item Desc";
            this.columnHeader2.Width = 238;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Measured By";
            this.columnHeader3.Width = 83;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Stocks";
            this.columnHeader4.Width = 67;
            // 
            // cmdFirst
            // 
            this.cmdFirst.Location = new System.Drawing.Point(213, 15);
            this.cmdFirst.Name = "cmdFirst";
            this.cmdFirst.Size = new System.Drawing.Size(26, 23);
            this.cmdFirst.TabIndex = 19;
            this.cmdFirst.Text = "|<";
            this.cmdFirst.UseVisualStyleBackColor = true;
            this.cmdFirst.Click += new System.EventHandler(this.cmdFirst_Click);
            // 
            // cmdPrevious
            // 
            this.cmdPrevious.Location = new System.Drawing.Point(245, 15);
            this.cmdPrevious.Name = "cmdPrevious";
            this.cmdPrevious.Size = new System.Drawing.Size(24, 23);
            this.cmdPrevious.TabIndex = 20;
            this.cmdPrevious.Text = "<";
            this.cmdPrevious.UseVisualStyleBackColor = true;
            this.cmdPrevious.Click += new System.EventHandler(this.cmdPrevious_Click);
            // 
            // lblNavigation
            // 
            this.lblNavigation.AutoSize = true;
            this.lblNavigation.Location = new System.Drawing.Point(275, 20);
            this.lblNavigation.Name = "lblNavigation";
            this.lblNavigation.Size = new System.Drawing.Size(30, 13);
            this.lblNavigation.TabIndex = 21;
            this.lblNavigation.Text = "1 / 4";
            // 
            // cmdLast
            // 
            this.cmdLast.Location = new System.Drawing.Point(355, 15);
            this.cmdLast.Name = "cmdLast";
            this.cmdLast.Size = new System.Drawing.Size(24, 23);
            this.cmdLast.TabIndex = 23;
            this.cmdLast.Text = ">|";
            this.cmdLast.UseVisualStyleBackColor = true;
            this.cmdLast.Click += new System.EventHandler(this.cmdLast_Click);
            // 
            // cmdNext
            // 
            this.cmdNext.Location = new System.Drawing.Point(323, 15);
            this.cmdNext.Name = "cmdNext";
            this.cmdNext.Size = new System.Drawing.Size(26, 23);
            this.cmdNext.TabIndex = 22;
            this.cmdNext.Text = ">";
            this.cmdNext.UseVisualStyleBackColor = true;
            this.cmdNext.Click += new System.EventHandler(this.cmdNext_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 13);
            this.label8.TabIndex = 24;
            this.label8.Text = "Search :";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(50, 17);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(100, 20);
            this.txtSearch.TabIndex = 25;
            // 
            // cmdSearch
            // 
            this.cmdSearch.Image = ((System.Drawing.Image)(resources.GetObject("cmdSearch.Image")));
            this.cmdSearch.Location = new System.Drawing.Point(154, 15);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(29, 23);
            this.cmdSearch.TabIndex = 26;
            this.cmdSearch.UseVisualStyleBackColor = true;
            this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // SearchGroupBox
            // 
            this.SearchGroupBox.Controls.Add(this.txtSearch);
            this.SearchGroupBox.Controls.Add(this.cmdSearch);
            this.SearchGroupBox.Controls.Add(this.cmdFirst);
            this.SearchGroupBox.Controls.Add(this.cmdPrevious);
            this.SearchGroupBox.Controls.Add(this.lblNavigation);
            this.SearchGroupBox.Controls.Add(this.label8);
            this.SearchGroupBox.Controls.Add(this.cmdNext);
            this.SearchGroupBox.Controls.Add(this.cmdLast);
            this.SearchGroupBox.Location = new System.Drawing.Point(18, 510);
            this.SearchGroupBox.Name = "SearchGroupBox";
            this.SearchGroupBox.Size = new System.Drawing.Size(393, 43);
            this.SearchGroupBox.TabIndex = 27;
            this.SearchGroupBox.TabStop = false;
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(13, 178);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(75, 23);
            this.cmdSave.TabIndex = 28;
            this.cmdSave.Text = "Save";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Location = new System.Drawing.Point(93, 178);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 29;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // NewItemForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(503, 559);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.SearchGroupBox);
            this.Controls.Add(this.cmdEdit);
            this.Controls.Add(this.cmdAdd);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.AddEditControls);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewItemForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New Item";
            this.Load += new System.EventHandler(this.NewItemForm_Load);
            this.AddEditControls.ResumeLayout(false);
            this.AddEditControls.PerformLayout();
            this.SearchGroupBox.ResumeLayout(false);
            this.SearchGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtItemDesc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button cmdAdd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboCategory;
        private System.Windows.Forms.Label lblSupplier;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboMeasuredBy;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtThreshold;
        private System.Windows.Forms.Label lblMeasuredBy;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtCost;
        private System.Windows.Forms.Button cmdEdit;
        private System.Windows.Forms.GroupBox AddEditControls;
        private System.Windows.Forms.CheckBox chkPhaseOut;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button cmdFirst;
        private System.Windows.Forms.Button cmdPrevious;
        private System.Windows.Forms.Label lblNavigation;
        private System.Windows.Forms.Button cmdLast;
        private System.Windows.Forms.Button cmdNext;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button cmdSearch;
        private System.Windows.Forms.GroupBox SearchGroupBox;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.Button cmdCancel;
    }
}
namespace FBFInventory.Winforms
{
    partial class HistoryViewForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HistoryViewForm));
            this.label1 = new System.Windows.Forms.Label();
            this.lblItemDesc = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmdFirst = new System.Windows.Forms.Button();
            this.cmdPrevious = new System.Windows.Forms.Button();
            this.lblNavigation = new System.Windows.Forms.Label();
            this.cmdNext = new System.Windows.Forms.Button();
            this.cmdLast = new System.Windows.Forms.Button();
            this.cmdClose = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.cmdDrSearch = new System.Windows.Forms.Button();
            this.cmdPrint = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lblMeasuredBy = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Item Desc :";
            // 
            // lblItemDesc
            // 
            this.lblItemDesc.AutoSize = true;
            this.lblItemDesc.Location = new System.Drawing.Point(98, 13);
            this.lblItemDesc.Name = "lblItemDesc";
            this.lblItemDesc.Size = new System.Drawing.Size(0, 13);
            this.lblItemDesc.TabIndex = 1;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(19, 63);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(1020, 371);
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Id";
            this.columnHeader1.Width = 37;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Receipt Type";
            this.columnHeader2.Width = 80;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "In/Out";
            this.columnHeader3.Width = 46;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "(S)DR";
            this.columnHeader4.Width = 116;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Beginning";
            this.columnHeader5.Width = 86;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Qty";
            this.columnHeader6.Width = 82;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Ending";
            this.columnHeader7.Width = 101;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Date";
            this.columnHeader8.Width = 119;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Note";
            this.columnHeader9.Width = 267;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "In/Out By";
            this.columnHeader10.Width = 80;
            // 
            // cmdFirst
            // 
            this.cmdFirst.Location = new System.Drawing.Point(12, 440);
            this.cmdFirst.Name = "cmdFirst";
            this.cmdFirst.Size = new System.Drawing.Size(26, 23);
            this.cmdFirst.TabIndex = 29;
            this.cmdFirst.Text = "|<";
            this.cmdFirst.UseVisualStyleBackColor = true;
            this.cmdFirst.Click += new System.EventHandler(this.cmdFirst_Click);
            // 
            // cmdPrevious
            // 
            this.cmdPrevious.Location = new System.Drawing.Point(44, 440);
            this.cmdPrevious.Name = "cmdPrevious";
            this.cmdPrevious.Size = new System.Drawing.Size(24, 23);
            this.cmdPrevious.TabIndex = 30;
            this.cmdPrevious.Text = "<";
            this.cmdPrevious.UseVisualStyleBackColor = true;
            this.cmdPrevious.Click += new System.EventHandler(this.cmdPrevious_Click);
            // 
            // lblNavigation
            // 
            this.lblNavigation.AutoSize = true;
            this.lblNavigation.Location = new System.Drawing.Point(74, 445);
            this.lblNavigation.Name = "lblNavigation";
            this.lblNavigation.Size = new System.Drawing.Size(30, 13);
            this.lblNavigation.TabIndex = 31;
            this.lblNavigation.Text = "1 / 4";
            // 
            // cmdNext
            // 
            this.cmdNext.Location = new System.Drawing.Point(122, 440);
            this.cmdNext.Name = "cmdNext";
            this.cmdNext.Size = new System.Drawing.Size(26, 23);
            this.cmdNext.TabIndex = 32;
            this.cmdNext.Text = ">";
            this.cmdNext.UseVisualStyleBackColor = true;
            this.cmdNext.Click += new System.EventHandler(this.cmdNext_Click);
            // 
            // cmdLast
            // 
            this.cmdLast.Location = new System.Drawing.Point(154, 440);
            this.cmdLast.Name = "cmdLast";
            this.cmdLast.Size = new System.Drawing.Size(24, 23);
            this.cmdLast.TabIndex = 33;
            this.cmdLast.Text = ">|";
            this.cmdLast.UseVisualStyleBackColor = true;
            this.cmdLast.Click += new System.EventHandler(this.cmdLast_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(277, 440);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(75, 23);
            this.cmdClose.TabIndex = 34;
            this.cmdClose.Text = "Close";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(398, 13);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 35;
            // 
            // cmdDrSearch
            // 
            this.cmdDrSearch.Image = ((System.Drawing.Image)(resources.GetObject("cmdDrSearch.Image")));
            this.cmdDrSearch.Location = new System.Drawing.Point(604, 10);
            this.cmdDrSearch.Name = "cmdDrSearch";
            this.cmdDrSearch.Size = new System.Drawing.Size(29, 23);
            this.cmdDrSearch.TabIndex = 36;
            this.cmdDrSearch.UseVisualStyleBackColor = true;
            this.cmdDrSearch.Click += new System.EventHandler(this.cmdDrSearch_Click);
            // 
            // cmdPrint
            // 
            this.cmdPrint.Location = new System.Drawing.Point(387, 440);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Size = new System.Drawing.Size(75, 23);
            this.cmdPrint.TabIndex = 37;
            this.cmdPrint.Text = "Print";
            this.cmdPrint.UseVisualStyleBackColor = true;
            this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 38;
            this.label2.Text = "Measured By :";
            // 
            // lblMeasuredBy
            // 
            this.lblMeasuredBy.AutoSize = true;
            this.lblMeasuredBy.Location = new System.Drawing.Point(98, 35);
            this.lblMeasuredBy.Name = "lblMeasuredBy";
            this.lblMeasuredBy.Size = new System.Drawing.Size(0, 13);
            this.lblMeasuredBy.TabIndex = 39;
            // 
            // HistoryViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1044, 475);
            this.Controls.Add(this.lblMeasuredBy);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmdPrint);
            this.Controls.Add(this.cmdDrSearch);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdFirst);
            this.Controls.Add(this.cmdPrevious);
            this.Controls.Add(this.lblNavigation);
            this.Controls.Add(this.cmdNext);
            this.Controls.Add(this.cmdLast);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.lblItemDesc);
            this.Controls.Add(this.label1);
            this.Name = "HistoryViewForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "History View";
            this.Load += new System.EventHandler(this.HistoryView_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblItemDesc;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.Button cmdFirst;
        private System.Windows.Forms.Button cmdPrevious;
        private System.Windows.Forms.Label lblNavigation;
        private System.Windows.Forms.Button cmdNext;
        private System.Windows.Forms.Button cmdLast;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button cmdDrSearch;
        private System.Windows.Forms.Button cmdPrint;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblMeasuredBy;
        private System.Windows.Forms.ColumnHeader columnHeader10;
    }
}
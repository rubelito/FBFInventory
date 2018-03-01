namespace FBFInventory.Winforms
{
    partial class InOutWithOutDRForm
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
            this.cmdInOut = new System.Windows.Forms.Button();
            this.lblMeasuredBy1 = new System.Windows.Forms.Label();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lblMeasuredBy = new System.Windows.Forms.Label();
            this.lblCurrentQty = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblItem = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.cmdClose = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdInOut
            // 
            this.cmdInOut.Location = new System.Drawing.Point(199, 79);
            this.cmdInOut.Name = "cmdInOut";
            this.cmdInOut.Size = new System.Drawing.Size(75, 23);
            this.cmdInOut.TabIndex = 3;
            this.cmdInOut.Text = "In";
            this.cmdInOut.UseVisualStyleBackColor = true;
            this.cmdInOut.Click += new System.EventHandler(this.cmdInOut_Click);
            // 
            // lblMeasuredBy1
            // 
            this.lblMeasuredBy1.AutoSize = true;
            this.lblMeasuredBy1.Location = new System.Drawing.Point(123, 84);
            this.lblMeasuredBy1.Name = "lblMeasuredBy1";
            this.lblMeasuredBy1.Size = new System.Drawing.Size(26, 13);
            this.lblMeasuredBy1.TabIndex = 19;
            this.lblMeasuredBy1.Text = "Qty.";
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(64, 82);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(52, 20);
            this.txtQty.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 85);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Quantity :";
            // 
            // lblMeasuredBy
            // 
            this.lblMeasuredBy.AutoSize = true;
            this.lblMeasuredBy.Location = new System.Drawing.Point(106, 61);
            this.lblMeasuredBy.Name = "lblMeasuredBy";
            this.lblMeasuredBy.Size = new System.Drawing.Size(26, 13);
            this.lblMeasuredBy.TabIndex = 16;
            this.lblMeasuredBy.Text = "Qty.";
            // 
            // lblCurrentQty
            // 
            this.lblCurrentQty.AutoSize = true;
            this.lblCurrentQty.Location = new System.Drawing.Point(65, 61);
            this.lblCurrentQty.Name = "lblCurrentQty";
            this.lblCurrentQty.Size = new System.Drawing.Size(13, 13);
            this.lblCurrentQty.TabIndex = 15;
            this.lblCurrentQty.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Stocks :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 13);
            this.label4.TabIndex = 13;
            // 
            // lblItem
            // 
            this.lblItem.AutoSize = true;
            this.lblItem.Location = new System.Drawing.Point(64, 35);
            this.lblItem.Name = "lblItem";
            this.lblItem.Size = new System.Drawing.Size(0, 13);
            this.lblItem.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Item :";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmdInOut);
            this.groupBox1.Controls.Add(this.txtNote);
            this.groupBox1.Controls.Add(this.lblMeasuredBy1);
            this.groupBox1.Controls.Add(this.lblItem);
            this.groupBox1.Controls.Add(this.txtQty);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lblMeasuredBy);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.lblCurrentQty);
            this.groupBox1.Location = new System.Drawing.Point(13, 26);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(498, 155);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(297, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Note :";
            // 
            // txtNote
            // 
            this.txtNote.Location = new System.Drawing.Point(300, 35);
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(192, 106);
            this.txtNote.TabIndex = 2;
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(430, 204);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(75, 23);
            this.cmdClose.TabIndex = 4;
            this.cmdClose.Text = "Close";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // InOutWithOutDRForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 239);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InOutWithOutDRForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "InOutWithOutDRForm";
            this.Load += new System.EventHandler(this.InOutWithOutDRForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdInOut;
        private System.Windows.Forms.Label lblMeasuredBy1;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblMeasuredBy;
        private System.Windows.Forms.Label lblCurrentQty;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblItem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNote;
        private System.Windows.Forms.Button cmdClose;
    }
}
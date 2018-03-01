using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FBFInventory.Infrastructure.EntityFramework;

namespace FBFInventory.DB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DbCreator d = new DbCreator();
            d.Create(txtIpAddress.Text);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

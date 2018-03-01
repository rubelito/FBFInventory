using System;
using System.Windows.Forms;
using FBFInventory.Domain.Entity;
using FBFInventory.Infrastructure.Service;

namespace FBFInventory.Winforms
{
    public partial class ChangePasswordForm : Form
    {
        private readonly UserService _userService;
        private readonly string _userName;

        private bool _hasChangePassword;

        public ChangePasswordForm(UserService userService, string userName)
        {
            _userService = userService;
            _userName = userName;
            InitializeComponent();
        }

        private void UserForm_Load(object sender, EventArgs e){
            lblName.Text = _userName;
        }

        private void button1_Click(object sender, EventArgs e){
            User u = _userService.GetUserbyName(_userName);

            if (string.IsNullOrEmpty(txtOldPassword.Text))
            {
                MessageBox.Show("Please provide old password",
                    "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (string.IsNullOrEmpty(txtNewPassword.Text))
            {
                MessageBox.Show("Please provide new password",
                    "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (u.Password != txtOldPassword.Text){
                MessageBox.Show("Old password is not match!",
                    "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            _userService.ChangePassword(u.Id, txtNewPassword.Text);
            MessageBox.Show("Success changing password. This application will be close.",
                    "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            _hasChangePassword = true;
            this.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            _hasChangePassword = false;
            this.Close();           
        }

        public bool HasChangePassword {get { return _hasChangePassword; }}
    }
}

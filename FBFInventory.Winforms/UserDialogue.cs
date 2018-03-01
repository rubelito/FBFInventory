using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using FBFInventory.Domain.Entity;
using FBFInventory.Infrastructure.Service;
using FBFInventory.Winforms.Helper;

namespace FBFInventory.Winforms
{
    public partial class UserDialogue : Form
    {
        private readonly Operation _mode;
        private readonly User _selectedUser;
        private readonly UserService _userService;
        private List<Role> _roles; 

        public UserDialogue(Operation mode, User selectedUser, UserService userService){
            _mode = mode;
            _selectedUser = selectedUser;
            _userService = userService;
            InitializeComponent();
        }

        private void UserDialogue_Load(object sender, EventArgs e){
            LoadRoles();
            SetValueToControl();           
        }

        private void LoadRoles(){
            _roles = _userService.GetAllRoles();
            cboRole.DataSource = _roles;
            cboRole.DisplayMember = "Name";
            cboRole.ValueMember = "Id";
            cboRole.SelectedValue = 2;
        }

        private void SetValueToControl(){
            if (_mode == Operation.Add){
                Text = "Add User";
                chkActive.Checked = true;
                chkActive.Enabled = false;
            }
            else{
                Text = "Edit User";
                txtUserName.Text = _selectedUser.UserName;
                txtUserName.Enabled = false;
                txtPassword.Enabled = false;
                chkActive.Checked = _selectedUser.IsActive;
                cboRole.SelectedValue = _selectedUser.Role.Id;
            }
        }

        private void cmdSave_Click(object sender, EventArgs e){
            if (_mode == Operation.Add)
                AddUser();
            else
                EditUser();
        }

        private void AddUser(){
            if (string.IsNullOrWhiteSpace(txtUserName.Text)){
                MessageBox.Show("Please provide username",
                        "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Please provide password",
                        "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            User newUser = new User();
            newUser.UserName = txtUserName.Text;
            newUser.Password = txtPassword.Text;
            newUser.IsActive = true;

            long selectedRole = ((Role)cboRole.SelectedItem).Id;
            _userService.AddUser(newUser, selectedRole);

            MessageBox.Show("User " + newUser.UserName + " is added with a password of '"
                            + newUser.Password + "'");
            this.Close();

        }

        private void EditUser(){
            if (string.IsNullOrWhiteSpace(txtUserName.Text))
            {
                MessageBox.Show("Please provide username",
                        "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            _selectedUser .IsActive = chkActive.Checked;
            _selectedUser.Role = _roles.FirstOrDefault(r => r.Id == ((Role) cboRole.SelectedItem).Id);

            _userService.EditUser(_selectedUser);

            MessageBox.Show("Success Edit User");
            this.Close();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

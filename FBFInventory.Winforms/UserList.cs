using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using FBFInventory.Domain.Entity;
using FBFInventory.Infrastructure.Service;
using FBFInventory.Winforms.Helper;

namespace FBFInventory.Winforms
{
    public partial class UserListForm : Form
    {
        private readonly UserService _userService;
        private readonly string _currentAdmin;
        private List<User> _users;
        private User _selectedUser;

        public UserListForm(UserService userService, string currentAdmin){
            _userService = userService;
            _currentAdmin = currentAdmin;
            InitializeComponent();
        }

        private void UserList_Load(object sender, EventArgs e){
            loadUser();
        }

        private void loadUser(){
            _users = _userService.GetAllUser().Where(u => u.UserName != _currentAdmin).ToList();
            FillListViewWithUsers();
        }

        private void FillListViewWithUsers(){
            ClearListView();
            foreach (var u in _users){
                string[] arr = new string[4];
                arr[0] = Convert.ToString(u.Id);
                arr[1] = Convert.ToString(u.UserName);
                arr[2] = Convert.ToString(u.Role.Name);

                arr[3] = u.IsActive ? "Active" : "Inactive";

                ListViewItem i = new ListViewItem(arr);

                listView1.Items.Add(i);
            }
        }

        private void ClearListView(){
            listView1.Items.Clear();
            listView1.Refresh();
        }

        private void cmdAdd_Click(object sender, EventArgs e){
            UserDialogue d = new UserDialogue(Operation.Add, null, _userService);
            d.ShowDialog();
            loadUser();
        }

        private void cmdEdit_Click(object sender, EventArgs e){
            if (listView1.SelectedItems.Count == 0){
                MessageBox.Show("Please select user from the list",
                    "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            UserDialogue d = new UserDialogue(Operation.Edit, _selectedUser, _userService);
            d.ShowDialog();
            loadUser();
        }

        private void cmdClose_Click(object sender, EventArgs e){
            this.Close();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e){
            if (listView1.SelectedItems.Count > 0){
                int Id = Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text);
                _selectedUser = _users.FirstOrDefault(s => s.Id == Id);
            }
        }
    }
}

using System;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using FBFInventory.Domain.Entity;
using FBFInventory.Infrastructure.EntityFramework;
using FBFInventory.Infrastructure.Service;

namespace FBFInventory.Winforms
{
    public partial class Login : Form
    {
        private UserService _userService;
        private FBFDBContext _context;
        private User _user;

        public Login(FBFDBContext context){
            _context = context;
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e){
            grdIpAddress.Enabled = false;
            txtIpAddress.Text = GetIpAddressOnTextFile();
        }

        private void cmdLogIn_Click(object sender, EventArgs e){
            if (string.IsNullOrWhiteSpace(txtName.Text)){
                MessageBox.Show("Please provide username",
                        "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text)){
                MessageBox.Show("Please provide Password",
                        "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            SetUpConnectionString();

            if (IsLogIn()){
                if (_user.IsActive){
                    IsLoggedIn = true;
                    UserName = txtName.Text;
                    Role = _user.Role.Name;
                    IpAddress = txtIpAddress.Text;
                    ChangeIpAddressOnTextfile();
                    this.Close();
                }
                else{
                    MessageBox.Show("Sorry, your account is no longer active. Please contact administrator",
                        "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else{
                MessageBox.Show("Wrong UserName or Password!",
                        "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void SetUpConnectionString(){
            IDatabaseType type = new EfSqlServer("SQLServerDb", txtIpAddress.Text);
            _context = new FBFDBContext(type);
            _userService = new UserService(_context);
        }

        private bool IsLogIn(){
            User u = _userService.GetUserbyNameAndPassword(txtName.Text, txtPassword.Text);
            _user = u;
            return _user != null;
        }

        private void cmdExit_Click(object sender, EventArgs e){
            this.Close();
            IsLoggedIn = false;
        }

        public bool IsLoggedIn { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }

        private void chkChangeDB_CheckedChanged(object sender, EventArgs e){
            if (chkChangeDB.Checked)
                grdIpAddress.Enabled = true;
            else{
                grdIpAddress.Enabled = false;
            }
        }

        private string GetIpAddressOnTextFile(){            
            StreamReader file =
                new StreamReader(Environment.CurrentDirectory + @"\DbIpAddress.txt");

            string line = file.ReadLine();
            file.Close();
            return line;
        }

        private void ChangeIpAddressOnTextfile(){
            File.WriteAllText(Environment.CurrentDirectory + @"\DbIpAddress.txt", IpAddress);
        }

        public string IpAddress { get; set; }
    }
}

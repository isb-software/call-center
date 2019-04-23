using DataAccess.Services;
using Entities.Models;
using System;
using System.Windows.Forms;

namespace Agent
{
    public partial class LoginForm : Form
    {
        private UserService userService;

        public LoginForm()
        {
            userService = new UserService();

            InitializeComponent();

            this.LoadUsers();
        }

        private void LoginButtonClick(object sender, EventArgs e)
        {
            User selectedUser = (User)UsersComboBox.SelectedItem;
            string userName = $"{selectedUser.LastName} {selectedUser.FirstName}";

            if (OutboundRadioButton.Checked)
            {
                CallAgent callAgentForm = new CallAgent(selectedUser.Id, userName);
                callAgentForm.Show();
                this.Hide();
            }
            else
            {
                // TODO: open the other form
            }
            
        }

        private void LoadUsers()
        {
            var users = this.userService.GetAll();
            UsersComboBox.DataSource = users;
            UsersComboBox.ValueMember = "Id";
            UsersComboBox.DisplayMember = "FullName";
            UsersComboBox.SelectedIndex = -1;
        }

        private void UsersComboBoxFormat(object sender, ListControlConvertEventArgs e)
        {
            string lastname = ((User)e.ListItem).LastName;
            string firstname = ((User)e.ListItem).FirstName;
            e.Value = $"{lastname} {firstname}";
        }

        private void UsersComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UsersComboBox.SelectedIndex == -1)
            {
                LoginButton.Enabled = false;
            }
            else
            {
                LoginButton.Enabled = true;
            }
        }
    }
}

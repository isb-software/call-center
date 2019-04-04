using System;
using System.Windows.Forms;

using DataAccess.Services;

using Entities.Models;

namespace Agent
{
    public partial class Form1 : Form
    {
        private readonly UserService userService;

        public Form1()
        {
            this.userService = new UserService();
            InitializeComponent();
        }

        private void lblLoggedSince_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var user = new User
                           {
                               Id = 6,
                               CreatedDate = DateTime.Now,
                               FirstName = "asd22",
                               LastName = "asd22"
                           };

            this.userService.Delete(5);

        }
    }
}

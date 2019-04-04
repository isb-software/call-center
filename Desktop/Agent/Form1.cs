using System;
using System.Data.Entity.Infrastructure;
using System.Windows.Forms;

using DataAccess.Services;

using Entities.Enums;
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
                               CreatedDate = DateTime.Now,
                               FirstName = "asd22",
                               LastName = "asd22"
                           };

            var call = new Call
                           {
                               CallType = CallType.Inbound,
                               DateTimeOfCall = DateTime.Now,
                               Duration = 444,
                               Notes = "dadasdasd asjkd kasd klskldn askld klakld mas",
                               RecordingPath = "asdmk nasj ndjasnd jasj as",
                               StatusId = 1,
                               UserId = 1
                           };

            this.userService.Create(user);

            var callservice = new CallService();
            callservice.Create(call);
        }
    }
}

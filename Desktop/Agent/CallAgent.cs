using System;
using System.Windows.Forms;

using DataAccess.Services;

namespace Agent
{
    public partial class CallAgent : Form
    {
        public CallAgent()
        {
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
            var asd = new CallCountService();

            asd.IncreaseCount(DateTime.Now, 1);
            //asd.IncreaseCount(DateTime.Now, 2);
            //asd.IncreaseCount(DateTime.Now.AddDays(-1), 1);
            //asd.IncreaseCount(DateTime.Now.AddDays(-2), 3);
            //asd.IncreaseCount(DateTime.Now.AddDays(10), 4);
            //asd.IncreaseCount(DateTime.Now.AddDays(-5), 4);
            //asd.IncreaseCount(DateTime.Now.AddDays(-3), 5);
            //asd.IncreaseCount(DateTime.Now.AddDays(-1), 5);
            //asd.IncreaseCount(DateTime.Now.AddMonths(1), 5);
            //asd.IncreaseCount(DateTime.Now.AddMonths(1), 4);
            //asd.IncreaseCount(DateTime.Now.AddMonths(1), 4);
            //asd.IncreaseCount(DateTime.Now.AddMonths(-1), 3);
            //asd.IncreaseCount(DateTime.Now.AddMonths(-1), 2);
            //asd.IncreaseCount(DateTime.Now.AddDays(12), 2);
            //asd.IncreaseCount(DateTime.Now.AddDays(22), 1);
            //asd.IncreaseCount(DateTime.Now.AddDays(6), 1);
            //asd.IncreaseCount(DateTime.Now.AddDays(9), 3);
            //asd.IncreaseCount(DateTime.Now.AddDays(11), 2);
            //asd.IncreaseCount(DateTime.Now.AddDays(15), 4);
            //asd.IncreaseCount(DateTime.Now.AddDays(18), 5);
            //asd.IncreaseCount(DateTime.Now.AddDays(21), 5);

            //asd.IncreaseCount(DateTime.Now, 2);
            //asd.IncreaseCount(DateTime.Now, 3);
            //asd.IncreaseCount(DateTime.Now.AddDays(-1), 2);
            //asd.IncreaseCount(DateTime.Now.AddDays(-2), 4);
            //asd.IncreaseCount(DateTime.Now.AddDays(10), 5);
            //asd.IncreaseCount(DateTime.Now.AddDays(-5), 5);
            //asd.IncreaseCount(DateTime.Now.AddDays(-3), 1);
            //asd.IncreaseCount(DateTime.Now.AddDays(-1), 1);
            //asd.IncreaseCount(DateTime.Now.AddMonths(1), 1);
            //asd.IncreaseCount(DateTime.Now.AddMonths(1), 5);
            //asd.IncreaseCount(DateTime.Now.AddMonths(1), 5);
            //asd.IncreaseCount(DateTime.Now.AddMonths(-1), 4);
            //asd.IncreaseCount(DateTime.Now.AddMonths(-1), 3);
            //asd.IncreaseCount(DateTime.Now.AddDays(12), 3);
            //asd.IncreaseCount(DateTime.Now.AddDays(22), 2);
            //asd.IncreaseCount(DateTime.Now.AddDays(6), 2);
            //asd.IncreaseCount(DateTime.Now.AddDays(9), 4);
            //asd.IncreaseCount(DateTime.Now.AddDays(11), 3);
            //asd.IncreaseCount(DateTime.Now.AddDays(15), 5);
            //asd.IncreaseCount(DateTime.Now.AddDays(18), 1);
            //asd.IncreaseCount(DateTime.Now.AddDays(21), 1);

            //asd.IncreaseCount(DateTime.Now, 3);
            //asd.IncreaseCount(DateTime.Now, 4);
            //asd.IncreaseCount(DateTime.Now.AddDays(-1), 3);
            //asd.IncreaseCount(DateTime.Now.AddDays(-2), 5);
            //asd.IncreaseCount(DateTime.Now.AddDays(10), 1);
            //asd.IncreaseCount(DateTime.Now.AddDays(-5), 1);
            //asd.IncreaseCount(DateTime.Now.AddDays(-3), 2);
            //asd.IncreaseCount(DateTime.Now.AddDays(-1), 2);
            //asd.IncreaseCount(DateTime.Now.AddMonths(1), 2);
            //asd.IncreaseCount(DateTime.Now.AddMonths(1), 1);
            //asd.IncreaseCount(DateTime.Now.AddMonths(1), 1);
            //asd.IncreaseCount(DateTime.Now.AddMonths(-1), 5);
            //asd.IncreaseCount(DateTime.Now.AddMonths(-1), 4);
            //asd.IncreaseCount(DateTime.Now.AddDays(12), 4);
            //asd.IncreaseCount(DateTime.Now.AddDays(22), 3);
            //asd.IncreaseCount(DateTime.Now.AddDays(6), 3);
            //asd.IncreaseCount(DateTime.Now.AddDays(9), 5);
            //asd.IncreaseCount(DateTime.Now.AddDays(11), 4);
            //asd.IncreaseCount(DateTime.Now.AddDays(15), 1);
            //asd.IncreaseCount(DateTime.Now.AddDays(18), 2);
            //asd.IncreaseCount(DateTime.Now.AddDays(21), 2);
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SipWrapper wrapper = new SipWrapper();
        }
    }
}

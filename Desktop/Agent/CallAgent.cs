using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;
using DataAccess.Services;

using SipWrapper;

namespace Agent
{
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    public partial class CallAgent : Form
    {
        public CallAgent()
        {
            InitializeComponent();
        }

        private void SaveButtonClick(object sender, EventArgs e)
        {
            StatusService asd = new StatusService();

            SipPhone da = new SipPhone();
        }
    }
}

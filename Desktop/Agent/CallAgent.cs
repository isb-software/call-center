using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;
using DataAccess.Services;

using SipWrapper;
using SipWrapper.EventHandlers;

namespace Agent
{
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    public partial class CallAgent : Form
    {
        private bool isCalling = false;

        private SipPhone sipPhone;

        public CallAgent()
        {
            InitializeComponent();
            this.InitializeSipPhone();
        }

        private void SaveButtonClick(object sender, EventArgs e)
        {
            StatusService asd = new StatusService();
        }

        private void CallHangUpButtonClick(object sender, EventArgs e)
        {
            if (!this.isCalling)
            {
                var phoneNumber = "40766614431";
                sipPhone.StartCall(phoneNumber);
                DisplayNotificationMessage($"Suna numarul {phoneNumber}");
                this.CallHangUpButton.Text = "Inchide";
            }
            else
            {
                sipPhone.HangUp();
                DisplayNotificationMessage($"Inchide apelul");
                this.CallHangUpButton.Text = "Suna";
            }
        }

        private void InitializeSipPhone()
        {
            DisplayNotificationMessage("Initializing...");
            sipPhone = new SipPhone();
            this.sipPhone.SipInitialize += this.OnSipInitialize;
            this.sipPhone.SipRegistered += this.OnSipRegistered;
            this.sipPhone.SipPhoneNotify += this.OnSipPhoneNotify;
        }

        private void DisplayNotificationMessage(string message)
        {
            notificationsListBox.Items.Add(message);
            notificationsListBox.TopIndex = notificationsListBox.Items.Count - 1; // scrol down
        }

        private void OnSipInitialize(object sender, MessageEventArgs e)
        {
            this.DisplayNotificationMessage(e.Message);
        }

        private void OnSipRegistered(object sender, MessageEventArgs e)
        {
            this.DisplayNotificationMessage(e.Message);
        }

        private void OnSipPhoneNotify(object sender, MessageEventArgs e)
        {
            this.DisplayNotificationMessage(e.Message);
        }
    }
}

using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;
using DataAccess.Services;
using Entities.Models;
using SipWrapper;
using SipWrapper.EventHandlers;

namespace Agent
{
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    public partial class CallAgent : Form
    {
        private bool isCalling = false;
        private string phoneNumber = string.Empty;
        private Timer timer;
        private int callDuration = 0;
        private string recordingPath = string.Empty;

        private SipPhone sipPhone;
        private PriorityQueueService priorityQueueService;
        private CallService callService;
        private StatusService statusService;
        private NormalQueueService normalQueueService;

        public CallAgent()
        {
            InitializeComponent();
            this.priorityQueueService = new PriorityQueueService();
            this.callService = new CallService();
            this.statusService = new StatusService();
            this.normalQueueService = new NormalQueueService();
            this.InitializeSipPhone();
            this.InitializeStatus();
        }

        private void SaveButtonClick(object sender, EventArgs e)
        {
            Call call = new Call
            {
                Age = Convert.ToInt32(AgeTextBox.Text),
                CallType = Entities.Enums.CallType.Outbound,
                City = CityTextBox.Text,
                County = CountyTextBox.Text,
                DateTimeOfCall = DateTime.Now,
                Duration = callDuration,
                Education = EducationTextBox.Text,
                Forename = ForenameTextBox.Text,
                Name = NameTextBox.Text,
                Notes = NotesTextBox.Text,
                PhoneNumber = this.phoneNumber,
                StatusId = Convert.ToInt32(this.StatusComboBox.SelectedValue),
                UserId = 2, //TODO: Add real user when available
                RecordingPath = recordingPath
            };

            callService.Create(call);
            this.callDuration = 0;
            this.CallHangUpButton.Enabled = true;
        }

        private void CallHangUpButtonClick(object sender, EventArgs e)
        {
            if (!this.isCalling)
            {
                phoneNumber = this.GetPhoneNumber();
                PhoneNumberTextBox.Text = phoneNumber;
                this.CallHangUpButton.Text = "Inchide";
                this.isCalling = true;

                this.LoadDetailsForPhoneNumber();

                DisplayNotificationMessage($"Se apeleaza numarul {phoneNumber}");
                sipPhone.StartCall(phoneNumber);
            }
            else
            {
                this.isCalling = false;
                sipPhone.HangUp();
                DisplayNotificationMessage($"Apel inchis");
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
            this.sipPhone.SipEstablishedCall += this.OnSipEstablishedCall;
            this.sipPhone.SipClearedCall += OnSipClearedCall;
        }

        private void OnSipClearedCall(object sender, EventArgs e)
        {
            if (timer != null)
            {
                timer.Stop();
                CallHangUpButton.Enabled = false;
                SaveButton.Enabled = true;
            }
        }

        private void InitializeStatus()
        {
            var statuses = this.statusService.GetAll();
            StatusComboBox.DataSource = statuses;
            StatusComboBox.ValueMember = "Id";
            StatusComboBox.DisplayMember = "Description";
            StatusComboBox.SelectedIndex = -1;
        }

        private void LoadDetailsForPhoneNumber()
        {
            Call call = callService.GetByPhoneNumber(this.phoneNumber);
            if (call != null)
            {
                NameTextBox.Text = call.Name;
                ForenameTextBox.Text = call.Forename;
                AgeTextBox.Text = call.Age.ToString();
                EducationTextBox.Text = call.Education;
                CityTextBox.Text = call.City;
                CountyTextBox.Text = call.County;

                NameTextBox.Enabled = false;
                ForenameTextBox.Enabled = false;
                AgeTextBox.Enabled = false;
                EducationTextBox.Enabled = false;
                CityTextBox.Enabled = false;
                CountyTextBox.Enabled = false;
            }
        }

        private void DisplayNotificationMessage(string message)
        {
            notificationsListBox.Items.Add(message);
            notificationsListBox.TopIndex = notificationsListBox.Items.Count - 1; // scrol down
        }

        #region SipEventHandlers

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

        private void OnSipEstablishedCall(object sender, MessageEventArgs e)
        {
            this.DisplayNotificationMessage("A raspuns");
            this.DisplayNotificationMessage($"Incepe inregistrarea");
            recordingPath = e.Message;
            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += OnTimerTick;
            timer.Start();
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            callDuration++;
        }

        #endregion

        private void AgeTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private string GetPhoneNumber()
        {
            var phoneNumber = priorityQueueService.GetNextNumber();
            if(phoneNumber == null)
            {
                phoneNumber = this.normalQueueService.GetNextNumber();
            }

            return phoneNumber;
        }
    }
}

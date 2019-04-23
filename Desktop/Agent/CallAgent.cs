﻿using System;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;
using DataAccess.Services;
using Entities.Enums;
using Entities.Models;
using SipWrapper;
using SipWrapper.EventHandlers;

namespace Agent
{
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    public partial class CallAgent : Form
    {
        private string phoneNumber = string.Empty;
        private int callDuration = 0;
        private string recordingPath = string.Empty;
        private int currentCallAtempts = 0;
        private int currentUserId = 0;

        private Timer timer;
        private SipPhone sipPhone;
        private QueueEnum currentQueue;

        private PriorityQueueService priorityQueueService;
        private CallService callService;
        private StatusService statusService;
        private NormalQueueService normalQueueService;
        private CallCountService callCountService;

        public CallAgent(int userId, string userName)
        {
            InitializeComponent();
            this.InitializeServices();
            this.InitializeSipPhone();
            this.InitializeStatus();

            this.DocumentRichTextBox.LoadFile(ConfigurationManager.AppSettings["DocumentFileLocation"], RichTextBoxStreamType.RichText);
            this.LoggedSinceLabel.Text = $"De La: {DateTime.Now.ToString("HH:mm")}";
            this.currentUserId = userId;
            this.UserNameLabel.Text = userName;
        }

        private void SaveButtonClick(object sender, EventArgs e)
        {
            if(StatusComboBox.SelectedIndex == -1)
            {
                this.StatusErrorLabel.Visible = true;
                return;
            }

            this.SaveCall();
            this.IncrementCallCount();

            if((int)StatusComboBox.SelectedValue == (int)StatusEnum.NuRaspunde ||
                (int)StatusComboBox.SelectedValue == (int)StatusEnum.Ocupat)
            {
                this.SaveQueuePhoneNumber();
            }

            this.ResetForm();
            this.ResetVariables();
            SetCallHangUpSaveButtonState(true, false, false);
        }

        private void CallButtonClick(object sender, EventArgs e)
        {
            this.GetNextPhoneNumber();
            PhoneNumberTextBox.Text = phoneNumber;
            this.LoadDetailsForPhoneNumber();

            DisplayNotificationMessage($"Se apeleaza numarul {phoneNumber}");
            sipPhone.StartCall(phoneNumber);
            SetCallHangUpSaveButtonState(false, true, false);
        }

        private void HangUpButtonClick(object sender, EventArgs e)
        {
            sipPhone.HangUp();
            DisplayNotificationMessage($"Apel inchis");
            SetCallHangUpSaveButtonState(false, false, true);
        }

        private void AgeTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
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
            this.DisplayNotificationMessage("Incepe inregistrarea");
            recordingPath = e.Message;
            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += OnTimerTick;
            timer.Start();
        }

        private void OnSipClearedCall(object sender, EventArgs e)
        {
            if (timer != null)
            {
                timer.Stop();
            }
            SetCallHangUpSaveButtonState(false, false, true);
            SaveButton.Enabled = true;
        }

        private void OnSipLineBusy(object sender, EventArgs e)
        {
            SetCallHangUpSaveButtonState(false, false, true);
            this.StatusComboBox.SelectedValue = (int)StatusEnum.Ocupat;
        }

        private void OnSipAnsweringMachine(object sender, EventArgs e)
        {
            sipPhone.HangUp();

            StatusComboBox.SelectedValue = (int)StatusEnum.Casuta;
            this.SaveCall();
            this.SaveQueuePhoneNumber();
            this.IncrementCallCount();

            SetCallHangUpSaveButtonState(true, false, false);

            this.ResetForm();
            this.DisplayNotificationMessage("Numar salvat cu succes");
        }

        private void OnSipFaxMachine(object sender, EventArgs e)
        {
            sipPhone.HangUp();

            StatusComboBox.SelectedValue = (int)StatusEnum.Fax;
            this.SaveCall();
            this.IncrementCallCount();
            this.SaveQueuePhoneNumber();

            SetCallHangUpSaveButtonState(true, false, false);

            this.ResetForm();
            this.DisplayNotificationMessage("Numar salvat cu succes");
        }

        #endregion

        private void OnTimerTick(object sender, EventArgs e)
        {
            callDuration++;
        }

        private void GetNextPhoneNumber()
        {
            var queuePhoneNumber = priorityQueueService.GetNextNumber();
            currentQueue = QueueEnum.Priority;

            if (queuePhoneNumber == null)
            {
                queuePhoneNumber = normalQueueService.GetNextNumber();
                currentQueue = QueueEnum.Normal;
            }

            this.phoneNumber = queuePhoneNumber.PhoneNumber;
            this.currentCallAtempts = queuePhoneNumber.CallAtempts;
        }

        private void ResetVariables()
        {
            this.callDuration = 0;
            this.recordingPath = string.Empty;
        }

        private void ResetForm()
        {
            NameTextBox.Text = string.Empty;
            ForenameTextBox.Text = string.Empty;
            AgeTextBox.Text = string.Empty;
            EducationTextBox.Text = string.Empty;
            CityTextBox.Text = string.Empty;
            CountyTextBox.Text = string.Empty;
            NotesTextBox.Text = string.Empty;
            PhoneNumberTextBox.Text = string.Empty;
            StatusComboBox.SelectedIndex = -1;

            NameTextBox.Enabled = true;
            ForenameTextBox.Enabled = true;
            AgeTextBox.Enabled = true;
            EducationTextBox.Enabled = true;
            CityTextBox.Enabled = true;
            CountyTextBox.Enabled = true;

            this.StatusErrorLabel.Visible = false;
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
            this.sipPhone.SipLineBusy += OnSipLineBusy;
            this.sipPhone.AnsweringMachine += OnSipAnsweringMachine;
            this.sipPhone.FaxMachine += OnSipFaxMachine;
        }

        private void InitializeStatus()
        {
            var statuses = this.statusService.GetAll();
            StatusComboBox.DataSource = statuses;
            StatusComboBox.ValueMember = "Id";
            StatusComboBox.DisplayMember = "Description";
            StatusComboBox.SelectedIndex = -1;
        }

        private void InitializeServices()
        {
            this.priorityQueueService = new PriorityQueueService();
            this.callService = new CallService();
            this.statusService = new StatusService();
            this.normalQueueService = new NormalQueueService();
            this.callCountService = new CallCountService();
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

        private void IncrementCallCount()
        {
            int currentCount = Convert.ToInt32(CallCountLabel.Text);
            CallCountLabel.Text = (++currentCount).ToString();

            this.callCountService.IncreaseCount(Convert.ToInt32(StatusComboBox.SelectedValue));
        }

        private void SaveCall()
        {
            Call call = new Call
            {
                Age = string.IsNullOrEmpty(AgeTextBox.Text) ? 0 : Convert.ToInt32(AgeTextBox.Text),
                CallType = CallType.Outbound,
                City = CityTextBox.Text,
                County = CountyTextBox.Text,
                DateTimeOfCall = DateTime.Now.AddSeconds(-callDuration),
                Duration = callDuration,
                Education = EducationTextBox.Text,
                Forename = ForenameTextBox.Text,
                Name = NameTextBox.Text,
                Notes = NotesTextBox.Text,
                PhoneNumber = this.phoneNumber,
                StatusId = Convert.ToInt32(this.StatusComboBox.SelectedValue),
                UserId = this.currentUserId,
                RecordingPath = recordingPath
            };
            callService.Create(call);
        }

        private void SaveQueuePhoneNumber()
        {
            QueuePhoneNumber queuePhoneNumber = new QueuePhoneNumber
            {
                CallAtempts = currentCallAtempts,
                PhoneNumber = phoneNumber
            };

            if (currentQueue == QueueEnum.Priority)
            {
                priorityQueueService.Create(queuePhoneNumber);
            }
            else
            {
                normalQueueService.Create(queuePhoneNumber);
            }
        }

        private void SetCallHangUpSaveButtonState(bool isCallButtonEnabled, bool isHangUpButtonEnabled, bool isSaveButtonEnabled)
        {
            HangUpButton.Enabled = isHangUpButtonEnabled;
            CallButton.Enabled = isCallButtonEnabled;
            SaveButton.Enabled = isSaveButtonEnabled;
        }

        private void CallAgent_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}

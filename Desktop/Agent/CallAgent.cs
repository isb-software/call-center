using System;
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
        private int callDuration;
        private int callStartDuration;
        private string recordingPath = string.Empty;
        private int currentCallAtempts;
        private readonly int currentUserId;
        private readonly int callStartDurationThreshold;
        private readonly int workDayStartHour;
        private readonly int workDayEndHour;

        private Timer callDurationTimer;
        private Timer callStartDurationTimer;
        private SipPhone sipPhone;
        private InitialData currentIntialData;

        private PriorityQueueService priorityQueueService;
        private CallService callService;
        private StatusService statusService;
        private NormalQueueService normalQueueService;
        private CallCountService callCountService;
        private InitialDataService initialDataService;
        private EducationTypeService educationTypeService;
        private AgeRangeService ageRangeService;
        private EmployeeTypeService employeeTypeService;

        public CallAgent(int userId, string userName)
        {
            InitializeComponent();
            this.InitializeServices();
            this.InitializeSipPhone();
            this.InitializeForm();

            this.callStartDurationThreshold = Convert.ToInt32(ConfigurationManager.AppSettings["SipCallStartThreshold"]);
            this.workDayStartHour = Convert.ToInt32(ConfigurationManager.AppSettings["WorkDayStartHour"]);
            this.workDayEndHour = Convert.ToInt32(ConfigurationManager.AppSettings["WorkDayEndHour"]);
            this.DocumentRichTextBox.LoadFile(ConfigurationManager.AppSettings["DocumentFileLocation"], RichTextBoxStreamType.RichText);
            this.LoggedSinceLabel.Text = $"De La: {DateTime.Now.ToString("HH:mm")}";
            this.currentUserId = userId;
            this.UserNameLabel.Text = userName;
        }

        private void SaveButtonClick(object sender, EventArgs e)
        {
            if (!StatusComboBox.Enabled)
            {
                // This branch is for Adding an ad-hoc number to queue.
                this.currentCallAtempts = -1; // to avoid having call count being set to 1.
                this.SaveQueuePhoneNumber();
                this.currentCallAtempts = 0;
                this.StatusComboBox.Enabled = true;
                this.AddNumberButton.Text = "Adauga numar";
                this.NotesTextBox.Enabled = true;
                this.PhoneNumberTextBox.Enabled = false;
            }
            else
            {

                if (StatusComboBox.SelectedIndex == -1)
                {
                    this.StatusErrorLabel.Visible = true;
                    return;
                }

                this.UpdateInitialData();
                this.SaveCall();
                this.IncrementCallCount();
                if ((int)StatusComboBox.SelectedValue == (int)StatusEnum.NuRaspunde ||
                    (int)StatusComboBox.SelectedValue == (int)StatusEnum.Ocupat ||
                    (int)StatusComboBox.SelectedValue == (int)StatusEnum.Casuta)
                {
                    this.SaveQueuePhoneNumber();
                }
            }

            this.ResetForm();
            this.ResetVariables();
            SetCallHangUpSaveButtonState(true, false, false);
        }

        private void CallButtonClick(object sender, EventArgs e)
        {
            var currentDate = DateTime.Now;

            if (currentDate.Hour < workDayStartHour || currentDate.Hour >= workDayEndHour)
            {
                workDayErrorLabel.Text = $"Puteti suna doar intre orele {workDayStartHour}:00 - {workDayEndHour}:00";
                workDayErrorLabel.Visible = true;
                return;
            }

            workDayErrorLabel.Visible = false;
            this.GetNextPhoneNumber();
            PhoneNumberTextBox.Text = phoneNumber;
            this.LoadDetailsForPhoneNumber();

            DisplayNotificationMessage($"Se apeleaza numarul {phoneNumber}");
            this.StartCallStartDurationTimer();
            sipPhone.StartCall(phoneNumber);
            SetCallHangUpSaveButtonState(false, true, false);
        }

        private void HangUpButtonClick(object sender, EventArgs e)
        {
            sipPhone.HangUp();
            this.callStartDurationTimer?.Stop();

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
            StopCallStartDurationTimer();
            StartCallDurationTimer();
        }

        private void StartCallDurationTimer()
        {
            callDurationTimer = new Timer();
            callDurationTimer.Interval = 1000;
            callDurationTimer.Tick += OnCallDurationTimerTick;
            callDurationTimer.Start();
        }

        private void StartCallStartDurationTimer()
        {
            callStartDurationTimer = new Timer();
            callStartDurationTimer.Interval = 1000;
            callStartDurationTimer.Tick += OnCallStartDurationTimerTick;
            callStartDurationTimer.Start();
        }

        private void OnSipClearedCall(object sender, EventArgs e)
        {
            if (callDurationTimer != null)
            {
                callDurationTimer.Stop();
            }

            StopCallStartDurationTimer();
            SetCallHangUpSaveButtonState(false, false, true);
            SaveButton.Enabled = true;
        }

        private void StopCallStartDurationTimer()
        {
            if (callStartDurationTimer != null)
            {
                callStartDurationTimer.Stop();
            }
        }

        private void OnSipLineBusy(object sender, EventArgs e)
        {
            SetCallHangUpSaveButtonState(false, false, true);
            StopCallStartDurationTimer();

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

            SetCallHangUpSaveButtonState(true, false, false);

            this.ResetForm();
            this.DisplayNotificationMessage("Numar salvat cu succes");
        }

        #endregion

        private void OnCallDurationTimerTick(object sender, EventArgs e)
        {
            callDuration++;
        }

        private void OnCallStartDurationTimerTick(object sender, EventArgs e)
        {
            callStartDuration++;

            if (callStartDuration == this.callStartDurationThreshold)
            {
                this.sipPhone.HangUp();

                StatusComboBox.SelectedValue = (int)StatusEnum.NuRaspunde;

                this.SaveCall();
                this.IncrementCallCount();
                this.SaveQueuePhoneNumber();

                this.ResetVariables();
                this.ResetForm();
                this.SetCallHangUpSaveButtonState(true, false, false);
                this.callStartDurationTimer.Stop();
            }
        }

        private void GetNextPhoneNumber()
        {
            var queuePhoneNumber = this.priorityQueueService.GetNextNumber() ?? this.normalQueueService.GetNextNumber();

            if (queuePhoneNumber == null)
            {
                // TODO add some graceful way of dealing with it.
            }
            
            this.phoneNumber = queuePhoneNumber.PhoneNumber;
            this.currentCallAtempts = queuePhoneNumber.CallAtempts;
        }

        private void ResetVariables()
        {
            this.callDuration = 0;
            this.callStartDuration = 0;
            this.recordingPath = string.Empty;
        }

        private void ResetForm()
        {
            AgeRangeComboBox.SelectedIndex = -1;
            EducationComboBox.SelectedIndex = -1;
            CityTextBox.Text = string.Empty;
            CountyTextBox.Text = string.Empty;
            NotesTextBox.Text = string.Empty;
            PhoneNumberTextBox.Text = string.Empty;
            StatusComboBox.SelectedIndex = -1;

            AgeRangeComboBox.Enabled = true;
            EducationComboBox.Enabled = true;
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

        private void InitializeForm()
        {
            var statuses = this.statusService.GetAll();
            StatusComboBox.DataSource = statuses;
            StatusComboBox.ValueMember = "Id";
            StatusComboBox.DisplayMember = "Description";
            StatusComboBox.SelectedIndex = -1;

            var educationTypes = this.educationTypeService.GetAll();
            EducationComboBox.DataSource = educationTypes;
            EducationComboBox.ValueMember = "Id";
            EducationComboBox.DisplayMember = "Name";
            EducationComboBox.SelectedIndex = -1;

            var ageRanges = this.ageRangeService.GetAll();
            AgeRangeComboBox.DataSource = ageRanges;
            AgeRangeComboBox.ValueMember = "Id";
            AgeRangeComboBox.DisplayMember = "Range";
            AgeRangeComboBox.SelectedIndex = -1;

            var employeeTypes = this.employeeTypeService.GetAll();
            EmployeeTypeComboBox.DataSource = employeeTypes;
            EmployeeTypeComboBox.ValueMember = "Id";
            EmployeeTypeComboBox.DisplayMember = "Name";
            EmployeeTypeComboBox.SelectedIndex = -1;
        }

        private void InitializeServices()
        {
            this.priorityQueueService = new PriorityQueueService();
            this.callService = new CallService();
            this.statusService = new StatusService();
            this.normalQueueService = new NormalQueueService();
            this.callCountService = new CallCountService();
            this.initialDataService= new InitialDataService();
            this.educationTypeService = new EducationTypeService();
            this.ageRangeService = new AgeRangeService();
            this.employeeTypeService = new EmployeeTypeService();
        }

        private void LoadDetailsForPhoneNumber()
        {
            this.currentIntialData = initialDataService.GetByPhoneNumber(this.phoneNumber);
            if (currentIntialData != null)
            {
                AgeRangeComboBox.SelectedValue = currentIntialData.AgeRangeId != null ? currentIntialData.AgeRangeId : 0;
                EducationComboBox.SelectedValue = currentIntialData.EducationTypeId != null ? currentIntialData.EducationTypeId : 0;
                CityTextBox.Text = currentIntialData.City;
                CountyTextBox.Text = currentIntialData.County;
                EmployeeTypeComboBox.SelectedValue = currentIntialData.EmployeeTypeId != null ? currentIntialData.EmployeeTypeId : 0;
            }
        }

        private void DisplayNotificationMessage(string message)
        {
            notificationsListBox.Items.Add(message);
            notificationsListBox.TopIndex = notificationsListBox.Items.Count - 1; // scroll down
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
                CallType = CallType.Outbound,
                DateTimeOfCall = DateTime.Now.AddSeconds(-callDuration),
                Duration = callDuration,
                Notes = NotesTextBox.Text,
                PhoneNumber = this.phoneNumber,
                StatusId = Convert.ToInt32(this.StatusComboBox.SelectedValue),
                UserId = this.currentUserId,
                RecordingPath = recordingPath,
                InitialDataId = this.currentIntialData.Id
            };

            callService.Create(call);
        }

        private void UpdateInitialData()
        {
            InitialData initialDataToUpdate = new InitialData
                                          {
                                              Id = this.currentIntialData.Id,
                                              EmployeeTypeId = Convert.ToInt32(this.EmployeeTypeComboBox.SelectedValue),
                                              AgeRangeId = Convert.ToInt32(this.AgeRangeComboBox.SelectedValue),
                                              City = this.CityTextBox.Text,
                                              County = this.CountyTextBox.Text,
                                              EducationTypeId = Convert.ToInt32(this.EducationComboBox.SelectedValue),
                                              PhoneNumber = this.phoneNumber
                                          };

            this.initialDataService.Update(initialDataToUpdate);
        }

        private void SaveQueuePhoneNumber()
        {
            QueuePhoneNumber queuePhoneNumber = new QueuePhoneNumber
            {
                CallAtempts = currentCallAtempts,
                PhoneNumber = phoneNumber
            };

            priorityQueueService.Create(queuePhoneNumber);
        }

        private void SetCallHangUpSaveButtonState(bool isCallButtonEnabled, bool isHangUpButtonEnabled, bool isSaveButtonEnabled)
        {
            HangUpButton.Enabled = isHangUpButtonEnabled;
            CallButton.Enabled = isCallButtonEnabled;
            AddNumberButton.Enabled = isCallButtonEnabled && !isHangUpButtonEnabled;
            SaveButton.Enabled = isSaveButtonEnabled;
        }

        private void CallAgent_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void AddNumberButton_Click(object sender, EventArgs e)
        {
            if (this.AddNumberButton.Text == "Adauga numar") {
                this.StatusComboBox.Enabled = false;
                this.PhoneNumberTextBox.Enabled = true;
                this.AgeRangeComboBox.Enabled = false;
                this.EducationComboBox.Enabled = false;
                this.CityTextBox.Enabled = false;
                this.CountyTextBox.Enabled = false;
                this.NotesTextBox.Enabled = false;
                this.AddNumberButton.Text = "Anuleaza adaugare";
            }
            else
            {
                this.AddNumberButton.Text = "Adauga numar";
                this.StatusComboBox.Enabled = true;
                this.PhoneNumberTextBox.Enabled = true;
                this.AgeRangeComboBox.Enabled = true;
                this.EducationComboBox.Enabled = true;
                this.CityTextBox.Enabled = true;
                this.NotesTextBox.Enabled = true;
                this.CountyTextBox.Enabled = true;
            }
        }
    }
}

using System;
using System.IO;

using SipWrapper.EventHandlers;

using SIPVoipSDK;


namespace SipWrapper
{
    public class SipPhone
    {
        private CAbtoPhone abtoPhone;
        private CConfig phoneCfg;

        /**
         * Logging levels for underlying SIP SDK.
         eLogNone = -1,
         eLogCritical = 2,
         eLogError = 3,
         eLogWarning = 4,
         eLogInfo = 6,
         eLogDebug = 7
        */

        const string LICENSE_USER_ID = "{Licensed_for_ISB_Software_Consulting-A4D8-475A-9AC9A63D-E2FD-1AC3-2B95-71CE19A3A3F1}";
        const string LICENSE_KEY = "{T+C/ve2txl8Gucgify5BcUuyEWXPdlxeHMI4ageygVBGGMRK0wdrGwg8IjGDTcOGUQj7YDL9lJSdSoCbQ4d86A==}";

        //TODO: point to network share.
        static readonly string RECORDINGS_PATH = @"c:\temp\recordings\";
        static readonly string STAR_RECORDING_PATH = @"c:\temp\starrecordings\mozart.wav";
        private string cfgFileName = "phoneCfg.ini";

        const string SIP_USER = "99051000507121";
        const string SIP_PWD = "5FSzPGcamwvhfw";

        private string phoneNumber;
        private bool recordingStarted;
        private int currentLineId;
        private bool playFinished;

        public SipPhone()
        {
            abtoPhone = new CAbtoPhone();
            if (!this.Initialize())
            {
                throw new Exception("Unable to initialize line");
            }
        }

        public bool Initialize()
        {
            this.abtoPhone.OnClearedCall += AbtoPhone_OnClearedCall;
            this.abtoPhone.OnEstablishedCall += AbtoPhone_OnEstablishedCall;
            this.abtoPhone.OnRegistered += AbtoPhone_OnRegistered;
            this.abtoPhone.OnIncomingCall += AbtoPhone_OnIncomingCall;
            this.abtoPhone.OnPlayFinished += AbtoPhone_OnPlayFinished;
            this.abtoPhone.OnToneReceived += AbtoPhone_OnToneReceived;
            this.abtoPhone.OnDetectedAnswerTime += AbtoPhone_OnDetectedAnswerTime;
            this.abtoPhone.OnPhoneNotify += AbtoPhone_OnPhoneNotify;
            this.abtoPhone.OnSubscribeStatus += AbtoPhone_OnSubscribeStatus;
            this.abtoPhone.OnInitialized += AbtoPhone_OnInitialized;

            phoneCfg = abtoPhone.Config;
            phoneCfg.Load(cfgFileName);

            phoneCfg.MP3RecordingEnabled = 1;

            phoneCfg.LicenseUserId = LICENSE_USER_ID;
            phoneCfg.LicenseKey = LICENSE_KEY;
            phoneCfg.RegDomain = "sip.skype.com";
            phoneCfg.RegUser = SIP_USER;
            phoneCfg.RegPass = SIP_PWD;
            phoneCfg.ListenPort = 5060;
            phoneCfg.RegExpire = 300;

            phoneCfg.TonesTypesToDetect = (int)ToneType.eToneDtmf;

            try
            {
                abtoPhone.ApplyConfig();
                abtoPhone.Initialize();

                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        private void AbtoPhone_OnSubscribeStatus(int subscriptionId, int statusCode, string statusMsg)
        {
            int a = 23;
        }

        private void AbtoPhone_OnPhoneNotify(string message)
        {
            MessageEventArgs eventArgs = new MessageEventArgs { Message = message };
            this.OnSipPhoneNotify(eventArgs);
        }

        private void AbtoPhone_OnRegistered(string message)
        {
            MessageEventArgs eventArgs = new MessageEventArgs { Message = message };
            this.OnSipRegistered(eventArgs);
        }

        #region events

        private void OnAnsweringMachine(EventArgs e)
        {
            EventHandler handler = AnsweringMachine;
            handler?.Invoke(this, e);
        }

        public event EventHandler AnsweringMachine;

        private void OnFaxMachine(EventArgs e)
        {
            EventHandler handler = FaxMachine;
            handler?.Invoke(this, e);
        }

        public event EventHandler FaxMachine;

        private void OnSipInitialize(MessageEventArgs e)
        {
            EventHandler<MessageEventArgs> handler = SipInitialize;
            handler?.Invoke(this, e);
        }

        public event EventHandler<MessageEventArgs> SipInitialize;

        private void OnSipRegistered(MessageEventArgs e)
        {
            EventHandler<MessageEventArgs> handler = SipRegistered;
            handler?.Invoke(this, e);
        }

        public event EventHandler<MessageEventArgs> SipRegistered;

        private void OnSipPhoneNotify(MessageEventArgs e)
        {
            EventHandler<MessageEventArgs> handler = SipPhoneNotify;
            handler?.Invoke(this, e);
        }

        public event EventHandler<MessageEventArgs> SipPhoneNotify;

        private void OnSipEstablishedCall(MessageEventArgs e)
        {
            EventHandler<MessageEventArgs> handler = SipEstablishedCall;
            handler?.Invoke(this, e);
        }

        public event EventHandler<MessageEventArgs> SipEstablishedCall;

        private void OnSipClearedCall(EventArgs e)
        {
            EventHandler handler = SipClearedCall;
            handler?.Invoke(this, e);
        }

        public event EventHandler SipClearedCall;

        #endregion events

        private void AbtoPhone_OnInitialized(string message)
        {
            MessageEventArgs eventArgs = new MessageEventArgs { Message = message };
            OnSipInitialize(eventArgs);
        }

        private void AbtoPhone_OnDetectedAnswerTime(int timeSpanMs, int connectionId)
        {
            if (timeSpanMs > 3000)
            {
                // means Answering Machine
                OnAnsweringMachine(EventArgs.Empty);
            }
            else
            {
                // human
            }
        }

        private void AbtoPhone_OnToneReceived(int tone, int connectionId, int lineId)
        {
            if (tone == 70)
            {
                this.abtoPhone.HangUpCallLine(lineId);
                OnFaxMachine(EventArgs.Empty);
            } // means Fax Machine
        }

        private void AbtoPhone_OnPlayFinished(string msg)
        {
            this.playFinished = true;
        }

        private void AbtoPhone_OnEstablishedCall(string message, int lineId)
        {
            this.currentLineId = lineId;
            var filename = $"{this.phoneNumber}_{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-fff")}.mp3";

            var filePath = Path.Combine(RECORDINGS_PATH, filename);
            this.abtoPhone.StartRecording(filePath);
            recordingStarted = true;

            MessageEventArgs eventArgs = new MessageEventArgs { Message = filePath };
            this.OnSipEstablishedCall(eventArgs);

            //this.abtoPhone.PlayFileLine(STAR_RECORDING_PATH, lineId);
        }

        private void AbtoPhone_OnClearedCall(string msg, int status, int lineId)
        {
            if (!playFinished)
            {
                this.abtoPhone.StopPlaybackLine(lineId);
            }

            if (recordingStarted)
            {
                this.abtoPhone.StopRecording();
                recordingStarted = false;
                this.OnSipClearedCall(EventArgs.Empty);
            }

            bool lineIsBusy = status == 480 && !recordingStarted;
            bool callEndedByAgent = status == 487 && !recordingStarted;

            if (lineIsBusy || callEndedByAgent)
            {
                //TODO: save the phone number to call again
            }
        }

        public void StartCall(string phoneNumber)
        {
            this.phoneNumber = phoneNumber;
            this.abtoPhone.StartCall2(phoneNumber);
        }

        public void HangUp()
        {
            if (this.abtoPhone.IsLineOccupied(this.currentLineId) != 0)
            {
                this.abtoPhone.HangUpCallLine(this.currentLineId);
            }
            else
            {
                // in case the user does not answear the phone and we watn the close the call
                this.abtoPhone.HangUpLastCall();
            }
        }

        private void AbtoPhone_OnIncomingCall(string adress, int lineId)
        {
            //AbtoPhone.AnswerCallLine(lineId);
            //else AbtoPhone.RejectCallLine(lineId);
        }
    }
}

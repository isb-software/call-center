using System;
using System.IO;
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

        /**
         * eToneDtmf = 1,
            eToneBaudot = 2,
            eToneSIT = 4,
            eToneMF = 8,
            eToneEnergy = 16
         * */

        const string LICENSE_USER_ID = "{Licensed_for_ISB_Software_Consulting-A4D8-475A-9AC9A63D-E2FD-1AC3-2B95-71CE19A3A3F1}";
        const string LICENSE_KEY = "{T+C/ve2txl8Gucgify5BcUuyEWXPdlxeHMI4ageygVBGGMRK0wdrGwg8IjGDTcOGUQj7YDL9lJSdSoCbQ4d86A==}";

        //TODO: point to network share.
        static readonly string RECORDINGS_PATH = @"c:\temp\recordings\";
        static readonly string STAR_RECORDING_PATH = @"c:\temp\starrecordings\mozart.wav";

        //const string ADDITIONAL_DNS_SERVER = "8.8.8.8";
        //const int TONE_TYPE_TO_DETECT = 1;
        // sip.skype.com
        // 99051000507121
        // QpqkhCJRXUhHzx


        const bool AUTO_RECORD = true;
        const string SIP_USER = "99051000507121";
        const string SIP_PWD = "5FSzPGcamwvhfw";

        private string phoneNumber;
        private bool recordingStarted;
        private int currentLineId;

        public SipPhone()
        {
            abtoPhone = new CAbtoPhone();
            phoneCfg = abtoPhone.Config;
        }

        public bool Initialize()
        {
            phoneCfg.MP3RecordingEnabled = 1;

            phoneCfg.LicenseUserId = LICENSE_USER_ID;
            phoneCfg.LicenseKey = LICENSE_KEY;
            //phoneCfg.log
            //phoneCfg.AdditionalDnsServer = ADDITIONAL_DNS_SERVER;
            phoneCfg.TonesTypesToDetect = (int)ToneType.eToneDtmf;

            try
            {
                abtoPhone.ApplyConfig();
                abtoPhone.Initialize();

                this.abtoPhone.OnClearedCall += AbtoPhone_OnClearedCall;
                this.abtoPhone.OnEstablishedCall += AbtoPhone_OnEstablishedCall;
                this.abtoPhone.OnIncomingCall += AbtoPhone_OnIncomingCall;
                this.abtoPhone.OnPlayFinished += AbtoPhone_OnPlayFinished;
                this.abtoPhone.OnToneReceived += AbtoPhone_OnToneReceived;
                this.abtoPhone.OnDetectedAnswerTime += AbtoPhone_OnDetectedAnswerTime;

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        #region events
        protected virtual void OnAnsweringMachine(EventArgs e)
        {
            EventHandler handler = AnsweringMachine;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler AnsweringMachine;

        protected virtual void OnFaxMachine(EventArgs e)
        {
            EventHandler handler = FaxMachine;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler FaxMachine;
        
        #endregion events

        private void AbtoPhone_OnDetectedAnswerTime(int timeSpanMs, int connectionId)
        {
            if (timeSpanMs > 3000) {
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
            if (tone == 70) {
                this.abtoPhone.HangUpCallLine(lineId);
                OnFaxMachine(EventArgs.Empty);
            }// means Fax Machine
        }

        private void AbtoPhone_OnPlayFinished(string msg)
        {
            //throw new NotImplementedException();
        }

        private void AbtoPhone_OnEstablishedCall(string msg, int lineId)
        {
            this.currentLineId = lineId;
            var filename = $"{this.phoneNumber}_{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-fff")}.mp3";

            var filePath = Path.Combine(RECORDINGS_PATH, filename);
            this.abtoPhone.StartRecording(filePath);
            recordingStarted = true;

            this.abtoPhone.PlayFileLine(STAR_RECORDING_PATH, lineId);
        }

        private void AbtoPhone_OnClearedCall(string msg, int status, int lineId)
        {
            this.abtoPhone.StopPlaybackLine(lineId);

            if (recordingStarted)
            {
                this.abtoPhone.StopRecording();
                recordingStarted = false;
            }
        }

        public void StartCall(string phoneNumber) {
            this.phoneNumber = phoneNumber;
            this.abtoPhone.StartCall(phoneNumber);
        }

        public void HangUp()
        {
            this.abtoPhone.HangUpCallLine(this.currentLineId);
        }

        private void AbtoPhone_OnIncomingCall(string adress, int lineId)
        {
            //AbtoPhone.AnswerCallLine(lineId);
            //else AbtoPhone.RejectCallLine(lineId);
        }
    }
}

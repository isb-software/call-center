using System;
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
        //const string ADDITIONAL_DNS_SERVER = "8.8.8.8";
        //const int TONE_TYPE_TO_DETECT = 1;
        // sip.skype.com
        // 99051000507121
        // QpqkhCJRXUhHzx


        const bool AUTO_RECORD = true;
        const string SIP_USER = "99051000507121";
        const string SIP_PWD = "5FSzPGcamwvhfw";

        private Object notifiMe;

        public SipPhone(Object notifyMe)
        {
            abtoPhone = new CAbtoPhone();
            phoneCfg = abtoPhone.Config;

            this.notifiMe = notifyMe;
        }

        public bool Initialize()
        {
            phoneCfg.MP3RecordingEnabled = 1;

            phoneCfg.LicenseUserId = LICENSE_USER_ID;
            phoneCfg.LicenseKey = LICENSE_KEY;
            //phoneCfg.log
            //phoneCfg.AdditionalDnsServer = ADDITIONAL_DNS_SERVER;
            //phoneCfg.TonesTypesToDetect = (int)ToneType.eToneDtmf;

            try
            {
                abtoPhone.ApplyConfig();
                abtoPhone.Initialize();

                this.abtoPhone.OnClearedCall += AbtoPhone_OnClearedCall;
                this.abtoPhone.OnEstablishedCall += AbtoPhone_OnEstablishedCall;
                this.abtoPhone.OnIncomingCall += AbtoPhone_OnIncomingCall1;
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

        private void AbtoPhone_OnDetectedAnswerTime(int timeSpanMs, int connectionId)
        {
            if (timeSpanMs > 3000) {
                // means Answering Machine
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
            }// means Fax Machine
        }

        private void AbtoPhone_OnPlayFinished(string msg)
        {
            //throw new NotImplementedException();
        }

        private void AbtoPhone_OnEstablishedCall(string msg, int lineId)
        {
            //throw new NotImplementedException();
        }

        //public event EventArgs OnClearedCall

        // pe outbound call trebuie sa fie un timer care daca nu raspunde nimeni si expira
        // pui call status "Nu a raspuns"


        // la outbound call trebuie sa incepi inregistrarea call -ului in momentul CallAnswered
        // la fel si la inbound se inregsitreaza.

        private void AbtoPhone_OnClearedCall(string msg, int status, int lineId)
        {
            //notifyMe.ClearCall(Msg, status, LineId);
            //OnClearedCall();
        }

        public void StartCall() { }

        private void AbtoPhone_OnIncomingCall(string adress, int lineId)
        {
            //AbtoPhone.AnswerCallLine(lineId);
            //else AbtoPhone.RejectCallLine(lineId);
        }
    }
}

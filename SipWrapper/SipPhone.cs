using System;
using System.Reflection;

using log4net;

using SIPVoipSDK;

namespace SipWrapper
{
    public class SipPhone
    {
        private const string LicenseUserId = "{Licensed_for_ISB_Software_Consulting-A4D8-475A-9AC9A63D-E2FD-1AC3-2B95-71CE19A3A3F1}";

        private const string LicenseKey = "{T+C/ve2txl8Gucgify5BcUuyEWXPdlxeHMI4ageygVBGGMRK0wdrGwg8IjGDTcOGUQj7YDL9lJSdSoCbQ4d86A==}";

        private const bool AutoRecord = true;

        private const string SipUser = "9905100039005";

        private const string SipPassword = "5QDkSgfSm8AN7j";

        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /* const string ADDITIONAL_DNS_SERVER = "8.8.8.8";
        const int TONE_TYPE_TO_DETECT = 1; */

        private readonly CAbtoPhone abtoPhone;

        private readonly CConfig phoneCfg;

        #region Comments

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

        // handle OnEstablishedConnection, OnClearedConnection, OnIncomingCall, OnEstablishedCall, OnClearedCall, 
        // OnPlayFinished, OnToneReceived, OnDetectedAnswerTime 
        //

        // pe outbound call trebuie sa fie un timer care daca nu raspunde nimeni si expira
        // pui call status "Nu a raspuns"


        // la outbound call trebuie sa incepi inregistrarea call -ului in momentul CallAnswered
        // la fel si la inbound se inregsitreaza.

        #endregion

        public SipPhone()
        {
            abtoPhone = new CAbtoPhone();
            phoneCfg = abtoPhone.Config;
        }

        #region EventHandlers

        public event EventHandler IncomingCall;

        public event EventHandler ClearedCall;

        public event EventHandler EstablishedConnection;

        public event EventHandler EstablishedCall;

        public event EventHandler ClearedConnection;

        public event EventHandler PlayFinished;

        public event EventHandler ToneReceived;

        public event EventHandler DetectedAnswerTime;

        #endregion


        public bool Initialize()
        {
            phoneCfg.MP3RecordingEnabled = 1;

            phoneCfg.LicenseUserId = LicenseUserId;
            phoneCfg.LicenseKey = LicenseKey;

            phoneCfg.AdditionalDnsServer = "8.8.8.8";
            phoneCfg.TonesTypesToDetect = (int)ToneType.eToneDtmf;

            try
            {
                // Apply modified config
                abtoPhone.ApplyConfig();
                abtoPhone.Initialize();
                RegisterEventHandlerForSip();

                return true;
            }
            catch (Exception exception)
            {
                Log.Error("Could not initialize the SipPhone", exception);
                return false;
            }
        }

        private void RegisterEventHandlerForSip()
        {
            abtoPhone.OnClearedCall += AbtoPhone_OnClearedCall;
            abtoPhone.OnIncomingCall += AbtoPhone_OnIncomingCall;
            abtoPhone.OnEstablishedConnection += AbtoPhone_OnEstablishedConnection;
            abtoPhone.OnEstablishedCall += AbtoPhone_OnEstablishedCall;
            abtoPhone.OnClearedConnection += AbtoPhone_OnClearedConnection;
            abtoPhone.OnPlayFinished += AbtoPhone_OnPlayFinished;
            abtoPhone.OnToneReceived += AbtoPhone_OnToneReceived;
            abtoPhone.OnDetectedAnswerTime += AbtoPhone_OnDetectedAnswerTime;
        }


        public void StartCall()
        {
        }

        #region SipEventHandlers

        private void AbtoPhone_OnIncomingCall(string adress, int lineId)
        {
            this.IncomingCall?.Invoke(this, EventArgs.Empty);
        }

        private void AbtoPhone_OnClearedCall(string message, int status, int lineId)
        {
            this.ClearedCall?.Invoke(this, EventArgs.Empty);
        }

        private void AbtoPhone_OnEstablishedConnection(string addrFrom, string addrTo, int connectionId, int lineId)
        {
            this.EstablishedConnection?.Invoke(this, EventArgs.Empty);
        }

        private void AbtoPhone_OnEstablishedCall(string adress, int lineId)
        {
            this.EstablishedCall?.Invoke(this, EventArgs.Empty);
        }

        private void AbtoPhone_OnClearedConnection(int connectionId, int lineId)
        {
            this.ClearedConnection?.Invoke(this, EventArgs.Empty);
        }

        private void AbtoPhone_OnPlayFinished(string message)
        {
            this.PlayFinished?.Invoke(this, EventArgs.Empty);
        }

        private void AbtoPhone_OnToneReceived(int tone, int connectionId, int lineId)
        {
            this.ToneReceived?.Invoke(this, EventArgs.Empty);
        }

        private void AbtoPhone_OnDetectedAnswerTime(int timespaneMiliseconds, int connectionId)
        {
            this.DetectedAnswerTime?.Invoke(this, EventArgs.Empty);
        }

        #endregion
    }
}

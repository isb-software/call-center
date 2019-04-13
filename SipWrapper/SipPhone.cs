using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        const bool AUTO_RECORD = true;
        const string SIP_USER = "9905100039005";
        const string SIP_PWD = "5QDkSgfSm8AN7j";


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

            //phoneCfg.AdditionalDnsServer = ADDITIONAL_DNS_SERVER;
            phoneCfg.TonesTypesToDetect = (int)ToneType.eToneDtmf;

            try
            {
                //Apply modified config
                abtoPhone.ApplyConfig();
                abtoPhone.Initialize();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private void AbtoPhone_OnIncomingCall(string adress, int lineId)
        {
            //AbtoPhone.AnswerCallLine(lineId);
            //else AbtoPhone.RejectCallLine(lineId);
        }
    }
}

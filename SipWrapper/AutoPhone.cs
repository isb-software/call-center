using System;
using System.IO;
using System.Reflection;
using log4net;
using SIPVoipSDK;
using System.Timers;
using System.Configuration;

namespace SipWrapper
{
    public class AutoPhone
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

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

        const string timerDelayKey = "timerDelaySeconds";
        static readonly int timerDelay = Int32.Parse(ConfigurationManager.AppSettings[timerDelayKey]);

        const string sipUserKey = "user";
        static readonly string sipUser = ConfigurationManager.AppSettings[sipUserKey];

        const string sipUserPwdKey = "pwd";
        static readonly string sipUserPwd = ConfigurationManager.AppSettings[sipUserPwdKey];

        const string sipDomainKey = "sipDomain";
        static readonly string sipDomain = ConfigurationManager.AppSettings[sipDomainKey];

        const string playFileKey = "playFileLocation";
        static readonly string playFilePath = ConfigurationManager.AppSettings[playFileKey];

        const string recordingFolderKey = "recordingFolder";
        static readonly string recordingFolderPath = ConfigurationManager.AppSettings[recordingFolderKey];

        private string cfgFileName = "phoneCfg.ini";

        private Timer timer;
        private long callStart;
        private long callStop;
        private bool hasTimedOut;
        private bool wasSuccessful;
        private string lastMessage;
        private string phoneNumber;
        private bool recordingStarted;
        private int currentLineId;
        private bool playStarted;

        public AutoPhone()
        {
            abtoPhone = new CAbtoPhone();
            this.Initialize();
        }

        public void StartCall(string phoneNumber)
        {
            this.phoneNumber = phoneNumber;
            this.timer.Start();
            this.abtoPhone.StartCall2(phoneNumber);
            callStart = DateTime.Now.Ticks;
        }

        private void Initialize()
        {
            try
            {
                this.abtoPhone.OnClearedCall += AbtoPhone_OnClearedCall;
                this.abtoPhone.OnEstablishedCall += AbtoPhone_OnEstablishedCall;
                this.abtoPhone.OnRegistered += AbtoPhone_OnRegistered;
                this.abtoPhone.OnPlayFinished += AbtoPhone_OnPlayFinished;

                phoneCfg = abtoPhone.Config;
                phoneCfg.Load(cfgFileName);

                phoneCfg.MP3RecordingEnabled = 1;

                phoneCfg.LicenseUserId = LICENSE_USER_ID;
                phoneCfg.LicenseKey = LICENSE_KEY;
                phoneCfg.RegDomain = sipDomain;
                phoneCfg.RegUser = sipUser;
                phoneCfg.RegPass = sipUserPwd;
                phoneCfg.ListenPort = 5060;

                phoneCfg.TonesTypesToDetect = (int)ToneType.eToneDtmf;

                abtoPhone.ApplyConfig();
                abtoPhone.Initialize();

                this.timer = new System.Timers.Timer(timerDelay * 1000);
                this.timer.Enabled = false;
                this.timer.Elapsed += Timer_Elapsed;
                this.timer.AutoReset = false;
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
            }
        }

        private void AbtoPhone_OnRegistered(string message)
        {
            Log.Info("Line registered : $message");
        }

        private void AbtoPhone_OnPlayFinished(string message)
        {
            Log.Info("Play finished : $message");
            hangUpThePhone();
            this.wasSuccessful = true;
            this.lastMessage = message;

            this.Completion(
                new RobotCallDataEventArgs
                {
                    Successful = wasSuccessful,
                    Status = lastMessage,
                    HasTimedOut = hasTimedOut,
                    CallDuration = TimeSpan.FromTicks(callStop - callStart)
                });
        }

        private void Completion(RobotCallDataEventArgs e)
        {
            var handler = OnCompletion;
            handler?.Invoke(this, e);
        }

        public event EventHandler<RobotCallDataEventArgs> OnCompletion;

        private void hangUpThePhone()
        {
            try
            {
                this.timer.Stop();
                this.abtoPhone.HangUpCallLine(this.currentLineId);
                callStop = DateTime.Now.Ticks;
                this.stopRecording();
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
            finally
            {
                this.CleanUp();
            }
        }

        private void AbtoPhone_OnEstablishedCall(string message, int lineId)
        {
            this.currentLineId = lineId;

            startRecording(message);
            startPlayFile(message);
        }

        private void startRecording(string message)
        {
            try
            {
                var filename = $"{this.phoneNumber}_{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-fff")}.mp3";
                var filePath = Path.Combine(recordingFolderPath, filename);
                this.abtoPhone.StartRecording(filePath);
                recordingStarted = true;

                Log.Info("Started recording on line : $this.currentLineId, message : $message");
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
        }

        private void startPlayFile(string message)
        {
            try
            {
                this.abtoPhone.PlayFileLine(playFilePath, this.currentLineId);
                this.playStarted = true;

                Log.Info("Play file started on line : $this.currentLineId, message : $message");
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
        }

        private void stopPlayback(int lineId)
        {
            if (this.playStarted)
            {
                try
                {
                    this.abtoPhone.StopPlaybackLine(lineId);
                }
                catch (Exception ex)
                {
                    Log.Error(ex.ToString());
                }
            }
        }

        private void stopRecording()
        {
            if (recordingStarted)
            {
                try
                {
                    this.abtoPhone.StopRecording();
                    recordingStarted = false;
                }
                catch (Exception ex)
                {
                    Log.Error(ex.ToString());
                }
            }
        }

        private void AbtoPhone_OnClearedCall(string message, int status, int lineId)
        {
            try
            {
                callStop = DateTime.Now.Ticks;
                this.timer.Stop();

                var msg = "Cleared call started on line : $lineId with status : $status and message : $message";
                Log.Info(msg);

                this.stopPlayback(lineId);
                this.stopRecording();

                this.wasSuccessful = false;
                this.lastMessage = msg;
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
            finally
            {
                this.CleanUp();
                this.Completion(
                    new RobotCallDataEventArgs
                    {
                        Successful = wasSuccessful,
                        Status = lastMessage,
                        HasTimedOut = hasTimedOut,
                        CallDuration = TimeSpan.FromTicks(callStop - callStart)
                    });
            }
        }

        private void CleanUp()
        {
            try
            {
                timer.Stop();
                timer.Close();
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                this.hasTimedOut = true;
                hangUpThePhone();
                this.wasSuccessful = false;
                this.lastMessage = "Has timed out.";
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
            finally
            {
                this.Completion(
                    new RobotCallDataEventArgs
                    {
                        Successful = wasSuccessful,
                        Status = lastMessage,
                        HasTimedOut = hasTimedOut,
                        CallDuration = TimeSpan.FromTicks(callStop - callStart)
                    });
            }
        }
    }
}

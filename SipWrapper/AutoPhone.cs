﻿using System;
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

        const string sipLogPathKey = "sipLogPath";
        static readonly string sipLogFolder = ConfigurationManager.AppSettings[sipLogPathKey];

        private string cfgFileName = "phoneCfg.ini";

        private Timer timer;
        private long callStart;
        private bool appCalledHangup;
        private long conversationStart;
        private bool callAnswered;
        private long callStop;
        private bool hasTimedOut;
        private bool wasSuccessful;
        private string lastMessage;
        private string phoneNumber;
        private bool connectionCleared;
        private int currentLineId;
        private bool playStarted;

        public AutoPhone()
        {
            abtoPhone = new CAbtoPhone();
            this.Initialize();
        }

        public void StartCall(string phoneNumber)
        {
            try
            {
                this.phoneNumber = phoneNumber;
                this.timer.Start();
                this.phoneNumber = phoneNumber;
                this.abtoPhone.StartCall2(phoneNumber);
                callStart = DateTime.Now.Ticks;
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
        }

        private void Initialize()
        {
            try
            {
                this.abtoPhone.OnEstablishedCall += AbtoPhone_OnEstablishedCall;
                this.abtoPhone.OnClearedConnection += AbtoPhone_OnClearedConnection;
                this.abtoPhone.OnClearedCall += AbtoPhone_OnClearedCall;
                this.abtoPhone.OnPlayFinished += AbtoPhone_OnPlayFinished;
                this.abtoPhone.OnToneDetected += AbtoPhone_OnToneDetected;
                this.abtoPhone.OnToneReceived += AbtoPhone_OnToneReceived;

                phoneCfg = abtoPhone.Config;
                phoneCfg.Load(cfgFileName);

                phoneCfg.MP3RecordingEnabled = 1;

                phoneCfg.LicenseUserId = LICENSE_USER_ID;
                phoneCfg.LicenseKey = LICENSE_KEY;
                phoneCfg.RegDomain = sipDomain;
                phoneCfg.RegUser = sipUser;
                phoneCfg.RegPass = sipUserPwd;
                phoneCfg.ListenPort = 5060;
                phoneCfg.LogPath = sipLogFolder;
                phoneCfg.LogLevel = LogLevelType.eLogCritical | LogLevelType.eLogError; // | LogLevelType.eLogWarning; // (LogLevelType)11;

                //phoneCfg.TonesTypesToDetect = (int)ToneType.eToneDtmf;

                abtoPhone.ApplyConfig();
                abtoPhone.Initialize();

                //abtoPhone.SetCurrentLine(2);

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

        private void AbtoPhone_OnToneReceived(int Tone, int ConnectionId, int LineId)
        {
            int a = 23;
        }

        private void AbtoPhone_OnToneDetected(ToneType tType, string ToneStr, int ConnectionId, int LineId)
        {
            int a = 23;
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
                this.abtoPhone.HangUpCallLine(this.currentLineId);
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
            this.timer.Stop();

            this.currentLineId = lineId;
            this.conversationStart = DateTime.Now.Ticks;
            this.callAnswered = true;

            startPlayFile(message);
        }

        private void startPlayFile(string message)
        {
            try
            {
                if (!this.playStarted)
                {
                    this.abtoPhone.PlayFileLine(playFilePath, this.currentLineId);
                    this.playStarted = true;

                    Log.Info(
                        String.Format(
                            "Play file started on line : {0}, message : {1}.",
                            this.currentLineId,
                            message));
                }
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

        private void AbtoPhone_OnClearedConnection(int ConnectionId, int LineId)
        {
            this.timer.Stop();

            Log.Info(String.Format("Connection cleared : {0} on line {1}", ConnectionId, LineId));
            connectionCleared = true;

            callStop = DateTime.Now.Ticks;
        }

        private void AbtoPhone_OnRegistered(string message)
        {
            Log.Info(String.Format("Line registered : {0}.", message));
        }

        private void AbtoPhone_OnPlayFinished(string message)
        {
            this.timer.Stop();

            Log.Info(String.Format("Play finished : {0}", message));

            if (!this.connectionCleared)
            {
                // This is the happy flow. Call completes properly, we need to hang up.
                this.appCalledHangup = true;
                this.lastMessage = "Call completed";

                hangUpThePhone();
            }
            else
            {
                // this is the flow where third party answered, file play started, and third party hung up before
                // file play completed.
                this.lastMessage = "Hung up.";
            }
        }

        private void AbtoPhone_OnClearedCall(string message, int status, int lineId)
        {
            try
            {
                this.timer.Stop();
                
                if (this.playStarted)
                {
                    this.stopPlayback(lineId);
                }

                Log.Info(String.Format(
                    "Cleared call on line : {0} with status : {1} and message : {2}.",
                        lineId,
                        status,
                        message));

                if (!this.hasTimedOut)
                {
                    this.wasSuccessful = this.appCalledHangup;

                    if(status > 0)
                    {
                        switch (status)
                        {
                            case 403:
                                this.lastMessage = "Fax machine";
                                break;
                            case 404:
                                this.lastMessage = "Numar inexistent";
                                break;
                            case 486:
                                this.lastMessage = "Apel refuzat";
                                break;
                            default:
                                this.lastMessage = String.Format("Status {0}", status);
                                break;
                        }
                    }
                }
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
                        PhoneNumber = this.phoneNumber,
                        Status = lastMessage,
                        HasTimedOut = hasTimedOut,
                        ConversationDuration = this.callAnswered ? TimeSpan.FromTicks(callStop - conversationStart) :TimeSpan.Zero,
                        TotalCallDuration = TimeSpan.FromTicks(callStop - callStart)
                    });
            }
        }

        private void CleanUp()
        {
            try
            {
                if (this.timer.Enabled)
                {
                    timer.Stop();
                }
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
                this.wasSuccessful = false;
                this.lastMessage = "Has timed out.";

                hangUpThePhone();
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
        }
    }
}

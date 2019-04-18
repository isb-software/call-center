using System;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Agent;

using log4net;

using Polly;

namespace AgentClient
{
    class Program
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        [STAThread]
        static void Main(string[] args)
        {
            Application.ThreadException += ApplicationThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainUnhandledException;

            // new UpdateAgent().Update();
            StartAgent();
        }

        private static void StartAgent()
        {
            using (var mutex = new Mutex(false, "isbsoftware.com CallCenter"))
            {
                bool isAnotherInstanceOpen = !mutex.WaitOne(TimeSpan.Zero);
                if (isAnotherInstanceOpen)
                {
                    Log.Info("Only one instance of this app is allowed.");
                    return;
                }

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new CallAgent());
            }
        }

        private static void ApplicationThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Log.Error("Thread exception - ", e.Exception);
        }

        private static void CurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Log.Error("Unhandled exception - ", e.ExceptionObject as Exception);
        }
    }
}

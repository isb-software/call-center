using System;
using System.Threading;
using System.Windows.Forms;
using Agent;

namespace UpdateAgent
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
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
                    Console.WriteLine(@"Only one instance of this app is allowed.");
                    return;
                }

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
        }
    }
}
